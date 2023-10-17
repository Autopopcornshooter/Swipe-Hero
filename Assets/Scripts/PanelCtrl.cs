using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelCtrl : MonoBehaviour
{

    [Header("Boundary Value")]
    [SerializeField]
    private float vertical_Swipe_boundary = 500.0f;
    [SerializeField]
    private float horizontal_Swipe_boundary = 500.0f;

    private float startTouch_Y;
    private float currentTouch_Y;
    private float startTouch_X;
    private float currentTouch_X;

    virtual protected void SwipeDTU() { }
    virtual protected void SwipeUTD() { }
    virtual protected void SwipeRTL() { }
    virtual protected void SwipeLTR() { }
    protected void ReceiveInput()
    {
        float X_length = 0;
        float Y_length = 0;

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            startTouch_X = Input.mousePosition.x;
            startTouch_Y = Input.mousePosition.y;
        }
        currentTouch_X = Input.mousePosition.x;
        currentTouch_Y = Input.mousePosition.y;

        X_length = Mathf.Abs(currentTouch_X - startTouch_X);
        Y_length = Mathf.Abs(currentTouch_Y - startTouch_Y);



        if (Input.GetMouseButtonUp(0))
        {
            if (X_length >= Y_length)
            {
                HorizontalSwipe();
            }
            else
            {

                VerticalSwipe();
            }
        }

#endif
#if UNITY_IOS
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                startTouch_Y = touch.position.y;
                startTouch_X = touch.position.x;
            }
            currentTouch_Y = touch.position.y;
            currentTouch_X = touch.position.x;

            X_length = Mathf.Abs(currentTouch_X - startTouch_X);
            Y_length = Mathf.Abs(currentTouch_Y - startTouch_Y);


            if (touch.phase == TouchPhase.Ended)
            {
                if (X_length >= Y_length)
                {
                    HorizontalSwipe();
                }
                else
                {

                    VerticalSwipe();
                }
            }
        }
#endif

#if UNITY_ANDROID
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                startTouch_Y = touch.position.y;
                startTouch_X = touch.position.x;
            }
            currentTouch_Y = touch.position.y;
            currentTouch_X = touch.position.x;

            X_length = Mathf.Abs(currentTouch_X - startTouch_X);
            Y_length = Mathf.Abs(currentTouch_Y - startTouch_Y);


            if (touch.phase == TouchPhase.Ended)
            {
                if (X_length >= Y_length)
                {
                    HorizontalSwipe();
                }
                else
                {

                    VerticalSwipe();
                }
            }
        }
#endif
    }
    private void VerticalSwipe()
    {
        if (Mathf.Abs(startTouch_Y - currentTouch_Y) < vertical_Swipe_boundary)   //절대값 비교
        {
            return;
        }

        bool UpToDown = currentTouch_Y < startTouch_Y ? true : false;

        if (UpToDown)
        {
            SwipeUTD();
        }
        else
        {
            SwipeDTU();
        }
    }
    private void HorizontalSwipe()
    {
        if (Mathf.Abs(startTouch_X - currentTouch_X) < horizontal_Swipe_boundary)   //절대값 비교
        {
            return;
        }
        //게임 시작, 게임 종료 결정
        bool RightToLeft = currentTouch_X < startTouch_X ? true : false;
        if (RightToLeft)
        {
            SwipeRTL();
        }
        else
        {
            SwipeLTR();
        }
    }

}
