using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField] ScriptableObject_Int _lifeToRecover;
    public int lifeToRecover()
    {
        return _lifeToRecover.intValue;
    }
}
