using UnityEngine;

namespace UIFramework.StateMachine
{
    /// <summary>
    /// Holds logic for the UIView visual elements.
    /// </summary>
    public class UIView : MonoBehaviour
    {
        /// <summary>
        /// Method called to show view.
        /// </summary>
        public virtual void ShowView()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Method called to hide view.
        /// </summary>
        public virtual void HideView()
        {
            gameObject.SetActive(false);
        }
    }
}
