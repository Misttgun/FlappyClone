using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour {

    public static SceneFader instance;

    [SerializeField]
    private GameObject _fadeCanvas;
    [SerializeField]
    private Animator _animator;

    private void Awake()
    {
        MakeSingleton();
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

    public void FadeIn(string levelName)
    {
        StartCoroutine(FadeInAnimation(levelName));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutAnimation());
    }

    IEnumerator FadeInAnimation(string levelName)
    {
        _fadeCanvas.SetActive(true);
        _animator.Play("fadein");
        yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(0.7f));
        SceneManager.LoadScene(levelName);
        FadeOut();
    }

    IEnumerator FadeOutAnimation()
    {
        _animator.Play("fadeout");
        yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(1f));
        _fadeCanvas.SetActive(false);
    }
}
