using TMPro;
using UnityEngine;

namespace UIFramework
{
    /// <summary>
    /// Base StatusBar values.
    /// </summary>
    public struct StatusValues
    {
        public string ownerName;
        public float minValue;
        public float maxValue;
        public float currentValue;

        public StatusValues(string ownerName, float minValue, float maxValue, float currentValue)
        {
            this.ownerName = ownerName;
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.currentValue = currentValue;
        }
    }

    /// <summary>
    /// Display a given stat. Can be used to display health, stamina, etc...
    /// </summary>
    public class StatusBar : MonoBehaviour
    {
        [SerializeField]
        private CustomSlider slider = null;
        [SerializeField]
        private TextMeshProUGUI characterNameDisplay = null;

        public CustomSlider Slider { get => slider; }

        public void Init(StatusValues statusValues)
        {
            characterNameDisplay.text = statusValues.ownerName;
            slider.Init(statusValues.minValue, statusValues.maxValue, statusValues.currentValue, false);
        }

        public void SetSliderValue(float value)
        {
            slider.SetValue(value);
        }

        /// <summary>
        /// Reset StatusBar to starting value.
        /// </summary>
        public void ResetSliderValue()
        {
            slider.ResetValue();
        }
    }
}
