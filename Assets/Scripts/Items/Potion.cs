using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField] ScriptableObject_Int _lifeToRecover;

    [SerializeField] GameObject _particleFx;

    public int lifeToRecover()
    {
        Instantiate(_particleFx, this.transform.position,Quaternion.identity);

        return _lifeToRecover.intValue;
    }
}
