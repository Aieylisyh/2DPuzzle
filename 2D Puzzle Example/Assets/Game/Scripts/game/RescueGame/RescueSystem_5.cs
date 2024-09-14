﻿using com;
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Rescue
{
    public partial class RescueSystem : MonoBehaviour
    {
        public RectTransform rect_BoyFood;
        public RectTransform[] foods;

        public void Eat(RectTransform rt)
        {
            SoundSystem.instance.Play("ding");
            rt.DOKill();
            rt.DOScale(0.5f, 0.5f).SetEase(Ease.InBack).OnComplete(() => { rt.gameObject.SetActive(false); FoodSceneEnd(); });
        }

        void FoodSceneEnd()
        {
            foreach (var f in foods)
            {
                if (f.gameObject.activeSelf)
                    return;
            }

            rect_BoyFood.DOKill();
            rect_BoyFood.DOScale(0.8f, 1f).OnComplete(() =>
            {

                // ToggleCg(???, true);
                // ToggleCg(sceneCg_FoodBoy, false);
            });
            Debug.Log("FoodSceneEnd!");
        }

        void ShowBoyFoodScene()
        {
            rect_BoyFood.DOKill();
            rect_BoyFood.localScale = Vector3.one * 0.8f;
            rect_BoyFood.DOScale(1, 2.5f);
        }
    }
}