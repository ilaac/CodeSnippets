using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunEnemy : MonoBehaviour
{
    public Transform player;
    [SerializeField] GameObject bullet;
    [SerializeField] private GameManager game;

    [SerializeField] private float speed;
    [SerializeField] private float health;
    [SerializeField] private float stoppingDistance;
    [SerializeField] private float retreatDistance;

    [SerializeField] private AudioSource enemyfireSound;

    private float nextFire;
    [SerializeField] private float bulletforce;
    [SerializeField] float fireRate;
    [SerializeField] private int points;

    [SerializeField] private GameObject destroyEffect;
    [SerializeField] private bool playOnceAndDestroy;

    public Transform[] firepoints;
        

private void Awake()
    {
        //Firerate
        game = FindObjectOfType<GameManager>();
        nextFire = fireRate;
        enemyfireSound.Stop();
    }

    void Start()
    {
        //Finds player
        player = GameObject.FindGameObjectWithTag("playerShip").transform;
        enemyfireSound.Stop();
    }

    void Update()
    {
        //Look at player
        this.gameObject.transform.LookAt(player);

        //Checks if its time to fire
        CheckIfTimeToFire();

        //Move with player
        if (Vector3.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else if (Vector3.Distance(transform.position, player.position) < stoppingDistance && Vector3.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if (Vector3.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }
    }

    private void CheckIfTimeToFire()
    {
        if (Time.time > nextFire)
        {
            Shoot1();
            Shoot2();
            Shoot3();
            nextFire = Time.time + fireRate;

        }
    }

    public void TakeDamage(float damageAmount)
    {
        //Damage calculation
        health -= damageAmount;

        if (health <= 0)
        {
            Instantiate(destroyEffect, transform.position, transform.rotation);
            game.AddScore(points);
            Destroy(gameObject);
        }
    }

    #region // Shoot logic
    private void Shoot1()
    {
        GameObject Bullet = Instantiate(bullet, firepoints[0].position, firepoints[0].rotation);
        Rigidbody rb = Bullet.GetComponent<Rigidbody>();
        rb.AddForce(firepoints[0].up * bulletforce, ForceMode.Impulse);
        enemyfireSound.Play();

        Destroy(Bullet, 5f);

        nextFire = Time.time + fireRate;
    }

    private void Shoot2()
    {
        GameObject Bullet = Instantiate(bullet, firepoints[1].position, firepoints[1].rotation);
        Rigidbody rb = Bullet.GetComponent<Rigidbody>();
        rb.AddForce(firepoints[1].up * bulletforce, ForceMode.Impulse);
        Destroy(Bullet, 5f);

        nextFire = Time.time + fireRate;
    }

    private void Shoot3()
    {
        GameObject Bullet = Instantiate(bullet, firepoints[2].position, firepoints[2].rotation);
        Rigidbody rb = Bullet.GetComponent<Rigidbody>();
        rb.AddForce(firepoints[2].up * bulletforce, ForceMode.Impulse);
        Destroy(Bullet, 5f);

        nextFire = Time.time + fireRate;
    }
    #endregion
}

