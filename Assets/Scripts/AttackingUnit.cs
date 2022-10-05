using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingUnit : Unit
{
    private Rigidbody2D rb;
    private Vector2 enenyLocation;
    private Bullet bulletPrefab;
    private float bulletSpeed = 20f;
    public float shootingInterval = 0.3f;
    private float period = 0.0f;
    private bool startShotting = false;
    public Transform firePoint;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Init(int attack, int defense, int cost, int range)
    {
        base.Init(attack, defense, cost, range);
    }

    

    public void lookAtTarget()
    {
        Vector2 lookDir = enenyLocation - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
        rb.rotation = angle;
    }

    void Update()
    {
        if (!startShotting)
            return;

        if (period > shootingInterval)
        {
            shoot();
            period = 0;
        }
        period += UnityEngine.Time.deltaTime;
    }

    private void shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.Init(attack);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
    }

}
