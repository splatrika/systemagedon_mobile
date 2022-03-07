using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Systemagedon.Tests
{

    public static class TestsUtility
    {
        public static GameObject MakePrefabReady(string prefabPath)
        {
            GameObject loadedPrefab =
                PrefabUtility.LoadPrefabContents(prefabPath);
            EditorSceneManager.MoveGameObjectToScene(loadedPrefab,
                EditorSceneManager.GetActiveScene());
            return loadedPrefab;
        }
    }

}
