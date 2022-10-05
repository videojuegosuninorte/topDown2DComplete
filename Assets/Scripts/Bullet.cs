using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bullerPower;

    internal void Init(int bullerPower)
    {
        this.bullerPower = bullerPower;
    }
}
