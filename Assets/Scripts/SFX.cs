using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFX : MonoBehaviour
{
    [Header("Canvas Position")]
    public RectTransform canvas_Upper;
    public RectTransform canvas_Lower;
    public RectTransform canvas_Left;
    public RectTransform canvas_Right;
    [Header("Value")]
    [SerializeField]
    private float lerp_time = 0.5f;
    [Header("Reference")]
    [SerializeField]
    private GameObject AttackEffect;
    [SerializeField]
    private Transform AttackPos_Up;
    [SerializeField]
    private Transform AttackPos_Down;
    [SerializeField]
    private Transform AttackPos_Left;
    [SerializeField]
    private Transform AttackPos_Right;
    [SerializeField]
    private List<GameObject> coloredObjects;
    [Header("Camera Shake")]
    [SerializeField]
    private float m_duration;
    [HideInInspector]
    public float m_strength;
    [SerializeField]
    private int m_vivrato;
    [SerializeField]
    private float m_randomness;
    [Header("Swiping Object")]
    [SerializeField]
    private GameObject pos_Zero;
    [SerializeField]
    private GameObject effect_window;
    public float lerpTime;



    private bool isChanging = false;
    public void ChangeColor_BG()
    {
        if (GameInfo.gamedata.Color_Select <= 0)
        {
            if (!isChanging)
            {
                foreach (GameObject obj in coloredObjects)
                {
                    StartCoroutine(ChangeCover(Utils.RandomColor(), obj));
                }
            }
        }
        else
        {
            foreach (GameObject obj in coloredObjects)
            {
                StartCoroutine(ChangeCover(Utils.SetHSVTORGB(GameInfo.gamedata.Color_Select), obj));
            }
        }
    }
    IEnumerator ChangeCover(Color nextColor, GameObject target)
    {
        float progress = 0.0f;
        if (target.GetComponent<SpriteRenderer>() != null)
        {
            Color prevColor = target.GetComponent<SpriteRenderer>().color;
            isChanging = true;
            while (progress < 1)
            {
                Color lerpedColor = Color.Lerp(prevColor, nextColor, progress);
                target.GetComponent<SpriteRenderer>().color = lerpedColor;
                progress += 0.05f;
                yield return new WaitForSecondsRealtime(0.01f);
            }
            isChanging = false;
        }
        else if(target.GetComponent<Camera>() != null)
        {
            Color prevColor = target.GetComponent<Camera>().backgroundColor;
            isChanging = true;
            while (progress < 1)
            {
                Color lerpedColor = Color.Lerp(prevColor, nextColor, progress);
                target.GetComponent<Camera>().backgroundColor = lerpedColor;
                progress += 0.05f;
                yield return new WaitForSecondsRealtime(0.01f);
            }
            isChanging = false;
        }
        else if (target.GetComponent<Image>() != null)
        {
            Color prevColor = target.GetComponent<Image>().color;
            isChanging = true;
            while (progress < 1)
            {
                Color lerpedColor = Color.Lerp(prevColor, nextColor, progress);
                target.GetComponent<Image>().color = lerpedColor;
                progress += 0.05f;
                yield return new WaitForSecondsRealtime(0.01f);
            }
            isChanging = false;
        }


    }
    public IEnumerator InstantiateSFX(Vector2 vector)
    {
        
        GameObject sfx = null;
        if (vector == Vector2.up)
        {
            sfx = Instantiate(AttackEffect, AttackPos_Up);
        }
        else if (vector == Vector2.down)
        {
            sfx = Instantiate(AttackEffect, AttackPos_Down);
        }
        else if (vector == Vector2.right)
        {
            sfx = Instantiate(AttackEffect, AttackPos_Right);
        }
        else if (vector == Vector2.left)
        {
            sfx = Instantiate(AttackEffect, AttackPos_Left);
        }

        yield return null;
    }
    public void SwipeEffect(Vector2 vector)
    {
        
        GameObject temp_window = Instantiate(effect_window, pos_Zero.transform);
        float sprite_X = temp_window.GetComponent<SpriteRenderer>().bounds.size.x;
        float sprite_Y = temp_window.GetComponent<SpriteRenderer>().bounds.size.y;
        temp_window.GetComponent<Transform>().localScale =
            new Vector2(Mathf.Ceil(GetCameraWorldScale().x / sprite_X), Mathf.Ceil(GetCameraWorldScale().y / sprite_Y));
        temp_window.GetComponent<SpriteRenderer>().color = Camera.main.backgroundColor;
        temp_window.GetComponent<Transform>().position=Vector2.zero;
        if (vector == Vector2.up)
        {
            Vector3 targetVec = Camera.main.ScreenToWorldPoint(canvas_Upper.position);
            temp_window.transform.DOMoveY(targetVec.y * 2.5f, lerpTime);
        }
        else if (vector == Vector2.down)
        {
            Vector3 targetVec = Camera.main.ScreenToWorldPoint(canvas_Lower.position);
            temp_window.transform.DOMoveY(targetVec.y * 2.5f, lerpTime);
        }
        else if (vector == Vector2.right)
        {
            Vector3 targetVec = Camera.main.ScreenToWorldPoint(canvas_Right.position);
            temp_window.transform.DOMoveX(targetVec.x * 2.5f, lerpTime);
        }
        else if (vector == Vector2.left)
        {
            Vector3 targetVec = Camera.main.ScreenToWorldPoint(canvas_Left.position);
            temp_window.transform.DOMoveX(targetVec.x * 2.5f, lerpTime);
        }
        temp_window.GetComponent<SpriteRenderer>().DOColor(Color.white, lerpTime);
        temp_window.GetComponent<SpriteRenderer>().DOFade(0.5f, lerpTime);
        Destroy(temp_window, lerpTime);

    }
    public Vector2 GetCameraWorldScale()
    {
        float screenY = Camera.main.orthographicSize * 2;
        float screenX = screenY / Screen.height * Screen.width;

        return new Vector2(screenX, screenY);
    }
    public void CameraShake()
    {
        m_strength = GameInfo.gamedata.Shake_Value;
        Camera.main.DOShakePosition(m_duration, m_strength, m_vivrato, m_randomness);
    }

}
