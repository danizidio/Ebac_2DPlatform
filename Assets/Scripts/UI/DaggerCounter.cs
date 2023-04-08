using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DaggerCounter : MonoBehaviour
{
    public static Action<string> OnUpdateValue;

    public static bool _canAddDagger;

    [SerializeField] TMP_Text _txt;
    [SerializeField] Image _daggerCounter, _daggerBorder;

    float _counter = 5;

    private void Update()
    {
        if (!_canAddDagger) return;

        _daggerCounter.GetComponent<Image>().fillAmount = (_counter * 2) / 10;

        if (_daggerCounter.fillAmount < 1)
        {
            _daggerBorder.color = Color.green;
            _daggerCounter.color = new Color(1, 0, 0, _daggerCounter.fillAmount + .1f);
        }
        else
        {
            _daggerBorder.color = Color.red;
            _daggerCounter.color = Color.green;
        }

        _counter -= Time.deltaTime;

        if (_counter <= 0)
        {
            Ninja.OnAddDagger?.Invoke();

            _counter = 5;
        }
    }

    void UpdateValue(string v)
    {
        _txt.text = v;
    }

    private void OnEnable()
    {
        OnUpdateValue = UpdateValue;
    }
}
