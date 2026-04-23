using UnityEngine;

//script that handles player animations
public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;
    private string moveDownAnimation = "isMovingDown";
    private string moveLeftAnimation = "isMovingLeft";
    private string moveRightAnimation = "isMovingRight";
    private string moveUpAnimation = "isMovingUp";
    private string deadAnimation = "isDead";
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    public void ResetAnimations()
    {
        animator.SetBool(moveDownAnimation, false);
        animator.SetBool(moveLeftAnimation, false);
        animator.SetBool(moveRightAnimation, false);
        animator.SetBool(moveUpAnimation, false);
    }

    public void MoveUp()
    {
        animator.SetBool(moveUpAnimation, true);
    }

    public void MoveDown()
    {
        animator.SetBool(moveDownAnimation, true);
    }

    public void MoveLeft()
    {
        animator.SetBool(moveLeftAnimation, true);
    }

    public void MoveRight()
    {
        animator.SetBool(moveRightAnimation, true);
    }
    
    public void Dead()
    {
        animator.SetTrigger(deadAnimation);
    }
}
