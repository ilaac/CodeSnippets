using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RamEnemy : MonoBehaviour
{ 

    [SerializeField] private GameManager game;

    public Transform player;
    [SerializeField] public float speed;
    [SerializeField] public float health;

    [SerializeField] private int points;
    [SerializeField] private AudioSource enemydeathSound;

    [SerializeField] private GameObject destroyEffect;
    [SerializeField] private bool playOnceAndDestroy;

        void Start()
        {
            //Finds player
            player = GameObject.FindGameObjectWithTag("playerShip").transform;
            enemydeathSound.Stop();
        }

        void Update()
        {
            //Look at player
            this.gameObject.transform.LookAt(player);

            //Movement
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            if (health <= 0)
            {
            enemydeathSound.Play();
            }

        }
        public void TakeDamage(float damageAmount)
        {
           
            //Damage calculation
            health -= damageAmount;

            enemydeathSound.Play();

        if (health <= 0)
            {
            Instantiate(destroyEffect, transform.position, transform.rotation);
            game.AddScore(points);
            Destroy(gameObject);
            }

        }

        private void OnCollisionEnter(Collision collision)
        {
            health --;

            if (health <= 0)
            {
            Instantiate(destroyEffect, transform.position, transform.rotation);
            game.AddScore(points);
            Destroy(this.gameObject);
            }

        }
}
