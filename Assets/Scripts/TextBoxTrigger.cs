using System;
using UnityEngine;

public class TextBoxTrigger : MonoBehaviour
{
    [SerializeField] private string textToSay;
    [SerializeField] private bool onlyPlayOnce = true;
    [SerializeField] private bool destroyOnPlay;

    private TextBox _textBox;
    private bool _played;
    
    private void Start()
    {
        _textBox = FindFirstObjectByType<TextBox>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (onlyPlayOnce && !_played)
            {
                _textBox.SayString(textToSay);
                _played = true;

                if (destroyOnPlay)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
