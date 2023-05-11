
/*
  OPEN_SOURCE
  Contribute on GitHub : https://github.com/Zain-ul-din/UnityScripting
*/

#if UNITY_EDITOR

using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;


namespace Randoms.SceneManagerEditorWindow
{
   using Randoms.SceneManagerUtil;

   public class SceneEditorWindow : EditorWindow {
   
    string searchText = "";
    Vector2 scrollPos;

    void OnGUI() => RenderWindow();

    /// <summary>
    /// Render main window
    /// </summary>
    private void RenderWindow ()
    {

      var scenesInfo = SceneManagerUtil.GetFilesInfoOfType ("*.unity");
      SceneManagerUtil.FileInfo[] filterInfo = scenesInfo.Where (scene => scene.fileName.ToLower ().Contains (searchText.ToLower())).ToArray ();
      
      scrollPos = EditorGUILayout.BeginScrollView(scrollPos, true, false, GUILayout.Width(this.position.width ), GUILayout.Height(this.position.height));
      Header(filterInfo);
      
      if (filterInfo.Length  == 0)
      {
        GUILayout.Button ("Not Found", EditorStyles.centeredGreyMiniLabel);
      }
   
      foreach (SceneManagerUtil.FileInfo info in filterInfo) 
      {
        GUILayout.BeginHorizontal();
        GUILayout.Space (30);
        
        // if (GUILayout.Button(info.fileName, UnityEditor.EditorStyles.miniButtonMid, GUILayout.Width(300)))
        if (SceneManagerUtil.GUIButton (info.fileName, Color.white, Color.white, 12, 300))
        {
          EditorSceneManager.OpenScene (info.relativepath);
        }
        
        GUILayout.Space (30);
        
        
        GUILayout.Space (3);
        if (SceneManagerUtil.GUIButton ("+", Color.green, Color.green, 20, 20))
        {
          SceneManagerUtil.AddSceneToBuild (info.relativepath);
          AssetDatabase.SaveAssets ();
        }
        
        var redBtnStyle = new GUIStyle (EditorStyles.miniButtonMid);
        redBtnStyle.normal.textColor = Color.red;
        
        GUILayout.Space (3);
        if (SceneManagerUtil.GUIButton ("-", Color.red, Color.red, 20, 20))
        {  
          SceneManagerUtil.RemoveSceneFromBuild(info.relativepath);
          AssetDatabase.SaveAssets ();
        }
        
        GUILayout.Space (3);
        // if (GUILayout.Button("Focus", UnityEditor.EditorStyles.miniButtonMid, GUILayout.Width(50)))
        if (SceneManagerUtil.GUIButton ("Focus", Color.grey, Color.gray, 12))
        {  
          SceneManagerUtil.FocusOnPath (info.relativepath);
        }
   
        GUILayout.EndHorizontal();
      }
   
      GUILayout.Space (10);
      GUILayout.Label("SceneManager Window", EditorStyles.centeredGreyMiniLabel);   
      EditorGUILayout.EndScrollView();  

    }
    
    /// <summary>
    /// Window Header
    /// </summary>
    private void Header (SceneManagerUtil.FileInfo[] filterInfos)
    {

      GUILayout.Space (30);
      GUILayout.BeginHorizontal ();
      GUILayout.Space (30);
      if (SceneManagerUtil.GUIButton ("Show Build", Color.white, Color.white, 12, 140))
      {
        EditorWindow window = GetWindow (typeof (BuildPlayerWindow),false);
        window.Show ();
      }
      
      GUILayout.Space (5); 
      if (SceneManagerUtil.GUIButton ("Add All", Color.green, Color.green, 12, 140))
      {
        // var scenesInfo = SceneManagerUtil.GetFilesInfoOfType ("*.unity");
        // filterInfos.ForEach (scene => SceneManagerUtil.AddSceneToBuild (scene.relativepath));
        foreach (SceneManagerUtil.FileInfo info in filterInfos) SceneManagerUtil.AddSceneToBuild (info.relativepath);
        AssetDatabase.SaveAssets ();
      }
      
      GUILayout.Space (5); 
      if (SceneManagerUtil.GUIButton ("Remove All", Color.red, Color.red, 12, 140))
      {
        // var scenesInfo = SceneManagerUtil.GetFilesInfoOfType ("*.unity");
        // scenesInfo.ForEach (scene => SceneManagerUtil.RemoveSceneFromBuild (scene.relativepath));
        foreach (SceneManagerUtil.FileInfo info in filterInfos) SceneManagerUtil.RemoveSceneFromBuild (info.relativepath);
        AssetDatabase.SaveAssets ();
      }
   
      GUILayout.EndHorizontal ();
   
      GUILayout.Space (20);
      searchText = SceneManagerUtil.LabeledTextField ("Search Scene : " , searchText);
      GUILayout.BeginHorizontal();
      if (filterInfos.Length > 0) GUILayout.Button ("Scenes",EditorStyles.centeredGreyMiniLabel,GUILayout.Width (300));
      GUILayout.EndHorizontal ();
      GUILayout.Space (5);

    }
    
    /// <summary>
    /// On Winow UnFocus
    /// </summary>
    void OnLostFocus()
    {
      GUI.FocusControl (null);
    }

  }
}

#endif
