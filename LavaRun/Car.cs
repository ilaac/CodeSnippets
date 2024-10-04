using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Car : MonoBehaviour
{
    [Header("Car Settings")]
    public Wheel[] All_Wheels;
    public float Power;
    public float MaxAngle;
    public float DownForceMagnitude;
    public float DriftTraction;
    public float MaxSpeed;
    public float currentSpeed;
    public GameObject[] BrakeLights;
    public GameObject[] FrontLights;

    [Header("Cutscenes settings")]
    // refference scripts
    public CameraSwap camSwap;
    public ColliderSettings Settings;

    // Cutscenes 
    [SerializeField] private int _cutSceneTimer;
    [SerializeField] private float _pitchMax;
    [SerializeField] private AudioSource _engine;
    [SerializeField] private AudioSource _engineIdle;
    private float m_forward;
    private float m_angle;
    private float m_brake;
    private float timer;
    private Rigidbody rb;

    private float _pitch;

    [Header("Particles")]
    // Visuals
    public ParticleSystem dustCloud;
    


    private bool isDrifting = false;

    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _engine.Stop();
        _engineIdle.Play();
        dustCloud = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        m_forward = Input.GetAxis("Vertical");
        m_angle = Input.GetAxis("Horizontal");

        if (currentSpeed >= 1)
        {
            _engine.Play();
            _engineIdle.Stop();
        }
        else if (currentSpeed <= 0)
        {
            _engine.Stop();
            _engineIdle.Play();
        }

        // Calculate current speed
        currentSpeed = transform.InverseTransformDirection(rb.velocity).z;

        // Limit the speed
        if (currentSpeed > MaxSpeed)
        {
            rb.velocity = rb.velocity.normalized * MaxSpeed;
        }

        // Check for brake input
        if (Input.GetKey(KeyCode.Space))
        {
            m_brake = 900.0f;
            TurnOnBrakeLights(); // Turn on brake lights when braking.
            Settings.ApplyDriftSettings();
        }
        else
        {
            m_brake = 0.0f; // No brakes applied.
            ResetTraction();
            TurnOffBrakeLights(); // Turn off brake lights when not braking.
            Settings.ApplyNormalSettings();
        }

        // Check if the car is drifting based on the angle of the wheels.
        if (isDrifting)
        {
            ApplyDriftTraction(DriftTraction);
        }
        else
        {
            m_angle = m_angle * MaxAngle; // Apply normal steering when not drifting.
        }

        if (camSwap.vulcanoCutScene)
        {
            VulcanicExplosionScene(_cutSceneTimer);
        }

        if (camSwap.playerDead)
        {
            m_brake = 1000.0f;
            ApplyDriftTraction(DriftTraction);
        }
        ApplyBrakes(m_brake);

        if (currentSpeed >= 0.1)
        {
            
        }
    }

    private void FixedUpdate()
    {
        // Apply additional forces for better road grip (e.g., downforce).
        Vector3 downForce = -transform.up * DownForceMagnitude;
        rb.AddForce(downForce, ForceMode.Force);

        // Accelerate and turn each wheel
        foreach (Wheel _wheel in All_Wheels)
        {
            _wheel.Accelerate(m_forward * Power);
            _wheel.Turn(m_angle);
        }
        _pitch = Mathf.Lerp(0, 1, currentSpeed / MaxSpeed);
        _engine.pitch = _pitch * _pitchMax;
    }

    void VulcanicExplosionScene(int duration)
    {

        if (timer <= duration)
        {
            Debug.Log(timer);
            timer += Time.deltaTime;
            m_brake = 900.0f;
            ApplyDriftTraction(DriftTraction);
            TurnOnBrakeLights(); // Turn on brake lights when braking.
            rb.isKinematic = true;
        }
        else
        {
            camSwap.pState = PlayerState.alive;
            GameManager.instance.vulcanoCutSceneHappend = true;
            rb.isKinematic = false;

        }

    }

    void ApplyBrakes(float brakeForce)
    {
        foreach (Wheel _wheel in All_Wheels)
        {
            _wheel.Brake(brakeForce);
            // set side ways friction settins
        }

    }

    void ApplyDriftTraction(float traction)
    {
        foreach (Wheel _wheel in All_Wheels)
        {
            _wheel.SetTraction(traction);
        }
    }

    void ResetTraction()
    {
        foreach (Wheel _wheel in All_Wheels)
        {
            _wheel.ResetTraction();
        }
    }

    void TurnOnBrakeLights()
    {
        foreach (GameObject brakeLight in BrakeLights)
        {
            brakeLight.SetActive(true);
        }
    }

    void TurnOffBrakeLights()
    {
        foreach (GameObject brakeLight in BrakeLights)
        {
            brakeLight.SetActive(false);

            if (camSwap.playerDead)
            {
                brakeLight.SetActive(false);
            }
        }
    }

    public void TurnOffFrontLights()
    {
        foreach (GameObject frontLight in FrontLights)
        {
            frontLight.SetActive(false);
        }
    }

    void UpdateDrifting()
    {
        float averageWheelAngle = 60f;
        int driftingWheels = 60;

        foreach (Wheel _wheel in All_Wheels)
        {
            if (_wheel.Collider != null && _wheel.Collider.isGrounded)
            {
                if (Mathf.Abs(_wheel.Collider.steerAngle) > 60f)
                {
                    averageWheelAngle += _wheel.Collider.steerAngle;
                    driftingWheels++;
                }
            }
        }

        if (driftingWheels > 0)
        {
            isDrifting = true;
            m_angle = averageWheelAngle / driftingWheels;
        }
    }
}
