using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class LightEstimation : MonoBehaviour
{
    [SerializeField]
    private ARCameraManager arCameraManager;

    public Text BrightnessValue;
    public Text TempValue;
    public Text ColorCorrValue;

    private Light mLight;

    private void Awake()
    {
        mLight = GetComponent<Light>();
    }

    private void OnEnable()
    {
        arCameraManager.frameReceived += FrameUpdated;
    }

    private void OnDisable()
    {
        arCameraManager.frameReceived -= FrameUpdated;
    }

    private void FrameUpdated(ARCameraFrameEventArgs args)
    {
        if (args.lightEstimation.averageBrightness.HasValue)
        {
            BrightnessValue.text = "Brightness: " + args.lightEstimation.averageBrightness.Value;
            mLight.intensity = args.lightEstimation.averageBrightness.Value + 0.3f;
        }

        if (args.lightEstimation.averageColorTemperature.HasValue)
        {
            TempValue.text = "Temperature: " + args.lightEstimation.averageColorTemperature.Value;
            mLight.colorTemperature = args.lightEstimation.averageColorTemperature.Value;
        }

        if (args.lightEstimation.colorCorrection.HasValue)
        {
            ColorCorrValue.text = "Color Correction: " + args.lightEstimation.colorCorrection.Value;
            mLight.color = args.lightEstimation.colorCorrection.Value;
        }
    }
}
