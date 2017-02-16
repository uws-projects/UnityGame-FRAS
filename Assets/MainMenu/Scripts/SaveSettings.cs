using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSettings : MonoBehaviour {
    /*
     * Starting values: 
     * TimeOfDay:
     * 0 - morning
     * 1 - noon (default)
     * 2 - evening
     * 3 - night
     * GameMode:
     * 0 - car (default)
     * 1 - fps 
     */

    private void Start()
    {
        PlayerPrefs.SetInt("TimeOfDay", 1);
        PlayerPrefs.SetInt("GameMode", 0);
        PlayerPrefs.Save();
    }

    public void updateTimeOfDay(float value)
    {
        PlayerPrefs.SetInt("TimeOfDay", (int) value);
        PlayerPrefs.Save();
    }

    public void updateGameMode(float value)
    {
        PlayerPrefs.SetInt("GameMode", (int) value);
        PlayerPrefs.Save();
    }
}
