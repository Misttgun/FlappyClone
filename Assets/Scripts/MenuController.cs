using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

    public static MenuController instance;

    [SerializeField]
    private GameObject[] _birds;

    private bool isGreenUnlocked, isRedUnlocked;

    private void Awake()
    {
        MakeSingleton();
    }

    private void Start()
    {
        _birds[GameController.instance.GetSelectedBird()].SetActive(true);
        CheckIfBirdUnlocked();
    }

    private void MakeSingleton()
    {
        if(instance ==null)
        {
            instance = this;
        }
    }

    private void CheckIfBirdUnlocked()
    {
        if(GameController.instance.IsRedBirdUnlocked() == 1)
        {
            isRedUnlocked = true;
        }

        if(GameController.instance.IsGreenBirdUnlocked() == 1)
        {
            isGreenUnlocked = true;
        }

    }

    public void ChangeBird()
    {
        if(GameController.instance.GetSelectedBird() == 0)
        {
            if(isGreenUnlocked)
            {
                _birds[0].SetActive(false);
                GameController.instance.SetSelectedBird(1);
                _birds[GameController.instance.GetSelectedBird()].SetActive(true);
            }
        }
        else if (GameController.instance.GetSelectedBird() == 1)
        {
            if(isRedUnlocked)
            {
                _birds[1].SetActive(false);
                GameController.instance.SetSelectedBird(2);
                _birds[GameController.instance.GetSelectedBird()].SetActive(true);
            }
            else
            {
                _birds[1].SetActive(false);
                GameController.instance.SetSelectedBird(0);
                _birds[GameController.instance.GetSelectedBird()].SetActive(true);
            }
        }
        else if(GameController.instance.GetSelectedBird() == 2)
        {
            _birds[2].SetActive(false);
            GameController.instance.SetSelectedBird(0);
            _birds[GameController.instance.GetSelectedBird()].SetActive(true);
        }
    }

    public void PlayGame()
    {
        SceneFader.instance.FadeIn("level_1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
