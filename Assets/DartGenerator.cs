using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DartGenerator : MonoBehaviour
{

    [SerializeField] private GameObject dartPrefab;
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Points")]
    [SerializeField] private float offset;
    [SerializeField] private int[] points;

    [Header("Double/ Triple")]
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
        Debug.Log("NEW CONFIGURATION");
        var text = "";
        var totalScore = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            var dart = transform.GetChild(i);
            var position = Random.insideUnitCircle * doubleEndRadius;
            dart.transform.localPosition = position;
            dart.transform.localRotation = Quaternion.Euler(0, 180, Random.Range(0, 360));

            var angle = UnitCircleToAngle(position); 
            var (score, ring) = getPointsForPosition(angle, position.magnitude);
            text += $"Dart{i}: {score} ";
            totalScore += score;
        }
        text += $"\nScore: {totalScore}";
        scoreText.text = text;
    }

    (int, string) getPointsForPosition(float angle, float distance) {
        var anglePerSegment = 360f / points.Length;
        var angleWithOffset = angle - offset;
        if (angleWithOffset > 360)
            angleWithOffset -= 360;
        var segmentIndex = Mathf.FloorToInt(angleWithOffset / anglePerSegment);

        var ring = "single";

        var score = points[segmentIndex];
        if (distance < bullRadius) {
            ring = "Bull";
            score = 50;
        } else if (distance < outerBullRadius) {
            ring = "Outer Bull";
            score = 25;
        } else if (distance < tripleEndRadius && distance > tripleStartRadius) {
            ring = "Triple";
            score *= 3;
        } else if (distance < doubleEndRadius && distance > doubleStartRadius) {
            ring = "Double";
            score *= 2;
        }

        return (score, ring);
    }

    void OnDrawGizmos()
    {
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, bullRadius);
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, outerBullRadius);
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, tripleStartRadius);
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, tripleEndRadius);
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, doubleStartRadius);
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, doubleEndRadius);

        Gizmos.color = Color.blue;
        var angle = 360f / points.Length;
        for (int i = 0; i < points.Length; i++) {
            var segmentDirection = AngleToUnitCircle(i * angle - offset);
            var endPos = transform.position + (segmentDirection * doubleEndRadius);
            Gizmos.DrawLine(transform.position, endPos);
        }
    }

    Vector3 AngleToUnitCircle(float angle) {
        float radians = angle * Mathf.Deg2Rad;
        float x = Mathf.Sin(radians);
        float y = Mathf.Cos(radians);
        return new Vector3(x, y, 0);
    }

    float UnitCircleToAngle(Vector3 vec) {
        var angleInRads = Mathf.Atan2(vec.x, vec.y);
        var angleInDeg = angleInRads * Mathf.Rad2Deg;
        if (angleInDeg < 0)
            angleInDeg += 360;

        return angleInDeg; 
    }
}
