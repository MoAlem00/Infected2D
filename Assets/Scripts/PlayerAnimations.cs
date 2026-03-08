using System;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        if(animator == null)
            animator = GetComponent<Animator>();
    }
    
    public void ResetAnimations()
    {
        animator.SetBool("isMovingDown", false);
        animator.SetBool("isMovingLeft", false);
        animator.SetBool("isMovingRight", false);
        animator.SetBool("isMovingUp", false);
    }

    public void MoveUp()
    {
        animator.SetBool("isMovingUp", true);
    }

    public void MoveDown()
    {
        animator.SetBool("isMovingDown", true);
    }

    public void MoveLeft()
    {
        animator.SetBool("isMovingLeft", true);
    }

    public void MoveRight()
    {
        animator.SetBool("isMovingRight", true);
    }
    
    public void Dead()
    {
        animator.SetTrigger("isDead");
    }
}
