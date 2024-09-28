using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _attackSprite;
    [SerializeField] private float _radius, _attackCD;
    private bool _canAttack = true;

    // Update is called once per frame
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

        foreach (Collider2D enemyGameobject in objects)
        {
            if (enemyGameobject.gameObject.CompareTag("Enemy"))
            {
                enemyGameobject.gameObject.GetComponent<Enemy>().ReceiveDamage(10);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_attackSprite.transform.position, _radius);
    }

    private IEnumerator ShowSwordArea()
    {
        _attackSprite.enabled = true;
        yield return new WaitForSeconds(0.05f);
        _attackSprite.enabled = false;
        StartCoroutine(AttackCD(_attackCD));
    }

    public IEnumerator AttackCD(float attackCD)
    {
        yield return new WaitForSeconds(attackCD);
        _canAttack = true;
    }
}
