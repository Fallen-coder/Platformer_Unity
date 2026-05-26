using UnityEngine;
using UnityEngine.Audio;

public class GlobalAudioManager : MonoBehaviour
{
    public static GlobalAudioManager instance;

    [Header("Audio Configurations")]
    public AudioMixer mainMixer;
    public AudioSource musicSource;

    private void Awake()
    {
        // Keeps this audio manager alive seamlessly across all scene changes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        // Instantly load their saved preference on game boot
        LoadVolumeSettings();
    }

    /// <summary>
    /// Hook this up directly to your UI Slider OnValueChanged (Dynamic float)
    /// </summary>
    public void SetMusicVolume(float value)
    {
        // Converts the slider 0-1 range cleanly to the Mixer's logarithmic decibel scale (-80 to 0)
        float volume = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f;
        
        mainMixer.SetFloat("MusicVol", volume);

        // Saves globally to the player's computer
        PlayerPrefs.SetFloat("SavedMusicVolume", value);
        PlayerPrefs.Save();
    }

private void LoadVolumeSettings()
    {
        // The '1f' at the end means: If the user has NO save file (completely new game), 
        // start them off at 1f (100% volume), NOT 0.
        float savedVolume = PlayerPrefs.GetFloat("SavedMusicVolume", 1f);
        
        SetMusicVolume(savedVolume);
    }
}