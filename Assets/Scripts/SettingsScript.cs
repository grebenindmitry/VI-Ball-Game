using TMPro;
using UnityEngine;

public class SettingsScript : MonoBehaviour
{
    public void SaveSettings()
    {
        //get the player height and save it to PlayerPrefs
        var heightText = GetComponent<TMP_Text>();

        PlayerPrefs.SetFloat("playerHeight", float.Parse(heightText.text));
    }
}
