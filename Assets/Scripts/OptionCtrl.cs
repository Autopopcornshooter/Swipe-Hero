using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionCtrl : MonoBehaviour
{

    [SerializeField]
    private Slider BGM_Volume;
    [SerializeField]
    private Slider SFX_Volume;
    [SerializeField]
    private Slider Shake_Value;
    [SerializeField]
    private Slider Color_Select;
    [SerializeField]
    private Toggle TutorialGuide;
    [SerializeField]
    private Image selected_Color;
    


    private void Start()
    {
        JsonCtrl.Instance().LoadData();
        BGM_Volume.value = GameInfo.gamedata.BGM_Volume;
        SFX_Volume.value = GameInfo.gamedata.SFX_Volume;
        Shake_Value.value = GameInfo.gamedata.Shake_Value;
        Color_Select.value = GameInfo.gamedata.Color_Select;
        TutorialGuide.isOn = GameInfo.gamedata.tutorialGuideOn;
    }

    private void Update()
    {
        GameInfo.gamedata.BGM_Volume = BGM_Volume.value;
        GameInfo.gamedata.SFX_Volume=SFX_Volume.value;
        GameInfo.gamedata.Shake_Value=Shake_Value.value;
        GameInfo.gamedata.Color_Select = Color_Select.value;
        GameInfo.gamedata.tutorialGuideOn = TutorialGuide.isOn;
    }
    public void Save()
    {
        JsonCtrl.Instance().SaveData();
    }
    public void DisplayingColor()
    {
        
            selected_Color.color = Utils.SetHSVTORGB(Color_Select.value);
    }
    public void SFXButtonUp()
    {
        GameSound.Instance().ShootUISound1(GameSound.Instance().swipeSFX1);
    }
}
