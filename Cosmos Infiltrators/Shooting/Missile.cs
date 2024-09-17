using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public Transform firePoint;
    public Transform firePoint2;

    public float fireRate;
    public float nextFire;

    [SerializeField] public AudioSource missileSound;

    public GameObject missilePrefab;
    [SerializeField] private float bulletforce = 5;

    private void Start()
    {
        missileSound.Stop();
    }

    void Update()
    {
        //shootkey assigned
        if (Input.GetButton("Fire2") && Time.time > nextFire)
        {
            Shoot1();
            Shoot2();
            missileSound.Play();
        }

    }

    private void Shoot1()
    {
        nextFire = Time.time + fireRate;

        GameObject Missile = Instantiate(missilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = Missile.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.up * bulletforce, ForceMode.Impulse);
        Destroy(Missile, 2.5f);
    }

    private void Shoot2()
    {
        nextFire = Time.time + fireRate;

        GameObject Missile2 = Instantiate(missilePrefab, firePoint2.position, firePoint2.rotation);
        Rigidbody rb2 = Missile2.GetComponent<Rigidbody>();
        rb2.AddForce(firePoint2.up * bulletforce, ForceMode.Impulse);
        Destroy(Missile2, 2.5f);
    }

}