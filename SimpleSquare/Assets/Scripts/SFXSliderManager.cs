using UnityEngine;
using UnityEngine.UI;
public class SFXSliderManager : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    private void Start()
    {
        if (!PlayerPrefs.HasKey("sfxVolume"))
        {
            PlayerPrefs.SetFloat("sfxVolume", 0.5f);
            Load();
        }
        else
        {
            Load();
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = musicSlider.value;
        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("sfxVolume", musicSlider.value);
    }

    private void Load()
    {
        musicSlider.value = PlayerPrefs.GetFloat("sfxVolume");
    }
}
