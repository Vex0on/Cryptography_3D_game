using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SubmitButton : MonoBehaviour
{

    public void OnSubmitButtonClick()
    {
        CipherSolver.Instance.CheckSolution();
    }

}
