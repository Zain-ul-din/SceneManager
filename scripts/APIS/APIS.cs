namespace Randoms.SceneManger
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    /// <summary>
    /// Partial class of SceneLoaderManager Provides public APIS
    /// </summary>
    public partial class SceneLoaderManager
    {
        #region PUBLIC_APIS

        /// <summary>
        /// Loads New Scene
        /// </summary>
        /// <param name="sceneName">sceneName</param>
        public static void LoadScene(SceneName sceneName)
        {
            if (System.Enum.GetValues(typeof(SceneName)).Length == 0)
            {
                Debug.LogError("No Scene Added. Open 'SceneManager' window to add new scene");
                return;
            }

            SceneManager.LoadScene(sceneName.ToString());
        }

        /// <summary>
        /// Returns the name of current active scene
        /// </summary>
        /// <returns>string</returns>
        public static string GetCurrentSceneName()
        {
            return SceneManager.GetActiveScene().name;
        }

        /// <summary>
        /// Loads Scene in the background without pausing the game
        /// </summary>
        /// <param name="sceneName">sceneName</param>
        public static void LoadAsync(SceneName sceneName)
        {
            var asyncOperation = PrepareAsyncLoad(sceneName.ToString());

            while (!asyncOperation.isDone)
            {
                if (asyncOperation.progress >= 0.9f)
                {
                    asyncOperation.allowSceneActivation = true;
                    Application.backgroundLoadingPriority = ThreadPriority.Normal;
                }
            }
        }

        /// <summary>
        /// Loads Scene in the background without pausing the game.
        /// progressCallback: use to get current progress from 0 to 1f.
        /// handle: use to allow scene activation. 
        /// </summary>
        /// <example>
        ///     LoadAsync(sceneName, (value)=> Debug.Log(value), (operation)=> { operation.allowSceneActivation = true; } );
        /// </example>
        /// <param name="sceneName">sceneName</param>
        /// <param name="progressCallback">progressCallBack</param>
        /// <param name="handler">handler</param>
        public static void LoadAsync(
            SceneName sceneName,
            System.Action<float> progressCallback,
            System.Action<AsyncOperation> handler = null
        )
        {
            var asyncOperation = PrepareAsyncLoad(sceneName.ToString());

            while (!asyncOperation.isDone)
            {
                if (asyncOperation.progress >= 0.9f)
                {
                    progressCallback(1f);
                    if (handler == null) asyncOperation.allowSceneActivation = true;
                    else handler(asyncOperation);
                    Application.backgroundLoadingPriority = ThreadPriority.Normal;
                }

                progressCallback(asyncOperation.progress >= 0.9f ? 1f : asyncOperation.progress);
            }
        }

        #endregion

        #region PRIVATE

        private static AsyncOperation PrepareAsyncLoad(string sceneName)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            Application.backgroundLoadingPriority = ThreadPriority.High;
            asyncOperation.allowSceneActivation = false;
            return asyncOperation;
        }

        #endregion

    }
}

