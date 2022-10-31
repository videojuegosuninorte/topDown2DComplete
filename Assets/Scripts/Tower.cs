using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : AttackingUnit
{

    public void Init(int unitType)
    {
        this.unitType = unitType;
        switch (unitType)
        {
            case UnitType.TOWER_H:
                base.Init(unitType, 2, 5, 1, 1);
                GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case UnitType.TOWER_L:
                base.Init(unitType, 3, 6, 1, 1);
                break;
        }
    }


}
