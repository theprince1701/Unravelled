using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private string nextLevelName;
    [SerializeField] private LevelStats levelStats;

    public LevelStats LevelStats => levelStats;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void NextLevel()
    {
        if (nextLevelName != string.Empty)
        {
            SceneManager.LoadScene(nextLevelName);
        }
        else
        {
            Quit();
        }
    }
}