using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{

    [SerializeField] private float maxRotation;

    void Start()
    {
        UpdateRotation();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateRotation();
        }
    }

    void UpdateRotation()
    {
        var rotationX = Random.Range(-maxRotation, maxRotation);
        var rotationY = Random.Range(-maxRotation, maxRotation);
        var rotationZ = Random.Range(-maxRotation, maxRotation);
        transform.localRotation = Quaternion.Euler(rotationX, rotationY, rotationZ);
    }
}
