package io.github.grebenindmitry.texttospeech;

import android.content.Context;
import android.speech.tts.TextToSpeech;
import java.util.Locale;

public class UnityTextToSpeech {
    private TextToSpeech textToSpeech;

    public UnityTextToSpeech(Context context) {
        textToSpeech = new TextToSpeech(context, status -> textToSpeech.setLanguage(Locale.getDefault()));
    }

    public void Speak(String text) {
        textToSpeech.speak(text, TextToSpeech.QUEUE_FLUSH, null);
    }
}
