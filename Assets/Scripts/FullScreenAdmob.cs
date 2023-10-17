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
            //�ʱ�ȭ �Ϸ�
        });

#if UNITY_ANDROID
        //���� ����
        adUnitId = "ca-app-pub-9242026964676189/1671460346";
        //�׽�Ʈ ����
        //adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IOS
            adUnitId = "ca-app-pub-9242026964676189/1671460346";
#else
            adUnitId = "unexpected_platform";
#endif

        LoadInterstitialAd();
    }

    public void LoadInterstitialAd() //���� �ε�
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
        RegisterEventHandlers(interstitialAd); //�̺�Ʈ ���
    }

    public void ShowAd() //���� ����
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
        
            interstitialAd.Show();
        }
        else
        {
            LoadInterstitialAd(); //���� ��ε�
        }
    }

    private void RegisterEventHandlers(InterstitialAd ad) //���� �̺�Ʈ
    {
        ad.OnAdPaid += (AdValue adValue) =>
        {

            //���� �ֱ�

           
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

    private void RegisterReloadHandler(InterstitialAd ad) //�������� ���� ��ε�(���� �ʿ�)
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
