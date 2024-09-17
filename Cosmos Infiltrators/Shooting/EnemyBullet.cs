using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float damage;

    [SerializeField] private GameObject hitEffect;
    [SerializeField] private bool playOnceAndDestroy;


    void Start()
    {
        //Collision ignores enemy and enemy bullets
        Destroy(gameObject, 3.5f);

        GameObject player = GameObject.FindGameObjectWithTag("Enemy");
        Physics.IgnoreCollision(player.GetComponent<Collider>(), GetComponent<Collider>());

        GameObject EnemyBullet = GameObject.FindGameObjectWithTag("EnemyBullet");
        Physics.IgnoreCollision(EnemyBullet.GetComponent<SphereCollider>(), GetComponent<SphereCollider>());

        GameObject playerBullet = GameObject.FindGameObjectWithTag("Bullet");
        Physics.IgnoreCollision(playerBullet.GetComponent<Collider>(), GetComponent<Collider>());
    }

    void Update()
    {
        //Speed of bullets
        transform.Translate(0, speed * Time.deltaTime, 0);

        //destroy hiteffect
        Destroy(hitEffect, 2f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Collision logic and kill function
        if (collision.gameObject.TryGetComponent<ShootEnemy>(out ShootEnemy enemyComponent))
        {
            enemyComponent.TakeDamage(1);
        }

        Instantiate(hitEffect, transform.position, transform.rotation);
        Destroy(this);
    }
}