using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPSliderCtrl : MonoBehaviour
{
    

    // Update is called once per frame
    void FixedUpdate()
    {
        this.GetComponent<Slider>().value =
            PlayerHPCtrl.s_currentHP / PlayerHPCtrl.s_MaxHP;
    }
}
