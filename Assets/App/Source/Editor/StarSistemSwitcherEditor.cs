#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.Collections;
using Systemagedon.App.Gameplay;
using Systemagedon.App.Services;
using Systemagedon.App.Configuration;

[CustomEditor(typeof(StarSystemSwitcherConfiguration))]
public class StarSystemSwitcherEditor : Editor
{
    private StarSystemSwitcherConfiguration _target;
    private Texture _error
        { get => EditorGUIUtility.FindTexture("console.erroricon"); }


    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var settings = _target.StarSystemConfiguration.ParseSettings();
        var generator = new StarSystemGenerator(settings); // todo dont init generator
        var maxPlanets = generator.CalculateMaxPlanets();
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
        _target = target as StarSystemSwitcherConfiguration;
    }
}

#endif