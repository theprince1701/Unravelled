using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevitationBox : MonoBehaviour
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
            float totalX = 0;
            foreach (Collider2D g in objectsInZone)
            {
                totalX += g.bounds.size.x;
            }

            bool allLevitating = true;

            foreach (Collider2D g in objectsInZone)
            {
                if (!g.GetComponent<AlterationObject>().IsLevitating)
                {
                    allLevitating = false;
                }
            }
            
            if (totalX >= boxCollider.bounds.size.x && allLevitating)
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
