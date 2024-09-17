using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : MonoBehaviour
{
    public Transform player;
    [SerializeField] GameObject bullet;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameManager game;

    [SerializeField] private float speed;
    [SerializeField] private float health;
    [SerializeField] private float stoppingDistance;
    [SerializeField] private float retreatDistance;

    [SerializeField] private AudioSource enemyfireSound;

    float nextFire;
    [SerializeField] private float bulletforce;
    [SerializeField] float fireRate;
    [SerializeField] private int points;

    [SerializeField] private GameObject destroyEffect;
    [SerializeField] private bool playOnceAndDestroy;

    private void Awake()
    {
        //Firerate
        game = FindObjectOfType<GameManager>();
        fireRate = 6;
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

        //Shoot at player
        CheckIfTimeToFire();
    }

    void CheckIfTimeToFire()
    {
        //Shooting logic with clone destroy funciton
        if (Time.time > nextFire)
        {
            GameObject Bullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
            Rigidbody rb = Bullet.GetComponent<Rigidbody>();
            rb.AddForce(firePoint.up * bulletforce, ForceMode.Impulse);
            enemyfireSound.Play();

            Destroy(Bullet, 5f);

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
}
