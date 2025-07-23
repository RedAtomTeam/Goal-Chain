using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [SerializeField] private float maxScale;
    [SerializeField] private float minScale;
    [SerializeField] private float topPosition;
    [SerializeField] private float bottomPosition;
    [SerializeField] private List<FieldAndImage> _fieldsAndImages;
    [SerializeField] private Rigidbody2D _rigidbody;

    private RectTransform _transform;


    private void OnEnable()
    {
        _transform = GetComponent<RectTransform>();

        foreach (FieldAndImage fieldAndImage in _fieldsAndImages)
        {
            if (fieldAndImage.field.isSelected)
            {
                fieldAndImage.image.gameObject.SetActive(true);
                break;
            }
        }
    }

    private void Update()
    {

        print($"0.1 - {_transform.anchoredPosition.y}");
        print($"0.2 - {bottomPosition}");
        print($"1 - {_transform.anchoredPosition.y - bottomPosition}");
        print($"2 - {topPosition - bottomPosition}");
        print($"3 - {(_transform.anchoredPosition.y - bottomPosition) / (topPosition - bottomPosition)}");
        print($"4 - {maxScale - minScale}");
        print($"5 - {(_transform.anchoredPosition.y - bottomPosition / (topPosition - bottomPosition)) * (maxScale - minScale)}");
        print($"6 - {maxScale - (_transform.anchoredPosition.y - bottomPosition / (topPosition - bottomPosition)) * (maxScale - minScale)}");

        float scale = maxScale - ((_transform.anchoredPosition.y - bottomPosition) / (topPosition - bottomPosition)) * (maxScale - minScale);
        print(scale);
        gameObject.transform.localScale = new Vector3(scale, scale, scale);

    }

    public void Stop()
    {
        _rigidbody.velocity = Vector3.zero;
    }

    public void Kick(Vector3 direction, float force)
    {
        _rigidbody.AddForce(direction * force, ForceMode2D.Force);
    }
}

[Serializable]
class BallAndImage
{
    public StoreStuff field;
    public Image image;
}