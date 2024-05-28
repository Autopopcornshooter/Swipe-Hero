using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Indicators : MonoBehaviour
{

    [SerializeField]
    private GameObject time_indicator;
    [SerializeField]
    private GameObject score_indicator;
    [SerializeField]
    private GameObject combo_indicator;
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
            "Time=" + min + ":" + sec
            +"\r\n"+
            "Combo=" + GameInfo.gamedata.highestCombo;
    }
    public void SetInGameIndicator()
    {
        int min = GameScoreCheck.playTime / 60;
        int sec = GameScoreCheck.playTime % 60;

        score_indicator.GetComponent<Text>().text = GameScoreCheck.killScore + " kill";
        time_indicator.GetComponent<Text>().text = "time=" + min + ":" + sec;
        combo_indicator.GetComponent<TextMeshProUGUI>().text = GameScoreCheck.currentCombo.ToString();
    }
    private void FixedUpdate()
    {
        if (GameManager.Instance().isGameRunning)
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
