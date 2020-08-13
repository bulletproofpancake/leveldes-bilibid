using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class gameplayScript : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] TextMeshProUGUI _instructionText;
    [SerializeField] float _timeToHide = 3.0f;

    [Header("EXIT")]
    [SerializeField] GameObject _exit;
    [SerializeField] Transform _exitPosition;

    [Header("KEYS")]
    [SerializeField] GameObject _keyObject;
    [SerializeField] Transform[] _keyPosition;

    public static bool _playerCaught = false;
    public static bool _reachedExit = false;
    public static int _hasKey;
    private void Start()
    {
        StartCoroutine(startGame());
    }

    private void Update()
    {
        _scoreText.text = "KEYS: " + _hasKey.ToString() + "/" + _keyPosition.Length.ToString();
        if (_playerCaught)
        {
            FindObjectOfType<audioManagerScript>().playSound("GuardAlarm");
            _hasKey = 0;
            reloadScene();
        }

        else if (_reachedExit)
        {
            if (_hasKey == _keyPosition.Length)
            {
                loadNextScene();
                _hasKey = 0;
            }

            else
            {
                StartCoroutine(incompleteKeys());
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        _playerCaught = false;
    }
    void loadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        if (SceneManager.GetActiveScene().buildIndex == 16)
        {
            SceneManager.LoadScene("Main Menu");
        }
        _reachedExit = false;
    }

    void spawnObjects()
    {
        //Instantiate(_player, _playerPosition.position, Quaternion.identity);
        Instantiate(_exit, _exitPosition.position, Quaternion.identity);
        for (int i = 0; i < _keyPosition.Length; i++)
        {
            Instantiate(_keyObject, _keyPosition[i].position, Quaternion.identity);
        }

    }

    IEnumerator incompleteKeys()
    {
        _instructionText.text = "Incomplete Keys!";
        yield return new WaitForSeconds(_timeToHide);
        _instructionText.text = string.Empty;
        _reachedExit = false;
    }

    IEnumerator startGame()
    {
        spawnObjects();
        yield return new WaitForSeconds(_timeToHide);
        _instructionText.text = string.Empty;

    }

}
