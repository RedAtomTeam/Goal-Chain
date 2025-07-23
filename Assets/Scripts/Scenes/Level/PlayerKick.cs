using UnityEngine;

public class PlayerKick : MonoBehaviour
{
    [SerializeField] private Transform _ballPos;
    [SerializeField] private KickInterface KickInterface;

    [SerializeField] private Player _player;

    [SerializeField] private Ball _ball; // ������ �� ������ ����
    [SerializeField] private float _maxForce = 20f; // ������������ ���� �����
    [SerializeField] private float _maxDragDistance = 200f; // ������������ ���������� ����������� (� ��������)

    [SerializeField] private float _relaxTime;

    private Vector2 _touchStartPos;
    private bool _isDragging = false;

    private float _passTime;


    private void OnEnable()
    {
        _passTime = 0;
        _ball.transform.position = _ballPos.position;
        _ball.Stop();
        KickInterface.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        
    }

    private void Update()
    {
        if (_passTime == 0)
        {
            HandleTouchInput();
        }
        else
        {
            _passTime += Time.deltaTime;
            if (_passTime >= _relaxTime)
            {
                _player.gameObject.SetActive(true);
                gameObject.SetActive(false);
            }
        }
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
        _ball.Kick(worldDirection, force);


        _isDragging = false;

        _passTime += Time.deltaTime;
        KickInterface.ChangeValue(0);
        KickInterface.gameObject.SetActive(false);



        // ����� ����� ������ ���������� �������� �����
    }


}
