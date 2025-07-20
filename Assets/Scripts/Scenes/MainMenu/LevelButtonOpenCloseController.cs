using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelButtonOpenCloseController : MonoBehaviour
{
    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private Image _buttonImage;
    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private GameObject _lockImage;
    [SerializeField] private Color _openColor;
    [SerializeField] private Color _lockColor;

    [SerializeField] private Color _openTextColor;
    [SerializeField] private Color _lockTextColor;

    private Button _button;


    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OpenScene);
    }

    private void OnEnable()
    {
        _button.interactable = _levelConfig.status;
        if (_lockImage != null)
            _lockImage.SetActive(!_levelConfig.status);

        if (_levelConfig.status)
        {
            _buttonImage.color = _openColor;
            _buttonText.color = _openTextColor;
        }
        else
        {
            _buttonImage.color = _lockColor;
            _buttonText.color = _lockTextColor;
        }
    }

    private void OpenScene()
    {
        SceneManager.LoadSceneAsync(_levelConfig.sceneName);
    }


}
