using UnityEngine;

public class AlterationObject : MonoBehaviour
{
    
    [SerializeField] private GameObject interactUI;
    
    public void OnLookAt()
    {
        interactUI.SetActive(true);
    }

    public void StopLookAt()
    {
        interactUI.SetActive(false);
    }

    public void StopAltering()
    {
        interactUI.SetActive(false);
    }

    public void StartAltering()
    {
        interactUI.SetActive(false);
    }

    public void Dismantle()
    {
        Destroy(gameObject);
    }
}
