using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public BaseScene initialScene;
    [SerializeField] LevelState levelState;

    private void Start()
    {
        if (initialScene != null)
        {
            initialScene.StartScene();
        }

        levelState.sceneName = SceneManager.GetActiveScene().name;
        levelState.ClearCardRewards();
    }
}
