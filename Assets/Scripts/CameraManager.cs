using System;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float moveSpeed;
    
    private void Update()
    {
       Vector3 targetPos = Vector2.Lerp(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
       targetPos.z = -10;
       transform.position = targetPos;
    }
}
