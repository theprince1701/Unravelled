using System;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

struct AlterationSaved
{
    public AlterationObject AlterationObject;
    public Alteration.AlterationType AlterationType;
}

public class Alteration : MonoBehaviour
{
    public enum AlterationType
    {
        Levitate,
        Stretch,
        GravityUp,
        GravityDown,
        None,
        Dismantle
    }

    [SerializeField] private float tileDist = 1f;
    [SerializeField] private AlterationUIManager alterationUIManager;
    
    private AlterationObject _alteration;
    private AlterationType _alterationType;
    private bool _isAltering;
    private bool _setInitialPositionStretch;
    private bool _didLevitateLogic;
    
    private List<AlterationSaved> _alterationSaved = new List<AlterationSaved>();
    
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out AlterationObject alteration))
        {
            _alteration = alteration;
            _alteration.alteration = this;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out AlterationObject alteration))
        {
            StopAlteration();
        }
    }

    public void Dismantle()
    {
        _alterationSaved.Add(new AlterationSaved { AlterationObject = _alteration, AlterationType = AlterationType.Dismantle });

        if (_alteration)
        {
            _alteration.SetAlterationState(AlterationObjectState.Dismantled);
        }
        
    }
    
    private void UndoAlteration()
    {
        if (_alterationSaved.Count <= 0)
        {
            return;
        }
        
        AlterationSaved saved = _alterationSaved[_alterationSaved.Count-1];

        if (!saved.AlterationObject)
        {
            return;
        }
        
        if (saved.AlterationType == AlterationType.Levitate)
        {
            saved.AlterationObject.IsLevitating = !saved.AlterationObject.IsLevitating;
        }
        else if (saved.AlterationType == AlterationType.Dismantle)
        {
            saved.AlterationObject.SetAlterationState(AlterationObjectState.Active);
        }
        else if (saved.AlterationType == AlterationType.Stretch)
        {
            saved.AlterationObject.transform.localScale = _alteration.defaultScale;
        }
        
        _alterationSaved.RemoveAt(_alterationSaved.Count - 1);
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
        if (_alteration)
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
                Dismantle();
            }
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            UndoAlteration();
        }
    }

    void HandleAlterationInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _alterationType = AlterationType.Levitate;
            alterationUIManager.OnAlterationSelected(AlterationType.Levitate);
            _alterationSaved.Add(new AlterationSaved { AlterationObject = _alteration, AlterationType = AlterationType.Levitate });

        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
              _alterationType = AlterationType.Stretch;  
              alterationUIManager.OnAlterationSelected(AlterationType.Stretch);
              _alterationSaved.Add(new AlterationSaved { AlterationObject = _alteration, AlterationType = AlterationType.Stretch });

        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
           _alterationType = AlterationType.GravityUp;   
           alterationUIManager.OnAlterationSelected(AlterationType.GravityUp);
           _alterationSaved.Add(new AlterationSaved { AlterationObject = _alteration, AlterationType = AlterationType.GravityUp });

        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _alterationType = AlterationType.GravityDown;
            alterationUIManager.OnAlterationSelected(AlterationType.GravityDown);
            _alterationSaved.Add(new AlterationSaved { AlterationObject = _alteration, AlterationType = AlterationType.GravityDown });

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
            _didLevitateLogic = true;
        }
    }

    private void Update()
    {
        HandleInput();

        if (!_alteration)
        {
            return;
        }
        
        Debug.Log(_alteration);

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
