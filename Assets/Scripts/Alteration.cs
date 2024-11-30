using System;
using Unity.VisualScripting;
using UnityEngine;

public class Alteration : MonoBehaviour
{
    public enum AlterationType
    {
        Levitate,
        Stretch,
        GravityUp,
        GravityDown,
        None
    }

    [SerializeField] private float tileDist = 1f;
    [SerializeField] private AlterationUIManager alterationUIManager;
    
    private AlterationObject _alteration;
    private AlterationType _alterationType;
    private bool _isAltering;
    private bool _setInitialPositionStretch;
    private bool _didLevitateLogic;
    
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out AlterationObject alteration))
        {
            Debug.Log("can alter");

            _alteration = alteration;
            _alteration.alteration = this;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out AlterationObject alteration))
        {
            Debug.Log("left alter");

            StopAlteration();
        }
    }

    public void StopAlteration()
    {
        if (_alteration)
        {
            _alteration.StopAltering();
            _isAltering = false;
            _alteration = null;
        }
        
        _alterationType = AlterationType.None;

        alterationUIManager.ToggleVisiblity(false);
        alterationUIManager.OnAlterationReset();
    }

    private void Start()
    {
        alterationUIManager.ToggleVisiblity(false);
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_isAltering)
            {
                _alteration.StopAltering();
                _isAltering = false;
                alterationUIManager.ToggleVisiblity(false);
                alterationUIManager.OnAlterationReset();
            }
            else
            {
                _alteration.StartAltering();
                _isAltering = true;
                alterationUIManager.ToggleVisiblity(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _alteration.Dismantle();
        }
    }

    void HandleAlterationInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _alterationType = AlterationType.Levitate;
            alterationUIManager.OnAlterationSelected(AlterationType.Levitate);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
              _alterationType = AlterationType.Stretch;  
              alterationUIManager.OnAlterationSelected(AlterationType.Stretch);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
           _alterationType = AlterationType.GravityUp;   
           alterationUIManager.OnAlterationSelected(AlterationType.GravityUp);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _alterationType = AlterationType.GravityDown;
            alterationUIManager.OnAlterationSelected(AlterationType.GravityDown);
        }
    }

    void HandleStretch()
    {
        if (!_setInitialPositionStretch)
        {
       //     transform.position = transform.position + (transform.position - _alteration.transform.position).normalized * tileDist;
       //     _setInitialPositionStretch = true;
        }
        
        Vector2 direction = (_alteration.transform.position - transform.position);
        float dist = direction.magnitude;

        Vector3 scale = _alteration.transform.localScale;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            scale.x = dist;
        }
        else
        {
            scale.y = dist;
        }

        _alteration.transform.localScale = scale;
    }

    void HandleGravity(bool up)
    {
        
    }

    void HandleLevitate()
    {
        if (!_didLevitateLogic)
        {
            _alteration.IsLevitating = !_alteration.IsLevitating;
            Debug.Log(_alteration.IsLevitating);
            _didLevitateLogic = true;
        }
    }

    private void Update()
    {
        if (!_alteration)
        {
            return;
        }
        
        HandleInput();


        if (_isAltering)
        {
            HandleAlterationInput();

            if (_alterationType == AlterationType.Levitate)
            {
                HandleLevitate();
            }
            else if (_alterationType == AlterationType.Stretch)
            {
                HandleStretch();
            }
            else if (_alterationType == AlterationType.GravityUp)
            {
                HandleGravity(true);
            }
            else if (_alterationType == AlterationType.GravityDown)
            {
                HandleGravity(false);
            }
        }

        if (_alterationType != AlterationType.Stretch)
        {
            _setInitialPositionStretch = false;
        }

        if (_alterationType != AlterationType.Levitate)
        {
            _didLevitateLogic = false;
        }
    }

}
