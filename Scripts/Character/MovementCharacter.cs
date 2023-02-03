using System;
using UnityEngine;

public class MovementCharacter : MonoBehaviour
{
    [SerializeField] private float movementVelocity;
    private Rigidbody2D _rigidbody2D;
    private CharacterLife _characterLife;
    private Vector2 _movementDirection;
    private Vector2 _input;

    public bool IsMovement => _movementDirection.magnitude > 0f;
    public Vector2 MovementDirection => _movementDirection;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _characterLife = GetComponent<CharacterLife>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (_characterLife.Defeated)
        {
            _movementDirection = Vector2.zero;
            return;
        }

        _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //X
        if (_input.x > 0.1f)
        {
            _movementDirection.x = 1f;
        }
        else if (_input.x < 0f)
        {
            _movementDirection.x = -1f;
        }
        else
        {
            _movementDirection.x = 0f;
        }

        //Y
        if (_input.y > 0.1f)
        {
            _movementDirection.y = 1f;
        }
        else if (_input.y < 0f)
        {
            _movementDirection.y = -1f;
        }
        else
        {
            _movementDirection.y = 0f;
        }
    }


    private void FixedUpdate()
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position +
                                  _movementDirection.normalized * movementVelocity * Time.fixedDeltaTime);
    }
}