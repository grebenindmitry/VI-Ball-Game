using TMPro;
using UnityEngine;

public class SettingsScript : MonoBehaviour
{
    public void SaveSettings()
    {
        var heightText = GetComponent<TMP_Text>();

        PlayerPrefs.SetFloat("playerHeight", float.Parse(heightText.text));
    }
}
