using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {

    public InputField IPAddress;

	public void updateIP(string newstring)
    {
        PlayerPrefs.SetString("IP", newstring);
    }

    public void onApply()
    {
        SceneManager.LoadScene("arview");
    }

    public void Awake()
    {
        // Load in the values
        // if keys in player pref are not present, set default values
        IPAddress.text = PlayerPrefs.GetString("IP", "192.168.1.1");
    }
}

