using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BossFighting : MonoBehaviour
{
    public Transform player;
    [SerializeField] GameObject Enemy1Prefab;
    [SerializeField] private GameManager game;

    [SerializeField] private float health = 30;

    private float nextFire;
    [SerializeField] private float bulletforce;
    [SerializeField] float fireRate = 3;
    [SerializeField] private int points;

    [SerializeField] private GameObject destroyEffect;
    [SerializeField] private bool playOnceAndDestroy;


    public Transform[] firepoints;


    private void Awake()
    {
        //Firerate
        game = FindObjectOfType<GameManager>();
        nextFire = fireRate;
    }

    void Start()
    {
        //Finds player
        player = GameObject.FindGameObjectWithTag("playerShip").transform;
    }

    public void TakeDamage(float damageAmount)
    {
        //Damage calculation
        health -= damageAmount;

        if (health <= 0)
        {
            Instantiate(destroyEffect, transform.position, transform.rotation);
            game.AddScore(points);
            StartCoroutine(WaitForNext());
        }
    }

    IEnumerator WaitForNext()
    {
        yield return new WaitForSeconds(2f);
        nLevel();
    }

    public void nLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    void Update()
    {
        //Look at player
        this.gameObject.transform.LookAt(player);

        //Checks if its time to fire
        CheckIfTimeToFire();


        void CheckIfTimeToFire()
        {
            //Shoots Ram Enemy at player
            if (Time.time > nextFire)
            {
                Shoot1();
                Shoot2();
                Shoot3();
                nextFire = Time.time + fireRate;
            }
        }
        #region // Shoot logic
        void Shoot1()
        {
            GameObject Ram = Instantiate(Enemy1Prefab, firepoints[0].position, firepoints[0].rotation);
            Rigidbody rb = Ram.GetComponent<Rigidbody>();
            rb.AddForce(firepoints[0].up * bulletforce, ForceMode.Impulse);

            nextFire = Time.time + fireRate;
        }

         void Shoot2()
        {
            GameObject Ram = Instantiate(Enemy1Prefab, firepoints[1].position, firepoints[1].rotation);
            Rigidbody rb = Ram.GetComponent<Rigidbody>();
            rb.AddForce(firepoints[1].up * bulletforce, ForceMode.Impulse);

            nextFire = Time.time + fireRate;
        }

        void Shoot3()
        {
            GameObject Ram = Instantiate(Enemy1Prefab, firepoints[2].position, firepoints[2].rotation);
            Rigidbody rb = Ram.GetComponent<Rigidbody>();
            rb.AddForce(firepoints[2].up * bulletforce, ForceMode.Impulse);

            nextFire = Time.time + fireRate;
        }
        #endregion
    }
}