using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RageSliderCtrl : MonoBehaviour
{
    [SerializeField]
    private ImagePounding imagePounding;
    private void FixedUpdate()
    {
        GetComponent<Slider>().value =
                GameScoreCheck.currentBonusCombo / GameScoreCheck.s_bonus_targetCombo;
        if (GameProcess.s_BTime_Remain <= 0)
        {
            if (imagePounding.enabled)
            {
                imagePounding.enabled = false;
            }
            GetComponent<Slider>().value =
                GameScoreCheck.currentBonusCombo / GameScoreCheck.s_bonus_targetCombo;
        }
        else
        {
            if (!imagePounding.enabled)
            {
                imagePounding.enabled = true;
            }
            GetComponent<Slider>().value =
               GameProcess.s_BTime_Remain / GameProcess.s_BTime_Max;
        }
    }
}
