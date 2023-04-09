using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : PlayerBehaviour
{
    [Header("Knight")]

    [SerializeField] Transform _flameSpawner;
    [SerializeField] GameObject _shield;
    public void SummonFire()
    {
        GameObject temp = Instantiate(characterStats.projectile, new Vector3(_flameSpawner.position.x, _flameSpawner.position.y), Quaternion.identity);
        temp.GetComponent<SpecialAttack>().MoveDirection(transform.localScale.x);
    }

    public void ActiveShield()
    {
        _shield.GetComponent<Animator>().Play("ShieldAnim");
    }

}
