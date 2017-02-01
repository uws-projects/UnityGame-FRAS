using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RearLightManager : MonoBehaviour {

    public Text TimeOfDay = null;
    public int HourToSwitchOnLights = 17;
    public int HourToSwitchOffLights = 8;

    private Renderer m_Renderer;

    // Use this for initialization
    void Start () {
        m_Renderer = GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {

        int hour = 0;
        int.TryParse(TimeOfDay.text.Split(":"[0])[0], out hour);
        m_Renderer.enabled = !(hour > HourToSwitchOffLights && hour < HourToSwitchOnLights);

    }
}