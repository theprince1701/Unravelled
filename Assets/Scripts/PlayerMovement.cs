using System;
using System.Collections;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float timeToMove = .2f;

    private bool _isMoving;
    private Vector2 _originalPos;
    private Vector2 _targetPos;
    
    private void Update()
    {
        if (_isMoving)
        {
            return;
        }
        
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(MovePlayer(Vector2.up));
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(MovePlayer(Vector2.left));
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(MovePlayer(Vector2.down));
        }
        
        if (Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(MovePlayer(Vector2.right));
        }
    }

    private IEnumerator MovePlayer(Vector2 dir)
    {
        _isMoving = true;

        float elapsedTime = 0;
        _originalPos = transform.position;
        _targetPos = _originalPos + dir;

        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(_originalPos, _targetPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = _targetPos;
        _isMoving = false;
    }
}
