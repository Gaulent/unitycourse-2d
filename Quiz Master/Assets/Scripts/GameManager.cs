using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using Slider = UnityEngine.UI.Slider;

public class GameManager : MonoBehaviour
{
    [Header("Objetos de Interfaz")]
    public Text questionText;
    public Image timer;
    public GameObject normalUI;
    public GameObject gameOverUI;
    public Button[] answerButton;
    public Slider progressBar;
    public Text scoreText;

    [Header("Objetos de Juego")]
    public float maxAnswerTime = 10;
    public float timeBetweenQuestions = 2f;

    private float _remainingTime;
    private int _currentQuestion = 0;
    private int score = 0;
    private bool stopTime = false;
   
    
    private QuestionSO[] _questions;
    
    void Start()
    {
        _questions = Resources.LoadAll<QuestionSO>("");
        LoadQuestion();
    }

    private void LoadQuestion()
    {
        _remainingTime = maxAnswerTime;
        questionText.text = _questions[_currentQuestion].question;
        stopTime = false;
        
        for (int i = 0; i<4; i++)
            answerButton[i].GetComponent<ButtonController>().UpdateText(_questions[_currentQuestion].GetAnswer(i));
        for (int i = 0; i < 4; i++)
            answerButton[i].GetComponent<Button>().enabled = true;
        for (int i = 0; i < 4; i++)
            answerButton[i].GetComponent<ButtonController>().UnHightlightAnswer();
    }

    public void GuessAnswer(int answer)
    {
        if (_questions[_currentQuestion].rightAnswer == answer)
            RightAnswer();
        else
            WrongAnswer();
        
        EndOfQuestion();
    }

    public void RightAnswer()
    {
        score++;
        questionText.text = "Respuesta Correcta!";
    }
    
    public void WrongAnswer()
    {
        questionText.text = "Respuesta Incorrecta. La Correcta era la " + (_questions[_currentQuestion].rightAnswer +1);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!stopTime)
        {
            _remainingTime -= Time.deltaTime;
            if (_remainingTime <= 0)
                OutOfTime();
            timer.fillAmount = _remainingTime / maxAnswerTime;
        }
    }

    private void OutOfTime()
    {
        questionText.text = "Se acabó el tiempo! La Correcta era la " + (_questions[_currentQuestion].rightAnswer +1);
        EndOfQuestion();
    }

    private void EndOfQuestion()
    {
        answerButton[_questions[_currentQuestion].rightAnswer].GetComponent<ButtonController>().HightlightAnswer();
        stopTime = true;
        for (int i = 0; i < 4; i++)
            answerButton[i].GetComponent<Button>().enabled = false;

        scoreText.text = "Score: " + GetScoreString();
        progressBar.value = (_currentQuestion + 1f) / _questions.Length;
        
        if(_currentQuestion+1 >= _questions.Length)
            Invoke(nameof(GameOver), timeBetweenQuestions);
        else
        {
            _currentQuestion++;
            Invoke(nameof(LoadQuestion), timeBetweenQuestions);
        }
    }

    public void PlayAgain()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void GameOver()
    {
        normalUI.SetActive(false);
        gameOverUI.SetActive(true);
    }

    public string GetScoreString()
    {
        return (score * 100f / _questions.Length) + "%";
    }
}
