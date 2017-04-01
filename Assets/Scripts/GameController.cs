using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance;

    private const string HIGH_SCORE = "High Score";
    private const string SELECTED_BIRD = "Selected Bird";
    private const string GREEN_BIRD = "Green Bird";
    private const string RED_BIRD = "Red Bird";

    private void Awake()
    {
        MakeSingleton();
        IsTheGameStartedFirstTime();
       // PlayerPrefs.DeleteAll();
    }
	
	private void MakeSingleton()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void IsTheGameStartedFirstTime()
    {
        if(!PlayerPrefs.HasKey("IsTheGameStartedFirstTime"))
        {
            PlayerPrefs.SetInt(HIGH_SCORE, 0);
            PlayerPrefs.SetInt(SELECTED_BIRD, 0);
            PlayerPrefs.SetInt(GREEN_BIRD, 0);
            PlayerPrefs.SetInt(RED_BIRD, 0);
            PlayerPrefs.SetInt("IsTheGameStartedFirstTime", 0);
        }
    }

    #region Getter and Setters
    public void SetHighScore(int score)
    {
        PlayerPrefs.SetInt(HIGH_SCORE, score);
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(HIGH_SCORE);
    }

    public void SetSelectedBird(int bird)
    {
        PlayerPrefs.SetInt(SELECTED_BIRD, bird);
    }

    public int GetSelectedBird()
    {
        return PlayerPrefs.GetInt(SELECTED_BIRD);
    }

    public void UnlockGreenBird()
    {
        PlayerPrefs.SetInt(GREEN_BIRD, 1);
    }

    public int IsGreenBirdUnlocked()
    {
        return PlayerPrefs.GetInt(GREEN_BIRD);
    }

    public void UnlockRedBird()
    {
        PlayerPrefs.SetInt(RED_BIRD, 1);
    }

    public int IsRedBirdUnlocked()
    {
        return PlayerPrefs.GetInt(RED_BIRD);
    }

    #endregion
}
