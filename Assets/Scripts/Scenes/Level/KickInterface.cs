using UnityEngine;
using UnityEngine.UI;

public class KickInterface : MonoBehaviour
{
    [SerializeField] private Slider _sliderPower;


    public void ChangeValue(float value)
    {
        _sliderPower.value = value; 
    }
}
