using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingUnit : Unit
{
    private Vector2 enenyLocation;
    private float bulletSpeed = 20f;
    private int period = 0;
    private bool startShotting = false;

    public Bullet bulletPrefab;
    public Transform firePoint;
    public int ShootingInterval = 10;

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<Unit>() != null) {
            Unit unit = collision.gameObject.GetComponent<Unit>();
            //Debug.Log("OnTriggerStay2D with Unit "+ printUnitType()+ " "+ unit.printUnitType());
            if (IsPlayer() != unit.IsPlayer())
            {
                //Debug.Log("OnTriggerStay2D with Unit Enemy");
                startShotting = true;
                enenyLocation = collision.gameObject.transform.position;
                lookAtTarget();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        startShotting = false; 
    }



    public void lookAtTarget()
    {
        //Debug.Log("lookAtTarget");
        Vector2 lookDir = enenyLocation - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;
        rb.rotation = angle;
    }

    void FixedUpdate()
    {

        VerifyShoot();
    }


    public void VerifyShoot()
    {

        //if (unitType == UnitType.INFANTERY_L)
        //{
        //    startShotting = true;
        //    enenyLocation = new Vector2(2, 19); ;
        //    lookAtTarget();
        //}

        if (!startShotting)
            return;



        shoot();
    }

    private void shoot()
    {
        if (period < ShootingInterval)
        {
            period++;
            return;
        }
        period = 0;

        Bullet bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        if (unitType == UnitType.INFANTERY_H || unitType == UnitType.INFANTERY_L)
        {
            bullet.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        bullet.Init(unitType,attack);
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
    }

}
