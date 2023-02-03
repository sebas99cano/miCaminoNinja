using System;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private Vector3[] points;
    public Vector3[] Points => points;
    public Vector3 ActualPosition { get; private set; }

    private bool _gameStarted;

    private void Start()
    {
        _gameStarted = true;
        ActualPosition = transform.position;
    }

    public Vector3 GetMovementPosition(int index)
    {
        return ActualPosition + points[index];
    }

    private void OnDrawGizmos()
    {
        if (_gameStarted == false && transform.hasChanged)
        {
            ActualPosition = transform.position;
        }

        if (points == null || points.Length <= 0)
        {
            return;
        }

        for (int i = 0; i < points.Length; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(points[i] + ActualPosition, 0.5f);
            if (i < points.Length - 1)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(points[i] + ActualPosition, points[i + 1] + ActualPosition);
            }
        }
    }
}