using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialGuide : MonoBehaviour
{
    [SerializeField]
    private GameObject guideHand;
    
    public Transform verticalPos;
    public Transform horizontalPos;
    public Text tutorialTime;
    public Ease customEase;

    private float lerpTime = 1.0f;

    private void Start()
    {
        tutorialTime.text = "";
            StartCoroutine(TutorialRoutine());
    }
    IEnumerator TutorialRoutine()
    {
        if (GameInfo.gamedata.tutorialGuideOn)
        {
            guideHand.SetActive(true);
        }
        else
        {
            guideHand.SetActive(false);
        }

        guideHand.transform.position = horizontalPos.position;
        GuideHandMove_X(-horizontalPos.position.x);
        tutorialTime.text = "4";
        yield return new WaitForSecondsRealtime(lerpTime);
        GuideHandMove_X(horizontalPos.position.x);
        tutorialTime.text = "3";
        yield return new WaitForSecondsRealtime(lerpTime);
        guideHand.transform.position = verticalPos.position;
        tutorialTime.text = "2";
        GuideHandMove_Y(-verticalPos.position.y);
        yield return new WaitForSecondsRealtime(lerpTime);
        tutorialTime.text = "1";
        GuideHandMove_Y(verticalPos.position.y);
        yield return new WaitForSecondsRealtime(lerpTime);
        tutorialTime.text = "Game start!";
        GameManager.Instance().GameStart();
        yield return new WaitForSecondsRealtime(lerpTime);
        tutorialTime.text = "";
        Destroy(gameObject);
    }
    private void GuideHandMove_X(float target_x)
    {
        guideHand.transform.DOMoveX(target_x, lerpTime).SetEase(customEase);
        guideHand.GetComponent<SpriteRenderer>().DOFade(1.0f, 0);
        guideHand.GetComponent<SpriteRenderer>().DOFade(0.0f, lerpTime);
    }
    private void GuideHandMove_Y(float target_y)
    {
        guideHand.transform.DOMoveY(target_y, lerpTime).SetEase(customEase);
        guideHand.GetComponent<SpriteRenderer>().DOFade(1.0f, 0);
        guideHand.GetComponent<SpriteRenderer>().DOFade(0.0f, lerpTime);
    }
}
