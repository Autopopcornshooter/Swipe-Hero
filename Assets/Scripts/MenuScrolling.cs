using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class MenuScrolling : PanelCtrl
{
    enum Panel { Option,Menu }
    [Header("Reference")]
    [SerializeField]
    private GameObject content_obj;
    [SerializeField]
    private GameObject menuPanel_obj;
    [SerializeField]
    private GameObject optionPanel_obj;
    [SerializeField]
    private GameObject startPanel_obj;
    [SerializeField]
    private GameObject quitPanel_obj;
    [Header("User Guide")]
    [SerializeField]
    private GameObject GuideImage;
    [SerializeField]
    private LoopType guide_looptype;
    [SerializeField]
    private Ease content_easeType;

  
    private bool isSwipeMode = false;
    private Panel currentPage = Panel.Menu;
    void Start()
    {
        GameSound.Instance().PlayBGM(GameSound.Instance().mainMenu);
        GuideImage.GetComponent<Text>().DOFade(0.0f, 1.0f).SetLoops(-1, guide_looptype);
    }
    private void ContentResizing(float width, float height)
    {
        content_obj.GetComponent<RectTransform>().sizeDelta = new Vector2(width * 3, height * 2);

        menuPanel_obj.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        optionPanel_obj.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        startPanel_obj.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        quitPanel_obj.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);

    }

    private void ToOptionPanel()
    {
        //StartCoroutine(VerticalSlide(1));
        GetComponent<RectTransform>().DOAnchorPosY(-GameManager.Instance().GetCanvasHeight(), 1.0f).SetEase(content_easeType);
        GameSound.Instance().ShootUISound1(GameSound.Instance().swipeSFX1);
        currentPage = Panel.Option;
        StartCoroutine(InputDelay());
    }
    private void ToMenuPanel()
    {
        //StartCoroutine(VerticalSlide(0));

        GetComponent<RectTransform>().DOAnchorPosY(0.0f, 1.0f).SetEase(content_easeType);
        GameSound.Instance().ShootUISound1(GameSound.Instance().swipeSFX1);
        currentPage = Panel.Menu;
        StartCoroutine(InputDelay());
    }
    private void ToGameStart()
    {
        //GuideImage.SetActive(false);
        //StartCoroutine(HorizontalSlide(1));
        GetComponent<RectTransform>().DOAnchorPosX(-GameManager.Instance().GetCanvasWidth(), 1.0f).SetEase(content_easeType);
        GameSound.Instance().ShootUISound1(GameSound.Instance().swipeSFX1);
        StartCoroutine(GameStart());
        isSwipeMode = true;
    }
    private void ToGameQuit()
    {
        //GuideImage.SetActive(false);
        //StartCoroutine(HorizontalSlide(0));
        isSwipeMode = true;
        GetComponent<RectTransform>().DOAnchorPosX(GameManager.Instance().GetCanvasWidth(), 1.0f).SetEase(content_easeType);
        GameSound.Instance().ShootUISound1(GameSound.Instance().swipeSFX1);
        StartCoroutine(Quit());
    }

    IEnumerator Quit()
    {
        yield return new WaitForSecondsRealtime(1.5f);
#if UNITY_ANDROID
        Application.Quit();
#endif

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    IEnumerator GameStart()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        GameManager.Instance().SceneChange("PlayScene");
    }
    protected override void SwipeDTU()
    {
        if (currentPage == Panel.Menu) return;
        ToMenuPanel();
    }
    protected override void SwipeUTD()
    {
        if (currentPage == Panel.Option) return;
        ToOptionPanel();
    }
    protected override void SwipeRTL()
    {
        if (currentPage == Panel.Option) return;
        ToGameStart();
    }
    protected override void SwipeLTR()
    {
        if (currentPage == Panel.Option) return;
        ToGameQuit();
    }
   
    IEnumerator InputDelay()
    {
        isSwipeMode = true;
       yield return new WaitForSecondsRealtime(0.5f);
        isSwipeMode = false;
    }
   
    // Update is called once per frame
    void Update()
    {
        if (!isSwipeMode)
        {
            ReceiveInput();
        }
        ContentResizing(GameManager.Instance().GetCanvasWidth(), GameManager.Instance().GetCanvasHeight());
    }

}
