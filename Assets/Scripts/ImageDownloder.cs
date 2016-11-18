/*
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;


public class ImageDownloder : MonoBehaviour {
  string url;
  public static bool isDownLoaded = false;

  public IEnumerator Start(){
      // wwwクラスのコンストラクタに画像URLを指定
      WWW www = new WWW(UrlSource(PlayerPrefs.GetInt("HighScore")));

      // 画像ダウンロード完了を待機
      yield return www;

      // webサーバから取得した画像をRaw Imagで表示する
      RawImage rawImage = GetComponent<RawImage>();
      rawImage.texture = www.textureNonReadable;

      //ピクセルサイズ等倍に
      rawImage.SetNativeSize();
      isDownLoaded = true;
  }

  private string UrlSource(int i) {

    if(i >= 20 ){
      url = "http://67.media.tumblr.com/3496554f5b4c21d3cc17ec01af2a7c48/tumblr_nwmo6uzymH1ujhx7ko1_1280.png";
    }else if(i >= 6 ){
      url = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/47/PNG_transparency_demonstration_1.png/400px-PNG_transparency_demonstration_1.png";
    }else {
      url = "http://www.line-tatsujin.com/stamp/outline/a27998-0.png";
    }

    return url;
  }




}
*/
