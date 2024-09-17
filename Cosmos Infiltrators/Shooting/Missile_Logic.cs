using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile_Logic : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;

    public ParticleSystem boost1;

    [SerializeField] private GameObject hitEffect;
    [SerializeField] private bool playOnceAndDestroy;


    void Start()
    {
        //Collision that ignores your own ship and bullets

        GameObject player = GameObject.FindGameObjectWithTag("playerShip");
        Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());

        GameObject ChainGunbullet = GameObject.FindGameObjectWithTag("Bullet");
        Physics.IgnoreCollision(ChainGunbullet.GetComponent<Collider>(), GetComponent<Collider>());
    }

    void Update()
    {
        //Speed of bullet
        transform.Translate(0, speed * Time.deltaTime, 0);
        boost1.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Collision 
        if (collision.gameObject.TryGetComponent<RamEnemy>(out RamEnemy enemyComponent1))
        {
            enemyComponent1.TakeDamage(3);
        }

        if (collision.gameObject.TryGetComponent<ShootEnemy>(out ShootEnemy enemyComponent2))
        {
            enemyComponent2.TakeDamage(3);
        }

        if (collision.gameObject.TryGetComponent<ShotgunEnemy>(out ShotgunEnemy enemyComponent3))
        {
            enemyComponent3.TakeDamage(3);
        }

        if (collision.gameObject.TryGetComponent<BossFighting>(out BossFighting enemyComponent4))
        {
            enemyComponent4.TakeDamage(3);
        }

        Instantiate(hitEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}