using System;
using UnityEngine;
using UnityEngine.InputSystem;
using SaveLoadPlayerPrefs;

public class PlayerBehaviour : MonoBehaviour, ICanBeDamaged
{
    [SerializeField] ScriptableCharacters _characterStats;
    public ScriptableCharacters characterStats { get { return _characterStats; } }

    public event Action OnActing;
    public event Action OnPausing;

    Rigidbody2D _rb;

    float _currentLife;

    float _move;
    float _moveX;

    bool _isRunning;

    [SerializeField] Transform _footDetector;

    [SerializeField] bool _canMove;
    public bool canMove { get { return _canMove; } }

    SaveLoad s;

    protected Animator _anim;

    [SerializeField] protected AudioSource _audio;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _canMove = true;

        _move = _characterStats.speed;

        _currentLife = _characterStats.maxHealth;

        #region -- For Exercise purpouse only --
        
        _currentLife -= 3;

        UpdateLife();

        #endregion

        OnActing = Attacking;

        _anim = GetComponentInChildren<Animator>();
    }

    void LateUpdate()
    {
        if (!_canMove) return;

        if (!_isRunning)
        {
            _isRunning = false;
            _move = _characterStats.speed;
        }

        if (_moveX == 0)
        {
            _anim.SetBool("WALK", false);
        }
        else
        {
            _anim.SetBool("WALK", true);
        }

        if (_canMove == false)
        {
            _moveX = 0;
        }

        _anim.SetBool("JUMP", !IsOnGround());

        transform.Translate(new Vector2(_moveX * _move * Time.deltaTime, 0));
    }

    #region - InputManager Buttons
    public void OnMove(InputAction.CallbackContext context)
    {
        if (_moveX == 1)
        {
            transform.localScale = new Vector3(1, 1, 0);
        }
        if (_moveX == -1)
        {
            transform.localScale = new Vector3(-1, 1, 0);
        }

        if (_canMove)
        {
            _moveX = context.ReadValue<Vector2>().x;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (IsOnGround())
            {
                _audio.clip = _characterStats.jumpSound[UnityEngine.Random.Range(0, _characterStats.jumpSound.Length)];
                _audio.Play();

                _rb.AddForce(Vector2.up * _characterStats.jump * 100, ForceMode2D.Force);
            }
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        _isRunning = context.ReadValueAsButton();

        if (context.ReadValueAsButton())
        {
            _move = _characterStats.speed * 2;
        }
    }

    public void OnInteracting(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnActing?.Invoke();
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnPausing?.Invoke();
        }
    }

    public void SpecialAttack1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _audio.clip = _characterStats.abilitiesSound[UnityEngine.Random.Range(0, _characterStats.abilitiesSound.Length)];
            _audio.Play();

            _anim.SetTrigger("SPECIAL1");
        }
    }

    public void SpecialAttack2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _audio.clip = _characterStats.abilitiesSound[UnityEngine.Random.Range(0, _characterStats.abilitiesSound.Length)];
            _audio.Play();

            _anim.SetTrigger("SPECIAL2");
        }
    }

    #endregion
    public void Attacking()
    {
        _anim.SetTrigger("ATTACK");
    }

    public bool CanMove(bool b)
    {
        return _canMove = b;
    }

    public void SufferDamage(int atk)
    {
        _currentLife -= atk;

        UpdateLife();

        if (_currentLife < 1)
        {
            _anim.SetBool("DEAD", true);
            _canMove = false;

            GameManager.OnNextGameState(GamePlayStates.GAMEOVER);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Component damageable = collision.gameObject.GetComponent(typeof(ICanBeDamaged));

        if (damageable == null) return;

        damageable.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(800 * transform.localScale.x, 500), ForceMode2D.Force);

        (damageable as ICanBeDamaged).SufferDamage(_characterStats.damage);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Potion p = collision.collider.GetComponent<Potion>();

        if (p == null)return;

        _currentLife += p.lifeToRecover();

        UpdateLife();

        Destroy(p.gameObject);
    }

    float UpdateLife()
    {
        LifeUI.OnUpdateUI?.Invoke(_currentLife / characterStats.maxHealth);

        if (_currentLife > characterStats.maxHealth) _currentLife = characterStats.maxHealth;
        if (_currentLife < 0) _currentLife = 0;
        
        return _currentLife;
    }

    public bool IsOnGround()
    {
        return (Physics2D.Linecast(transform.position, _footDetector.position, 1 << LayerMask.NameToLayer("GROUND")));
    }
}