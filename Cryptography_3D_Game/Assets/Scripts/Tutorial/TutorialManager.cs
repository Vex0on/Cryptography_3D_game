using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static CipherSolver;

public class TutorialManager : MonoBehaviour
{
    [Header("Actions")]
    public GameObject movementAction;
    public GameObject lookAction;
    public GameObject comeAction;
    public GameObject useAction;
    public GameObject tabletAction;
    public GameObject solutionAction;
    public GameObject congratsText;
    public GameObject playerPos;
    public GameObject tablet;

    [Header("Objects")]
    public GameObject diamondIndicator;
    public GameObject gate;
    public GameObject leverObj;
    public Transform lever;

    [Header("UI")]
    public Button closeButton;

    private bool closeButtonPressed = false;

    private void Start()
    {
        StartCoroutine(ShowMovementTutorial());

        CipherSolver.OnSolutionChecked += OnSolutionChecked;
    }

    IEnumerator ShowMovementTutorial()
    {
        movementAction.SetActive(true);

        yield return new WaitUntil(() => HasPlayerMoved());
        yield return new WaitForSeconds(2f);

        movementAction.SetActive(false);

        StartCoroutine(ShowLookTutorial());
    }

    IEnumerator ShowLookTutorial()
    {
        lookAction.SetActive(true);

        yield return new WaitUntil(() => HasPlayerLooked());
        yield return new WaitForSeconds(2f);

        lookAction.SetActive(false);

        StartCoroutine(ShowComeTutorial());
    }

    IEnumerator ShowComeTutorial()
    {
        comeAction.SetActive(true);
        diamondIndicator.SetActive(true);

        yield return new WaitUntil(() => HasPlayerReachedLever());
        yield return new WaitForSeconds(1f);
        diamondIndicator.SetActive(false);
        comeAction.SetActive(false);

        StartCoroutine(ShowUseTutorial());
    }

    IEnumerator ShowUseTutorial()
    {
        useAction.SetActive(true);
        yield return new WaitUntil(() => HasPlayerUsedLever());
        Debug.Log("Uøyto düwigni!");
        leverObj.GetComponent<LeverManager>().PullLever();
        yield return new WaitForSeconds(2f);
        gate.GetComponent<GateController>().OpenGate();
        useAction.SetActive(false);

        StartCoroutine(ShowTabletTutorial());
    }

    IEnumerator ShowTabletTutorial()
    {
        Vector3 tabletTutorialPoint = new Vector3(0, 0, 7);
        yield return new WaitUntil(() => HasPlayerReachedPoint(tabletTutorialPoint));
        tabletAction.SetActive(true);
        yield return new WaitUntil(() => HasPlayerOpenedTablet());
        tabletAction.SetActive(false);

        StartCoroutine(ShowSolutionTutorial());
    }

    IEnumerator ShowSolutionTutorial()
    {
        solutionAction.SetActive(true);
        yield return new WaitUntil(() => closeButtonPressed);
        solutionAction.SetActive(false);
    }

    public void OnCloseButtonClick()
    {
        closeButtonPressed = true;
    }

    private void OnSolutionChecked(bool isCorrect)
    {
        if (isCorrect)
        {
            tablet.SetActive(false);
            congratsText.SetActive(true);
            Invoke("LoadSteganoScene", 5f);
        }
    }

    private void LoadSteganoScene()
    {
        SceneManager.LoadScene("Stegano");
    }

    private bool HasPlayerOpenedTablet()
    {
        return Keyboard.current.iKey.wasPressedThisFrame;
    }

    private bool HasPlayerReachedPoint(Vector3 point)
    {
        return Vector3.Distance(playerPos.transform.position, point) < 2f;
    }

    private bool HasPlayerMoved()
    {
        return Keyboard.current.wKey.isPressed || Keyboard.current.aKey.isPressed || Keyboard.current.sKey.isPressed || Keyboard.current.dKey.isPressed;
    }

    private bool HasPlayerLooked()
    {
        return Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0;
    }

    private bool HasPlayerReachedLever()
    {
        return Vector3.Distance(playerPos.transform.position, lever.position) < 2f;
    }

    private bool HasPlayerUsedLever()
    {
        if (Vector3.Distance(playerPos.transform.position, lever.position) <= 2f && Keyboard.current.eKey.isPressed)
        {
            Vector3 playerToLever = (lever.position - playerPos.transform.position).normalized;
            float dotProduct = Vector3.Dot(playerToLever, playerPos.transform.forward);
            float angleThreshold = 0.2f;

            return dotProduct >= Mathf.Cos(angleThreshold);
        }

        return false;
    }
}

