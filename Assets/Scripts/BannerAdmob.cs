using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
public class BannerAdmob : MonoBehaviour
{
    private BannerView bannerView;
    string adUnitID;
    private void Start()
    {
        MobileAds.Initialize(initStatus => { });
        RequestBanner();
    }

    private void RequestBanner()
    {
#if UNITY_ANDROID
        // ¸ÞÀÎ ±¤°í ID
        adUnitID = "ca-app-pub-9242026964676189/3323167957";
        //Å×½ºÆ® ±¤°í ID
        //adUnitID = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IOS
            adUnitId = "ca-app-pub-9242026964676189/3323167957";
#else
        string adUnitID = "unexpected_platform";
#endif
        if (this.bannerView != null)
        {
            this.bannerView.Destroy();
        }

        AdSize adaptiveSize =
            AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        

        this.bannerView = new BannerView(adUnitID, adaptiveSize, AdPosition.Bottom);

        AdRequest request = new AdRequest();

        this.bannerView.LoadAd(request);
    }
}
