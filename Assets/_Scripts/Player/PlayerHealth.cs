using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int _health = 5;
    private SpriteRenderer _spriteRenderer;
    private bool _hasImmunity = false;

    public HUD _hud;
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (GetComponent<PlayerMovement>()._potions > 0) UsePotion();
        }

        Dies();
    }

    public void ReceiveDamage(int damageReceived = 1)
    {
        if (_hasImmunity) return;
        StartCoroutine(ChangeColor(Color.red, 0.2f));
        _hasImmunity = true;
        _health -= damageReceived;
        StartCoroutine(ImmunityDuration());
        _hud.UpdateHearts(_health);
    }

    public void UsePotion()
    {
        if (_health == 5) return;
        GetComponent<PlayerMovement>()._potions--;
        _health++;
        _hud.UpdateHearts(_health);
    }

    private void Dies()
    {
        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator ChangeColor(Color _newColor, float _duration)
    {
        Color _originalColor = _spriteRenderer.color;
        _spriteRenderer.color = _newColor;

        yield return new WaitForSeconds(_duration);

        _spriteRenderer.color = _originalColor;
    }

    private IEnumerator ImmunityDuration()
    {
        yield return new WaitForSeconds(0.75f);
        _hasImmunity = false;
    }
}