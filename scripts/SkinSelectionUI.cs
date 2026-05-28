using UnityEngine;
using UnityEngine.UI;

public class SkinSelectionUI : MonoBehaviour
{
    [Header("UI Layout")]
    [SerializeField] private Transform buttonContainer;
    [SerializeField] private Button buttonPrefab;

    private void Start()
    {
        PopulateAlbumUI();
    }

    private void PopulateAlbumUI()
    {
        if (SkinManager.Instance == null) return;

        PlayerSkinAlbum album = SkinManager.Instance.GetAlbum();
        
        // Clear old buttons if any
        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
        }

        // Generate a button for every skin in the album
        for (int i = 0; i < album.skins.Length; i++)
        {
            int index = i; // Local copy for closure
            PlayerSkin skin = album.skins[i];

            Button newButton = Instantiate(buttonPrefab, buttonContainer);
            newButton.gameObject.SetActive(true);
            
            // Set the button image to the skin icon
            if (skin.uiIcon != null)
            {
                newButton.image.sprite = skin.uiIcon;
            }

            // Assign the click functionality
            newButton.onClick.AddListener(() => OnSkinButtonClicked(index));
        }
    }

    private void OnSkinButtonClicked(int index)
    {
        SkinManager.Instance.EquipeSkin(index);
        Debug.Log($"Equipped skin index: {index}");
    }
}