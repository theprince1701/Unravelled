using TMPro;
using UnityEngine;

public class AlterationIcon : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [Space] 
    
    [SerializeField] private TextMeshProUGUI inputText;

    public Alteration.AlterationType alterationType;

    public bool isSelected { get; set; }
    
    public void Init(int i)
    {
        inputText.text = (i+1).ToString();
    }
    
    public void PlayAnim(bool bSelect)
    {
        if (bSelect)
        {
            animator.Play("Select");
            isSelected = true;
        }
        else
        {
            animator.Play("Deselect");
            isSelected = false;
        }
    }
}
