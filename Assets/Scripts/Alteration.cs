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
    
    [Space]
    
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color levitateColor;


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

        Player.Instance.CurrentThreads += GameManager.Instance.LevelStats.threadsBackForDismantle;
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
            
            if ( saved.AlterationObject.IsLevitating)
            {
                saved.AlterationObject.SetColor(levitateColor);
            }
            else
            {
                saved.AlterationObject.SetColor(defaultColor);
            }

            Player.Instance.CurrentThreads += GameManager.Instance.LevelStats.threadsCostForLevitate;

        }
        else if (saved.AlterationType == AlterationType.Dismantle)
        {
            saved.AlterationObject.SetAlterationState(AlterationObjectState.Active);
            Player.Instance.CurrentThreads -= GameManager.Instance.LevelStats.threadsBackForDismantle;

        }
        else if (saved.AlterationType == AlterationType.Stretch)
        {
            saved.AlterationObject.transform.localScale = _alteration.defaultScale;
            Player.Instance.CurrentThreads += GameManager.Instance.LevelStats.threadsCostForStretch;
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
    }

    private void Start()
    {
        alterationUIManager.ToggleVisiblity(false);
        _alterationType = AlterationType.None;
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
            if (Player.Instance.CurrentThreads >= GameManager.Instance.LevelStats.threadsCostForLevitate)
            {
                _alterationType = AlterationType.Levitate;
                alterationUIManager.OnAlterationSelected(AlterationType.Levitate);
                _alterationSaved.Add(new AlterationSaved
                    { AlterationObject = _alteration, AlterationType = AlterationType.Levitate });
                Player.Instance.CurrentThreads -= GameManager.Instance.LevelStats.threadsCostForLevitate;
            }

        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (Player.Instance.CurrentThreads >= GameManager.Instance.LevelStats.threadsCostForStretch)
            {
                _alterationType = AlterationType.Stretch;
                alterationUIManager.OnAlterationSelected(AlterationType.Stretch);
                _alterationSaved.Add(new AlterationSaved
                    { AlterationObject = _alteration, AlterationType = AlterationType.Stretch });
                Player.Instance.CurrentThreads -= GameManager.Instance.LevelStats.threadsCostForStretch;
            }

        }

        //not implemented
        /*/
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
        /*/
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

            if (_alteration.IsLevitating)
            {
                _alteration.SetColor(levitateColor);
            }
            else
            {
                _alteration.SetColor(defaultColor);
            }
            
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
