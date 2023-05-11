

/*
  OPEN_SOURCE
  Contribute on GitHub : https://github.com/Zain-ul-din/UnityScripting
*/

#if UNITY_EDITOR

using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Randoms.SceneManagerUtil
{
  /// Utilities
  public static class SceneManagerUtil 
  { 
    /// <summary>
    /// Returns all scenes path in build
    /// </summary>
    public static List <string> BuildScenesPath ()
    {
      List <string> scenes = new List<string> ();
      int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings; 
      for (int i = 0 ; i < sceneCount ; i += 1)
      {
        string scenePath = UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i);
        scenes.Add (scenePath);
      }
      return scenes;
    }

    /// <summary>
    /// Returns Scene name
    /// </summary>
    public static string GetSceneName (string scenePath) => Path.GetFileNameWithoutExtension(scenePath);

    /// <summary>
    /// Returns script abs path
    /// <summary>
    public static string GetScriptPath (string scriptName) 
    {
      string[] res = System.IO.Directory.GetFiles(Application.dataPath, scriptName, SearchOption.AllDirectories);
      return res.Length > 0 ? res[0].Replace ("\\", "/") : "";
    }
    
    /// <summary>
    /// Keeps SceneLoaderManager Up to date
    /// <summmary>
    public static void UpdateSceneLoaderScript (string scriptPath, string enumVal)
    {
      string fileContent = 
      "namespace Randoms.SceneManger \n{\n public static class SceneLoaderManager\n {\n  public enum SceneName\n  {\n$  }\n\n  public static void LoadScene (SceneName sceneName) \n  {\n   if (System.Enum.GetValues (typeof (SceneName)).Length == 0) return;\n   UnityEngine.SceneManagement.SceneManager.LoadScene (sceneName.ToString());\n  }\n\n }\n}";   
      File.WriteAllText (scriptPath, fileContent.Replace ("$",enumVal));
    }

  /* IO APIS */

  /// <summary>
  /// Returns all files path of a given type
  /// USAGE : GetFilesOfType ("*.cs");
  /// </summary>
  public static string[] GetFilesOfType (string fileExtension) 
  {
    string root = Application.dataPath;
    return System.IO.Directory.GetFiles (root, fileExtension, System.IO.SearchOption.AllDirectories);
  }
   
  /// <summary>
  /// returns files info like: fileName, relative Path
  /// Usage : GetFilesInfoOfType ("*.cs")
  /// </summary>
  public static List <FileInfo> GetFilesInfoOfType (string fileExtension)
  {
    List<FileInfo> FilesInfo = new List<FileInfo> ();
    foreach (string fileAbsPath in GetFilesOfType (fileExtension))
    {
      string relativepath =  ("Assets" + fileAbsPath.Substring(Application.dataPath.Length)).Replace ("\\","/");
      string fileName = System.IO.Path.GetFileName (fileAbsPath).Split ('.')[0];
      FilesInfo.Add (new FileInfo {fileName = fileName, relativepath = relativepath});
    }
    return FilesInfo;
  } 

  /// <summary>
  /// Opens a path in project window
  /// <summary>
  public static void FocusOnPath (string path)
  {
    UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath(path, typeof(UnityEngine.Object));
    Selection.activeObject = obj;
    EditorGUIUtility.PingObject(obj);
  }
  
  public class FileInfo 
  {
    public string fileName, relativepath;
    public override string ToString () => $"SceneName : {fileName} , Path : {relativepath}";
  }     
  
  /// <summmary>
  /// Ads scene to build setting
  /// </summary>
  public static void AddSceneToBuild (string sceneRelativePath)
  {
    var original = EditorBuildSettings.scenes; 
    if (original.Where (scene => scene.path == sceneRelativePath).ToArray().Length > 0) return; // already in build settings
    var newSettings = new EditorBuildSettingsScene[original.Length + 1]; 
    System.Array.Copy(original, newSettings, original.Length); 
    var sceneToAdd = new EditorBuildSettingsScene(sceneRelativePath, true); 
    newSettings[newSettings.Length - 1] = sceneToAdd; 
    EditorBuildSettings.scenes = newSettings;
  }

  /// <summmary>
  /// Removes scene from build
  /// </summary>
  public static void RemoveSceneFromBuild (string sceneRelativePath)
  {
    EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
    EditorBuildSettings.scenes = scenes.Where (scene => scene.path != sceneRelativePath).ToArray();
  }

  /// <summary>
  /// Custom Search Field
  /// </summary>
  public static string LabeledTextField(string aLabel, string aText, int marginLeft = 30)
  {
    GUILayout.BeginHorizontal();
    GUILayout.Space (marginLeft);
    GUILayout.Label(aLabel, GUILayout.MaxWidth (100));
    aText = GUILayout.TextField(aText.Trim(),GUILayout.Width (320));
    if (Event.current.type == EventType.MouseDown)
    {
      GUI.FocusControl (null);
    }
    GUILayout.EndHorizontal();
    GUILayout.Space (5);
    return aText;
  }
  
  /// <summary>
  /// Custom Button
  /// <summary>
  public static bool GUIButton (string text, Color normalColor, Color activeColor, int fontSize,int width = 50)
  {
    var style = new GUIStyle(EditorStyles.miniButtonMid);
    style.normal.textColor = normalColor;
    style.active.textColor = activeColor;
    style.fontSize = fontSize;
    style.fontStyle = FontStyle.Bold;
    return GUILayout.Button (text, style, GUILayout.Width (width));
  }

  /// <summary>
  /// Custom Button
  /// <summary>
  public static bool GUIButton (string text, Color normalColor, Color activeColor, Color backgroundColor, int fontSize, int width = 50)
  {
    var style = new GUIStyle(EditorStyles.miniButtonMid);
    style.normal.textColor = normalColor;
    style.active.textColor = activeColor;
    style.fontSize = fontSize;
    style.fontStyle = FontStyle.Bold;
    style.normal.background = DrawTexture (width, 10, backgroundColor); 
    return GUILayout.Button (text, style, GUILayout.Width (width));
  }


  /// <summary>
  /// Draws Texture
  /// </summary>
  public static Texture2D DrawTexture (int width, int height, Color textureColor)
  {
    Color[] pixels = new Color[width * height];

    for (int i = 0; i < pixels.Length; i++)
    {
      pixels[i] = textureColor;
    }

    Texture2D backgroundTexture = new Texture2D(width, height);

    backgroundTexture.SetPixels(pixels);
    backgroundTexture.Apply();

    return backgroundTexture;    
  }
 }

}

#endif