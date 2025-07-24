using UnityEngine;
using UnityEngine.UI;

public class KickInterface : MonoBehaviour
{
    [SerializeField] private Slider _sliderPower;
    [SerializeField] private GameObject Arrow;


    public void ChangeValue(float value)
    {
        _sliderPower.value = value; 
    }

    public void UpdateArrow(Vector3 startPos, Vector3 endPos)
    {

    }
}
