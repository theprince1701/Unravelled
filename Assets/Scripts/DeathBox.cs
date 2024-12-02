using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathBox : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private GameObject icon;

    
    private List<Collider2D> objectsInZone = new List<Collider2D>();
    
    private bool _canCross;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!_canCross)
            {
                SceneManager.LoadScene("GameScene");
            }
        }
        else if (other.tag == "Alteration")
        {
            objectsInZone.Add(other);

            float totalX = 0;

            foreach (Collider2D g in objectsInZone)
            {
                totalX += g.bounds.size.x;
            }
            Debug.Log("total x: " + totalX);
            Debug.Log("box bounds: "  + boxCollider.bounds.size.x);
            if (totalX >= boxCollider.bounds.size.x-0.25f)
            {
                _canCross = true;
                if (icon)
                {
                    icon.SetActive(false);
                }
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Alteration")
        {
            _canCross = false;
        }
    }
}
