using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CipherSolver : MonoBehaviour
{
    public static CipherSolver Instance;

    // Struktura przechowuj�ca dane o szyfrach
    [System.Serializable]
    public struct CipherData
    {
        public string solution;
    }

    // S�ownik, gdzie kluczem jest numer szyfru, a warto�ci� s� dane o szyfrze
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

        // Wype�nij s�ownik danymi o szyfrach
        FillCipherDictionary();
    }

    // Metoda wype�niaj�ca s�ownik danymi o szyfrach
    private void FillCipherDictionary()
    {
        cipherDictionary.Add(0, new CipherData { solution = "Rozwi�zanie 0" });
        cipherDictionary.Add(1, new CipherData { solution = "Rozwi�zanie 1" });
        cipherDictionary.Add(2, new CipherData { solution = "Rozwi�zanie 2" });
        // Dodaj kolejne szyfry wed�ug potrzeb
    }

    public void CheckSolution()
    {
        int enteredCipherNumber;
        string enteredSolution;

        // Sprawd�, czy wprowadzono poprawne dane
        if (int.TryParse(cipherNumberInput.text, out enteredCipherNumber) && enteredCipherNumber >= 0)
        {
            enteredSolution = solutionInput.text;

            // Sprawd�, czy numer szyfru istnieje w s�owniku
            if (cipherDictionary.ContainsKey(enteredCipherNumber))
            {
                CipherData cipher = cipherDictionary[enteredCipherNumber];

                // Sprawd�, czy rozwi�zanie jest poprawne
                if (cipher.solution.Equals(enteredSolution))
                {
                    Debug.Log("Poprawne rozwi�zanie!");
                    // Tutaj mo�esz doda� kod do obs�ugi poprawnego rozwi�zania
                    return;
                }
                else
                {
                    Debug.Log("Nieprawid�owe rozwi�zanie.");
                    // Tutaj mo�esz doda� kod do obs�ugi nieprawid�owego rozwi�zania
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
