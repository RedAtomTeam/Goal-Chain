using System.Collections.Generic;
using UnityEngine;

public class PlayerKick : MonoBehaviour
{
    [SerializeField] private Transform _ballPos;
    [SerializeField] private KickInterface KickInterface;

    [SerializeField] private Player _player;
    [SerializeField] private List<Player> _anotherPlayers;

    [SerializeField] private Ball _ball; // ������ �� ������ ����
    [SerializeField] private float _maxForce = 20f; // ������������ ���� �����
    [SerializeField] private float _maxDragDistance = 200f; // ������������ ���������� ����������� (� ��������)


    [SerializeField] private AudioClip _launchBallClip;

    private Vector2 _touchStartPos;
    private bool _isDragging = false;


    private bool _send;


    private void OnEnable()
    {
        _ball.transform.position = _ballPos.position;
        _ball.Stop();
        _ball.transform.SetParent(gameObject.transform);
        KickInterface.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        
    }

    private void Update()
    {
            HandleTouchInput();
        //if (_passTime == 0)
        //{
        //}
        //else
        //{
        //    _passTime += Time.deltaTime;
        //    if (_passTime >= _relaxTime)
        //    {
        //        _player.gameObject.SetActive(true);
        //        gameObject.SetActive(false);
        //    }
        //}
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    StartDrag(touch.position);
                    break;

                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    ContinueDrag(touch.position);
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    EndDrag(touch.position);
                    break;
            }
        }
    }

    private void StartDrag(Vector2 touchPosition)
    {
        _touchStartPos = touchPosition;
        _isDragging = true;
    }

    private void ContinueDrag(Vector2 touchPosition)
    {
        if (!_isDragging) return;

        Vector2 dragVector = _touchStartPos - touchPosition;
        float dragDistance = dragVector.magnitude;

        float normalizedDistance = Mathf.Clamp01(dragDistance / _maxDragDistance);

        // ������������ ���� �����
        float force = normalizedDistance * _maxForce;

        KickInterface.ChangeValue(force/_maxForce);
    }

    private void EndDrag(Vector2 touchEndPos)
    {
        if (!_isDragging) return;

        Vector2 dragVector = _touchStartPos - touchEndPos;
        float dragDistance = dragVector.magnitude;

        float normalizedDistance = Mathf.Clamp01(dragDistance / _maxDragDistance);

        // ������������ ���� �����
        float force = normalizedDistance * _maxForce;

        // ����������� ������ �����������
        Vector2 direction = dragVector.normalized;

        // ����������� �������� ���������� � ������� (���� �����)
        Vector3 worldDirection = new Vector3(-direction.x, -direction.y, 0);



        // ��������� ���

        _player.Send();
        foreach (var player in _anotherPlayers)
            player.AnotherSend();
        _player.gameObject.SetActive(true);
        gameObject.SetActive(false);
        _ball.Kick(worldDirection, force);
        AudioService.Instance?.PlayEffect(_launchBallClip);

        _isDragging = false;

        KickInterface.ChangeValue(0);
        KickInterface.gameObject.SetActive(false);



        // ����� ����� ������ ���������� �������� �����
    }


}
