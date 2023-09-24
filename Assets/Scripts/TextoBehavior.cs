using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextoBehavior : MonoBehaviour
{
    [SerializeField] private Text textToControl;

    public void ShowText(string newText)
    {
        if (textToControl != null)
        {
            textToControl.text = newText;
            textToControl.enabled = true; // Ativa o objeto Text
        }
    }

    // Método para apagar o texto
    public void HideText()
    {
        if (textToControl != null)
        {
            textToControl.text = "";
            textToControl.enabled = false; // Desativa o objeto Text
        }
    }
}
