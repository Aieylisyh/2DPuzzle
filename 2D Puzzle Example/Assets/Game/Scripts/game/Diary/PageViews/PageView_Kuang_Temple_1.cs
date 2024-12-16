
using UnityEngine;
using Assets.Game.Scripts.game.Diary;
using Assets.Game.Scripts.game.Diary.Common;
using echo17.EndlessBook.Demo02;
using System.Collections;
using com;

public class PageView_Kuang_Temple_1 : PageView
{
    public Collider muyuCol;

    public Transform[] toReveals;
    private int nextIndex;

    public string sfxMuyuBig;
    public string sfxMuyuSmall;
    public MimicPanelObject muyuAnim;
    public MimicPanelObject monkAnim;
    public GameObject prefabDong;
    public float dongSpawnOffset;

    public override void Activate()
    {
        base.Activate();
        CheckLock();
    }
    public override void Deactivate()
    {
        //Debug.Log("Deactivate");
        base.Deactivate();
    }

    public void CheckLock()
    {
        var allRevealed = nextIndex >= toReveals.Length;

        if (allRevealed)
        {
            DiaryGameSystem.instance.ToggleLockTurnPage(false);
        }
        else
        {
            DiaryGameSystem.instance.ToggleLockTurnPage(true);
        }
    }


    protected override bool HandleHit(RaycastHit hit, BookActionDelegate action)
    {
        var res = hit.collider == muyuCol;
        if (res)
            OnClickMuyu();
        return res;
    }

    void OnClickMuyu()
    {
        var dong = Instantiate(prefabDong, prefabDong.transform.position, prefabDong.transform.rotation);
        dong.transform.position += new Vector3(Random.Range(-dongSpawnOffset, dongSpawnOffset),
            Random.Range(-dongSpawnOffset, dongSpawnOffset) * 0.5f, 0);
        dong.SetActive(true);
        Destroy(dong.gameObject, 2);

        if (_isMuyuBusy)
        {
            SoundSystem.instance.Play(sfxMuyuSmall);
            return;
        }

        SoundSystem.instance.Play(sfxMuyuBig);
        StartCoroutine(MuyuCoroutine());
    }

    bool _isMuyuBusy;

    IEnumerator MuyuCoroutine()
    {
        _isMuyuBusy = true;
        ShowNextItem();
        monkAnim.gameObject.SetActive(true);
        muyuAnim.StartAnim();
        yield return new WaitForSeconds(1);
        monkAnim.gameObject.SetActive(false);
        _isMuyuBusy = false;
    }

    void ShowNextItem()
    {
        var allRevealed = nextIndex >= toReveals.Length;

        if (allRevealed)
        {
            CheckLock();
            return;
        }

        var item = toReveals[nextIndex];
        item.gameObject.SetActive(true);
        var mpo = item.GetComponent<MimicPanelObject>();
        if (mpo != null)
        {
            mpo.Init();
            mpo.StartAnim();
        }
        nextIndex++;
    }
}