public class SceneManager : SingletonMonoBehaviour<SceneManager>
{
    #region Public Methods

    public void ResetLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    #endregion
}
