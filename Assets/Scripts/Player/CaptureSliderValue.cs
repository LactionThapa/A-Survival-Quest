using UnityEngine;
using UnityEngine.UI;

public class CaptureSliderValue : MonoBehaviour
{
    // Reference to the Slider in the Unity Editor
    public Slider slider;
    public Toggle toggle;
    public Toggle instantretry;

    void Update()
    {
        // Check if the slider reference is not null
        if (slider != null)
        {
            // Update the static variable with the Slider's value
            SliderManager.sliderValue = slider.value;
        }
        if (toggle != null)
        {
            SliderManager.invertCamera = toggle.isOn;
        }
        if (instantretry != null)
        {
            SliderManager.instantRetry = instantretry.isOn;
        }
    }
}
