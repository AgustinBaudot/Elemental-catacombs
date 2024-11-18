using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class Enemy : MonoBehaviour
{
    protected EnemyState _enemyState;
    protected CinemachineImpulseSource _impulseSource;
    [SerializeField] protected GameObject _player;
    protected float _attackCD;
    private SpriteRenderer _spriteRenderer;
    private GameStateManager _stateManager;
    protected enum EnemyState { Idle, Persuing, Attacking };

    [Header("Enemy Characteristics")]
    public float _hp;
    public List<LootItem> _lootTable = new List<LootItem>();
    public float _speed, _atkRange, _persueRange;
    protected string _type, _element;
    protected bool _canAttack = true;
    private float _maxHp;
    private bool _isHealing = false;
    private float _healAmount;
    private float _healCD = 1;

    public UnityEvent Death;

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
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _stateManager = GameObject.Find("GameStateManager").GetComponent<GameStateManager>();
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

                if (_player == null) break;

                HealOverTime();

                if (Vector2.Distance(_player.transform.position, transform.position) < _persueRange)
                {
                    _enemyState = EnemyState.Persuing;
                    break;
                }
                break;

            case EnemyState.Persuing:

                if (_player == null) break;

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
                if (_player == null) break;

                if (_canAttack && !HitStopManager._instance.IsStopped()) Attack();
                break;
        }
    }

    public virtual void ReceiveDamage(int damageReceived, Vector2 knockback)
    {
        CameraShakeManager._instance.CameraShake(_impulseSource); //Screen shake
        HitStopManager._instance.StartHitStop(0.1f);
        _hp -= damageReceived;
        GetComponent<Rigidbody2D>().mass = 1;
        GetComponent<Rigidbody2D>().AddForce(knockback, ForceMode2D.Impulse);
        GetComponent<Rigidbody2D>().mass = 1000000;

        StartCoroutine(ChangeColor(Color.red, 0.2f));
        if (_hp <= 0)
        {
            DropLoot();
            Dies();
        }
    }

    public virtual void Dies()
    {
        _stateManager.EnemyDeath(gameObject);
        Destroy(gameObject);
    }

    public virtual void Attack()
    {
        StartCoroutine(AttackCD(_attackCD));
    }

    public void DropLoot()
    {
        foreach (LootItem lootItem in _lootTable)
        {
            float randomValue = Random.Range(0f, 1f);
            if (randomValue <= lootItem.dropChance)
            {
                Instantiate(lootItem._lootPrefab, transform.position, Quaternion.identity, transform.parent);
            }
        }
    }

    public virtual IEnumerator AttackCD(float attackCD)
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

    private IEnumerator ChangeColor(Color _newColor, float _duration)
    {
        Color _originalColor = _spriteRenderer.color;
        _spriteRenderer.color = _newColor;

        yield return new WaitForSeconds(_duration);

        _spriteRenderer.color = _originalColor;
    }

    [System.Serializable]
    public class LootItem
    {
        public GameObject _lootPrefab;
        [Range(0f, 1f)] public float dropChance;
    }
}
