using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("All Menu Screens")]
    public GameObject[] allScreens;

    [Header("First Screen to Open by Default")]
    public GameObject defaultStartScreen;

    private void Start()
    {
        // Check if the player just came back from a level
        if (PlayerPrefs.HasKey("SavedScreenIndex"))
        {
            int savedIndex = PlayerPrefs.GetInt("SavedScreenIndex");
            
            if (savedIndex >= 0 && savedIndex < allScreens.Length)
            {
                OpenScreenByIndex(savedIndex);
                PlayerPrefs.DeleteKey("SavedScreenIndex"); 
                return;
            }
        }

        // Default setup
        OpenScreen(defaultStartScreen);
    }

    public void OpenScreen(GameObject targetScreen)
    {
        foreach (GameObject screen in allScreens)
        {
            if (screen != null)
            {
                screen.SetActive(screen == targetScreen);
            }
        }
    }

    private void OpenScreenByIndex(int index)
    {
        for (int i = 0; i < allScreens.Length; i++)
        {
            if (allScreens[i] != null)
            {
                allScreens[i].SetActive(i == index);
            }
        }
    }

    // --- FIXES FOR THE SCENE LOADING BUTTONS ---

    /// <summary>
    /// BUTTON FOR LV1: Saves the index and loads LV1
    /// </summary>
    public void LoadLevel1(int currentScreenIndex)
    {
        PlayerPrefs.SetInt("SavedScreenIndex", currentScreenIndex);
        PlayerPrefs.Save();
        SceneManager.LoadScene("LV1"); // Matches your scene name exactly!
    }

    /// <summary>
    /// BUTTON FOR LV2: Saves the index and loads lv2
    /// </summary>
    public void LoadLevel2(int currentScreenIndex)
    {
        PlayerPrefs.SetInt("SavedScreenIndex", currentScreenIndex);
        PlayerPrefs.Save();
        SceneManager.LoadScene("lv2"); // Matches your lowercase 'l' scene name!
    }

    /// <summary>
    /// BUTTON FOR LV3: Saves the index and loads lv3
    /// </summary>
    public void LoadLevel3(int currentScreenIndex)
    {
        PlayerPrefs.SetInt("SavedScreenIndex", currentScreenIndex);
        PlayerPrefs.Save();
        SceneManager.LoadScene("lv3");
    }

    //QUITING
private void OnApplicationQuit()
{
    // This handles Alt+F4, the 'X' button, and mobile app closing
    ClearSavedScreen();
}

public void QuitGameComplete()
{
    // This handles your custom in-game Quit button
    ClearSavedScreen();
    
    Debug.Log("Game is exiting...");
    Application.Quit(); 
}

// This magic line tells Unity: "Run this code ONCE when the game first launches"
[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
private static void CleanUpOnFreshBoot()
{
    // If the game is starting completely fresh, clear the key
    if (PlayerPrefs.HasKey("SavedScreenIndex"))
    {
        PlayerPrefs.DeleteKey("SavedScreenIndex");
        PlayerPrefs.Save();
    }
}

// Clean up the key before leaving
private void ClearSavedScreen()
{
    if (PlayerPrefs.HasKey("SavedScreenIndex"))
    {
        PlayerPrefs.DeleteKey("SavedScreenIndex");
        PlayerPrefs.Save();
    }
}
}