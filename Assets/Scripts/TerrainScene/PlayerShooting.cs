using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform firePoint;
    public Bullet bulletPrefab;
    public float bulletForce = 20f;
    public float shootingInterval = 0.3f;
    private float period = 0.0f;
    private bool startShotting = false;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //https://www.youtube.com/watch?v=LNLVOjbrQj4

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PowerSource")
        {
            startShotting = true;
            lookAtTarget(collision.gameObject.transform.position);
        }
    }

    public void lookAtTarget(Vector3 targetPosition)
    {
        Vector2 lookDir = new Vector2(targetPosition.x, targetPosition.y) - rb.position;
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
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}
