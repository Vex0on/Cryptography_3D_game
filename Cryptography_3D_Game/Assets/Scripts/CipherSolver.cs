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
        cipherDictionary.Add(0, new CipherData { solution = "Rozwiązanie 0" });
        cipherDictionary.Add(1, new CipherData { solution = "Rozwiązanie 1" });
        cipherDictionary.Add(2, new CipherData { solution = "Rozwiązanie 2" });
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
                    Debug.Log("Poprawne rozwiązanie!");
                    return;
                }
                else
                {
                    Debug.Log("Nieprawidłowe rozwiązanie.");
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
            Debug.Log("Nieprawidłowy numer szyfru.");
        }
    }
}
