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


        /// <summary>
        /// 開始処理
        /// </summary>
        void Start() {
            ShareLevel();
            // Post画像は端末から読み込むので、ない場合はあらかじめ保存しておくこと
						string imagePath = Application.persistentDataPath + "/" + share_images + ExtensionImage;
            if (!File.Exists(imagePath))
            {
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
					string message   = "message";
					string url       = "http://yedo-factory.co.jp/";
					string imagePath = Application.persistentDataPath + "/image" + ExtensionImage;
					SocialWorker.PostTwitter(message, url, imagePath, OnResult);
//				SocialWorker.PostTwitter(message, "", OnResult);
//				SocialWorker.PostTwitter("", imagePath, OnResult);
        }
        /// <summary>
        /// アプリ選択式での投稿
        /// </summary>
        public void OnCreateChooser()  {
					string message   = "message";
					string imagePath = Application.persistentDataPath + "/image" + ExtensionImage;
					SocialWorker.CreateChooser(message, imagePath, OnResult);
					//			SocialWorker.CreateChooser(message, "", OnResult);
					//			SocialWorker.CreateChooser("", imagePath, OnResult);
        }


				public void ReviewPlease() {
					#if UNITY_IOS
						Application.OpenURL("itms-apps://itunes.apple.com/WebObjects/MZStore.woa/wa/viewContentsUserReviews?id=APP_ID&onlyLatestVersion=true&pageNumber=0&sortOrdering=1&type=Purple+Software");
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

        private string ShareLevel() {
          if(PB_GameController.instance._highScore > 40) {
            share_images = "4";
          }
          if(PB_GameController.instance._highScore > 20) {
            share_images = "3";
          }
          if(PB_GameController.instance._highScore > 2) {
            share_images = "2";
          }
          else {
            share_images = "1";
          }
          return share_images;
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
