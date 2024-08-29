using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class DreamBubble : MonoBehaviour
{
    //左右对称
    //梦想图片
    //颜色
    //持续时间
    //位置
    //飘动
    //大小
    //声音

    [SerializeField] Image bubbleImage;
    [SerializeField] Image dreamImage;
    [SerializeField] RectTransform rect;
    [SerializeField] com.MoveSinBehaviour moveSinBehaviour;
    [SerializeField] CanvasGroup cg;
    [SerializeField] Button btn;

    [System.Serializable]
    public class 气泡参数
    {
        public bool faceRight;//✔
        public Sprite deamSp;//✔
        public Color color;//✔
        public float duration;//✔
        public Vector2 pos;//✔
        public float floatingSpeed;//✔
        public float size;//✔
        public string feedbackSfx;
    }


    public void Init(气泡参数 param)
    {
        rect.localScale = param.size * (param.faceRight ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1));
        bubbleImage.color = param.color;
        rect.anchoredPosition = param.pos;
        moveSinBehaviour.amplitude = param.floatingSpeed;
        dreamImage.sprite = param.deamSp;

        StartCoroutine(DelayAction(
             param.duration, DeleteMe
            ));
    }

    IEnumerator DelayAction(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }

    void DeleteMe()
    {
        btn.enabled = false;
        cg.DOFade(0, 1.5f).OnComplete(() => { Destroy(this.gameObject); });
    }
}