using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTuto : MonoBehaviour
{
    public Animator animator;
    public AudioSource as_catSound;
    public AudioSource as_victory;
    public CanvasGroup cg_victory;

    public void OnClick_è�ж���()
    {
        animator.SetTrigger("sound");
    }

    public void OnClick_è������()
    {
        as_catSound.Play();
    }

    public void OnClick_ʤ������()
    {
        as_victory.Play();
    }

    public void OnClick_ʤ������()
    {
        cg_victory.DOFade(1, 2).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            cg_victory.alpha = 0;
        });
    }
}