using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class MenuScript : MonoBehaviour
{
    public GameObject mainMenuTexts;
    public GameObject optionsTexts;
    public GameObject levelsTexts;
    public float fadeDuration = 1f;

    public void OnPlayTutorialClick()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void OnPlaySteganoClick()
    {
        SceneManager.LoadScene("Stegano");
    }

    public void OnPlayCryptoClick()
    {
        SceneManager.LoadScene("Crypto");
    }

    public void OnOptionsClick()
    {
        StartCoroutine(FadeOut(mainMenuTexts, fadeDuration));
        StartCoroutine(FadeIn(optionsTexts, fadeDuration));
    }

    public void OnLevelsClick()
    {
        StartCoroutine(FadeOut(mainMenuTexts, fadeDuration));
        StartCoroutine(FadeIn(levelsTexts, fadeDuration));
    }

    public void OnExitClick()
    {
        Application.Quit();
    }

    public void OnOptionsBackClick()
    {
        StartCoroutine(FadeOut(optionsTexts, fadeDuration));
        StartCoroutine(FadeIn(mainMenuTexts, fadeDuration));
    }

    public void OnOptionsBackClick2()
    {
        StartCoroutine(FadeOut(levelsTexts, fadeDuration));
        StartCoroutine(FadeIn(mainMenuTexts, fadeDuration));
    }

    IEnumerator FadeOut(GameObject obj, float duration)
    {
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
        float startAlpha = canvasGroup.alpha;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, time / duration);
            yield return null;
        }

        obj.SetActive(false);
    }

    IEnumerator FadeIn(GameObject obj, float duration)
    {
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
        float startAlpha = canvasGroup.alpha;
        float time = 0f;

        obj.SetActive(true);

        while (time < duration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, time / duration);
            yield return null;
        }
    }
}
