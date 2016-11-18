//----------------------------------------------
// SocialWorker
// © 2015 yedo-factory
//----------------------------------------------
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

namespace SWorker {
    /// <summary>
    /// デモシーン
    /// </summary>
	public class Share : MonoBehaviour	{
		private static readonly string ExtensionImage = ".png";
//		private static readonly string ExtensionImage = ".jpeg";

		public Text Result;
		public RawImage Image;
    public GameObject PleaseReview;
    public GameObject BuyBeer;
    public string share_images;
    private int highScore;
    private string _shared;
    public  bool isShared = false;




        /// <summary>
        /// 開始処理
        /// </summary>
        void Start() {
          isShared = PlayerPrefs.HasKey("Shared");
          highScore = PlayerPrefs.GetInt("HighScore");
          // Post画像は端末から読み込むので、ない場合はあらかじめ保存しておくこと
          string imagePath = Application.persistentDataPath + "/" + "ShareGazo" + ExtensionImage;
          if (!File.Exists(imagePath)) {
                Texture2D texture = (Texture2D)Image.texture;
                    byte[] data = (ExtensionImage == ".png") ? texture.EncodeToPNG () :   texture.EncodeToJPG ();
                    File.WriteAllBytes(imagePath, data);
          }

          PleaseReview.SetActive(false);
          BuyBeer.SetActive(false);


        }
        /// <summary>
        /// Twitter投稿
        /// </summary>
        public void OnPostTwitter() {
        //  SetPath();
          string message   =  highScore + "点でした！";
					string url       = "https://itunes.apple.com/jp/app/bounce-jimuzu-dotto-huigemu/id1173871260?l=ja&ls=1&mt=8";
					string imagePath = Application.persistentDataPath + "/ShareGazo" + ExtensionImage;
					SocialWorker.PostTwitter(message, url, imagePath, OnResult);
          if(!isShared) {
            PlayerPrefs.SetString("Shared", _shared);
          }
//				SocialWorker.PostTwitter(message, "", OnResult);
//				SocialWorker.PostTwitter("", imagePath, OnResult);
        }
        /// <summary>
        /// アプリ選択式での投稿
        /// </summary>
        public void OnCreateChooser()  {
    //      SetPath();
					string message   = highScore + "点でした！";
					string imagePath = Application.persistentDataPath + "/ShareGazo" + ExtensionImage;
					SocialWorker.CreateChooser(message, imagePath, OnResult);
          if(!isShared) {
            PlayerPrefs.SetString("Shared", _shared);
          }
					//			SocialWorker.CreateChooser(message, "", OnResult);
					//			SocialWorker.CreateChooser("", imagePath, OnResult);
        }


				public void ReviewPlease() {
					#if UNITY_IOS
						Application.OpenURL("itms-apps://itunes.apple.com/us/app/bounce-jimuzu-dotto-huigemu/id1173871260?l=ja&ls=1&mt=8");
					#endif

					#if UNITY_ANDROID
						Application.OpenURL("https://play.google.com/store/apps/details?id=com.zavukodlak.pixbit");
					#endif
				}

        public void ActivateReview() {
          PleaseReview.SetActive(true);
        }

        public void ActivateBuyBeer() {
          BuyBeer.SetActive(true);
        }

        public void InActivateReview() {
          PleaseReview.SetActive(false);
        }

        public void InActivateBuyBeer() {
          BuyBeer.SetActive(false);
        }
        /// <summary>
        /// 結果コールバック
        /// </summary>
        /// <param name="res">結果値</param>
        public void OnResult(SocialWorkerResult res) {
            switch(res) {
							case SocialWorkerResult.Success:
								Result.text = "Result : Success";
                    break;
              case SocialWorkerResult.NotAvailable:
								Result.text = "Result : NotAvailable";
                    break;
              case SocialWorkerResult.Error:
								Result.text = "Result : Error";
                    break;
            }
        }

	}
}
