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
    private Tween tween;
    // Start is called before the first frame update
    private void Start()
    {
       // Flick();
    }
    private void OnEnable()
    {
        Flick();
    }
    private void Flick()
    {
        Color target = GetComponent<Image>().color;
        float H, S, V;
        Color.RGBToHSV(target, out H, out S, out V);
        tween = GetComponent<Image>().DOFade(flicking_level, rate).SetLoops(-1, LoopType.Yoyo);
    }
    public void StopTweening()//OnEnable에서 발생하는 문제 해결위한 안전장치
    {
        GetComponent<Image>().DOFade(1, 0.0f);
        tween.Kill();
    }
    
}
