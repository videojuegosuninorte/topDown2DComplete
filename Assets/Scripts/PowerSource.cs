using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSource : Unit
{
    public void Init()
    {
        base.Init(UnitType.POWER_SOURCE, 1, 14, 1, 1);
    }
}
