using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : AttackingUnit
{

    public void Init(UnitType unitType)
    {
        this.unitType = unitType;
        switch (unitType)
        {
            case UnitType.TOWER_H:
                base.Init(unitType, 2, 3, 1, 1);
                GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case UnitType.TOWER_L:
                base.Init(unitType, 3, 4, 1, 1);
                break;
        }
    }

    public void SetGrid(Grid grid)
    {
        grid.SetWalkable(transform.localPosition, false);
    }
}
