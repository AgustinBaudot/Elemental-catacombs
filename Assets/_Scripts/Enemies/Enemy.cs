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
        _enemyState = EnemyState.Idle;
        _impulseSource = GetComponent<CinemachineImpulseSource>();
        Init();
    }

    protected void Update()
    {
        if (_enemyState == EnemyState.Idle && Vector2.Distance(_player.transform.position, transform.position) < _persueRange)
        {
            _enemyState = EnemyState.Persuing;
            return;
        }

        if (_enemyState == EnemyState.Persuing && !HitStopManager._instance.IsStopped())
        {
            if (Vector2.Distance(_player.transform.position, transform.position) < _atkRange)
            {
                _enemyState = EnemyState.Attacking;
                return;
            }

            if (Vector2.Distance(_player.transform.position, transform.position) > _persueRange)
            {
                _enemyState = EnemyState.Idle;
            }

            var step = _speed * Time.deltaTime; //Calculate distance to move
            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, step);
        }

        else if (_enemyState == EnemyState.Attacking && !HitStopManager._instance.IsStopped() && _canAttack)
        {
            Attack();
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
            Dies();
            DropLoot();
        }
    }

    public virtual void Dies()
    {
        //Add anim.
        Destroy(gameObject);
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

    [System.Serializable]
    public class LootItem
    {
        public GameObject _lootPrefab;
        [Range(0f, 1f)] public float dropChance;
    }
}
