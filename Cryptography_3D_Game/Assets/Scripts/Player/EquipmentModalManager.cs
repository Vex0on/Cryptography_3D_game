using UnityEngine;
using UnityEngine.InputSystem;

public class EquipmentModalManager : MonoBehaviour
{
    public static EquipmentModalManager Instance;

    public GameObject equipmentModal;
    private bool isModalVisible = false;

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
    }

    void Start()
    {
        if (equipmentModal == null)
        {
            Debug.LogError("Nie przypisano modala z ekwipunkiem!");
        }
        else
        {
            equipmentModal.SetActive(false);
        }
    }

    public void ToggleEquipmentModal()
    {
        Cursor.lockState = Cursor.lockState == CursorLockMode.None ? CursorLockMode.Locked : CursorLockMode.None;
        isModalVisible = !isModalVisible;
        equipmentModal.SetActive(isModalVisible);
    }
}
