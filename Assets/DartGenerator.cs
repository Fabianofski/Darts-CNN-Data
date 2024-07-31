using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartGenerator : MonoBehaviour
{

    [SerializeField] private GameObject dartPrefab;
    [SerializeField] private float bullRadius;
    [SerializeField] private float outerBullRadius;
    [SerializeField] private float tripleStartRadius;
    [SerializeField] private float tripleEndRadius;
    [SerializeField] private float doubleStartRadius;
    [SerializeField] private float doubleEndRadius;

    void Start()
    {
        UpdatePosition();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdatePosition();
        }
    }

    void UpdatePosition()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var dart = transform.GetChild(i);
            dart.transform.localPosition = Random.insideUnitCircle * doubleEndRadius;
            dart.transform.localRotation = Quaternion.Euler(0, 180, Random.Range(0, 360));
        }
    }

    void OnDrawGizmos()
    {
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, bullRadius);
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, outerBullRadius);
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, tripleStartRadius);
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, tripleEndRadius);
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, doubleStartRadius);
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, doubleEndRadius);
    }
}
