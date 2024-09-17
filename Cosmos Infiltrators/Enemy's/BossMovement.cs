using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{

    [SerializeField] private float bossMoveSpeed = 20f;
    [SerializeField] private float stoppingDistance;
    [SerializeField] private float retreatDistance;

    private Rigidbody rb;
    public Transform player;

    private float bossTransform;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("playerShip").transform;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, bossMoveSpeed * Time.deltaTime);
        }
        else if (Vector3.Distance(transform.position, player.position) < stoppingDistance && Vector3.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if (Vector3.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, -bossMoveSpeed * Time.deltaTime);
        }
        transform.position = new Vector3(transform.position.x, 1, 3.2f);

    }
}
