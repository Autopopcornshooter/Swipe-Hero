using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour
{
    private bool isMonsterHit=false;
    private void Start()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().DOFade(0.0f, 0.4f).SetLoops(-1, LoopType.Yoyo);
    }
    public IEnumerator ActivateHitPoint()
    {
        GetComponent<Collider2D>().enabled = true;
        yield return new WaitForSecondsRealtime(0.1f);
        ComboCheck();
        GetComponent<Collider2D>().enabled = false;
    }
    private void ComboCheck()
    {
        if (!isMonsterHit)
        {
            GameScoreCheck.currentCombo = 0;
        }
        else
        {
            GameScoreCheck.currentCombo += 1;
            if (GameInfo.gamedata.highestCombo <= GameScoreCheck.currentCombo)
            {
                GameInfo.gamedata.highestCombo = GameScoreCheck.currentCombo;
            }
            isMonsterHit = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            //사망이벤트 발생하지 않는 문제- 고치기 필요 : 해결 완
            isMonsterHit = true;
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
