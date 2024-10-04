using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    public bool IsPowered;
    public bool CanTurn;

    public GameObject Visual;

    public WheelCollider Collider { get; private set; }

    public float spring = 15000.0f;
    public float damper = 2000.0f;
    public float targetPosition = 0.3f;

    // Store the default sideways friction for resetting.
    private WheelFrictionCurve defaultFriction;

    private void Start()
    {
        // Assign the WheelCollider to the public property.
        Collider = GetComponent<WheelCollider>();

        // Set up suspension settings.
        WheelColliderSetup();

        // Store the default sideways friction.
        defaultFriction = Collider.sidewaysFriction;
    }

    private void WheelColliderSetup()
    {
        JointSpring suspension = Collider.suspensionSpring;
        suspension.spring = spring;
        suspension.damper = damper;
        Collider.suspensionSpring = suspension;
        Collider.forceAppPointDistance = targetPosition;
    }

    private void Update()
    {
        WheelColliderVisualUpdate();
    }

    private void WheelColliderVisualUpdate()
    {
        Vector3 _position;
        Quaternion _rotation;
        Collider.GetWorldPose(out _position, out _rotation);
        Visual.transform.position = _position;
        Visual.transform.rotation = _rotation;
    }

    public void Accelerate(float _power)
    {
        if (IsPowered)
        {
            Collider.motorTorque = _power;
        }
    }

    public void Turn(float _angle)
    {
        if (CanTurn)
        {
            Collider.steerAngle = _angle;
        }
    }

    public void Brake(float _power)
    {
        Collider.brakeTorque = _power;
    }

    // Function to set traction (e.g., for drifting).
    public void SetTraction(float traction)
    {
        WheelFrictionCurve friction = Collider.sidewaysFriction;
        friction.stiffness = traction;
        Collider.sidewaysFriction = friction;
    }

    // Function to reset traction to default values.
    public void ResetTraction()
    {
        Collider.sidewaysFriction = defaultFriction;
    }
}
