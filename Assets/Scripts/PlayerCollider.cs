using System;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField] private AlterationObject alterationObject;


    public void OnMouseDown()
    {
        alterationObject.OnMouseDown();
    }

    public void OnMouseUp()
    {
        alterationObject.OnMouseUp();
    }
}
