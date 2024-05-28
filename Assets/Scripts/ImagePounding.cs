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
    // Start is called before the first frame update
    void Start()
    {

        GetComponent<RectTransform>().
            DOScale(new Vector3(pounding_level,pounding_level,0),rate).
            SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
