using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ScriptableObject_Int : ScriptableObject
{
    [SerializeField] int _intValue;
    public int intValue { get { return _intValue; } set { _intValue = value; } }
}
