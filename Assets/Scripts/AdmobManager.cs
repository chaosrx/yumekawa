using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class AdmobManager : MonoBehaviour {
	private InterstitialAd interstitial;
	private BannerView bannerView;
	public static AdmobManager instance;
	public bool isCloseBigAD = false;


	public void OnApplicationQuit() {			// Ensure that the instance is destroyed when the game is stopped in the editor.
	    instance = null;
	}

	public void Awake() {
		if (instance != null){
				Destroy (gameObject);
		}
		else {
				instance = this;
				DontDestroyOnLoad (gameObject);
		}
	}


	void Start() {
		bool isBought = PlayerPrefs.HasKey("Bought");
		if(!isBought) {
			RequestBanner();
			RequestInterstitial();
			/*
			interstitial.AdClosed += delegate(object sender, EventArgs args) {
				interstitial.Destroy();
				RequestInterstitial();
			};
			*/
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
    AdRequest request = new AdRequest.Builder().Build();
    bannerView.LoadAd(request);
	}

	private void RequestInterstitial() {
	    #if UNITY_ANDROID
					string adUnitId = "ca-app-pub-6479246200198022/4046170395";
	    #elif UNITY_IPHONE
					string adUnitId = "ca-app-pub-6479246200198022/4046170395";
	    #else
	        string adUnitId = "unexpected_platform";
	    #endif

			if (isCloseBigAD == true) {
				interstitial.Destroy ();
			}


	    // Initialize an InterstitialAd.
	    interstitial = new InterstitialAd(adUnitId);
	    // Create an empty ad request.
	    AdRequest request = new AdRequest.Builder().Build();
	    // Load the interstitial with the request.
	    interstitial.LoadAd(request);
			isCloseBigAD = false;
	}

	public void GameOver() {
	  if (interstitial.IsLoaded()) {
	    interstitial.Show();
	  }
	}

	void HandleAdClosed (object sender, System.EventArgs e) {
		isCloseBigAD = true;
	}





	public void OnDestroy() {
    bannerView.Destroy();
		interstitial.Destroy();
//		isCloseBigAD = true;
	}
}
