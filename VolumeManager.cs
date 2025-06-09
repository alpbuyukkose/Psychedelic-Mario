using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        
        float savedVolume = PlayerPrefs.GetFloat("volume", 0.5f);
        volumeSlider.SetValueWithoutNotify(savedVolume);
        AudioListener.volume = savedVolume;

        
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("volume", value);
    }
}
