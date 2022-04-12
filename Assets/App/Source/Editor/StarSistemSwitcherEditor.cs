#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.Collections;
using Systemagedon.App.Gameplay;

[CustomEditor(typeof(StarSystemSwitcher))]
public class StarSystemSwitcherEditor : Editor
{
    private StarSystemSwitcher _target;
    private Texture _error
        { get => EditorGUIUtility.FindTexture("console.erroricon"); }


    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        int maxPlanets = _target.Generator.CalculateMaxPlanets();
        if (maxPlanets < 2)
        {
            GUILayout.BeginHorizontal("box");
            GUILayout.Label(_error);
            GUI.skin.label.wordWrap = true;
            GUILayout.Label("Needs a star system with minimum of 2 planets. " +
                "Given generator config can't take it");
            GUILayout.EndHorizontal();
        }
        else
        {
            GUILayout.Label($"Max planets: {maxPlanets}");
        }
    }


    private void OnEnable()
    {
        _target = target as StarSystemSwitcher;
    }
}

#endif