using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIFramework.StateMachine
{
    public class UIState : MonoBehaviour
    {
        protected UIStateMachine owner;

        /// <summary>
        /// Called when initialising the State Machine, much like the Start and Awake functions.
        /// </summary>
        /// <param name="owner"></param>
        public virtual void PrepareState(UIStateMachine owner) { if (!this.owner) this.owner = owner; }

        public virtual void ShowState() { }
        public virtual void HideState() { }

        /// <summary>
        /// Optional.
        /// </summary>
        public virtual void UpdateState() { }

        /// <summary>
        /// Safely load Scene.
        /// </summary>
        /// <param name="index"></param>
        public virtual void LoadScene(int index)
        {
            if (index > SceneManager.sceneCountInBuildSettings - 1)
            {
                Debug.LogWarning($"No scene with index {index}. Go to File > Build Settings to check the available scenes.");
                return;
            }

            SceneManager.LoadScene(index);
        }

    }
}