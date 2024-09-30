using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Enemy : MonoBehaviour
{
    protected EnemyState _enemyState;
    protected CinemachineImpulseSource _impulseSource;
    [SerializeField] protected GameObject _player;

    public List<LootItem> _lootTable = new List<LootItem>();
    protected enum EnemyState { Persuing, Attacking };
    
    [Header("Enemy Characteristics")]
    public float _hp;
    protected float _speed, _range;
    protected string _type, _element;

    public virtual void Init()
    {
        tag = "Enemy";
    }

    public virtual void Start()
    {
        _enemyState = EnemyState.Persuing;
        _impulseSource = GetComponent<CinemachineImpulseSource>();
        Init();
    }

    protected void Update()
    {
        if (_enemyState == EnemyState.Persuing && !HitStopManager._instance.IsStopped())
        {
            if (Vector2.Distance(_player.transform.position, transform.position) < _range)
            {
                _enemyState = EnemyState.Attacking;
                return;
            }

            var step = _speed * Time.deltaTime; //Calculate distance to move
            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, step);
        }

        else if (_enemyState == EnemyState.Attacking && !HitStopManager._instance.IsStopped())
        {
            Attack();
        }
    }

    public virtual void ReceiveDamage(int damageReceived)
    {
        //Make difference between normal hit and final blow.
        CameraShakeManager._instance.CameraShake(_impulseSource); //Screen shake
        StartCoroutine(HitStopManager._instance.HitStop());
        _hp -= damageReceived;
        Debug.Log(damageReceived);
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
        _enemyState = EnemyState.Persuing;
        //Attack;
    }

    public virtual void DropLoot()
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

    [System.Serializable]
    public class LootItem
    {
        public GameObject _lootPrefab;
        [Range(0f, 1f)] public float dropChance;
    }
}