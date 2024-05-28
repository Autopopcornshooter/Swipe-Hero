using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameProcess/* : MonoBehaviour*/
{
    public static int s_BTime_Max=10;
    public static int s_BTime_Remain=0;
    public static IEnumerator Process()
    {
        GameObject.Find("SpawnPos").GetComponent<MonsterSpawnCtrl>().MonsterSpawnStart();
        while (true)
        {
            if (GameManager.Instance().isGameRunning)
            {
                yield return new WaitForSecondsRealtime(1.0f);
                if (s_BTime_Remain<=0)
                {
                    Normal_Process();
                }
                else
                {
                    Bonus_Process();
                }
            }
            yield return null;
        }
    }
    public static void ToBonusTime()
    {
        s_BTime_Remain = s_BTime_Max;
    }

    private static void Normal_Process()
    {
        PlayerHPCtrl.HP_Decrease();
        GameScoreCheck.playTime++;
    }
    private static void Bonus_Process()
    {
        if (s_BTime_Remain > 0)
        {
            s_BTime_Remain--;
        }
        GameScoreCheck.playTime++;
    }
}
