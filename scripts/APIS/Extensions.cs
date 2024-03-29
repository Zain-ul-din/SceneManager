namespace Randoms.SceneManager
{
    using UnityEngine;

    /// <summary>
    /// Helpful Extension Methods to make APIS much simpler
    /// </summary>
    public static class RandomsSceneManagerExtensions__
    {
        /// <summary>
        /// Loads New Scene
        /// </summary>
        /// <param name="sceneName"></param>
        public static void Load(this Scene sceneName) => SceneManager.LoadScene(sceneName);

        /// <summary>
        /// Loads Scene in the background without pausing the game
        /// </summary>
        /// <param name="sceneName">sceneName</param>
        public static void LoadAsync(this Scene sceneName) => SceneManager.LoadAsync(sceneName);

        /// <summary>
        /// Loads Scene in the background without pausing the game.
        /// progressCallback: use to get current progress from 0 to 1f.
        /// handle: use to allow scene activation. 
        /// </summary>
        /// <example>
        ///     <code>
        ///      LoadAsync(sceneName, (value)=> Debug.Log(value), (operation)=> { operation.allowSceneActivation = true; } );
        ///     </code>
        /// </example>
        /// <param name="sceneName">sceneName</param>
        /// <param name="progressCallback">progressCallBack</param>
        /// <param name="handler">handler</param>
        public static void LoadAsync(
            this Scene sceneName,
            System.Action<float> progressCallback,
            System.Action<AsyncOperation> handler = null
        ) => SceneManager.LoadAsync(sceneName, progressCallback, handler);
    }
}
