using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    [SerializeField] TMP_Text _text;
    [SerializeField] ScriptableObject_Int _soInt;

    private void Start()
    {
        _text.text = _soInt.intValue.ToString();
    }

    void TakeCoins()
    {
        _soInt.intValue++;
        _text.text = _soInt.intValue.ToString();
    }

    private void OnEnable()
    {
        GameManager.OnTakingCoins = TakeCoins;
    }
}
