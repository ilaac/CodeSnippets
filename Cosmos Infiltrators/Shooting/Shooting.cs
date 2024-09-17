using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public Transform firePoint2;
    public Transform firePoint3;
    public Transform firePoint4;

    public float fireRate;
    public float nextFire;

    [SerializeField] public AudioSource gunSound;

    public GameObject bulletPrefab;
    [SerializeField] private float bulletforce = 5;

    public static bool shootEnabled = true;

    private void Start()
    {
        gunSound.Stop();
    }

    void Update()
    {
        //shootkey assigned
        if (Input.GetButton("Jump") && Time.time > nextFire && shootEnabled == true) 
        {
            Shoot1();
            Shoot2();
            Shoot3();
            Shoot4();
            gunSound.Play();
        }
    }

private void Shoot1()
    {
        nextFire = Time.time + fireRate;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.up * bulletforce, ForceMode.Impulse);
        Destroy(bullet, 2.5f);
    }

    private void Shoot2()
    {
        nextFire = Time.time + fireRate;

        GameObject bullet2 = Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);
        Rigidbody rb2 = bullet2.GetComponent<Rigidbody>();
        rb2.AddForce(firePoint2.up * bulletforce, ForceMode.Impulse);
        Destroy(bullet2, 2.5f);
    }

    private void Shoot3()
    {
        nextFire = Time.time + fireRate;

        GameObject bullet3 = Instantiate(bulletPrefab, firePoint3.position, firePoint3.rotation);
        Rigidbody rb3 = bullet3.GetComponent<Rigidbody>();
        rb3.AddForce(firePoint3.up * bulletforce, ForceMode.Impulse);
        Destroy(bullet3, 2.5f);
    }

    private void Shoot4()
    {
        nextFire = Time.time + fireRate;

        GameObject bullet4 = Instantiate(bulletPrefab, firePoint4.position, firePoint4.rotation);
        Rigidbody rb4 = bullet4.GetComponent<Rigidbody>();
        rb4.AddForce(firePoint4.up * bulletforce, ForceMode.Impulse);
        Destroy(bullet4, 2.5f);
    }
}
