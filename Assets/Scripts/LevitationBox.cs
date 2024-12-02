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
            objectsInZone.Add(other);

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

            Debug.Log(totalX);
            Debug.Log(boxCollider.bounds.size.x-0.25f);
            Debug.Log(allLevitating);
            if (totalX >= boxCollider.bounds.size.x-0.25f && allLevitating)
            {
                _canCross = true;

                foreach (Collider2D g in objectsInZone)
                {
                    if (g.TryGetComponent(out AlterationObject alterationObject))
                    {
                        alterationObject.playerCollider2D.gameObject.SetActive(false);
                    }
                }
                
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
            if (other.TryGetComponent(out AlterationObject alterationObject))
            {
                alterationObject.playerCollider2D.gameObject.SetActive(true);
            }
            _canCross = false;
        }
    }
}
