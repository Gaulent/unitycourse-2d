using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int Score = 0;
    public Text text;
    
    // Start is called before the first frame update
    void Start()
    {
        text = FindObjectOfType<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = Score.ToString();
    }

    public void AddPoints()
    {
        Score++;
        Debug.Log(Score);
    }
}
