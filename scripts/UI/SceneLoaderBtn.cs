using UnityEngine;
using UnityEngine.UI;

namespace Randoms.SceneManger.UI
{
    /// <summary>
    /// Loads Scene on btn click
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class SceneLoaderBtn : MonoBehaviour
    {
        public SceneLoaderManager.SceneName sceneName;
        public float delay = 0f;

        public void Start()
        {
            GetComponent<Button>().onClick.AddListener(() => Invoke(nameof(LoadScene), delay));
        }

        public void LoadScene () => sceneName.Load();
    }
}

