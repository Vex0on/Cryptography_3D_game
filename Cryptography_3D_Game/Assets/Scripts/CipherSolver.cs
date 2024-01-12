using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CipherSolver : MonoBehaviour
{
    public static CipherSolver Instance;

    // Struktura przechowuj¹ca dane o szyfrach
    [System.Serializable]
    public struct CipherData
    {
        public string solution;
    }

    // S³ownik, gdzie kluczem jest numer szyfru, a wartoœci¹ s¹ dane o szyfrze
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

        // Wype³nij s³ownik danymi o szyfrach
        FillCipherDictionary();
    }

    // Metoda wype³niaj¹ca s³ownik danymi o szyfrach
    private void FillCipherDictionary()
    {
        cipherDictionary.Add(0, new CipherData { solution = "Rozwi¹zanie 0" });
        cipherDictionary.Add(1, new CipherData { solution = "Rozwi¹zanie 1" });
        cipherDictionary.Add(2, new CipherData { solution = "Rozwi¹zanie 2" });
        // Dodaj kolejne szyfry wed³ug potrzeb
    }

    public void CheckSolution()
    {
        int enteredCipherNumber;
        string enteredSolution;

        // SprawdŸ, czy wprowadzono poprawne dane
        if (int.TryParse(cipherNumberInput.text, out enteredCipherNumber) && enteredCipherNumber >= 0)
        {
            enteredSolution = solutionInput.text;

            // SprawdŸ, czy numer szyfru istnieje w s³owniku
            if (cipherDictionary.ContainsKey(enteredCipherNumber))
            {
                CipherData cipher = cipherDictionary[enteredCipherNumber];

                // SprawdŸ, czy rozwi¹zanie jest poprawne
                if (cipher.solution.Equals(enteredSolution))
                {
                    Debug.Log("Poprawne rozwi¹zanie!");
                    // Tutaj mo¿esz dodaæ kod do obs³ugi poprawnego rozwi¹zania
                    return;
                }
                else
                {
                    Debug.Log("Nieprawid³owe rozwi¹zanie.");
                    // Tutaj mo¿esz dodaæ kod do obs³ugi nieprawid³owego rozwi¹zania
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
            Debug.Log("Nieprawid³owy numer szyfru.");
        }
    }
}
