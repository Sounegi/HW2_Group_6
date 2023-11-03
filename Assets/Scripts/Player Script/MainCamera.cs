using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private static MainCamera instance;

    void Awake()
    {
        instance = this;
    }

    public static MainCamera GetInstance()
    {
        return instance;
    }

    public void RotateCamera(float rotationValue)
    {
        float minXRotation = 15f;
        float maxXRotation = 40f;

        float newRotationX = transform.localEulerAngles.x + rotationValue * 5f;

        newRotationX = Mathf.Clamp(newRotationX, minXRotation, maxXRotation);

        Quaternion verticalRotation = Quaternion.Euler(newRotationX, 0f, 0f);

        transform.localRotation = Quaternion.Slerp(transform.localRotation, verticalRotation, Time.deltaTime);
    }
    
}
