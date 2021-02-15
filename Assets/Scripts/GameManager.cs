using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;
    [SerializeField]
    private bool _isCoopMode = false;

    [SerializeField]
    private GameObject _pauseMenuPanel;
    private Animator _pauseAnimator;

    public bool isCoopMode => _isCoopMode;

    private void Start()
    {
        _pauseAnimator = _pauseMenuPanel.GetComponent<Animator>();
        _pauseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver)
        {
            SceneManager.LoadScene(1);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }

    public void PauseGame()
    {
        _pauseMenuPanel.SetActive(true);
        _pauseAnimator.SetBool("isPaused", true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        _pauseMenuPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
