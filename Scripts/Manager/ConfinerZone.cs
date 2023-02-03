using System;
using Cinemachine;
using UnityEngine;

public class ConfinerZone : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera newCamera;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            newCamera.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            newCamera.gameObject.SetActive(false);
        }
    }
}