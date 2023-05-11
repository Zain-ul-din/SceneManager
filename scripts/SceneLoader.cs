/*
  OPEN_SOURCE
  Contribute on GitHub : https://github.com/Zain-ul-din/UnityScripting
*/

using UnityEngine;
using UnityEngine.UI;

namespace Randoms.SceneManger 
{
  /// Loads Scene On Btn Click
  [RequireComponent (typeof (Button))]
  public class SceneLoader : MonoBehaviour
  {
    [SerializeField] private SceneLoaderManager.SceneName sceneToLoad;
    
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
       GetComponent <Button> ().onClick.AddListener (()=>{
         SceneLoaderManager.LoadScene (sceneToLoad);
       }); 
    }
  }
}

