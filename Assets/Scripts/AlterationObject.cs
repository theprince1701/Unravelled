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
    [SerializeField] private GameObject icon;
    [SerializeField] private Vector2 offset;
    
    private bool _isInteracting;
    public bool IsLevitating { get; set; }

    public Alteration alteration { get; set; }
    public AlterationObjectState alterationState { get; set; }
    
    public Vector3 defaultScale { get; set; }

    private SpriteRenderer _spriteRenderer;


    private void Start()
    {
        defaultScale = this.transform.localScale;
        _spriteRenderer  = GetComponent<SpriteRenderer>();
    }

    public void SetColor(Color color)
    {
        _spriteRenderer.color = color;
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
        
        float tileSize = 1.0f; // Adjust this to match your tile size

        Vector3 mouseScreenPosition = Input.mousePosition;

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        mouseWorldPosition.x = Mathf.Round(mouseWorldPosition.x / tileSize) * tileSize;
        mouseWorldPosition.y = Mathf.Round(mouseWorldPosition.y / tileSize) * tileSize;

        mouseWorldPosition.z = transform.position.z;

        transform.position = mouseWorldPosition + new Vector3(0.5f, 0.5f,0 );
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
