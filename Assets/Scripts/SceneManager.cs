using System;
using UnityEngine;

public class SceneManager : SingletonMonoBehaviour<SceneManager>
{
    #region Variables

    [SerializeField] public Transform enterPoint;

    [SerializeField]private string currentScene;

    #endregion


    #region Unity Lifecycle

    private void Start()
    {
        SetCurrentScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        FindObjectOfType<Player>().Reset();
    }

    #endregion


    #region Public Methods

    public void ResetLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene);
    }

    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        SetCurrentScene(sceneName);
    }

    #endregion


    #region Private Methods

    private void SetCurrentScene(string sceneName)
    {
        currentScene = sceneName;
    }

    #endregion
}
