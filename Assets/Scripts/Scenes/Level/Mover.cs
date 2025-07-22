using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Vector3 firstPos;
    [SerializeField] private Vector3 secondPos;
    [SerializeField] private float speed;

    private Vector3 targetPos;
    private RectTransform _rect;


    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        targetPos = firstPos;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float step = speed * Time.deltaTime;
        //transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, step);
        _rect.anchoredPosition = Vector3.MoveTowards(_rect.anchoredPosition, targetPos, step);

        if (Vector3.Distance(_rect.anchoredPosition, targetPos) < 0.01f)
        {
            if (targetPos == firstPos)
                targetPos = secondPos;
            else
                targetPos = firstPos;
        }
    }

}
