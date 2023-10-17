
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    


    private static GameManager instance;
    [HideInInspector]
    public int playTime=0;
    [HideInInspector]
    public int killScore=0;
    static public bool isGameRunning = false;
    private IEnumerator currentGameProcess;
    public FullScreenAdmob fullScreenAdmob;
    private int fullScreenAD_percentage = 75;
    public bool isApplicationPaused = false;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        Singlton();
    }
   
    private void ScoreCheck()
    {
        if (GameInfo.gamedata.longestPlayTime < playTime)
        {
            GameInfo.gamedata.longestPlayTime = playTime;
        }
        if (GameInfo.gamedata.highestKillScore < killScore)
        {
            GameInfo.gamedata.highestKillScore = killScore;
        }
    }
    public void GameStart()
    {
        playTime = 0;
        killScore = 0;
        currentGameProcess = GameProcess();
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
        ScoreCheck();
        GameInfo.gamedata.totalPlayTime += playTime;
        GameInfo.gamedata.totalMonsterKills += killScore;
        JsonCtrl.Instance().SaveData();

        StopCoroutine(currentGameProcess);
    }
    public void ResetData()
    {
        JsonCtrl.Instance().ResetData();
    }
    private void Singlton()
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


    // Update is called once per frame
    void Update()
    {
        if (PlayerColliderCheck.isDead && isGameRunning)
        {
            GameEnd();
        }
    }
    

    public void SceneChange(string SceneName)
    {
        // Debug.Log("SceneChange to " + SceneName);
        //if (fullScreenAdmob != null)
        //{
        //    int rand_num =Random.Range(1, 101);
        //    if (rand_num <= fullScreenAD_percentage)
        //    {
        //        fullScreenAdmob.nextScene = SceneName;
        //        fullScreenAdmob.ShowAd();
        //    }
        //    else
        //    {
        //        SceneManager.LoadScene(SceneName);
        //    }
        //}
        //else
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
    public IEnumerator GameProcess()
    {
        GameObject.Find("SpawnPos").GetComponent<MonsterSpawnCtrl>().MonsterSpawnStart();
        while (true)
        {
            if (isGameRunning)
            {
                yield return new WaitForSecondsRealtime(1.0f);
                playTime++;
            }
            yield return null;
        }
    }

}
