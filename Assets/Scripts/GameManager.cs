using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int world;
    public int stage;
    public int currentLives;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        currentLives = 3;

        LoadLevel(1, 1);
    }

    private void LoadLevel(int _world, int _stage)
    {
        world = _world;
        stage = _stage;

        SceneManager.LoadScene($"{world}-{stage}");
    }

    public void ResetLevel()
    {
        --currentLives;

        if (currentLives > 0)
        {
            LoadLevel(world, stage);
        }
        else
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        //gameover
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
