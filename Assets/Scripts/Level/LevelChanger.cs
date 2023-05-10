using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChanger : PanelUI
{
    [SerializeField] private Animator animator;

    public void FadeIn()
    {
        animator.SetTrigger("FadeIn");
    }

    public void FadeOut()
    {
        animator.SetTrigger("FadeOut");
    }
}
