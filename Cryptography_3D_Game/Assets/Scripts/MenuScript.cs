using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class MenuScript : MonoBehaviour
{
    public GameObject mainMenuTexts;
    public GameObject optionsTexts;
    public float fadeDuration = 1f;

    public void OnPlayClick()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnOptionsClick()
    {
        StartCoroutine(FadeOut(mainMenuTexts, fadeDuration));
        StartCoroutine(FadeIn(optionsTexts, fadeDuration));
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
