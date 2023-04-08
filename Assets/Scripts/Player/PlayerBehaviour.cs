using System;
using UnityEngine;
using UnityEngine.InputSystem;
using SaveLoadPlayerPrefs;

public class PlayerBehaviour : MonoBehaviour, ICanBeDamaged
{
    [SerializeField] protected ScriptableCharacters _char;

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

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _canMove = true;

        _move = _char.speed;

        _currentLife = _char.maxHealth;

        OnActing = Attacking;

        _anim = GetComponentInChildren<Animator>();
    }

    void LateUpdate()
    {
        if (!_canMove) return;

        if (!_isRunning)
        {
            _isRunning = false;
            _move = _char.speed;
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
                _rb.AddForce(Vector2.up * _char.jump * 100, ForceMode2D.Force);
            }
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        _isRunning = context.ReadValueAsButton();

        if (context.ReadValueAsButton())
        {
            _move = _char.speed * 2;
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
            _anim.SetTrigger("SPECIAL1");
        }
    }

    public void SpecialAttack2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
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

        (damageable as ICanBeDamaged).SufferDamage(_char.damage);
    }

    public bool IsOnGround()
    {
        return (Physics2D.Linecast(transform.position, _footDetector.position, 1 << LayerMask.NameToLayer("GROUND")));
    }
}