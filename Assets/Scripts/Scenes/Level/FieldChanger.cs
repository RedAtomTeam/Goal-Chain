using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldChanger : MonoBehaviour
{
    [SerializeField] private List<FieldAndImage> _fieldsAndImages;

    private void OnEnable()
    {
        foreach (FieldAndImage fieldAndImage in _fieldsAndImages)
        {
            if (fieldAndImage.field.isSelected) 
            {
                fieldAndImage.image.gameObject.SetActive(true);
                break;
            }
        }
    }
}

[Serializable]
class FieldAndImage
{
    public StoreStuff field;
    public Image image;
}