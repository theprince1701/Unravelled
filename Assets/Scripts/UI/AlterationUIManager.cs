using System;
using System.Collections.Generic;
using UnityEngine;

public class AlterationUIManager : MonoBehaviour
{
    public GameObject alterationBox;
    public List<AlterationIcon> alterationIcons = new List<AlterationIcon>();

    public void OnAlterationSelected(Alteration.AlterationType type)
    {
        foreach (AlterationIcon icon in alterationIcons)
        {
            if (icon.isSelected)
            {
                icon.PlayAnim(false);
            }
            if (icon.alterationType == type)
            {
                icon.PlayAnim(true);
            }
        }
    }

    public void OnAlterationReset()
    {
        foreach (AlterationIcon icon in alterationIcons)
        {
            if (icon.isSelected)
            {
                icon.PlayAnim(false);
            }
        }
    }

    public void ToggleVisiblity(bool isVisible)
    { 
        foreach (AlterationIcon icon in alterationIcons)
        {
            icon.PlayAnim(false);
        }
        alterationBox.SetActive(isVisible);   
    }
}
