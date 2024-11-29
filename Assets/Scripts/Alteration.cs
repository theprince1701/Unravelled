using System;
using UnityEngine;

public class Alteration : MonoBehaviour
{
    public enum AlterationType
    {
        Levitate,
        Stretch,
        GravityUp,
        GravityDown
    }
    
    private AlterationObject _alteration;
    private AlterationType _alterationType;
    private bool _isAltering;
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out AlterationObject alteration))
        {
            _alteration = alteration;
            _alteration.OnLookAt();
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out AlterationObject alteration))
        {
            _alteration.StopLookAt();
            _alteration = null;
        }
    }
    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_isAltering)
            {
                _alteration.StopAltering();
                _isAltering = true;
            }
            else
            {
                _alteration.StartAltering();
                _isAltering = false;
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
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
              _alterationType = AlterationType.Stretch;  
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
           _alterationType = AlterationType.GravityUp;     
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _alterationType = AlterationType.GravityDown;
        }
    }

    void HandleStretch()
    {
        
    }

    void HandleGravity(bool up)
    {
        
    }

    void HandleLevitate()
    {
        
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
    }

}
