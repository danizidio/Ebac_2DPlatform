using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SpecialsEvents : MonoBehaviour
{

    public void EnterSuperTime()
    {
        GameObject _globalLight = GameObject.FindGameObjectWithTag("Global_Light");
        _globalLight.GetComponent<Light2D>().intensity = 0;

        GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;

        Time.timeScale = 0;
    }
    public void ExitSuperTime()
    {
        GameObject _globalLight = GameObject.FindGameObjectWithTag("Global_Light");
        _globalLight.GetComponent<Light2D>().intensity = 1;

        GetComponent<Animator>().updateMode = AnimatorUpdateMode.Normal;

        Time.timeScale = 1;
    }

}
