using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemySpawner : MonoBehaviour
{

    private float spawnTimer;
    private int enemyAmount;
    private int waveCount = 1;
    private float waveCooldown = 3f;

    void Start()
    {
        StartCoroutine(Waves());
    }
    void Update()
    {
        spawnTimer += Time.deltaTime;
        //Debug.Log(spawnTimer);

        //spawn boss with time
        if (spawnTimer >= 125f)
        {
            SpawnBoss();
            spawnTimer = -105f;
        }
        if (spawnTimer <= -100)
        {
            spawnTimer--;
        }

    }

    private void SpawnEnemyOne()
    {
        for (int i = 0; i < 1; i++)
        {
            enemyAmount = UnityEngine.Random.Range(6, 8);

            for (int j = 0; j < enemyAmount; j++)
            {
                float xPos = UnityEngine.Random.value > 0.5f ? 15f : -15f; //value between float of 0-1, if value over .5 spawn at 15f, otherwise at -15f. 
                float yPos = UnityEngine.Random.Range(1, 1);
                float zPos = UnityEngine.Random.value > 0.5f ? 15f : -15f;
                Vector3 spawnPos = new Vector3(xPos, yPos, zPos); //random spawn location
                if (FindCollisions(spawnPos) < 2) //prevents enemies from spawning on top of each other
                {
                    GameObject c = (GameObject)Instantiate(Resources.Load("Prefab/Ships/Enemy1"), spawnPos, Quaternion.identity); //loads in the enemy
                }
            }
        }

    }

    private void SpawnEnemyTwo()
    {
        for (int i = 0; i < 1; i++)
        {
            enemyAmount = UnityEngine.Random.Range(2, 5);

                for (int j = 0; j < enemyAmount; j++)
                {
                    float xPos = UnityEngine.Random.value > 0.5f ? 15f : -15f; //value between float of 0-1, if value over .5 spawn at 15f, otherwise at -15f. 
                    float yPos = UnityEngine.Random.Range(1, 1);
                    float zPos = UnityEngine.Random.value > 0.5f ? 15f : -15f;
                    Vector3 spawnPos = new Vector3(xPos, yPos, zPos); //random spawn location
                    if (FindCollisions(spawnPos) < 2) //prevents enemies from spawning on top of each other
                    {
                        GameObject c = (GameObject)Instantiate(Resources.Load("Prefab/Ships/Enemy2"), spawnPos, Quaternion.identity); //loads in the enemy
                    }
                }
        }
        
    }

    private void SpawnEnemyThree()
    {
        for (int i = 0; i < 1; i++)
        {
            enemyAmount = UnityEngine.Random.Range(2, 3);

            for (int j = 0; j < enemyAmount; j++)
            {
                float xPos = UnityEngine.Random.value > 0.5f ? 15f : -15f; //value between float of 0-1, if value over .5 spawn at 15f, otherwise at -15f. 
                float yPos = UnityEngine.Random.Range(1, 1);
                float zPos = UnityEngine.Random.value > 0.5f ? 15f : -15f;
                Vector3 spawnPos = new Vector3(xPos, yPos, zPos); //random spawn location
                if (FindCollisions(spawnPos) < 2) //prevents enemies from spawning on top of each other
                {
                    GameObject c = (GameObject)Instantiate(Resources.Load("Prefab/Ships/Enemy3"), spawnPos, Quaternion.identity); //loads in the enemy
                }
            }
        }

    }

    private void SpawnBoss()
    {
        float xPos = UnityEngine.Random.value > 0.5f ? 5f : 5f; //value between float of 0-1, if value over .5 spawn at 15f, otherwise at -15f. 
        float yPos = UnityEngine.Random.Range(1, 1);
        float zPos = UnityEngine.Random.value > 0.5f ? 1f : -1f;
        Vector3 spawnPos = new Vector3(xPos, yPos, zPos); //random spawn location
        GameObject c = (GameObject)Instantiate(Resources.Load("Prefab/Ships/Boss Ring"), spawnPos, Quaternion.identity); //loads in the enemy

    }

    //Detect number of collisions for only spawned objects
    private int FindCollisions(Vector3 pos)
    {
        Collider[] hits = Physics.OverlapSphere(pos, 2f);
        return hits.Length;
    }

    IEnumerator Waves()
    {
        if (waveCount == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                SpawnEnemyTwo();
                yield return new WaitForSeconds(waveCooldown);
            }
            waveCount = 2;
        }

        if (waveCount == 2)
        {
            for (int i = 0; i < 3; i++)
            {
                SpawnEnemyOne();
                yield return new WaitForSeconds(waveCooldown);
            }
            waveCount = 3;
        }

        if (waveCount == 3)
        {
            for (int i = 0; i < 3; i++)
            {
                SpawnEnemyThree();
                yield return new WaitForSeconds(waveCooldown);
            }
            waveCount = 4;
        }

        if (waveCount == 4)
        {
            for (int i = 0; i < 3; i++)
            {
                SpawnEnemyTwo();
                SpawnEnemyOne();
                yield return new WaitForSeconds(waveCooldown);
            }
            waveCount = 5;
        }

        if (waveCount == 5)
        {
            for (int i = 0; i < 3; i++)
            {
                SpawnEnemyOne();
                SpawnEnemyThree();
                yield return new WaitForSeconds(waveCooldown);
            }
            waveCount = 6;
        }

        if (waveCount == 6)
        {
            for (int i = 0; i < 3; i++)
            {
                SpawnEnemyThree();
                SpawnEnemyTwo();
                yield return new WaitForSeconds(waveCooldown);
            }
            waveCount = 7;
        }

        if (waveCount == 7)
        {
            for (int i = 0; i < 3; i++)
            {
                SpawnEnemyThree();
                SpawnEnemyThree();
                yield return new WaitForSeconds(waveCooldown);
            }
            waveCount = 8;
        }

        if (waveCount == 8)
        {
            for (int i = 0; i < 3; i++)
            {
                SpawnEnemyTwo();
                SpawnEnemyTwo();
                yield return new WaitForSeconds(waveCooldown);
            }
            waveCount = 9;
        }

        if (waveCount == 9)
        {
            for (int i = 0; i < 3; i++)
            {
                SpawnEnemyOne();
                SpawnEnemyOne();
                yield return new WaitForSeconds(waveCooldown);
            }
            waveCount = 10;
        }

        if (waveCount == 10)
        {
            for (int i = 0; i < 3; i++)
            {
                SpawnEnemyThree();
                SpawnEnemyTwo();
                yield return new WaitForSeconds(waveCooldown);
            }
            waveCount = 11;
        }

        if (waveCount == 11)
        {
            for (int i = 0; i < 3; i++)
            {
                SpawnEnemyThree();
                SpawnEnemyOne();
                yield return new WaitForSeconds(waveCooldown);
            }
            waveCount = 12;
        }

        if (waveCount == 12)
        {
            for (int i = 0; i < 3; i++)
            {
                SpawnEnemyThree();
                SpawnEnemyTwo();
                SpawnEnemyOne();
                yield return new WaitForSeconds(waveCooldown);
            }
            waveCount = 12;
        }

        if (waveCount == 13)
        {
            for (int i = 0; i < 3; i++)
            {
                SpawnEnemyThree();
                SpawnEnemyTwo();
                SpawnEnemyOne();
                yield return new WaitForSeconds(waveCooldown);
            }
            waveCount = 14;
        }

        if (waveCount == 14)
        {
            SpawnBoss();
        }

    }

}
