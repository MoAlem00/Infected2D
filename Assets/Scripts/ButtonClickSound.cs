using UnityEngine;

//script to play button clicks sounds in game scene,
//because sounds manager is on the main menu scene 
public class ButtonClickSound : MonoBehaviour
{
    [SerializeField] private AudioClip buttonSound;
    [SerializeField] private AudioClip upgradeButtonSound;


    public void OnButtonClick()
    {
        SoundsManager.Instance.PlaySFX(buttonSound,1f);
    }

    public void OnUpgradeButtonClick()
    {
        SoundsManager.Instance.PlaySFX(upgradeButtonSound,1f);
    }
}
