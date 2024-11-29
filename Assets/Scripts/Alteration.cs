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
        GravityDown
    }

    [SerializeField] private float tileDist = 1f;
    
    private AlterationObject _alteration;
    private AlterationType _alterationType;
    private bool _isAltering;
    private bool _setInitialPositionStretch;
    
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
                _isAltering = false;
            }
            else
            {
                _alteration.StartAltering();
                _isAltering = true;
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
        if (!_setInitialPositionStretch)
        {
            transform.position = transform.position + (transform.position - _alteration.transform.position).normalized * tileDist;
            _setInitialPositionStretch = true;
        }
        
        Vector2 direction = (_alteration.transform.position - transform.position);
        float dist = direction.magnitude;

        Vector3 scale = Vector2.one;
        scale.x = dist;
        _alteration.transform.localScale = scale;
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _alteration.transform.rotation = Quaternion.Euler(0, 0, angle); 
        
        bool stretchingLeft = _alteration.transform.localScale.x > 0;

        Vector3 offset = direction.normalized * (dist / 2);

        if (stretchingLeft)
        {
          //  _alteration.transform.position =  _alteration.transform.position - offset;
        }
        else
        {
          //  _alteration.transform.position =  _alteration.transform.position + offset;
        }
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
            Debug.Log(_alterationType);

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
    }

}
