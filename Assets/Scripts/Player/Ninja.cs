using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Ninja : PlayerBehaviour
{
    [Header("Ninja")]

    public static System.Action OnAddDagger;

    [SerializeField] Transform _daggerSpawner;

    [SerializeField] int _maxDaggers;

    int _currentDaggers;

    bool _canAddDaggers;

    public void SpecialDagger(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _anim.SetTrigger("SPECIAL2");
            ThrowingDagger();
        }
    }

    void ThrowingDagger()
    {
        if (_currentDaggers <= 0) return;

        GameObject temp = Instantiate(characterStats.projectile, new Vector3(_daggerSpawner.position.x, _daggerSpawner.position.y), Quaternion.identity);
        temp.GetComponent<Dagger>().MoveDirection(transform.localScale.x);

        RemoveDaggers();

        StartCoroutine(CountingDaggers());
    }

    void RemoveDaggers()
    {
        _currentDaggers--;

        DaggerCounter.OnUpdateValue?.Invoke(_currentDaggers.ToString());
    }

    void AddDagger()
    {
        _currentDaggers++;

        if(_currentDaggers >= _maxDaggers)
        {
            _currentDaggers = _maxDaggers;

            DaggerCounter._canAddDagger = false;
        }
        else
        {
            DaggerCounter.OnUpdateValue?.Invoke(_currentDaggers.ToString());
        }
    }

    public IEnumerator CountingDaggers()
    {
        yield return new WaitForSeconds(2);

        DaggerCounter._canAddDagger = true;

        yield return 0;
    }

    void SettingDaggers()
    {
        _currentDaggers = _maxDaggers;

        DaggerCounter.OnUpdateValue?.Invoke(_currentDaggers.ToString());
    }

    private void OnEnable()
    {
        OnAddDagger = AddDagger;

        SettingDaggers();
    }
}
