
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    private static GameManager instance;
    public bool isGameRunning = false;
    private IEnumerator currentGameProcess;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        Singltonize();
    }
   
    public void GameStart()
    {
        GameScoreCheck.ScoreInitiate();
        currentGameProcess = GameProcess.Process();
        GameSound.Instance().PlayRandomInGameBGM();
        isGameRunning = true;
        StartCoroutine(currentGameProcess);
    }
    public void GamePause()
    {
        isGameRunning = false;
    }
    public void GameEnd()
    {
        isGameRunning = false;
        GameSound.Instance().PlayBGM(GameSound.Instance().result);
        GameScoreCheck.ScoreCheck();
        GameScoreCheck.SaveScore();
        JsonCtrl.Instance().SaveData();

        StopCoroutine(currentGameProcess);
    }
    public void ResetData()
    {
        JsonCtrl.Instance().ResetData();
    }
    private void Singltonize()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public static GameManager Instance()
    {
        return instance;
    }
    // Start is called before the first frame update


    public void SceneChange(string SceneName)
    {
        GameSound.Instance().PauseBGM();
        Debug.Log("SceneChange to " + SceneName);
        //if (FullScreenAdmob.RequestInterstitial())
        //{
        //    int rand_num = Random.Range(1, 101);
        //    if (rand_num <= fullScreenAD_probability)       //È®·ü·Î ±¤°í Àç»ý
        //    {
        //        FullScreenAdmob.nextScene = SceneName;
        //        FullScreenAdmob.ShowAd();
        //    }
        //    else
        //    {
        //        SceneManager.LoadScene(SceneName);
        //    }
        //}
        //else
        //{
        //    SceneManager.LoadScene(SceneName);
        //}
        //if (!FullScreenAdmob.isInterstitialAd_ON)
        {
            SceneManager.LoadScene(SceneName);
        }
    }
    public float GetCanvasWidth()
    {
        return GameObject.Find("Canvas").GetComponent<RectTransform>().sizeDelta.x;
    }
    public float GetCanvasHeight()
    {
        return GameObject.Find("Canvas").GetComponent<RectTransform>().sizeDelta.y;
    }

    
}
