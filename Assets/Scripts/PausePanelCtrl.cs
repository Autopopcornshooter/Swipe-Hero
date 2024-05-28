using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PausePanelCtrl : PanelCtrl
{
    [Header("Value")]
    [SerializeField]
    private float panel_switch_term = 1.0f;
    [SerializeField]
    private float extra_waiting_term = 1.0f;
    [Header("Reference")]
    [SerializeField]
    private GameObject pauseWindow;
    private bool isSwipeMode = false;
    private bool isPauseOn = false;
    // Update is called once per frame
    void Update()
    {
        if (!isSwipeMode && !GameManager.Instance().isGameRunning && isPauseOn)
        {
            ReceiveInput();
        }
    }
    protected override void SwipeDTU()
    {
        StartCoroutine(Resumegame());
    }
    protected override void SwipeLTR()
    {
        StartCoroutine(QuitGame());
    }
    protected override void SwipeRTL()
    {
        StartCoroutine(RestartGame());
    }

    public void ShowPausePanel()
    {
        if (GameManager.Instance().isGameRunning)
        {
            isPauseOn = true;
            GameManager.Instance().isGameRunning = false;
            GameSound.Instance().PauseBGM();
            GetComponent<RectTransform>().DOAnchorPosY(
                -GameManager.Instance().GetCanvasHeight(), panel_switch_term).SetEase(Ease.OutBack);
            GameSound.Instance().ShootUISound2(GameSound.Instance().swipeSFX5);
            StartCoroutine(InputDealy());
        }
    }
    IEnumerator InputDealy()
    {
        isSwipeMode = true;
        yield return new WaitForSecondsRealtime(panel_switch_term);
        isSwipeMode = false;
    }
    IEnumerator RestartGame()
    {
        isSwipeMode = true;
        GameSound.Instance().ShootUISound2(GameSound.Instance().swipeSFX6);
        //Debug.Log("GameRestart");
        GetComponent<RectTransform>().DOAnchorPosX(
            -GameManager.Instance().GetCanvasWidth(), panel_switch_term).SetEase(Ease.OutBounce);
        yield return new WaitForSecondsRealtime(panel_switch_term + extra_waiting_term);
        isSwipeMode = false;
        GameManager.Instance().SceneChange("PlayScene");
    }
    IEnumerator QuitGame()
    {
        isSwipeMode = true;
        GameSound.Instance().ShootUISound2(GameSound.Instance().swipeSFX6);
        //Debug.Log("GameRestart");
        GetComponent<RectTransform>().DOAnchorPosX(
            GameManager.Instance().GetCanvasWidth(), panel_switch_term).SetEase(Ease.OutBounce);
        yield return new WaitForSecondsRealtime(panel_switch_term + extra_waiting_term);
        isSwipeMode = false;
        GameManager.Instance().GameEnd();
        GameManager.Instance().SceneChange("MainMenu");
    }
    IEnumerator Resumegame()
    {
        isSwipeMode = true;
        isPauseOn = false;
        GameSound.Instance().ShootUISound2(GameSound.Instance().swipeSFX6);
        //Debug.Log("GameRestart");
        GetComponent<RectTransform>().DOAnchorPosY(
            0, panel_switch_term).SetEase(Ease.OutBounce);
        yield return new WaitForSecondsRealtime(panel_switch_term + extra_waiting_term);
        isSwipeMode = false;
        GameManager.Instance().isGameRunning = true;
        GameSound.Instance().ResumeBGM();
    }

    private void FixedUpdate()
    {
        ContentResizing(GameManager.Instance().GetCanvasWidth(), GameManager.Instance().GetCanvasHeight());
    }
    private void ContentResizing(float width, float height)
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        pauseWindow.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
    }
}
