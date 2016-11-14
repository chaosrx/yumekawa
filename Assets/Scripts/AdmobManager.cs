using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class AdmobManager : MonoBehaviour {
	BannerView bannerView;

	void Start() {
		bool isBought = PlayerPrefs.HasKey("Bought");
		if(!isBought) {
			RequestBanner();
		}
	}


	private void RequestBanner() {

		#if UNITY_ANDROID
			string adUnitId = "ca-app-pub-6479246200198022/1367709194";
			bannerView = new BannerView(adUnitId, AdSize.SMART_BANNER, AdPosition.Top);
    #elif UNITY_IOS
      string adUnitId = "ca-app-pub-6479246200198022/1367709194";
			bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);

    #else
      string adUnitId = "unexpected_platform";
    #endif

    // Create a 320x50 banner at the top of the screen.
		//BannerView bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
    // Create an empty ad request.
    AdRequest request = new AdRequest.Builder().Build();
	/*	AdRequest request = new AdRequest.Builder()
				.AddTestDevice(AdRequest.TestDeviceSimulator)       // Simulator.
				.AddTestDevice("2077ef9a63d2b398840261c8221a0c9b")  // My test device.
				.Build();
	*/
    // Load the banner with the request.
    bannerView.LoadAd(request);
	}

	public void OnDestroy() {
    bannerView.Destroy();
	}
}
