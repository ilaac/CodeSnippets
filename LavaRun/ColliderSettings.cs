using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderSettings : MonoBehaviour
{
    [Header("Front Wheels")] public WheelCollider[] frontWheels;

    [Header("Back Wheels")] public WheelCollider[] backWheels;

    void SetWheelSettings(WheelCollider[] wheels /*, other parameters*/)
    {
        foreach (var wheel in wheels)
        {

            // Set sideways friction
            WheelFrictionCurve sidewaysFriction = wheel.sidewaysFriction;
            sidewaysFriction.extremumSlip = 0.2f;
            sidewaysFriction.extremumValue = 1f;
            sidewaysFriction.asymptoteSlip = 1f;
            sidewaysFriction.asymptoteValue = 0.75f;
            sidewaysFriction.stiffness = 1f;
            wheel.sidewaysFriction = sidewaysFriction;
        }
    }

    public void ApplyNormalSettings()
    {
        SetWheelSettings(frontWheels  /*, other front wheel parameters*/);
        SetWheelSettings(backWheels /*, other back wheel parameters*/);
        // Additional settings for normal driving
    }

    public void ApplyDriftSettings()
    {
        SetWheelSettings(frontWheels  /*, other front wheel parameters*/);
        SetWheelSettings(backWheels /*, other back wheel parameters*/);

        // Adjust settings for drift mode
        foreach (var wheel in backWheels)
        {
            // Set sideways friction
            WheelFrictionCurve sidewaysFriction = wheel.sidewaysFriction;
            sidewaysFriction.extremumSlip = 1.25f;
            sidewaysFriction.extremumValue = 1f;
            sidewaysFriction.asymptoteSlip = 1f;
            sidewaysFriction.asymptoteValue = 1f;
            sidewaysFriction.stiffness = 1f;
            wheel.sidewaysFriction = sidewaysFriction;
        }
    }
}
