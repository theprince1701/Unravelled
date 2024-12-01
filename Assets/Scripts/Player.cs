using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public int CurrentThreads { get; set; }


    public void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        CurrentThreads = GameManager.Instance.LevelStats.startingThreads;
    }
}
