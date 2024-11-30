using UnityEngine;
using UnityEngine.SceneManagement;

public class LevitationBox : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider;
    
    private bool _canCross;
    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("something entered");
        if (other.tag == "Player")
        {
            Debug.Log("was player");

            if (!_canCross)
            {
                SceneManager.LoadScene("GameScene");
            }
        }
        else if (other.tag == "Alteration")
        {
            if (other.GetComponent<AlterationObject>().IsLevitating && other.bounds.size.x > boxCollider.bounds.size.x)
            {
                _canCross = true;
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
