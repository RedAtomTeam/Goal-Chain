using System;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private string _ballTag;
    [SerializeField] private AudioClip _gateClip;


    public event Action ballDetectedEvent;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(_ballTag))
            DetectBall();
    }

    private void DetectBall()
    {
        AudioService.Instance?.PlayEffect(_gateClip);
        ballDetectedEvent?.Invoke();
    }
}
