using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletPower;
    public int unitType;


    internal void Init(int unitType, int bulletPower)
    {
        this.unitType = unitType;
        this.bulletPower = bulletPower;
        //GetComponent<Rigidbody2D>().tag = tag;
    }
}
