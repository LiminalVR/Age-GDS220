#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public class SaveScenesDevelopers {

    static SaveScenesDevelopers()  {

        EditorApplication.playModeStateChanged += LoadDefultScene;
       
}

    static void LoadDefultScene(PlayModeStateChange state)  {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        }
    }


}

	
#endif