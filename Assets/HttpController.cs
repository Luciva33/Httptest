using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

//インターネット上のテキストを出力する

public class HttpController : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(GetRequest("https://joytas.net/php/hello.php"));

    }
    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest uwr = UnityWebRequest.Get(uri))
        {
            yield return uwr.SendWebRequest();
            if (uwr.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                Debug.Log(uwr.downloadHandler.text);
            }
        }
    }
}