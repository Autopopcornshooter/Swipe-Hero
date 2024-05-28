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

    private Tweener tweener;
    // Start is called before the first frame update
    void Start()
    {
       
        

    }
    private void OnEnable()
    {
        Color target = GetComponent<Image>().color;
        float H, S, V;
        Color.RGBToHSV(target, out H, out S, out V);
        tweener = GetComponent<Image>().DOFade(flicking_level, rate).SetLoops(-1, LoopType.Yoyo);
    }
    private void OnDisable()
    {
        GetComponent<Image>().DOFade(1, 0.0f);
        tweener.Kill();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
