using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : WaypointMovement
{
    [SerializeField] private Direction direction;
    private readonly int _walkDown = Animator.StringToHash("WalkDown");
    private readonly int _idle = Animator.StringToHash("Idle");

    protected override void RotateCharacter()
    {
        if (direction != Direction.Horizontal)
        {
            return;
        }

        if (NextPoint.x > LastPosition.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    protected override void RotateVerticalCharacter()
    {
        if (direction != Direction.Vertical)
        {
            return;
        }

        if (NextPoint.y > LastPosition.y)
        {
            Animator.SetBool(_walkDown, false);
        }
        else
        {
            Animator.SetBool(_walkDown, true);
        }
    }

    protected override void IdleCharacter()
    {
        if (isWaiting)
        {
            Animator.SetBool(_idle, true);
        }
        else
        {
            Animator.SetBool(_idle, false);
        }
    }
}