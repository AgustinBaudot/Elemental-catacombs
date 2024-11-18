using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private HUD _hud;
    [SerializeField] private AudioClip _damageTaken, _potionUsed;
    public int _health = 5;
    public UnityEvent Dead; 
    private SpriteRenderer _spriteRenderer;
    private bool _hasImmunity = false;

    public GameStateManager _gameStateManager;
    private bool _isDead;
    [SerializeField] private AudioSource _combatSource, _itemSource;
    private CinemachineImpulseSource _impulseSource;
    private FullInventory _inventoryScript;


    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (Dead == null)
        {
            Dead = new UnityEvent();
        }
        _impulseSource = GetComponent<CinemachineImpulseSource>();
        _inventoryScript = GetComponent<FullInventory>();
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
        _combatSource.clip = _damageTaken;
        _combatSource.Play();
        CameraShakeManager._instance.CameraShake(_impulseSource);
        HitStopManager._instance.StartHitStop(0.1f);
        StartCoroutine(ChangeColor(Color.red, 0.2f));
        _hasImmunity = true;
        _health -= damageReceived;
        StartCoroutine(ImmunityDuration());
        _hud.UpdateHearts(_health);
    }

    public void UsePotion()
    {
        if (_health == 5) return;
        _itemSource.clip = _potionUsed;
        _itemSource.Play();
        GetComponent<PlayerMovement>()._potions--;
        _health++;
        _hud.UpdatePotions(GetComponent<PlayerMovement>()._potions);
        _inventoryScript.UpdatePotions(GetComponent<PlayerMovement>()._potions);
        _hud.UpdateHearts(_health);
    }

    private void Dies()
    {
        if (_health <= 0 && !_isDead)
        {
            _isDead = true;
            Dead.Invoke();
            Destroy(gameObject);
            _gameStateManager.Lose();
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