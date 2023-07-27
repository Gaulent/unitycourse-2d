using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public Text scoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager _myGM = FindObjectOfType<GameManager>();
        scoreText.text = "Congratulations!\nYou scored " + _myGM.GetScoreString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
