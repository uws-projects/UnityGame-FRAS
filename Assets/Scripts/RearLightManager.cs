using UnityEngine;
using UnityEngine.UI;

namespace UnityStandardAssets.Vehicles.Car
{
    public class RearLightManager : MonoBehaviour
    {

        public Text TimeOfDay = null;
        public Light left = null;
        public Light right = null;
        public CarController car; // reference to the car controller, must be dragged in inspector
        public int HourToSwitchOnLights = 17;
        public int HourToSwitchOffLights = 8;

        private Renderer m_Renderer;

        // Use this for initialization
        void Start()
        {
            m_Renderer = GetComponent<Renderer>();
        }

        // Update is called once per frame
        void Update()
        {
            int hour = 0;
            int.TryParse(TimeOfDay.text.Split(":"[0])[0], out hour);
            m_Renderer.enabled = !(hour > HourToSwitchOffLights && hour < HourToSwitchOnLights);
            left.enabled = m_Renderer.enabled || car.BrakeInput > 0f;
            right.enabled = left.enabled;
        }
    }
}