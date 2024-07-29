using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartGenerator : MonoBehaviour
{

    [SerializeField]
    private GameObject dartPrefab;
    [SerializeField]
    private float dartBoardRadius;

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
            dart.transform.localPosition = Random.insideUnitCircle * dartBoardRadius;
            dart.transform.localRotation = Quaternion.Euler(0, 180, Random.Range(0, 360));
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, dartBoardRadius);
    }
}
