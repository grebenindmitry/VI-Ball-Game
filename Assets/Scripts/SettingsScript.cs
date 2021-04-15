using TMPro;
using UnityEngine;

public class SettingsScript : MonoBehaviour
{
    public void SaveSettings()
    {
        var heightText = GetComponent<TMP_Text>();
        var unitsDropdown = GetComponent<TMP_Dropdown>();
        
        float height;
        if (unitsDropdown.itemText.text == "CM") 
            height = float.Parse(heightText.text);
        else
            height = float.Parse(heightText.text) / 2.54f;

        PlayerPrefs.SetFloat("playerHeight", height);
    }
}
