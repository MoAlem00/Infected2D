using UnityEngine;
using UnityEngine.UI;

//script for music volume slider in settings menu
public class Settings : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;


    private void Start()
    {
        volumeSlider.value = SoundsManager.Instance.musicSource.volume;
    }

    public void SetVolume()
    {
        SoundsManager.Instance.musicSource.volume = volumeSlider.value;
    }
}
