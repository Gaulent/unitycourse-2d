using UnityEngine;

[CreateAssetMenu(fileName = "question", menuName = "ScriptableObjects/Question", order = 1)]
public class QuestionSO : ScriptableObject
{
    public string question;
    public string[] answers;
    public int rightAnswer;

    public string GetAnswer(int number)
    {
        if (answers.Length > number && number>=0)
            return answers[number];
        return "";
    }
}