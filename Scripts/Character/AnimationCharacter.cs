using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCharacter : MonoBehaviour
{
    [SerializeField] private string layerIdle;
    [SerializeField] private string layerWalk;

    private Animator _animator;
    private MovementCharacter _movementCharacter;

    private readonly int _directionX = Animator.StringToHash("X");
    private readonly int _directionY = Animator.StringToHash("Y");
    private readonly int _defeated = Animator.StringToHash("Defeated");


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movementCharacter = GetComponent<MovementCharacter>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLayer();

        if (_movementCharacter.IsMovement == false)
        {
            return;
        }

        _animator.SetFloat(_directionX, _movementCharacter.MovementDirection.x);
        _animator.SetFloat(_directionY, _movementCharacter.MovementDirection.y);
    }

    private void ActivateLayer(string layerName)
    {
        for (int i = 0; i < _animator.layerCount; i++)
        {
            _animator.SetLayerWeight(i, 0);
        }

        _animator.SetLayerWeight(_animator.GetLayerIndex(layerName), 1);
    }

    private void UpdateLayer()
    {
        if (_movementCharacter.IsMovement)
        {
            ActivateLayer(layerWalk);
        }
        else
        {
            ActivateLayer(layerIdle);
        }
    }

    public void ReviveCharacter()
    {
        ActivateLayer(layerIdle);
        _animator.SetBool(_defeated, false);
    }

    private void CharacterDefeatedResponse()
    {
        ActivateLayer(layerIdle);
        _animator.SetBool(_defeated, true);
    }

    private void OnEnable()
    {
        CharacterLife.EventCharacterDefeated += CharacterDefeatedResponse;
    }

    private void OnDisable()
    {
        CharacterLife.EventCharacterDefeated -= CharacterDefeatedResponse;
    }
}