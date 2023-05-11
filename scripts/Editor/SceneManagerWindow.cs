
/*
  OPEN_SOURCE
  Contribute on GitHub : https://github.com/Zain-ul-din/UnityScripting
*/

#if UNITY_EDITOR

using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Randoms.SceneManagerEditorWindow
{
  using Randoms.SceneManagerUtil;
  public class SceneManagerWindow : EditorWindow
  {
      string searchText = "";
      Vector2 scrollPos;
      static int lastFilesLength;
  
      /// <summary>
      /// This is called as soon as you open the project in Unity
      /// and after script compilations
      /// </summary>
      [InitializeOnLoadMethod]
      private static void Init()
      {
        RemoveSpaceFromFilesName ();
        UpdateSceneManagerScript ();
      }
      
      /// <summary>
      /// Removes space from files name
      /// </summary>
      private static void RemoveSpaceFromFilesName ()
      {
        lastFilesLength = SceneManagerUtil.GetFilesOfType ("*.unity").Length;
        foreach (string path in SceneManagerUtil.GetFilesOfType ("*.unity"))
        {
          FileInfo fileInfo = new FileInfo (path);
          fileInfo.MoveTo(fileInfo.Directory.FullName + "\\" + fileInfo.Name.Replace (" ", "_"));  
        }
        
        foreach (string path in SceneManagerUtil.GetFilesOfType ("*.unity.meta"))
        {
          FileInfo fileInfo = new FileInfo (path);
          fileInfo.MoveTo(fileInfo.Directory.FullName + "\\" + fileInfo.Name.Replace (" ", "_"));  
        }
        // AssetDatabase.Refresh ();
      } 
  
      /// <summary>
      /// Draws Scene Manager Window 
      /// MenuItem: https://hugecalf-studios.github.io/unity-lessons/lessons/editor/menuitem/
      // %	Ctrl/Command #	Shift &	Alt _	None
      /// </summary>
      [MenuItem ("Randoms/SceneManager #s")]
      private static void DrawWindow ()
      {
        bool drawFixedWindow = false;
        EditorWindow window = GetWindow(typeof(SceneManagerWindow),drawFixedWindow,"Scene Manager");
        window.Show();
      }

      /// <summary>
      /// reloads project
      /// </summary>
      [MenuItem ("Randoms/Reload #r")]
      private static void ReloadProject ()
      {
        UpdateSceneManagerScript();
        AssetDatabase.Refresh ();
      }
      
      /// <summary>
      /// GUI startup
      /// </summary>
      private void OnGUI ()
      {
        SceneLoaderWindow ();
        if (lastFilesLength != SceneManagerUtil.GetFilesOfType ("*.unity").Length)
        {
          RemoveSpaceFromFilesName ();
          UpdateSceneManagerScript ();
        }
      }
      
      /// <summary>
      /// Scene Loader Ui Renderer
      /// </summary>
      private void SceneLoaderWindow ()
      {
        var scenesPath = SceneManagerUtil.BuildScenesPath ();
        var filterScenesPath = scenesPath.Where (path => {
         return SceneManagerUtil.GetSceneName(path).ToLower().Contains (searchText.ToLower());
        }).ToArray();
        
        GUILayout.Space (10);
  
        if (scenesPath.Count == 0)
        {
         GUILayout.Label("Warning : No Scene Added So Far", EditorStyles.helpBox);
         if (GUILayout.Button ("Add Scenes"))
         {
          EditorWindow window = GetWindow (typeof (BuildPlayerWindow));
          window.Show ();
         }
        }
        else 
        {
          searchText = SceneManagerUtil.LabeledTextField ("Search Scene : ", searchText, 5);
          if (filterScenesPath.Length == 0)
          {
           GUILayout.Button ("Not Found", EditorStyles.centeredGreyMiniLabel);
          }
        }
  
        scrollPos = GUILayout.BeginScrollView(scrollPos, false, false, GUIStyle.none, GUI.skin.verticalScrollbar);
        GUILayout.Space (10);
  
        foreach (var scenePath in filterScenesPath)
        {
          GUILayout.BeginHorizontal ();
          GUILayout.Space (5);
          if (GUILayout.Button (SceneManagerUtil.GetSceneName(scenePath), EditorStyles.miniButton)) 
          {
            EditorSceneManager.OpenScene (scenePath);
          } 
  
          GUILayout.Space (5);
          if (SceneManagerUtil.GUIButton ("-",Color.white,Color.white,Color.red,30,30))
          {
            SceneManagerUtil.RemoveSceneFromBuild (scenePath);
            AssetDatabase.SaveAssets ();
          }
  
          GUILayout.Space (5);
          GUILayout.EndHorizontal ();
          GUILayout.Space (5);
        }
  
        GUILayout.Space (20);
        EditorGUILayout.EndScrollView ();
        
        GUILayout.Space (20);
        
        GUILayout.BeginHorizontal ();
         
        GUILayout.Space (10);
        if (GUILayout.Button ("Open Scene Editor",EditorStyles.miniButtonMid))
        {
          searchText = "";
          EditorWindow window = GetWindow (typeof (SceneEditorWindow), true);
          window.Show ();
        }
        
        GUILayout.Space (10);
        if (GUILayout.Button ("Refresh", EditorStyles.miniButtonMid,GUILayout.Width (60)))
        {
          searchText = "";
          UpdateSceneManagerScript();
          AssetDatabase.SaveAssets ();
        }
        
        GUILayout.Space (10);
        if (GUILayout.Button ("Reload", EditorStyles.miniButtonMid, GUILayout.Width (60)))
        {
          searchText = "";
          UpdateSceneManagerScript();
          AssetDatabase.Refresh ();
        }
  
        GUILayout.Space (10);
        GUILayout.EndHorizontal ();
        GUILayout.Label("SceneManager Window", EditorStyles.centeredGreyMiniLabel);  
        GUILayout.Space (5);
      } 
  
      /// <summary>
      /// Updates changes in script
      /// </summary>
      private static void UpdateSceneManagerScript ()
      {
        string scriptPath = SceneManagerUtil.GetScriptPath ("SceneLoaderManager.cs");
        if (scriptPath == "") return;
        string fileContent = File.ReadAllText (scriptPath);
        string newSceneNameEnum = "";
        foreach (string path in SceneManagerUtil.BuildScenesPath()) 
         newSceneNameEnum += "   " + SceneManagerUtil.GetSceneName (path) + ",\n";
        newSceneNameEnum.Remove (newSceneNameEnum.Length - 1); 
        SceneManagerUtil.UpdateSceneLoaderScript (scriptPath, newSceneNameEnum);
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
