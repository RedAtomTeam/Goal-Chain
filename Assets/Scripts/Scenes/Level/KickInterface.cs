using UnityEngine;
using UnityEngine.UI;

public class KickInterface : MonoBehaviour
{
    [SerializeField] private Slider _sliderPower;
    [SerializeField] private GameObject _arrow;


    public void ChangeValue(float value)
    {
        _sliderPower.value = value; 
    }


    public void HideArrow()
    {
        _arrow.SetActive(false);
    }

    public void ShowArrow()
    {
        _arrow?.SetActive(true);
    }


    public void UpdateArrow(Vector3 startPos, Vector3 endPos)
    {
        // ���������� �������, ���� ��� ���� ���������
        if (!_arrow.activeSelf)
            _arrow.SetActive(true);

        // ��������� ������ �����������
        Vector3 direction = endPos - startPos;
        float distance = direction.magnitude;

        // ������������� ������� ���������� ����� �������
        _arrow.transform.position = (startPos + endPos) / 2f;

        // ������������ ������� � ����������� �����
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _arrow.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        // ������������ ������� �� ����� ���������� (������������, ��� �������� ������ ������� 1 �������)
        // ���� ����������� SpriteRenderer:
        _arrow.transform.localScale = new Vector3(_arrow.transform.localScale.x, distance/200, _arrow.transform.localScale.z);
    }
}
