using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShooting : MonoBehaviour
{
    public Transform player;
    [SerializeField] GameObject bullet;
    [SerializeField] private GameManager game;

    [SerializeField] private AudioSource enemyfireSound;

    private float nextFire;
    [SerializeField] private float bulletforce;
    [SerializeField] float fireRate = 3;

    public Transform[] firepoints;


    private void Awake()
    {
        //Firerate
        game = FindObjectOfType<GameManager>();
        enemyfireSound.Stop();
        nextFire = fireRate;
    }

    void Start()
    {
        //Finds player
        player = GameObject.FindGameObjectWithTag("playerShip").transform;
        enemyfireSound.Stop();
    }

    void Update()
    {
        CheckIfTimeToFire();
    }
        private void CheckIfTimeToFire()
        {
            if (Time.time > nextFire)
            {
                Shoot1();
                Shoot2();
                Shoot3();
                Shoot4();
                Shoot5();
                Shoot6();
                Shoot7();
                Shoot8();
                Shoot9();
                nextFire = Time.time + fireRate;
            }
        }

        #region // Shoot logic
        private void Shoot1()
        {
            GameObject Bullet = Instantiate(bullet, firepoints[0].position, firepoints[0].rotation);
            Rigidbody rb = Bullet.GetComponent<Rigidbody>();
            rb.AddForce(firepoints[0].up * bulletforce, ForceMode.Impulse);
            enemyfireSound.Play();
            Destroy(Bullet, 2.5f);

            nextFire = Time.time + 0.1f + fireRate;
        }

        private void Shoot2()
        {
            GameObject Bullet = Instantiate(bullet, firepoints[1].position, firepoints[1].rotation);
            Rigidbody rb = Bullet.GetComponent<Rigidbody>();
            rb.AddForce(firepoints[1].up * bulletforce, ForceMode.Impulse);
            Destroy(Bullet, 2.5f);

            nextFire = Time.time + 0.2f + fireRate;
        }

        private void Shoot3()
        {
            GameObject Bullet = Instantiate(bullet, firepoints[2].position, firepoints[2].rotation);
            Rigidbody rb = Bullet.GetComponent<Rigidbody>();
            rb.AddForce(firepoints[2].up * bulletforce, ForceMode.Impulse);
            Destroy(Bullet, 2.5f);

            nextFire = Time.time + 0.3f + fireRate;
        }

    private void Shoot4()
        {
            GameObject Bullet = Instantiate(bullet, firepoints[3].position, firepoints[3].rotation);
            Rigidbody rb = Bullet.GetComponent<Rigidbody>();
            rb.AddForce(firepoints[3].up * bulletforce, ForceMode.Impulse);
            Destroy(Bullet, 2.5f);

            nextFire = Time.time + 0.4f + fireRate;
        }

    private void Shoot5()
        {
            GameObject Bullet = Instantiate(bullet, firepoints[4].position, firepoints[4].rotation);
            Rigidbody rb = Bullet.GetComponent<Rigidbody>();
            rb.AddForce(firepoints[4].up * bulletforce, ForceMode.Impulse);
            Destroy(Bullet, 2.5f);

            nextFire = Time.time + 0.5f + fireRate;
        }

    private void Shoot6()
    {
            GameObject Bullet = Instantiate(bullet, firepoints[5].position, firepoints[5].rotation);
            Rigidbody rb = Bullet.GetComponent<Rigidbody>();
            rb.AddForce(firepoints[5].up * bulletforce, ForceMode.Impulse);
            Destroy(Bullet, 2.5f);

            nextFire = Time.time + 0.6f + fireRate;
    }
    private void Shoot7()
    {
            GameObject Bullet = Instantiate(bullet, firepoints[6].position, firepoints[6].rotation);
            Rigidbody rb = Bullet.GetComponent<Rigidbody>();
            rb.AddForce(firepoints[6].up * bulletforce, ForceMode.Impulse);
            Destroy(Bullet, 2.5f);

            nextFire = Time.time + 0.7f+ fireRate;
    }
    private void Shoot8()
    {
            GameObject Bullet = Instantiate(bullet, firepoints[7].position, firepoints[7].rotation);
            Rigidbody rb = Bullet.GetComponent<Rigidbody>();
            rb.AddForce(firepoints[7].up * bulletforce, ForceMode.Impulse);
            Destroy(Bullet, 2.5f);

            nextFire = Time.time + 0.8f + fireRate;
    }
    private void Shoot9()
    {
            GameObject Bullet = Instantiate(bullet, firepoints[8].position, firepoints[8].rotation);
            Rigidbody rb = Bullet.GetComponent<Rigidbody>();
            rb.AddForce(firepoints[8].up * bulletforce, ForceMode.Impulse);
            Destroy(Bullet, 2.5f);

            nextFire = Time.time + 0.9f + fireRate;
    }
    #endregion
}

