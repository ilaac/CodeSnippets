using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;

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

        //destroy hiteffect
        Destroy(hitEffect, 2f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Collision 
        if (collision.gameObject.TryGetComponent<RamEnemy>(out RamEnemy enemyComponent1))
        {
            enemyComponent1.TakeDamage(1);
        }

        if (collision.gameObject.TryGetComponent<ShootEnemy>(out ShootEnemy enemyComponent2))
        {
            enemyComponent2.TakeDamage(1);
        }

        if (collision.gameObject.TryGetComponent<ShotgunEnemy>(out ShotgunEnemy enemyComponent3))
        {
            enemyComponent3.TakeDamage(1);
        }

        if (collision.gameObject.TryGetComponent<BossFighting>(out BossFighting enemyComponent4))
        {
            enemyComponent4.TakeDamage(1);
        }

        Instantiate(hitEffect, transform.position, transform.rotation);
        Destroy(this);
    }
}

