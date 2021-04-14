using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextToSpeechScript : MonoBehaviour
{
    private AndroidJavaObject _unityActivity;
    private AndroidJavaObject _unityTextToSpeech;
    private void Start()
    {

        _unityActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        _unityTextToSpeech = new AndroidJavaObject("io.github.grebenindmitry.texttospeech.UnityTextToSpeech", _unityActivity);
    }

    private void ShowToast(string message)
    {
        var toastClass = new AndroidJavaClass("android.widget.Toast");
        _unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
        {
            var toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", _unityActivity, message, 0);
            toastObject.Call("show");
        }));
    }

    private void NativeTts(string message)
    {
        _unityTextToSpeech.Call("Speak", message);
    }

    public void SpeakText(string text)
    {
        NativeTts(text);
    }

    public void SpeakText(TextMeshProUGUI textBox)
    {
        NativeTts(textBox.text);
    }
}
