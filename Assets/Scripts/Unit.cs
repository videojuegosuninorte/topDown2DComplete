using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    protected int attack;
    private int defense;
    private int cost;
    private int range;
    protected bool started;
    
    public void Init(int attack, int defense, int cost, int range)
    {
        this.attack = attack;
        this.defense = defense;
        this.cost = cost;
        this.range = range;
        started = true;
    }
}
