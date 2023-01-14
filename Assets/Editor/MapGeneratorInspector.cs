using UnityEditor;
using UnityEngine;

namespace TheCity.ChessMaze
{
    [CustomEditor(typeof(MapGenerator))]
    public class MapGeneratorInspector : Editor
    {
        private MapGenerator map;

        private void OnEnable()
        {
            map = (MapGenerator)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (Application.isPlaying)
            {
                if (GUILayout.Button("Generate new map"))
                {
                    map.GenerateNewMap();
                }

                if (GUILayout.Button("Repair map"))
                {
                    map.TryRepair();
                }
            }
        }
    }
}