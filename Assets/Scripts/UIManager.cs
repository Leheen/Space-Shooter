using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText, _bestScoreText;
    [SerializeField]
    private Image _livesImg; 
    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    private int _bestScore;

    private GameManager _gameManager;

    void Start()
    {
        _bestScore = PlayerPrefs.GetInt("HighScore", 0);

        _scoreText.text = "Score: 0";
        _bestScoreText.text = "Best: " + _bestScore;
        _gameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (_gameManager == null)
        {
            Debug.LogError("Game Manager is null");
        }
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void CheckBestScore(int playerScore)
    {
        if (playerScore > _bestScore)
        {
            _bestScore = playerScore;
            PlayerPrefs.SetInt("HighScore", _bestScore);
            _bestScoreText.text = "Best: " + _bestScore.ToString();
        }
    }

    public void UpdateLives(int currentLives)
    {
        int index = currentLives >= 0 ? currentLives : 0;
        _livesImg.sprite = _livesSprites[index];

        if (currentLives == 0)
        {
            StartGameOverSequence();
        }
    }

    void StartGameOverSequence()
    {
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        _gameManager.ResumeGame();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
