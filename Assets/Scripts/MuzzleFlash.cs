using UnityEngine;

//script on a GameObject child of the weapon to play the flash animation when player shoots
public class MuzzleFlash : MonoBehaviour
{
    private Animator animator;
    private string flashAnimationName = "MuzzleFlash";


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerMuzzleFlash()
    {
        animator.SetTrigger(flashAnimationName);
    }
}
