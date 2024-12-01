using System.Collections;
using TMPro;
using UnityEngine;

public class TextBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textUI;
    [SerializeField] private float delayBetweenLetters = 0.05f;

    public void SayString(string text)
    {
        StartCoroutine(AnimateTextCoroutine(text));
    }
    
    private IEnumerator AnimateTextCoroutine(string text)
    {
        textUI.text = ""; 
        foreach (char letter in text)
        {
            textUI.text += letter; 
            yield return new WaitForSeconds(delayBetweenLetters); 
        }


        yield return new WaitForSeconds(1f);
        textUI.text = "";
    } 
}
