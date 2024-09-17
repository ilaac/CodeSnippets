using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationOfWeapon : MonoBehaviour
{
    [Header("Position")]
    [SerializeField] private float _amount;
    [SerializeField] private float _maxAmount;
    [SerializeField] private float _smoothAmount;

    [Header("Rotation")]
    [SerializeField] private float _rotationAmount;
    [SerializeField] private float _maxRotationAmount;
    [SerializeField] private float _smoothRotation;

    [Space]
    public bool rotationX = true;
    public bool rotationY = true;
    public bool rotationZ = true;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private float movementX;
    private float movementY;

    void Start()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
    }

    void Update()
    {
        CalculateSway();

        MoveSway();
        TiltSway();
    }

    private void CalculateSway()
    {
        movementX = -Input.GetAxis("Mouse X");
        movementY = -Input.GetAxis("Mouse Y");
    }

    private void MoveSway()
    {
        float moveX = Mathf.Clamp(movementX * _amount, -_maxAmount, _maxAmount);
        float moveY = Mathf.Clamp(movementY * _amount, -_maxAmount, _maxAmount);

        Vector3 finalPosition = new Vector3(moveX, moveY, 0);

        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition, Time.deltaTime * _smoothAmount);
    }

    private void TiltSway()
    {
        float tiltX = Mathf.Clamp(movementX * _rotationAmount, -_maxRotationAmount, _maxRotationAmount);
        float tiltY = Mathf.Clamp(movementY * _rotationAmount, -_maxRotationAmount, _maxRotationAmount);

        Quaternion finalRotation = Quaternion.Euler(new Vector3 (
            rotationX ? -tiltX : 0f,
            rotationY ?  tiltY : 0f,
            rotationZ ?  tiltY : 0f
            ));

        transform.localRotation = Quaternion.Slerp(transform.localRotation, finalRotation * initialRotation, Time.deltaTime * _smoothAmount);
    }
}
