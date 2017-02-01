using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpotLightManager : MonoBehaviour {

    public Light light = null;
    public Text TimeOfDay = null;
    public int HourToSwitchOnLights = 17;
    public int HourToSwitchOffLights = 8;



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        {
            int hour = 0;
            int.TryParse(TimeOfDay.text.Split(":"[0])[0], out hour) ;
            if (hour > HourToSwitchOffLights && hour < HourToSwitchOnLights)
            {
                if (light != null) light.enabled = false;
            } else
            {
                if (light != null) light.enabled = true;
            }
        }
	}
}
