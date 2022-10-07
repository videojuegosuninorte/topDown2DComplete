using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSource : Unit
{

    public delegate void PowerSourceDestroyed();
    public static event PowerSourceDestroyed onPowerSourceDestroy;

    private void OnDestroy()
    {
        if (onPowerSourceDestroy != null)
        {
            onPowerSourceDestroy();
        }
    }

    public void Init()
    {
        base.Init(UnitType.POWER_SOURCE, 1, 14, 1, 1);
    }
}
