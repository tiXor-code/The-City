using UnityEditor;
using UnityEngine;

namespace TheCity.ChessMaze
{
    [CustomEditor(typeof(MapBrain))]
    public class MapBrainInspector : Editor
    {
        private MapBrain mapBrain;

        private void OnEnable()
        {
            mapBrain = (MapBrain)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (Application.isPlaying)
            {
                GUI.enabled = !mapBrain.IsAlgorithmRunning;
                if (GUILayout.Button("Run Genetic Algorithm"))
                {
                    mapBrain.RunAlgorithm();
                }
            }
        }
    }
}