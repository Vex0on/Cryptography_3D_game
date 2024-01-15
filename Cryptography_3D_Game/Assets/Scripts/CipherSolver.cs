using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CipherSolver : MonoBehaviour
{
    public static CipherSolver Instance;

    [System.Serializable]
    public struct CipherData
    {
        public string solution;
    }
    public delegate void SolutionChecked(bool isCorrect);
    public static event SolutionChecked OnSolutionChecked;

    private Dictionary<int, CipherData> cipherDictionary = new Dictionary<int, CipherData>();

    public TMP_InputField cipherNumberInput;
    public TMP_InputField solutionInput;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        FillCipherDictionary();
    }

    private void FillCipherDictionary()
    {
        cipherDictionary.Add(0, new CipherData { solution = "Wiadomo��" });
        cipherDictionary.Add(1, new CipherData { solution = "�rodkowy" });
        cipherDictionary.Add(2, new CipherData { solution = "Szyfrowanie" });
        cipherDictionary.Add(3, new CipherData { solution = "Przycisk" });
        cipherDictionary.Add(4, new CipherData { solution = "G�ra" });
        cipherDictionary.Add(5, new CipherData { solution = "Pierwszy" });
        cipherDictionary.Add(6, new CipherData { solution = "D�" });
        cipherDictionary.Add(7, new CipherData { solution = "Genialne" });
        cipherDictionary.Add(8, new CipherData { solution = "Ostatni" });
        cipherDictionary.Add(9, new CipherData { solution = "D�wignia" });
        cipherDictionary.Add(10, new CipherData { solution = "Jest" });
    }

    public void CheckSolution()
    {
        int enteredCipherNumber;
        string enteredSolution;

        if (int.TryParse(cipherNumberInput.text, out enteredCipherNumber) && enteredCipherNumber >= 0)
        {
            enteredSolution = solutionInput.text;

            if (cipherDictionary.ContainsKey(enteredCipherNumber))
            {
                CipherData cipher = cipherDictionary[enteredCipherNumber];

                if (cipher.solution.Equals(enteredSolution))
                {
                    Debug.Log("Poprawne rozwi�zanie!");
                    OnSolutionChecked?.Invoke(true);
                    return;
                }
                else
                {
                    Debug.Log("Nieprawid�owe rozwi�zanie.");
                    OnSolutionChecked?.Invoke(false);
                    return;
                }
            }
            else
            {
                Debug.Log("Nie znaleziono szyfru o numerze " + enteredCipherNumber);
            }
        }
        else
        {
            Debug.Log("Nieprawid�owy numer szyfru.");
        }
    }
}
