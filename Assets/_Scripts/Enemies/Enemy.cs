using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Enemy : MonoBehaviour
{
    protected EnemyState _enemyState;
    protected CinemachineImpulseSource _impulseSource;
    [SerializeField] protected GameObject _player;
    protected float _attackCD;
    private bool _canAttack = true;
    private float _maxHp;
    private bool _isHealing = false;
    private float _healAmount;
    private float _healCD = 1;

    protected enum EnemyState { Idle, Persuing, Attacking };

    [Header("Enemy Characteristics")]
    public float _hp;
    public List<LootItem> _lootTable = new List<LootItem>();
    protected float _speed, _atkRange, _persueRange;
    protected string _type, _element;

    public virtual void Init()
    {
        tag = "Enemy";
    }

    public virtual void Start()
    {
        _maxHp = _hp;
        _healAmount = _maxHp / 4;
        _enemyState = EnemyState.Idle;
        _impulseSource = GetComponent<CinemachineImpulseSource>();
        Init();
    }

    protected void Update()
    {
        ManageEnemyState();
    }

    private void ManageEnemyState()
    {
        switch (_enemyState)
        {
            case EnemyState.Idle:

                HealOverTime();

                if (Vector2.Distance(_player.transform.position, transform.position) < _persueRange)
                {
                    _enemyState = EnemyState.Persuing;
                    break;
                }
                break;

            case EnemyState.Persuing:

                if (Vector2.Distance(_player.transform.position, transform.position) < _atkRange)
                {
                    _enemyState = EnemyState.Attacking;
                    break;
                }

                else if(Vector2.Distance(_player.transform.position, transform.position) > _persueRange)
                {
                    _enemyState = EnemyState.Idle;
                    break;
                }

                if (!HitStopManager._instance.IsStopped())
                {
                    var step = _speed * Time.deltaTime; //Calculate distance to move
                    transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, step); 
                }
                break;

            case EnemyState.Attacking:
                if (_canAttack && !HitStopManager._instance.IsStopped()) Attack();
                break;
        }
    }

    public virtual void ReceiveDamage(int damageReceived)
    {
        //Make difference between normal hit and final blow.
        CameraShakeManager._instance.CameraShake(_impulseSource); //Screen shake
        HitStopManager._instance.StartHitStop(0.1f);
        _hp -= damageReceived;
        if (_hp <= 0)
        {
            DropLoot();
            Dies();
        }
    }

    public virtual void Dies()
    {
        //Add anim.
        //Destroy(gameObject);
        gameObject.SetActive(true);
    }

    public virtual void Attack()
    {
        StartCoroutine(AttackCD(_attackCD));
        //Attack;
    }

    public void DropLoot()
    {
        foreach (LootItem lootItem in _lootTable)
        {
            float randomValue = Random.Range(0f, 1f);
            if (randomValue <= lootItem.dropChance)
            {
                Instantiate(lootItem._lootPrefab, transform.position, Quaternion.identity);
            }
        }
    }

    public IEnumerator AttackCD(float attackCD)
    {
        _canAttack = false;
        yield return new WaitForSeconds(attackCD);
        _enemyState = EnemyState.Persuing;
        _canAttack = true;
    }

    protected virtual void HealOverTime()
    {
        if (!_isHealing)
        {
            StartCoroutine(HealCD(_healCD));
            Debug.Log("enemy healed");
        }
    }

    private IEnumerator HealCD(float healCD)
    {
        _isHealing = true;
        yield return new WaitForSeconds(healCD);
        _hp += _healAmount;
        if (_hp > _maxHp) _hp = _maxHp;
        _isHealing = false;
    }

    [System.Serializable]
    public class LootItem
    {
        public GameObject _lootPrefab;
        [Range(0f, 1f)] public float dropChance;
    }
}
