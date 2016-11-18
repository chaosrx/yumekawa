using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using System;

public class AdmobManager : MonoBehaviour {
	private InterstitialAd interstitial;
	private BannerView bannerView;
	public static AdmobManager instance;
	private bool isCloseBigAD = false;
	bool isBought;


	public void OnApplicationQuit() {			// Ensure that the instance is destroyed when the game is stopped in the editor.
	    instance = null;
	}

	public void Awake() {
		isBought = PlayerPrefs.HasKey("Bought");

		if (isBought) {
			this.gameObject.SetActive(false);

		}
		if (instance != null){
				Destroy (gameObject);
		}
		else {
				instance = this;
				DontDestroyOnLoad (gameObject);
		}
	}


	void Start() {
		if(!isBought) {
			RequestBanner();
			RequestInterstitial();

		}
	}

	private void RequestBanner() {

		#if UNITY_ANDROID
			string adUnitId = "ca-app-pub-6479246200198022/4535364790";
			bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
    #elif UNITY_IOS
      string adUnitId = "ca-app-pub-6479246200198022/1367709194";
			bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);

    #else
      string adUnitId = "unexpected_platform";
    #endif
    AdRequest request = new AdRequest.Builder().Build();
    bannerView.LoadAd(request);
	}

	private void RequestInterstitial() {
	    #if UNITY_ANDROID
					string adUnitId = "ca-app-pub-6479246200198022/6012097999";
	    #elif UNITY_IPHONE
					string adUnitId = "ca-app-pub-6479246200198022/4046170395";
	    #else
	        string adUnitId = "unexpected_platform";
	    #endif

			if (isCloseBigAD == true) {
				//interstitial.Destroy ();
			}


	    // Initialize an InterstitialAd.
	    interstitial = new InterstitialAd(adUnitId);
	    // Create an empty ad request.
	    AdRequest request = new AdRequest.Builder().Build();
	    // Load the interstitial with the request.
	    interstitial.LoadAd(request);
			interstitial.OnAdClosed += HandleAdClosed;

			isCloseBigAD = false;
	}

	public void GameOver() {
	  if (interstitial.IsLoaded()) {
	    interstitial.Show();
	  }
	}

	void HandleAdClosed (object sender, System.EventArgs e) {
		interstitial.Destroy();
		RequestInterstitial();
	}


	public void OnDestroy() {
    bannerView.Destroy();
		interstitial.Destroy();
//		isCloseBigAD = true;
	}
}
