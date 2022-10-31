using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected int attack;
    private int defense;
    private int cost;
    private int range;
    protected int unitType;
    protected bool started;
    protected Grid grid;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(int unitType, int attack, int defense, int cost, int range)
    {
        this.unitType = unitType;
        this.attack = attack;
        this.defense = defense;
        this.cost = cost;
        this.range = range;
        started = true;
    }

    public bool IsPlayer()
    {
        switch (unitType)
        {
            case UnitType.TOWER_L:
            case UnitType.TOWER_H:
            case UnitType.POWER_SOURCE:
                return false;
        }
        return true;
    }

    public bool IsSameTeam(int someUnitType)
    {
        switch (someUnitType)
        {
            case UnitType.TOWER_L:
            case UnitType.TOWER_H:
            case UnitType.POWER_SOURCE:
                if (!IsPlayer())
                {
                    return true;
                }
                break;
            case UnitType.INFANTERY_H:
            case UnitType.INFANTERY_L:
            case UnitType.INFANTERY_K:
                if (IsPlayer())
                {
                    return true;
                }
                break;
        }
        return false;
    }

    public string printUnitType()
    {
        switch (unitType)
        {
            case UnitType.TOWER_L:
                return "TOWER_L";
            case UnitType.TOWER_H:
                return "TOWER_H";
            case UnitType.POWER_SOURCE:
                return "POWER_SOURCE";
            case UnitType.INFANTERY_H:
                return "INFANTERY_H";
            case UnitType.INFANTERY_L:
                return "INFANTERY_L";
            case UnitType.INFANTERY_K:
                return "INFANTERY_K";
        }
        return "??";
        
    }

    public bool isPowerSource()
    {
        return (unitType == UnitType.POWER_SOURCE) ;
    }

    protected void LowerDefense(Collider2D collision)
    {
        // Debug.Log("Hit");
        if (collision.gameObject.tag == "Bullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (!IsSameTeam(bullet.unitType)){
                defense -= bullet.bulletPower;
                //Debug.Log("Hit by a bullet, new HP "+ HP);
                Destroy(collision.gameObject);
                if (defense < 0)
                {
                    if (unitType != UnitType.POWER_SOURCE)
                        grid.SetWalkable(transform.localPosition, true);
                    Destroy(this.gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        LowerDefense(collision);
    }

    public void SetGrid(Grid grid)
    {
        this.grid = grid;
        grid.SetWalkable(transform.localPosition, false);
    }
}
