using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class CipherSolver : MonoBehaviour
{
    public static CipherSolver Instance;
    public TMP_Text correctAnswer;
    public TMP_Text incorrectAnswer;
    public TMP_Text progressText;
    private int currentProgress = 0;
    private int totalCiphers = 21;

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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        FillCipherDictionary();
        LoadProgress();
    }

    private void FillCipherDictionary()
    {
        cipherDictionary.Add(0, new CipherData { solution = "Wiadomoœæ", hint1 = "PodpowiedŸ 1", hint2 = "PodpowiedŸ 2", hint3 = "PodpowiedŸ 3" });
        cipherDictionary.Add(1, new CipherData { solution = "Œrodkowy", hint1 = "Produkty", hint2 = "Kolumny", hint3 = "Pierwsze litery" });
        cipherDictionary.Add(2, new CipherData { solution = "Szyfrowanie", hint1 = "Litery", hint2 = "Ró¿nice tekstu", hint3 = "Wysokoœæ liter" });
        cipherDictionary.Add(3, new CipherData { solution = "Przycisk", hint1 = "Pocz¹tki", hint2 = "Kolumny", hint3 = "Odwrotna kolejnoœæ" });
        cipherDictionary.Add(4, new CipherData { solution = "Góra", hint1 = "Atrament sympatyczny", hint2 = "Bliskoœæ", hint3 = "PodejdŸ i przyjrzyj siê!" });
        cipherDictionary.Add(5, new CipherData { solution = "Pierwszy", hint1 = "Skupienie", hint2 = "Ma³y druczek", hint3 = "Ko³o kropki" });
        cipherDictionary.Add(6, new CipherData { solution = "Dó³", hint1 = "Zeskanuj kod", hint2 = "Jak to otworzyæ?", hint3 = "Hmm, coœ bym zanotowa³" });
        cipherDictionary.Add(7, new CipherData { solution = "Genialne", hint1 = "Klucz", hint2 = "Alfabet", hint3 = "02:B, 08:H" });
        cipherDictionary.Add(8, new CipherData { solution = "Ostatni", hint1 = "Przyjrzyj siê", hint2 = "Polska jêzyk, trudna jêzyk", hint3 = "B³êdy w s³owach?" });
        cipherDictionary.Add(9, new CipherData { solution = "DŸwignia", hint1 = "WejdŸ w link", hint2 = "Spójrz na tytu³ posta", hint3 = "Decoder :)" });
        cipherDictionary.Add(10, new CipherData { solution = "Jest", hint1 = "Coœ tu nie gra", hint2 = "Przyjrzyj siê dok³adnie s³owom", hint3 = "Fonty?" });
        cipherDictionary.Add(11, new CipherData { solution = "Morsowanie", hint1 = "Zazwyczaj dŸwiêkowy", hint2 = "Trzy sygna³y", hint3 = "Mors, taki zwierz" });
        cipherDictionary.Add(12, new CipherData { solution = "Matematyka", hint1 = "Klucz powy¿ej", hint2 = "Kolejnoœæ", hint3 = "2/1 = B, 4/3 = N" });
        cipherDictionary.Add(13, new CipherData { solution = "My pierwsza brygada", hint1 = "Nieco siê popl¹ta³o", hint2 = "Deseczki", hint3 = "Lustrzane odbicie" });
        cipherDictionary.Add(14, new CipherData { solution = "Historyczny szyfr", hint1 = "Przesuniêcie", hint2 = "Litera do literki", hint3 = "Ave Cezar!" });
        cipherDictionary.Add(15, new CipherData { solution = "Tablica", hint1 = "Dwuliterowe wspó³rzêdne", hint2 = "Kontrola wierszy i kolumn", hint3 = "Uk³ad szachownicy" });
        cipherDictionary.Add(16, new CipherData { solution = "Spiralowe szyfrowanie", hint1 = "Orientacja", hint2 = "Od œrodka", hint3 = "Spirala" });
        cipherDictionary.Add(17, new CipherData { solution = "Bardzo trudny szyfr", hint1 = "Klucz", hint2 = "Powtarzaj¹ce siê fragmenty", hint3 = "Przesuniêcie w alfabecie Vig.." });
        cipherDictionary.Add(18, new CipherData { solution = "Jajecznica", hint1 = "D³ugoœci kawa³ków", hint2 = "Wzorce", hint3 = "Masz ochotê na bacon?" });
        cipherDictionary.Add(19, new CipherData { solution = "Kie³basa", hint1 = "Pomieszane szyki", hint2 = "Pierwsza litera bez zmian", hint3 = "Ostatnia litera te¿" });
        cipherDictionary.Add(20, new CipherData { solution = "Ukryte informacje niczym skarb piratów", hint1 = "Po jednym", hint2 = "U³ó¿ jedno na drugim", hint3 = "góra dó³, góra dó³" });
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
                        currentProgress++;
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
                    Debug.Log("Ten szyfr zosta³ ju¿ rozwi¹zany.");
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
            Debug.Log("Nieprawid³owy numer szyfru.");
        }
    }

    private IEnumerator ShowFeedbackAndHide()
    {
        yield return new WaitForSeconds(3f);

        if (correctAnswer != null)
            correctAnswer.gameObject.SetActive(false);

        if (incorrectAnswer != null)
            incorrectAnswer.gameObject.SetActive(false);
    }

    public void SaveProgressOnClick()
    {
        SaveProgress();
    }

    public void LoadProgressOnClick()
    {
        LoadProgress();
    }

    public void ResetProgressOnClick()
    {
        ResetProgress();
    }

    private void UpdateProgress()
    {
        progressText.text = $"{currentProgress}/{totalCiphers}";
    }

    private void LoadProgress()
    {
        currentProgress = PlayerPrefs.GetInt("CipherProgress", 0);
        UpdateProgress();
        string solvedCiphers = PlayerPrefs.GetString("CipherSolved", "");
        string[] cipherArray = solvedCiphers.Split(',');

        foreach (string cipherStr in cipherArray)
        {
            if (int.TryParse(cipherStr, out int cipherNumber))
            {
                cipherAlreadySolved.Add(cipherNumber);
            }
        }

        Debug.Log("Loaded Progress: " + currentProgress);
    }

    private void SaveProgress()
    {
        PlayerPrefs.SetInt("CipherProgress", currentProgress);
        string solvedCiphers = string.Join(",", cipherAlreadySolved);
        PlayerPrefs.SetString("CipherSolved", solvedCiphers);
        PlayerPrefs.Save();
        Debug.Log("Saved Progress: " + currentProgress);
    }

    private void ResetProgress()
    {
        PlayerPrefs.DeleteKey("CipherProgress");
        PlayerPrefs.DeleteKey("CipherSolved");
        PlayerPrefs.Save();

        currentProgress = 0;
        UpdateProgress();
        cipherAlreadySolved.Clear();

        Debug.Log("Progress reset.");
    }
}
