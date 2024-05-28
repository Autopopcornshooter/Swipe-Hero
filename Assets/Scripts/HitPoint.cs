using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPoint : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().DOFade(0.0f, 0.4f).SetLoops(-1, LoopType.Yoyo);
    }
    public IEnumerator ActivateHitPoint()
    {
        GetComponent<Collider2D>().enabled = true;
        yield return new WaitForSecondsRealtime(0.1f);
        GetComponent<Collider2D>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            //����̺�Ʈ �߻����� �ʴ� ����- ��ġ�� �ʿ� : �ذ� ��
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
