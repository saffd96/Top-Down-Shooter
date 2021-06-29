using System;
using UnityEngine;

public class SceneManager : SingletonMonoBehaviour<SceneManager>
{
    #region Variables

    [SerializeField] public Transform ExitPoint;
   // [SerializeField] private Transform enterPoint;

    public string CurrentScene;

    #endregion


    #region Unity Lifecycle

    private void Awake()
    {
        base.Awake();
        UpdateCurrentScene();
    }

    #endregion


    #region Private Methods

    private void UpdateCurrentScene()
    {
        CurrentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
    }

    #endregion


    #region Public Methods

    public void ResetLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneNames.Scene1);
        UpdateCurrentScene();
    }

    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        UpdateCurrentScene();
    }

    #endregion
}
