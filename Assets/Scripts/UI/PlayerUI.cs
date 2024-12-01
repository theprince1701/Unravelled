using System;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI threadsText;


    public void Update()
    {
        if (Player.Instance)
        {
            threadsText.text = "THREADS: " + Player.Instance.CurrentThreads;
        }
    }
}
