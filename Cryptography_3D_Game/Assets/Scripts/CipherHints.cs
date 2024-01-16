using UnityEngine;
using TMPro;

public class CipherHints : MonoBehaviour
{
    public TextMeshProUGUI hint1Text;
    public TextMeshProUGUI hint2Text;
    public TextMeshProUGUI hint3Text;

    private void Start()
    {
        DeactivateAllHints();
    }
    public void OnHint1ButtonClick()
    {
        DisplayHint(1);
    }

    public void OnHint2ButtonClick()
    {
        DisplayHint(2);
    }

    public void OnHint3ButtonClick()
    {
        DisplayHint(3);
    }

    private void DisplayHint(int hintNumber)
    {
        DeactivateAllHints();

        int cipherNumber;
        if (int.TryParse(CipherSolver.Instance.cipherNumberInput.text, out cipherNumber) && cipherNumber >= 0)
        {
            if (CipherSolver.Instance.cipherDictionary.ContainsKey(cipherNumber))
            {
                CipherSolver.CipherData cipherData = CipherSolver.Instance.cipherDictionary[cipherNumber];

                switch (hintNumber)
                {
                    case 1:
                        hint1Text.text = cipherData.hint1;
                        hint1Text.gameObject.SetActive(true);
                        break;
                    case 2:
                        hint2Text.text = cipherData.hint2;
                        hint2Text.gameObject.SetActive(true);
                        break;
                    case 3:
                        hint3Text.text = cipherData.hint3;
                        hint3Text.gameObject.SetActive(true);
                        break;
                }
            }
            else
            {
                Debug.LogError("Nie znaleziono szyfru o numerze " + cipherNumber);
            }
        }
        else
        {
            Debug.LogError("Nieprawid³owy numer szyfru.");
        }
    }

    private void DeactivateAllHints()
    {
        hint1Text.gameObject.SetActive(false);
        hint2Text.gameObject.SetActive(false);
        hint3Text.gameObject.SetActive(false);
    }
}
