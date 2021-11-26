using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIFramework
{
    class UIElements : EditorWindow
    {
        private static GameObject[] gameObjects;
        private Transform parent;
        private GameObject eventSystem;

        [MenuItem("UI Framework/UI Elements")]
        public static void ShowWindow()
        {
            GetWindow(typeof(UIElements));
            RefreshSelection();
        }

        private static void RefreshSelection()
        {
            GetObjects();
        }

        private static void GetObjects()
        {
            gameObjects = Resources.LoadAll<GameObject>("SpawnableElements");
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Select Element to Spawn", EditorStyles.boldLabel);

            EditorGUILayout.Separator();
            EditorGUILayout.Separator();

            DrawObjectsButton();
        }

        private void OnFocus()
        {
            RefreshSelection();
        }

        private void DrawObjectsButton()
        {
            foreach (GameObject gameObject in gameObjects)
            {
                if (!gameObject)
                    continue;

                if (GUILayout.Button($"{gameObject.name}"))
                {
                    SpawnObject(gameObject);
                }
            }
        }

        private void SpawnObject(GameObject gameObject)
        {
            parent = Selection.activeTransform;

            if (!parent)
                PrefabUtility.InstantiatePrefab(gameObject);
            else
                PrefabUtility.InstantiatePrefab(gameObject, parent);

            if (!eventSystem && GameObject.Find("EventSystem") == null)
            {
                eventSystem = new GameObject();
                eventSystem.name = "EventSystem";
                eventSystem.AddComponent<EventSystem>();
                eventSystem.AddComponent<StandaloneInputModule>();
            }
        }
    }
}
