using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class MouseSensitivity : MonoBehaviour
{
    public Slider sensitivitySlider;
    private Transform playerBody;

    public float defaultSensitivity = 100f;
    public static MouseSensitivity instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        FindPlayerBody();
    }

    void Start()
    {
        LoadSensitivity();
    }

    private void FindPlayerBody()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            playerBody = playerObject.transform;
        }
        else
        {
            Debug.LogError("Nie znaleziono obiektu gracza o tagu 'Player'. Upewnij siê, ¿e obiekt istnieje i ma w³aœciwy tag.");
        }
    }

    public void AdjustSensitivity(float value)
    {
        if (playerBody == null)
        {
            FindPlayerBody();
        }

        float sensitivity = value * defaultSensitivity;

        PlayerLook playerLookScript = playerBody.GetComponent<PlayerLook>();
        if (playerLookScript != null)
        {
            playerLookScript.SetMouseSensitivity(sensitivity);
        }

        SaveSensitivity(value);
    }

    private void SaveSensitivity(float value)
    {
        PlayerPrefs.SetFloat("MouseSensitivity", value);
    }

    private void LoadSensitivity()
    {
        float savedSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 0.5f);
        sensitivitySlider.value = savedSensitivity;
        AdjustSensitivity(savedSensitivity);
    }
}
