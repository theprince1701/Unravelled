using System;
using UnityEngine;

public class LevelEndTrigger : MonoBehaviour
{
    [SerializeField] private GameObject endScreenUI;
    [SerializeField] private GameObject[] gameObjectsToDisable;
    

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            endScreenUI.SetActive(true);
            other.GetComponent<PlayerMovement>().BlockMovement(true);
            
            foreach(GameObject g in gameObjectsToDisable)
            {
                g.SetActive(false);
            }
        }
    }
}
