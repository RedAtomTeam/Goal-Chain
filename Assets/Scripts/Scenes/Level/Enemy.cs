using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private string _ballTag;


    public event Action ballDetectedEvent;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(_ballTag))
            if (!collision.GetComponent<Ball>().IsUnderPlayer)
                ballDetectedEvent?.Invoke();
    }
}
