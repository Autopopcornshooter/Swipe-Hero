using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;

public class FullScreenAdmob : MonoBehaviour
{
    string adUnitId;
    [HideInInspector]
    public string nextScene="";
    private InterstitialAd interstitialAd;

    public void Start()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            //초기화 완료
        });

#if UNITY_ANDROID
        //메인 광고
        adUnitId = "ca-app-pub-9242026964676189/1671460346";
        //테스트 광고
        //adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IOS
            adUnitId = "ca-app-pub-9242026964676189/1671460346";
#else
            adUnitId = "unexpected_platform";
#endif

        LoadInterstitialAd();
    }

    public void LoadInterstitialAd() //광고 로드
    {
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        var adRequest = new AdRequest.Builder()
                .AddKeyword("unity-admob-sample")
                .Build();

        InterstitialAd.Load(adUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                 
                    return;
                }

                interstitialAd = ad;
            });
        RegisterEventHandlers(interstitialAd); //이벤트 등록
    }

    public void ShowAd() //광고 보기
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
        
            interstitialAd.Show();
        }
        else
        {
            LoadInterstitialAd(); //광고 재로드
        }
    }

    private void RegisterEventHandlers(InterstitialAd ad) //광고 이벤트
    {
        ad.OnAdPaid += (AdValue adValue) =>
        {

            //보상 주기

           
        };
        ad.OnAdImpressionRecorded += () =>
        {
          
        };
        ad.OnAdClicked += () =>
        {
            
        };
        ad.OnAdFullScreenContentOpened += () =>
        {
           
        };
        ad.OnAdFullScreenContentClosed += () =>
        {
            
        };
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {

        };
    }

    private void RegisterReloadHandler(InterstitialAd ad) //수동으로 광고 재로드(선언 필요)
    {
        ad.OnAdFullScreenContentClosed += (null);
        {

            LoadInterstitialAd();
        };
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {

            LoadInterstitialAd();
        };
    }
}
