using UnityEngine;
using UnityEngine.UI;

public class SyncSliderValue : MonoBehaviour
{
    private void OnEnable()
    {
        Slider slider = GetComponent<Slider>();
        if (slider != null)
        {
            // Match the manager! Default to 1f (100%) if no save exists.
            slider.value = PlayerPrefs.GetFloat("SavedMusicVolume", 1f);
        }
    }
}