using UnityEngine;
using UnityEngine.UI;

namespace UnityStandardAssets.Vehicles.Car
{
    public class RearLightManager : MonoBehaviour
    {

        public Text TimeOfDay = null;
        public Light rearLeft = null;
        public Light rearRight = null;
        public Light frontRight = null;
        public Light frontLeft = null;
        public CarController car; // reference to the car controller, must be dragged in inspector
        public int HourToSwitchOnLights = 17;
        public int HourToSwitchOffLights = 8;

        private Renderer m_Renderer;
        private bool lightsOff;

        // Use this for initialization
        void Start()
        {
            int hour = 0;
            int.TryParse(TimeOfDay.text.Split(":"[0])[0], out hour);
            m_Renderer = GetComponent<Renderer>();
            lightsOff = hour > HourToSwitchOffLights && hour < HourToSwitchOnLights;

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.L)) {
                lightsOff = !lightsOff;
            }
            m_Renderer.enabled = !lightsOff;
            rearLeft.enabled = m_Renderer.enabled || car.BrakeInput > 0f;
            rearRight.enabled = rearLeft.enabled;
            frontLeft.enabled = !lightsOff;
            frontRight.enabled = !lightsOff;
        }
    }
}