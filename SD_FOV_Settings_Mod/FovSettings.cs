using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SD_FOV_Settings_Mod
{
    public class FovSettings : MonoBehaviour
    {
        const byte MAX_FOV = 120;
        const byte MIN_FOV = 20;
        private Slider? fovSlider;
        private TMP_Text? fovSliderLabel;
        public byte fov;
        private static Camera[] cameras;

        public void Start()
        {
            fov = FOV_Mod.fovPreference.Value;

            //Get Slider or add new
            Transform? foundSliderTransform = transform.Find("FovSlider");
            if (foundSliderTransform == null)
            {
                GameObject fovSettings = Instantiate(transform.GetComponentInChildren<Slider>().transform.parent.gameObject);
                fovSettings.transform.SetParent(transform);
                fovSettings.name = "FovSlider";
                
                foundSliderTransform = fovSettings.GetComponentInChildren<Slider>().transform;
            }

            //Set Values
            fovSlider = foundSliderTransform.GetComponent<Slider>();
            fovSlider.maxValue = MAX_FOV;
            fovSlider.minValue = MIN_FOV;
            fovSlider.onValueChanged.AddListener(OnSliderValueChanged);
            fovSlider.value = fov;
            fovSlider.Rebuild(CanvasUpdate.Layout);

            fovSliderLabel = fovSlider.transform.parent.GetComponentInChildren<TMP_Text>();
            fovSliderLabel.text = "FOV: " + fov + " ";
            fovSliderLabel.Rebuild(CanvasUpdate.Layout);

            cameras = GameObject.FindObjectsByType<Camera>(FindObjectsSortMode.None);
        }

        private void OnSliderValueChanged(float value)
        {
            fov = (byte)Mathf.RoundToInt(value);
            fovSliderLabel.text = "FOV: " + fov + " ";

            FOV_Mod.fovPreference.Value = fov;

            foreach (Camera cam in cameras) cam.fieldOfView = fov;
        }
    }
}
