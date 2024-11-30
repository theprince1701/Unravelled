using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class AlterationObject : MonoBehaviour
{
    
    
    private bool _isInteracting;
    
    public Alteration alteration { get; set; }

    public void StopAltering()
    {
    }

    public void StartAltering()
    {
    }

    public void Dismantle()
    {
        Destroy(gameObject);
    }
    
    private void Update()
    {
        if (!_isInteracting)
        {
            return;
        }
        
        Vector3 mouseScreenPosition = Input.mousePosition;

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        mouseWorldPosition.z = transform.position.z;

        transform.position = mouseWorldPosition;
    }

    private void OnMouseDown()
    {
        _isInteracting = true;
        alteration.StopAlteration();
    }

    public void OnMouseUp()
    {
        _isInteracting = false;
    }
}
