using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{

    [SerializeField] private LevelConfig _levelConfig;
    [SerializeField] private AllLevelsConfig _allLevelsConfig;

    [SerializeField] private StoreConfig _storeConfig;

    [SerializeField] private GameObject _winWindow;
    [SerializeField] private GameObject _loseWindow;

    [SerializeField] private List<Enemy> _enemies;
    [SerializeField] private Gate _gate;


    private bool _isWin = false;
    private bool _isLose = false;

    private void Awake()
    {
        foreach (Enemy enemy in _enemies)
        {
            enemy.ballDetectedEvent += Lose;
        }
        _gate.ballDetectedEvent += Win;
    }

    private void Win()
    {
        if (!_isLose)
        {
            _isWin = true;
            _storeConfig.money += 20;
            _storeConfig.PerformUpdateBalance();
            _levelConfig.status = true;
            if (_levelConfig.level != _allLevelsConfig.levels[_allLevelsConfig.levels.Count - 1].level)
                _allLevelsConfig.levels.Where((e) => e.level == _levelConfig.level + 1).ToArray()[0].status = true;
            SaveLoadConfigsService.Instance?.SaveAll();
            Time.timeScale = 0f;
            _winWindow.SetActive(true);
        }
    }

    private void Lose()
    {
        if (!_isWin)
        {
            _isLose = true;
            _storeConfig.money += 5;
            _storeConfig.PerformUpdateBalance();
            SaveLoadConfigsService.Instance?.SaveAll();
            Time.timeScale = 0f;
            _loseWindow.SetActive(true);
        }
    }

    public void Retry()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync(gameObject.scene.name);
    }

    public void NextLevel()
    {
        Time.timeScale = 1.0f;
        if (_levelConfig != _allLevelsConfig.levels[_allLevelsConfig.levels.Count - 1])
            SceneManager.LoadSceneAsync(_allLevelsConfig.levels.Where((e) => e.level == _levelConfig.level + 1).ToList()[0].sceneName);
    }
}
