

public static class GameInfo
{
    public static GameData gamedata = new GameData();
}


public class GameData
{
    public float BGM_Volume = 0.5f;
    public float SFX_Volume = 0.5f;
    public float Shake_Value = 0.5f;
    public float Color_Select = 0.0f;

    public int totalMonsterKills = 0;
    public int totalPlayTime = 0;

    public bool tutorialGuideOn = true;

    public int longestPlayTime = 0;
    public int highestKillScore = 0;
    public int highestCombo = 0;
}