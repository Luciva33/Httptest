using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

//インターネット上の画像を持ってこれる
//画像、動画は？
//サーバーに置いてある画像を持ってくるやり方

public class ImageController : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(GetRequest("https://as1.ftcdn.net/v2/jpg/02/36/99/22/1000_F_236992283_sNOxCVQeFLd5pdqaKGh8DRGMZy7P4XKm.jpg"));
        // StartCoroutine(GetRequest("https://www.shutterstock.com/ja/blog/wp-content/uploads/sites/6/2017/08/596036951.gif"));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(uri))
        {
            yield return uwr.SendWebRequest();
            if (uwr.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                Texture texture = DownloadHandlerTexture.GetContent(uwr);
                Sprite sp = Sprite.Create(
                    (Texture2D)texture,
                    new Rect(0, 0, texture.width, texture.height),
                    new Vector2(0.5f, 0.5f));
                Image image = GetComponent<Image>();

                //ネットで拾ってきた画像と同じ大きさにセット
                image.rectTransform.sizeDelta = new Vector2(
                // sp.rect.width, sp.rect.height
                //大きさ指定
                100f, 100f
                );

                image.sprite = sp;

            }
        }
    }
}
