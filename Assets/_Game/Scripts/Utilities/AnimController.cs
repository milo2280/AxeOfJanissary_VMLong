using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private Enum currentAnim;

    public void ChangeAnim(Enum nextAnim)
    {
        if (currentAnim != nextAnim)
        {
            if (currentAnim != null)
            {
                animator.ResetTrigger(currentAnim.ToString());
            }
            animator.SetTrigger(nextAnim.ToString());
            currentAnim = nextAnim;
        }
    }
}

public enum CharacterAnim
{
    idle,
    jump,
    charge,
}
