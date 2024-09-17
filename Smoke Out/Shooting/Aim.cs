using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public GameObject Gun;
    public bool isADS;

    [SerializeField] private Camera playerCamera;
    [SerializeField] private float normalFov;
    [SerializeField] private float adsFov;
    [SerializeField] private float fovChangeSpeed ;

    [Header("References")]
    public PlayerMovement playerMovement;
    public Gun gunScript;

    private Vector3 moveVector;

    void Start()
    {
        isADS = false;
        playerCamera.fieldOfView = normalFov;
    }

    void Update()
    {
        if (!gunScript.isReloading && Input.GetMouseButtonDown(1))
        {
            Gun.GetComponent<Animator>().Play("AimDownSights");
            isADS = true;
            playerMovement.SetRunningAllowed(false); // Prevent running while aiming
        }

        if (Input.GetMouseButtonUp(1))
        {
            Gun.GetComponent<Animator>().Play("New State");
            isADS = false;
            playerMovement.SetRunningAllowed(true); // Allow running after releasing aim
        }

        // Smoothly change FOV based on isADS
        float targetFov = isADS ? adsFov : normalFov;
        playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, targetFov, Time.deltaTime * fovChangeSpeed);

        // Adjust player speed when ADS
        moveVector = playerMovement.GetMoveVector();
        playerMovement.UpdateSpeed(isADS, moveVector);
    }
}
