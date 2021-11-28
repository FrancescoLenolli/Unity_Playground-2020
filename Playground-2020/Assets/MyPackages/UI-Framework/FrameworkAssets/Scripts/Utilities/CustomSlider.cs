using UnityEngine;
using UnityEngine.UI;

namespace UIFramework
{
    public class CustomSlider : MonoBehaviour
    {
        private Slider slider;

        private void Awake()
        {
            slider = GetComponent<Slider>();
        }

        /// <summary>
        /// Set the Slider's starting values.
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="startingValue"></param>
        /// <param name="wholeNumbers"> Set to TRUE to have the Slider value accepts whole numbers only. </param>
        public void Init(float minValue, float maxValue, float startingValue, bool wholeNumbers = false)
        {
            slider.minValue = minValue;
            slider.maxValue = maxValue;
            slider.value = startingValue;

            slider.wholeNumbers = wholeNumbers;
        }

        /// <summary>
        /// Set new Slider value.
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(float value)
        {
            slider.value = value;
        }

        /// <summary>
        /// Get Slider's current value.
        /// </summary>
        /// <returns></returns>
        public float GetValue()
        {
            return slider.value;
        }

        public void ResetValue()
        {
            slider.value = slider.maxValue;
        }
    }
}