using Assets.Game.Scripts.game.LondonLifeGame.PlaneScene;
using com;
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] UnityEngine.UI.Button btn;
    气泡参数 _我的参数;

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
        _我的参数 = param;
        rect.localScale = param.size * (param.faceRight ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1));
        bubbleImage.color = param.color;
        rect.anchoredPosition = param.pos;
        moveSinBehaviour.amplitude = param.floatingSpeed;
        moveSinBehaviour.offset = UnityEngine.Random.Range(0, 6);
        dreamImage.sprite = param.deamSp;

        StartCoroutine(DelayAction(
             param.duration, DeleteMe
            ));

        cg.alpha = 0;
        cg.DOFade(1, 0.7f);
    }

    IEnumerator DelayAction(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }

    void DeleteMe()
    {
        btn.enabled = false;
        cg.DOFade(0, 1.2f).OnComplete(() =>
        {
            PlaneSceneGameSystem.instance.AddTo待生成的气泡们(_我的参数);
            Destroy(this.gameObject);
        });
    }

    public void OnClick()
    {
        btn.enabled = false;
        SoundSystem.instance.Play("item");
        rect.DOPunchScale(Vector3.one * 0.3f, 0.6f, 4, 1);
        PlaneSceneGameSystem.instance.OnClickBubble();
        cg.DOFade(0, 1.0f).OnComplete(() =>
        {
            Destroy(this.gameObject);
        });
    }
}