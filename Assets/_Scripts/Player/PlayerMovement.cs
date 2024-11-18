using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DentedPixel;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera _cameraScript;
    [SerializeField] private HUD _hud;
    [SerializeField] private float _speed, _dashSpeed; //Normal moving speed and dashing speed;
    [SerializeField] private float _dashCD, _dashTime; //Colldown between dashes & dash duration;
    [SerializeField] private ParticleSystem _dust;
    [SerializeField] private AudioClip _pickUp, _footsteps, _dash;
    public int _potions;

    private Rigidbody2D _rb;
    private Vector2 _inputMovement;
    private Animator _anim;

    private bool _canDash = true;

    private AudioSource _itemSource, _movementSource;

    private FullInventory _inventoryScript;

    [SerializeField] private GameObject _dashBar;

    public UnityEvent UpdateMovementHealthBar;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _itemSource = GameObject.Find("Items SFX").GetComponent<AudioSource>();
        _movementSource = GameObject.Find("Movement SFX").GetComponent<AudioSource>();
        _inventoryScript = GetComponent<FullInventory>();
        if (UpdateMovementHealthBar == null)
        {
            UpdateMovementHealthBar = new UnityEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        _inputMovement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized; //Input movement.

        if (Input.GetKeyDown(KeyCode.Space) && _canDash) //If player presses space, he dashes.
        {
            Dash();
        }

        if (_inputMovement.magnitude != 0)
        {
            transform.rotation = (_inputMovement.x > 0) ? Quaternion.identity : Quaternion.Euler(0, 180, 0);
        }

        _anim.speed = (HitStopManager._instance.IsStopped()) ? 0 : 1; //Set animation time to 0 if time is stopped.
        ManageAnimations();
    }

    void FixedUpdate()
    {
        if (!HitStopManager._instance.IsStopped()) ManageMovement();
    }

    private void Dash() //Player dashes.
    {
        CreateDust();
        _movementSource.clip = _dash;
        _movementSource.pitch = 1;
        _movementSource.loop = false;
        _movementSource.Play();
        _canDash = false;
        Physics2D.IgnoreLayerCollision(9, 7, true);
        Physics2D.IgnoreLayerCollision(9, 6, true);
        _speed *= _dashSpeed;
        LeanTween.scaleX(_dashBar, 0, 0.05f);
        UpdateMovementHealthBar.Invoke();
        StartCoroutine(DashTime(_dashTime));
    }

    private IEnumerator DashTime(float dashDuration) //Dash duration time.
    {
        yield return new WaitForSeconds(dashDuration);
        _speed /= _dashSpeed;
        Physics2D.IgnoreLayerCollision(9, 7, false);
        Physics2D.IgnoreLayerCollision(9, 6, false);
        StartCoroutine(DashCD(_dashCD));
    }

    private IEnumerator DashCD(float dashCD) //Dash cooldown time.
    {
        LeanTween.scaleX(_dashBar, 1, dashCD);
        yield return new WaitForSeconds(dashCD);
        _canDash = true;
        UpdateMovementHealthBar.Invoke();
    }

    private void ManageMovement()
    {
        _rb.MovePosition(transform.position + new Vector3(_inputMovement.x, _inputMovement.y, 0) * Time.deltaTime * _speed);
        if (_inputMovement.magnitude == 0)
        {
            _movementSource.Stop();
            return;
        }
        if (_movementSource.isPlaying) return;
        _movementSource.clip = _footsteps;
        _movementSource.pitch = 1.5f;
        _movementSource.loop = true;
        _movementSource.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (RoomManager._instance.IsClosed())
        {
            return;
        }

        if (collision.gameObject.CompareTag("Exit"))
        {
            if (collision.gameObject.name == "Left Exit")
            {
                _cameraScript.MoveCamera(Vector2.left);
            }
            else if (collision.gameObject.name == "Bottom Exit")
            {
                _cameraScript.MoveCamera(Vector2.down);
            }
            else if (collision.gameObject.name == "Top Exit")
            {
                _cameraScript.MoveCamera(Vector2.up);
            }
            else if (collision.gameObject.name == "Right Exit")
            {
                _cameraScript.MoveCamera(Vector2.right);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Loot"))
        {
            _potions++;
            Destroy(collision.gameObject);
            _hud.UpdatePotions(_potions);
            _inventoryScript.UpdatePotions(_potions);
            _itemSource.clip = _pickUp;
            _itemSource.Play();
        }
    }

    private void ManageAnimations()
    {
        _anim.SetFloat("Horizontal", _inputMovement.x);
        _anim.SetFloat("Vertical", _inputMovement.y);

        _anim.SetFloat("Speed", _inputMovement.sqrMagnitude);
    }

    private void CreateDust()
    {
        _dust.Play();
    }

    public bool GetDashStatus() { return _canDash; }
}
