using GameLoop;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(GameStateService))]
    public class StateManagerEditor : UnityEditor.Editor
    {
        private GameStateService _gameStateService;

        private void OnEnable()
        {
            _gameStateService = (GameStateService)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Apply GamePlay State"))
            {
                _gameStateService.ApplyState(GameState.GamePlay);
            }

            if (GUILayout.Button("Apply Pause State"))
            {
                _gameStateService.ApplyState(GameState.Pause);
            }
            
            if (GUILayout.Button("Apply EndGame State"))
            {
                _gameStateService.ApplyState(GameState.EndGame);
            }
        }
    }
}