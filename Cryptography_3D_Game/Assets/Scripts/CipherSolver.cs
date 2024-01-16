using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class CipherSolver : MonoBehaviour
{
    public static CipherSolver Instance;
    public TMP_Text correctAnswer;
    public TMP_Text incorrectAnswer;
    public TMP_Text progressText;
    private int currentProgress = 0;
    private int totalCiphers = 11;

    private List<int> cipherAlreadySolved = new List<int>();

    [System.Serializable]
    public struct CipherData
    {
        public string solution;
        public string hint1;
        public string hint2;
        public string hint3;
    }

    public delegate void SolutionChecked(bool isCorrect);
    public static event SolutionChecked OnSolutionChecked;

    public Dictionary<int, CipherData> cipherDictionary = new Dictionary<int, CipherData>();

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
        cipherDictionary.Add(0, new CipherData { solution = "Wiadomo��", hint1 = "Podpowied� 1", hint2 = "Podpowied� 2", hint3 = "Podpowied� 3" });
        cipherDictionary.Add(1, new CipherData { solution = "�rodkowy", hint1 = "Produkty", hint2 = "Kolumny", hint3 = "Pierwsze litery" });
        cipherDictionary.Add(2, new CipherData { solution = "Szyfrowanie", hint1 = "Litery", hint2 = "R�nice tekstu", hint3 = "Wysoko�� liter" });
        cipherDictionary.Add(3, new CipherData { solution = "Przycisk", hint1 = "Pocz�tki", hint2 = "Kolumny", hint3 = "Odwrotna kolejno��" });
        cipherDictionary.Add(4, new CipherData { solution = "G�ra", hint1 = "Atrament sympatyczny", hint2 = "Blisko��", hint3 = "Podejd� i przyjrzyj si�!" });
        cipherDictionary.Add(5, new CipherData { solution = "Pierwszy", hint1 = "Skupienie", hint2 = "Ma�y druczek", hint3 = "Ko�o kropki" });
        cipherDictionary.Add(6, new CipherData { solution = "D�", hint1 = "Zeskanuj kod", hint2 = "Jak to otworzy�?", hint3 = "Hmm, co� bym zanotowa�" });
        cipherDictionary.Add(7, new CipherData { solution = "Genialne", hint1 = "Klucz", hint2 = "Alfabet", hint3 = "02:B, 08:H" });
        cipherDictionary.Add(8, new CipherData { solution = "Ostatni", hint1 = "Przyjrzyj si�", hint2 = "Polska j�zyk, trudna j�zyk", hint3 = "B��dy w s�owach?" });
        cipherDictionary.Add(9, new CipherData { solution = "D�wignia", hint1 = "Wejd� w link", hint2 = "Sp�jrz na tytu� posta", hint3 = "Decoder :)" });
        cipherDictionary.Add(10, new CipherData { solution = "Jest", hint1 = "Co� tu nie gra", hint2 = "Przyjrzyj si� dok�adnie s�owom", hint3 = "Fonty?" });
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

                if (!cipherAlreadySolved.Contains(enteredCipherNumber))
                {
                    if (cipher.solution.Equals(enteredSolution))
                    {
                        correctAnswer.gameObject.SetActive(true);
                        OnSolutionChecked?.Invoke(true);
                        cipherAlreadySolved.Add(enteredCipherNumber);
                        UpdateProgress();
                        StartCoroutine(ShowFeedbackAndHide());
                    }
                    else
                    {
                        incorrectAnswer.gameObject.SetActive(true);
                        OnSolutionChecked?.Invoke(false);
                        StartCoroutine(ShowFeedbackAndHide());
                    }
                }
                else
                {
                    Debug.Log("Ten szyfr zosta� ju� rozwi�zany.");
                }

                return;
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

    private IEnumerator ShowFeedbackAndHide()
    {
        yield return new WaitForSeconds(3f);

        correctAnswer.gameObject.SetActive(false);
        incorrectAnswer.gameObject.SetActive(false);
    }

    private void UpdateProgress()
    {
        currentProgress++;

        progressText.text = $"{currentProgress}/{totalCiphers}";
    }
}
