using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScoreCheck : MonoBehaviour
{

    static public int playTime = 0;
    static public int killScore = 0;
    static public int currentCombo = 0;
    static private int s_bonus_targetCombo;
    static private int currentBonusCombo;
    [SerializeField]
    private int bonus_targetCombo;

    private void Awake()
    {
        s_bonus_targetCombo = bonus_targetCombo;
    }
    static public void ScoreInitiate()
    {
        playTime = 0;
        killScore = 0;
        currentCombo = 0;
    }
    static public void SaveScore()
    {
        GameInfo.gamedata.totalPlayTime += playTime;
        GameInfo.gamedata.totalMonsterKills += killScore;
    }
   static public void ScoreCheck()
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
    
    // Start is called before the first frame update
    static public void BonusComboIncrease()
    {
        if (GameProcess.isBonusTime <= 0)
        {
            if (s_bonus_targetCombo <= currentBonusCombo)
            {

            }
            else
            {
                currentBonusCombo++;
            }
        }

    }
}
