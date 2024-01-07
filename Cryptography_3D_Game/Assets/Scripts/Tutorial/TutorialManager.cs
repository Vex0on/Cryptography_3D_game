using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialManager : MonoBehaviour
{
    [Header("Actions")]
    public GameObject movementAction;
    public GameObject lookAction;
    public GameObject comeAction;
    public GameObject useAction;
    public GameObject playerPos;


    [Header("Objects")]
    public GameObject diamondIndicator;
    public GameObject gate;
    public GameObject leverObj;
    public Transform lever;

    private void Start()
    {
        StartCoroutine(ShowMovementTutorial());
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

