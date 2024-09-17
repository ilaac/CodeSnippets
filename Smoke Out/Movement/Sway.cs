using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour
{
    [Header("hipFire Sway")]
    [SerializeField] private float _intensity;
    [SerializeField] private float _smooth;

    [Header("AimSway")]
    [SerializeField] private float _aimIntensity;
    [SerializeField] private float _aimSmooth;

    private Aim aimScript;
    private bool wasAiming = false; // Track if aiming down sights in the previous frame

    private Quaternion origin_rotation;

    private void Start()
    {
        origin_rotation = transform.localRotation;
        aimScript = GameObject.FindObjectOfType<Aim>();

        if (aimScript == null)
        {
            Debug.LogWarning("Aim script not found in the scene!");
        }
    }

    void Update()
    {
        if (aimScript != null)
        {
            bool isAiming = aimScript.isADS;

            if (isAiming && !wasAiming)
            {
                // Reset sway immediately when entering ADS
                ResetSwayImmediate();
            }

            if (isAiming)
            {
                UpdateAimsway();
            }
            else
            {
                Updatesway();
            }

            wasAiming = isAiming;
        }
    }

    private void Updatesway()
    {
        float x_mouse = Input.GetAxis("Mouse X");
        float y_mouse = Input.GetAxis("Mouse Y");

        Quaternion x_adj = Quaternion.AngleAxis(-_intensity * x_mouse, Vector3.up);
        Quaternion y_adj = Quaternion.AngleAxis(_intensity * y_mouse, Vector3.right);
        Quaternion target_rotation = origin_rotation * x_adj * y_adj;

        transform.localRotation = Quaternion.Lerp(transform.localRotation, target_rotation, Time.deltaTime * _smooth);
    }

    private void UpdateAimsway()
    {
        float x_aimMouse = Input.GetAxis("Mouse X");
        float y_aimMouse = Input.GetAxis("Mouse Y");

        Quaternion x_Aimadj = Quaternion.AngleAxis(-_aimIntensity * x_aimMouse, Vector3.up);
        Quaternion y_Aimadj = Quaternion.AngleAxis(_aimIntensity * y_aimMouse, Vector3.right);
        Quaternion target_rotation = origin_rotation * x_Aimadj * y_Aimadj;

        transform.localRotation = Quaternion.Lerp(transform.localRotation, target_rotation, Time.deltaTime * _aimSmooth);
    }

    private void ResetSwayImmediate()
    {
        transform.localRotation = origin_rotation;
    }
}
