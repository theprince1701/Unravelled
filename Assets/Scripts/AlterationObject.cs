using System;
using UnityEngine;
using UnityEngine.EventSystems;

public enum AlterationObjectState
{
    InScene,
    Active,
    Dismantled
}

public class AlterationObject : MonoBehaviour
{    
    private bool _isInteracting;
    public bool IsLevitating { get; set; }

    public Alteration alteration { get; set; }
    public AlterationObjectState alterationState { get; set; }
    
    public Vector3 defaultScale { get; set; }


    private void Start()
    {
        defaultScale = this.transform.localScale;
    }

    public void StopAltering()
    {
    }

    public void StartAltering()
    {
    }

    public void Dismantle()
    {
        SetAlterationState(AlterationObjectState.Dismantled);
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


    public void SetAlterationState(AlterationObjectState state)
    {
        alterationState = state;

        switch (state)
        {
            case AlterationObjectState.Dismantled:
                gameObject.SetActive(false);
                break;
            case AlterationObjectState.InScene:
                gameObject.SetActive(true);
                break;
            case AlterationObjectState.Active:
                gameObject.SetActive(true);
                break;
        }
    }
}
