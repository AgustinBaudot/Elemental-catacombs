using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DentedPixel;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _attackSprite;
    [SerializeField] private float _radius, _attackCD;
    [SerializeField] private AudioClip _attackClip, _attackHitClip;
    [SerializeField] private float _knockbackForce;
    [SerializeField] private AudioSource _source;
    private bool _canAttack = true;

    [SerializeField] private GameObject _attackBar;

    public UnityEvent UpdateAttackHealthBar;

    private void Start()
    {
        if (UpdateAttackHealthBar == null)
        {
            UpdateAttackHealthBar = new UnityEvent();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _canAttack)
        {
            Attack();
        }
    }

    public void Attack()
    {
        _canAttack = false;
        LeanTween.scaleX(_attackBar, 0, 0);
        StartCoroutine(ShowSwordArea());
        UpdateAttackHealthBar.Invoke();

        Collider2D[] objects = Physics2D.OverlapCircleAll(_attackSprite.transform.position, _radius);

        _source.clip = _attackClip;

        foreach (Collider2D enemyGameobject in objects)
        {
            if (enemyGameobject.gameObject.CompareTag("Enemy"))
            {
                Vector2 direction = (enemyGameobject.transform.position - transform.position).normalized; //Offset for direction of knockback

                Vector2 knockback = direction * _knockbackForce;

                enemyGameobject.gameObject.GetComponent<Enemy>().ReceiveDamage(10, Vector2.zero);

                //GetComponent<Rigidbody2D>().AddForce(-knockback, ForceMode2D.Impulse);
                _source.clip = _attackHitClip;
            }
        }
        _source.Play();
    }

    private IEnumerator ShowSwordArea()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        transform.GetChild(0).gameObject.SetActive(false);
        StartCoroutine(AttackCD(_attackCD));
    }

    public IEnumerator AttackCD(float attackCD)
    {
        LeanTween.scaleX(_attackBar, 1, attackCD);
        yield return new WaitForSeconds(attackCD);
        _canAttack = true;
        UpdateAttackHealthBar.Invoke();
    }

    public bool GetAttackStatus() { return _canAttack; }
}
