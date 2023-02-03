using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Horizontal,
    Vertical
}

public class WaypointMovement : MonoBehaviour
{
    [SerializeField] protected float velocity;
    [SerializeField] protected float awaitTime;

    protected Vector3 NextPoint => _waypoint.GetMovementPosition(_actualPointIndex);
    private Waypoint _waypoint;
    protected Animator Animator;
    private int _actualPointIndex;
    protected Vector3 LastPosition;
    public bool isWaiting = false;


    void Start()
    {
        _actualPointIndex = 0;
        _waypoint = GetComponent<Waypoint>();
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateCharacter();
        RotateVerticalCharacter();
        IdleCharacter();
        if (!isWaiting)
        {
            MoveCharacter();
            if (CheckPointArrived())
            {
                UpdateIndex();
            }
        }
    }

    IEnumerator WaitAtPoint(Action callback)
    {
        isWaiting = true;
        yield return new WaitForSeconds(awaitTime);
        callback.Invoke();
        isWaiting = false;
    }

    private void MoveCharacter()
    {
        transform.position = Vector3.MoveTowards(transform.position, NextPoint,
            velocity * Time.deltaTime);
    }

    private bool CheckPointArrived()
    {
        float distanceToActualPoint = (transform.position - NextPoint).magnitude;
        if (distanceToActualPoint < 0.1f)
        {
            LastPosition = transform.position;
            return true;
        }

        return false;
    }

    private void UpdateIndex()
    {
        StartCoroutine(WaitAtPoint(() =>
        {
            if (_actualPointIndex == _waypoint.Points.Length - 1)
            {
                _actualPointIndex = 0;
            }
            else
            {
                if (_actualPointIndex < _waypoint.Points.Length - 1)
                {
                    _actualPointIndex++;
                }
            }
        }));
    }

    protected virtual void RotateCharacter()
    {
    }

    protected virtual void RotateVerticalCharacter()
    {
    }

    protected virtual void IdleCharacter()
    {
    }
}