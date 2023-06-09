using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    public static SceneTransitions Instance { get; set; }

    private void Start()
    {
        Instance = this;
    }

    public void LoadMenuScene()
    {
        StartCoroutine(DoLoadMenuScene());
    }

    public void LoadMainScene()
    {
        StartCoroutine(DoLoadMainScene());
    }

    IEnumerator DoLoadMenuScene()
    {
        FadeController.Instance.ShowFade();
        yield return new WaitForSeconds(0.9f);
        SceneManager.LoadScene("Menu");
    }

    IEnumerator DoLoadMainScene()
    {
        FadeController.Instance.ShowFade();
        yield return new WaitForSeconds(0.9f);
        SceneManager.LoadScene("MainScene");
    }
}
