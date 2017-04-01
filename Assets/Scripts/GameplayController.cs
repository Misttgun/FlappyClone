using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour {

    public static GameplayController instance;

    [SerializeField]
    private Text _scoreText, _endScore, _bestScore, _gameOverText;

    [SerializeField]
    private Button _restartGameButton, _instructionButton;

    [SerializeField]
    private GameObject _pausePanel;

    [SerializeField]
    private GameObject[] _birds;

    [SerializeField]
    private Sprite[] _medals;

    [SerializeField]
    private Image _medalImage;

    private void Awake()
    {
        MakeInstance();
        Time.timeScale = 0f;
    }

    private void MakeInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void PauseGame()
    {
        if(BirdScript.instance != null)
        {
            if(BirdScript.instance.isAlive)
            {
                _pausePanel.SetActive(true);
                _gameOverText.gameObject.SetActive(false);
                _endScore.text = "" + BirdScript.instance.score;
                _bestScore.text = "" + GameController.instance.GetHighScore();
                Time.timeScale = 0f;
                _restartGameButton.onClick.RemoveAllListeners();
                _restartGameButton.onClick.AddListener(() => ResumeGame());

            }
        }
    }

    public void ResumeGame()
    {
        _pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        SceneFader.instance.FadeIn(SceneManager.GetActiveScene().name);
    }

    public void PlayGame()
    {
        _scoreText.gameObject.SetActive(true);
        _birds[GameController.instance.GetSelectedBird()].SetActive(true);
        _instructionButton.gameObject.SetActive(false);
        Time.timeScale = 1f;

    }

    public void GoToMenu()
    {
        SceneFader.instance.FadeIn("mainmenu");
    }

    public void SetScore(int score)
    {
        _scoreText.text = "" + score;
    }

    public void GameOverShowScore(int score)
    {
        _pausePanel.SetActive(true);
        _gameOverText.gameObject.SetActive(true);
        _scoreText.gameObject.SetActive(false);

        _endScore.text = "" + score;

        if(score > GameController.instance.GetHighScore())
        {
            GameController.instance.SetHighScore(score);
        }

        _bestScore.text = "" + GameController.instance.GetHighScore();

        if(score <= 10)
        {
            _medalImage.sprite = _medals[0];
        }
        else if(score > 10 && score <= 20)
        {
            _medalImage.sprite = _medals[1];
            if(GameController.instance.IsGreenBirdUnlocked() == 0)
            {
                GameController.instance.UnlockGreenBird();
            }
        }
        else
        {
            _medalImage.sprite = _medals[2];
            if(GameController.instance.IsGreenBirdUnlocked() == 0)
            {
                GameController.instance.UnlockGreenBird();
            }
            if(GameController.instance.IsRedBirdUnlocked() == 0)
            {
                GameController.instance.UnlockRedBird();
            }

        }

        _restartGameButton.onClick.RemoveAllListeners();
        _restartGameButton.onClick.AddListener(() => RestartGame());
    }
}
