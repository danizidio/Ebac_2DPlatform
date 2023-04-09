using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour
{
    [SerializeField] Image _redBar;

    public static System.Action<float> OnUpdateUI;

    void UpdateUI(float v)
    {
        _redBar.fillAmount = v;
    }

    private void OnEnable()
    {
        OnUpdateUI = UpdateUI;
    }
}
