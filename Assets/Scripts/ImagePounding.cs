using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ImagePounding : MonoBehaviour
{
    [SerializeField]
    private float rate;
    [SerializeField]
    private float pounding_level;
    private Tween tween;
    // Start is called before the first frame update
    private void Start()
    {
       //Pounding();
    }
    private void OnEnable()
    {
        Pounding();
    }
    private void Pounding()
    {
       tween= GetComponent<RectTransform>().
          DOScale(new Vector3(pounding_level, pounding_level, 0), rate).
          SetLoops(-1, LoopType.Yoyo);
    }
     public void StopTweening() //OnEnable에서 발생하는 문제 해결위한 안전장치
    {
        GetComponent<RectTransform>().
         DOScale(new Vector3(1.0f, 1.0f, 0), 0);
        tween.Kill();
    }
} 
