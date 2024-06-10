using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RageSliderCtrl : MonoBehaviour
{
    [SerializeField]
    private ImagePounding imagePounding;
    [SerializeField]
    private Flicking flicking;
    [SerializeField]
    private GameObject rage_Effect;
    private void FixedUpdate()
    {
        if (GameProcess.s_BTime_Remain <= 0)
        {
            if (imagePounding.enabled)
            {
                imagePounding.StopTweening();
                imagePounding.enabled = false;
            }
            if (flicking.enabled)
            {
                flicking.StopTweening();
                flicking.enabled = false;
            }
            rage_Effect.SetActive(false);
            GetComponent<Slider>().value =
                (float)GameScoreCheck.currentBonusCombo / (float)GameScoreCheck.s_bonus_targetCombo;

        }
        else
        {
            if (!imagePounding.enabled)
            {
                imagePounding.enabled = true;
            }
            if (!flicking.enabled)
            {
                flicking.enabled = true;
            }

            rage_Effect.SetActive(true);
            GetComponent<Slider>().value =
               (float)GameProcess.s_BTime_Remain / (float)GameProcess.s_BTime_Max;
        }
    }
}
