using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ResultPanelCtrl : PanelCtrl
{
    [Header("Value")]
    public Ease ease;
    public float panel_switch_term;
    [Header("Reference")]
    [SerializeField]
    private GameObject subPanel;
    [SerializeField]
    private GameObject resultPanel;
    [SerializeField]
    private Text resultText;

    private bool panelActivated=false;

    private bool isSwipeMode = false;
    private void Awake()
    {
    }
    private void Start()
    {
        panelActivated = false;
    }
    private void ContentResizing(float width, float height)
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(width, height*2);
        subPanel.GetComponent<RectTransform>().sizeDelta= new Vector2(width, height);
        resultPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
    }
    
    protected override void SwipeDTU()
    {
        StartCoroutine(GameRestart());
    }
    protected override void SwipeUTD()
    {
        StartCoroutine(GameOver());
    }

    public IEnumerator ShowResultPanel()
    {
        isSwipeMode = true;
        GameSound.Instance().ShootUISound2(GameSound.Instance().swipeSFX3);
        SetResult();
        GetComponent<RectTransform>().DOAnchorPosY(
            -GetComponent<RectTransform>().rect.size.y/2, panel_switch_term).SetEase(ease);
        panelActivated = true;
        yield return new WaitForSecondsRealtime(panel_switch_term);
        isSwipeMode = false;
    }
    private void SetResult()
    {
        int min = GameManager.Instance().playTime / 60;
        int sec = GameManager.Instance().playTime % 60;
        string tempText = 
            "Kill="+GameManager.Instance().killScore
            +"\r\n\r\n\r\n" +
            "time="+ min + ":" + sec;

        resultText.text = tempText;
    }
    IEnumerator GameRestart()
    {
        isSwipeMode = true;
        GameSound.Instance().ShootUISound2(GameSound.Instance().swipeSFX2);
        //Debug.Log("GameRestart");
        GetComponent<RectTransform>().DOAnchorPosY(
            0, panel_switch_term).SetEase(ease);
        yield return new WaitForSecondsRealtime(panel_switch_term);
        isSwipeMode = false;
       ToPlayScene();
    }
    IEnumerator GameOver()
    {
        isSwipeMode = true;
        GameSound.Instance().ShootUISound2(GameSound.Instance().swipeSFX4);
        //free aspect로 창 해상도 변화 시 위치 맞지 않는문제 발생 - 수정 완료
        GetComponent<RectTransform>().DOAnchorPosY(
            -GetComponent<RectTransform>().rect.size.y, panel_switch_term).SetEase(ease);
        yield return new WaitForSecondsRealtime(panel_switch_term);
        isSwipeMode = false;
       ToMenuScene();
    }


    private void ToPlayScene()
    {
        GameManager.Instance().SceneChange("PlayScene");
    }
    private void ToMenuScene()
    {
        GameManager.Instance().SceneChange("MainMenu");
    }

    private void Update()
    {
        if (!panelActivated && PlayerColliderCheck.isDead)
        {
            //Debug.Log("PlayerDead_ On ResultPanel");
            StartCoroutine(ShowResultPanel());
        }
        if (panelActivated && !isSwipeMode)
        {
            ReceiveInput();
        }
    }
    private void FixedUpdate()
    {
        ContentResizing(GameManager.Instance().GetCanvasWidth(), GameManager.Instance().GetCanvasHeight());
    }
}
