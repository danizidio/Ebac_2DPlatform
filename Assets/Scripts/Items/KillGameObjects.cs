using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimeCounter;

public class KillGameObjects : Timer
{
    private void Start()
    {
        SetTimer(GetTimer,() => Destroy(this.gameObject));
    }

    private void Update()
    {
        CountDown();
    }
}
