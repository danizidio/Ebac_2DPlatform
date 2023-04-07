using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour, ICanBeDamaged
{
    [SerializeField] ScriptableCharacters _char;

    Animator _animBehaviour;

    GameObject _player;

    GameObject _lifebar;

    [SerializeField] Transform _groundDetector;
    [SerializeField] Transform _wallDetector;
    [SerializeField] Transform _enemyView;

    float _currentLife;

    bool _dead;
    bool _touchGround;
    bool _touchWall;

    bool _lookingHero;

    float _contador = 3;
    float _contador2 = 3;

    private void Awake()
    {
        _animBehaviour = GetComponent<Animator>();
    }
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        _currentLife = _char.maxHealth;

       _lifebar = Instantiate(_char.lifeBar, transform);
    }
    private void Update()
    {
        Routine();
    }

    #region - AnimationStates

    public void Idle()
    {
        _animBehaviour.SetBool("IDLE", true);
        _animBehaviour.SetBool("WALK", false);
        _animBehaviour.SetBool("ATTACK", false);
        _animBehaviour.SetBool("DEAD", false);
    }
    public void Attack()
    {
        _animBehaviour.SetBool("IDLE", false);
        _animBehaviour.SetBool("ATTACK", true);
        _animBehaviour.SetBool("WALK", false);
        _animBehaviour.SetBool("DEAD", false);
    }

    public void Movimento()
    {
        _animBehaviour.SetBool("WALK", true);
        _animBehaviour.SetBool("IDLE", false);
        _animBehaviour.SetBool("ATTACK", false);
        _animBehaviour.SetBool("DEAD", false);
    }
    public void Dead()
    {
        _dead = true;

        _animBehaviour.SetBool("DEAD", true);
        _animBehaviour.SetBool("WALK", false);
        _animBehaviour.SetBool("IDLE", false);
        _animBehaviour.SetBool("ATTACK", false);
    }
    #endregion

    public void Routine()
    {
        if (_dead) return;
        if (!IsOnGround()) return;

        Vector3 v2 = _player.transform.position - transform.position;

        v2 = v2.normalized;

        if (IsLookingPlayer())
        {
            Attack();
        }
        else
        {
            if (Vector2.Distance(_player.transform.position, transform.position) > 1.0f && v2.x > 0)
            {
                _contador2 += Time.deltaTime;
                if (_contador2 < 2)
                {
                    Idle();
                }
                if (_contador2 > 2)
                {
                    Movimento();
                    transform.position += (v2 * _char.speed * Time.deltaTime);
                    transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                    _contador = 0;
                }
            }
            else
            {
                _contador += Time.deltaTime;
                if (_contador < 2)
                {
                    Idle();
                }
                if (_contador > 2)
                {
                    Movimento();
                    transform.position += (v2 * _char.speed * Time.deltaTime);
                    transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                }
                _contador2 = 0;
            }
        }
    }

    bool IsOnGround()
    {
        return _touchGround = Physics2D.Linecast(transform.position, _groundDetector.position, 1 << LayerMask.NameToLayer("GROUND"));
    }
    bool IsOnWall()
    {
        return _touchWall = Physics2D.Linecast(transform.position, _wallDetector.position, 1 << LayerMask.NameToLayer("WALL"));
    }
    bool IsLookingPlayer()
    {
        return _lookingHero = Physics2D.Linecast(transform.position, _enemyView.position, 1 << LayerMask.NameToLayer("PLAYER"));
    }

    public void SufferDamage(int atk)
    {
        _currentLife -= atk;

        _lifebar.GetComponent<LifebarBehaviour>().Show(_currentLife / _char.maxHealth);

        if(_currentLife < 1)
        {
            _currentLife = 0;

            Dead();

            EnemySpawner.OnSpawnEnemy?.Invoke();
            GetComponent<EnemyBehaviour>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Component damageable = collision.gameObject.GetComponent(typeof(ICanBeDamaged));

        if (damageable == null) return;

        damageable.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(800 * transform.localScale.x, 500), ForceMode2D.Force);

        (damageable as ICanBeDamaged).SufferDamage(_char.damage);
    }
}

