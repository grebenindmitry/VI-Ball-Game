using TMPro;
using UnityEngine;

#if PLATFORM_ANDROID
public class TextToSpeechScript : MonoBehaviour
{
    private AndroidJavaObject _unityActivity;
    private AndroidJavaObject _unityTextToSpeech;

    private void Awake()
    {
        //initialize the Java objects
        _unityActivity =
            new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        _unityTextToSpeech =
            new AndroidJavaObject("io.github.grebenindmitry.texttospeech.UnityTextToSpeech", _unityActivity);
    }

    //TextToSpeech.Speak() wrapper
    private void NativeTts(string message)
    {
        _unityTextToSpeech.Call("Speak", message);
    }

    //speak a string
    public void SpeakText(string text)
    {
        NativeTts(text);
    }

    //Stop the speech
    public void Stop()
    {
        _unityTextToSpeech.Call("Stop");
    }

    //Stop the speech on application pause
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus) _unityTextToSpeech.Call("Stop");
    }
    
    //Stop the speech on application focus lost
    private void OnApplicationFocus(bool focusStatus)
    {
        if (!focusStatus) _unityTextToSpeech.Call("Stop");
    }
}
#endif