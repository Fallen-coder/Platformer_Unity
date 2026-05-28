using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public static SkinManager Instance { get; private set; }

    [SerializeField] private PlayerSkinAlbum skinAlbum;
    private int currentSkinIndex = 0;

    // Event that the player script will listen to
    public System.Action<PlayerSkin> OnSkinChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSkin();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public PlayerSkin GetCurrentSkin()
    {
        if (skinAlbum == null || skinAlbum.skins.Length == 0) return null;
        return skinAlbum.skins[currentSkinIndex];
    }

    public PlayerSkinAlbum GetAlbum() => skinAlbum;

    public void EquipeSkin(int index)
    {
        if (skinAlbum == null || index < 0 || index >= skinAlbum.skins.Length) return;

        currentSkinIndex = index;
        SaveSkin();

        // Notify the player to update its look immediately if it exists
        OnSkinChanged?.Invoke(skinAlbum.skins[currentSkinIndex]);
    }

    private void SaveSkin()
    {
        PlayerPrefs.SetInt("SelectedSkinIndex", currentSkinIndex);
        PlayerPrefs.Save();
    }

    private void LoadSkin()
    {
        currentSkinIndex = PlayerPrefs.GetInt("SelectedSkinIndex", 0);
    }
}