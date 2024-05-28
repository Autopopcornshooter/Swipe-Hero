using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;

static class FullScreenAdmob 
{
    static string adUnitId;
    public static string nextScene="";
   static InterstitialAd interstitialAd;
    public static bool isInterstitialAd_ON = false;

    public static bool RequestInterstitial()
    {
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            Debug.LogWarning("MobileADD Initialized");
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
        return true;
    }

    static void LoadInterstitialAd() //광고 로드
    {
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        var adRequest = new AdRequest();
        

        InterstitialAd.Load(adUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogWarning("MobileADD Load Fail");
                    return;
                }
                Debug.Log("Interstitial ad loaded with response : "
                        + ad.GetResponseInfo());
                interstitialAd = ad;
            });
        RegisterEventHandlers(interstitialAd); //이벤트 등록
    }

    public static void ShowAd() //광고 보기
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

    static void RegisterEventHandlers(InterstitialAd ad) //광고 이벤트
    {
        ad.OnAdPaid += (AdValue adValue) =>
        {

        };
        ad.OnAdImpressionRecorded += () =>
        {
          
        };
        ad.OnAdClicked += () =>
        {
            
        };
        ad.OnAdFullScreenContentOpened += () =>
        {
            isInterstitialAd_ON = true;
        };
        ad.OnAdFullScreenContentClosed += () =>
        {
            isInterstitialAd_ON = false;
            SceneManager.LoadScene(nextScene);
        };
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            LoadInterstitialAd(); //광고 재로드
        };
    }

    static void RegisterReloadHandler(InterstitialAd ad) //수동으로 광고 재로드(선언 필요)
    {
        ad.OnAdFullScreenContentClosed += (null);
        {
            SceneManager.LoadScene(nextScene);
            LoadInterstitialAd();
        };
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {

            LoadInterstitialAd();
        };
    }
}
