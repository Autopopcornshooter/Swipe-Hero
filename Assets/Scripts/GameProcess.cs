using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameProcess/* : MonoBehaviour*/
{
    public static int isBonusTime=0;
    public static IEnumerator Process()
    {
        GameObject.Find("SpawnPos").GetComponent<MonsterSpawnCtrl>().MonsterSpawnStart();
        while (true)
        {
            if (GameManager.Instance().isGameRunning)
            {
                yield return new WaitForSecondsRealtime(1.0f);
                if (isBonusTime<=0)
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

    private static void Normal_Process()
    {
        PlayerHPCtrl.HP_Decrease();
        GameScoreCheck.playTime++;
    }
    private static void Bonus_Process()
    {
        if (isBonusTime > 0)
        {
            isBonusTime -= 0;
        }
        GameScoreCheck.playTime++;
    }
}
