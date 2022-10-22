using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellInfo
{
    public int x;
    public int y;
    public int unitType;

    public CellInfo(int x, int y,int unitType)
    {
        this.x = x;
        this.y = y;
        this.unitType = unitType;
    }

    public override string ToString() {

        return x + " " + y + " " + (int) unitType+";";
    }

}
