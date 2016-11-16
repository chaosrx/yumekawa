using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;

public class AdmobManager : MonoBehaviour {
	//BannerView bannerView;
	bool is_close_interstitial = false;
	private InterstitialAd _interstitial;
	private BannerView bannerView;



	void Start() {
		bool isBought = PlayerPrefs.HasKey("Bought");
		if(!isBought) {
			RequestBanner();
			RequestInterstitial();
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

	public void RequestInterstitial() {
		#if UNITY_ANDROID
		string adUnitId = "ca-app-pub-6479246200198022/4046170395";
		#elif UNITY_IPHONE
		string adUnitId = "ca-app-pub-6479246200198022/4046170395";
		#else
		string adUnitId = "unexpected_platform";
		#endif

		if (is_close_interstitial == true) {
			_interstitial.Destroy ();
		}

		// Initialize an InterstitialAd.
		_interstitial = new InterstitialAd (adUnitId);
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder ().AddTestDevice("").Build ();
		// Load the interstitial with the request.
		_interstitial.LoadAd (request);

		is_close_interstitial = false;
	}

	void HandleAdClosed (object sender, System.EventArgs e) {
		is_close_interstitial = true;
	}

	private void LoadBigAd() {
		if(is_close_interstitial) {
			int i = Random.Range(0,4);
			if(i==0) {
				_interstitial.Show();
			}
		}
	}




	public void OnDestroy() {
    bannerView.Destroy();
	}
}
