using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Sprite defaultAnswer;
    public Sprite rightAnswer;
    private Image _image;
    
    // Start is called before the first frame update
    void Awake()
    {
        _image = GetComponent<Image>();
        UnHightlightAnswer();
    }
    
    public void UnHightlightAnswer()
    {
        _image.sprite = defaultAnswer;
    }

    public void HightlightAnswer()
    {
        _image.sprite = rightAnswer;
    }

    public void UpdateText(string myText)
    {
        GetComponentInChildren<Text>().text = myText;
    }
}
