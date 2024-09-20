using UnityEngine;
using UnityEditor;

public class AdsEditorWindow : EditorWindow
{

    //[MenuItem("Example/Custom Defines/Test")]
    //public static void Check()
    //{
    //    PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "TESTING");

    //}

    [MenuItem("SmileSoft/Ads")]
    private static void OpenWindow()
    {
        //PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "TESTING");
       
        const float wndWidth = 200.0f;
        const float wndHeight = 200.0f;
        var pos = new Vector2(0.5f * (Screen.currentResolution.width - wndWidth),
                              0.5f * (Screen.currentResolution.height - wndHeight));
        var window = GetWindow<AdsEditorWindow>();
        window.titleContent = new GUIContent(AdsEditorUtils.ADS_EDITOR_NAME);
        window.position = new Rect(pos, new Vector2(wndWidth, wndHeight));
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Enable AdMob"))
        {
             AdsEditorUtils.AddDefineIfNecessary(AdsEditorUtils.SMILE_SOFT_ADMOB, BuildTargetGroup.Android);
            AdsEditorUtils.AddDefineIfNecessary(AdsEditorUtils.SMILE_SOFT_ADMOB, BuildTargetGroup.iOS);
        }
        if (GUILayout.Button("Disable AdMob"))
        {
             AdsEditorUtils.RemoveDefineIfNecessary(AdsEditorUtils.SMILE_SOFT_ADMOB, BuildTargetGroup.Android);
            AdsEditorUtils.RemoveDefineIfNecessary(AdsEditorUtils.SMILE_SOFT_ADMOB, BuildTargetGroup.iOS);
        }
    }
}
