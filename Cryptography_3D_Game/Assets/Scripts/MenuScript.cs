using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class MenuScript : MonoBehaviour
{
    public GameObject mainMenuTexts;
    public GameObject optionsTexts;
    public GameObject levelsTexts;
    public AudioClip clickSound;
    private AudioSource audioSource;
    public float fadeDuration = 1f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnPlayTutorialClick()
    {
        PlayClickSound();
        SceneManager.LoadScene("Tutorial");
    }

    public void OnPlaySteganoClick()
    {
        PlayClickSound();
        SceneManager.LoadScene("Stegano");
    }

    public void OnPlayCryptoClick()
    {
        PlayClickSound();
        SceneManager.LoadScene("Crypto");
    }

    public void OnOptionsClick()
    {
        PlayClickSound();
        StartCoroutine(FadeOut(mainMenuTexts, fadeDuration));
        StartCoroutine(FadeIn(optionsTexts, fadeDuration));
    }

    public void OnLevelsClick()
    {
        PlayClickSound();
        StartCoroutine(FadeOut(mainMenuTexts, fadeDuration));
        StartCoroutine(FadeIn(levelsTexts, fadeDuration));
    }

    public void OnExitClick()
    {
        PlayClickSound();
        Application.Quit();
    }

    public void OnOptionsBackClick()
    {
        PlayClickSound();
        StartCoroutine(FadeOut(optionsTexts, fadeDuration));
        StartCoroutine(FadeIn(mainMenuTexts, fadeDuration));
    }

    public void OnOptionsBackClick2()
    {
        PlayClickSound();
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

    private void PlayClickSound()
    {
        if (audioSource && clickSound)
        {
            audioSource.PlayOneShot(clickSound);
        }
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
