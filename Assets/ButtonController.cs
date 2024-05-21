using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.Networking;

public class ButtonController : MonoBehaviour
{
    public TMP_InputField tmInputX;
    public TMP_InputField tmInputY;
    public TextMeshProUGUI result;
    public void BtClick()
    {
        string x = tmInputX.text;
        string y = tmInputY.text;
        StartCoroutine(Calc(x, y));
    }
    IEnumerator Calc(string x, string y)
    {
        string uri = "https://joytas.net/php/calc.php";
        WWWForm form = new WWWForm();
        form.AddField("x", x);
        form.AddField("y", y);
        using (UnityWebRequest uwr = UnityWebRequest.Post(uri, form))
        {
            yield return uwr.SendWebRequest();
            if (uwr.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                result.text = uwr.downloadHandler.text;
            }

        }

    }
}
