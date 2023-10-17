using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicators : MonoBehaviour
{

    [SerializeField]
    private GameObject time;
    [SerializeField]
    private GameObject Score;
    [SerializeField]
    private GameObject highestScore;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetMenuIndicator()
    {
        int min = GameInfo.gamedata.longestPlayTime / 60;
        int sec = GameInfo.gamedata.longestPlayTime % 60;
        highestScore.GetComponent<Text>().text =
            "High Score"
            + "\r\n" +
            "Kill=" + GameInfo.gamedata.highestKillScore
            + "\r\n" +
            "Time=" + min + ":" + sec;
    }
    public void SetInGameIndicator()
    {
        int min = GameManager.Instance().playTime / 60;
        int sec = GameManager.Instance().playTime % 60;

        Score.GetComponent<Text>().text = GameManager.Instance().killScore + " kill";
        time.GetComponent<Text>().text = "time=" + min + ":" + sec;
    }
    private void FixedUpdate()
    {
        if (GameManager.isGameRunning)
        {
            SetInGameIndicator();
        }
        if (highestScore != null)
        {
            SetMenuIndicator();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
