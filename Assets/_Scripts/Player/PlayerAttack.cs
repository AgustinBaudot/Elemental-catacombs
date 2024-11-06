using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _attackSprite;
    [SerializeField] private float _radius, _attackCD;
    [SerializeField] private AudioClip _attackClip, _attackHitClip;
    [SerializeField] private float _knockbackForce;
    private AudioSource _source;
    private bool _canAttack = true;

    private void Start()
    {
        _source = GameObject.Find("Combat SFX").GetComponent<AudioSource>();
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
        StartCoroutine(ShowSwordArea());

        Collider2D[] objects = Physics2D.OverlapCircleAll(_attackSprite.transform.position, _radius);

        _source.clip = _attackClip;

        foreach (Collider2D enemyGameobject in objects)
        {
            if (enemyGameobject.gameObject.CompareTag("Enemy"))
            {
                Vector2 direction = (enemyGameobject.transform.position - transform.position).normalized; //Offset for direction of knockback

                Vector2 knockback = direction * _knockbackForce;

                Debug.Log(knockback);

                Debug.Log(_knockbackForce);

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
        yield return new WaitForSeconds(attackCD);
        _canAttack = true;
    }
}
