using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private string _ballTag;
    [SerializeField] private Mover _moverScript;
    [SerializeField] private GameObject _playerKick;

    public event Action ballDetectedEvent;

    private bool _send;


    public void Send()
    {
        _send = true;
    }

    public void AnotherSend()
    {
        _send = false;
    }

    private void Awake()
    {
        ballDetectedEvent += () =>
        {
            gameObject.SetActive(false);
            _playerKick.SetActive(true);
        };
    }

    private void OnEnable()
    {
        _moverScript.enabled = true;
    }

    private void OnDisable()
    {
        _moverScript.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(_ballTag) && !_send)
        {
            ballDetectedEvent?.Invoke();
        }
    }
}
