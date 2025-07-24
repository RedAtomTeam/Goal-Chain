using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [SerializeField] private float _maxScale;
    [SerializeField] private float _minScale;
    [SerializeField] private float _topPosition;
    [SerializeField] private float _bottomPosition;
    [SerializeField] private List<FieldAndImage> _fieldsAndImages;
    [SerializeField] private Rigidbody2D _rigidbody;

    [SerializeField] private GameObject _topParent;
    [SerializeField] private GameObject _bottomParent;

    private RectTransform _transform;
    private bool _isUnderPlayer = false;


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
        if (!_isUnderPlayer)
            UpdateScale();

    }


    private void UpdateScale()
    {

        float scale = _maxScale - ((_transform.anchoredPosition.y - _bottomPosition) / (_topPosition - _bottomPosition)) * (_maxScale - _minScale);
        gameObject.transform.localScale = new Vector3(scale, scale, scale);
    }

    public void Stop()
    {
        _rigidbody.velocity = Vector3.zero;
        SetBottomParent();
        _isUnderPlayer = true;
    }

    public void Kick(Vector3 direction, float force)
    {
        SetTopParent();
        _rigidbody.AddForce(direction * force, ForceMode2D.Force);
        _isUnderPlayer = false;
    }

    private void SetTopParent()
    {
        gameObject.transform.SetParent(_topParent.transform);
    }

    public void SetBottomParent()
    {
        gameObject.transform.SetParent(_bottomParent.transform);
    }
}

[Serializable]
class BallAndImage
{
    public StoreStuff field;
    public Image image;
}