using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Unit
{     
    public void SetGrid(Grid grid)
    {
        grid.SetWalkable(transform.localPosition, false);
    }
}
