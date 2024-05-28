using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Flicking : MonoBehaviour
{
    [SerializeField]
    private float rate;
    [SerializeField]
    private float flicking_level;
    // Start is called before the first frame update
    void Start()
    {
        Color target = GetComponent<Image>().color;
        float H, S, V;
        Color.RGBToHSV(target,out H, out S, out V);
        GetComponent<Image>().DOFade(flicking_level, rate).SetLoops(-1, LoopType.Yoyo);
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
