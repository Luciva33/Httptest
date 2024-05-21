using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class MusicController : MonoBehaviour
{
    //アタッチされたAudioSourceをキャッシュするための変数
    AudioSource myAudio;

    //スライダーの値をもとにボリューム調整
    public Slider volSlider;
    //選択項目をもとに読み込むファイルを決定
    public TMP_Dropdown musicSelect;
    //番号とファイルUrlのDicitonary
    Dictionary<int, string> dict = new Dictionary<int, string>();

    void Start()
    {
        dict.Add(0, "https://joytas.net/php/futta-fly3t.wav");
        dict.Add(1, "https://joytas.net/php/futta-rainbow3t.wav");
        dict.Add(2, "https://joytas.net/php/futta-snowman3t.wav");

        myAudio = GetComponent<AudioSource>();
        myAudio.volume = volSlider.value;
        //1曲目をセット
        StartCoroutine(GetAudioClip(dict[0]));

    }
    IEnumerator GetAudioClip(string musicUrl)
    {
        //音楽ファイルURLを元にuwrインスタンスを生成(今回ファイルタイプはwav)
        using (var uwr = UnityWebRequestMultimedia.GetAudioClip(musicUrl, AudioType.WAV))
        {
            yield return uwr.SendWebRequest();
            // if (uwr.isNetworkError || uwr.isHttpError)
            if (uwr.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(uwr.error);
                yield break;
            }
            //無事音楽ファイルが取得出来たらオーディオソースにセット
            myAudio.clip = DownloadHandlerAudioClip.GetContent(uwr);
        }
    }

    public void playBt()
    {
        myAudio.Play();
    }
    public void pauseBt()
    {
        myAudio.Pause();
    }
    public void stopBt()
    {
        myAudio.volume = volSlider.value;
    }

    public void onVolChange()
    {
        myAudio.volume = volSlider.value;
    }


    public void ChangeMusic()
    {
        //ドロップダウンの情報を引数にしてコルーチン起動
        StartCoroutine(GetAudioClip(dict[musicSelect.value]));
    }

}
