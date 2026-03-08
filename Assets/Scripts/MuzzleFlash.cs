using System;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    private Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerMuzzleFlash()
    {
        animator.SetTrigger("Fire");
    }
}
