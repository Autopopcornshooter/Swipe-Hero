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
    private Tweener tweener;
    // Start is called before the first frame update

    private void OnEnable()
    {
        tweener = GetComponent<RectTransform>().
           DOScale(new Vector3(pounding_level, pounding_level, 0), rate).
           SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable()
    {
        GetComponent<RectTransform>().
           DOScale(new Vector3(1.0f, 1.0f, 0), 0);
        tweener.Kill();   
    }
}
