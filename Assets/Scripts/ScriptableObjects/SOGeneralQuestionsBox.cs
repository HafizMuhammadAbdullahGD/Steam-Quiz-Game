using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct GeneralQuestionsData
{
    public string question;
    public List<string> options;
    public List<AnswerEfficencyLevel> answerEfficencyLevel;


}
[CreateAssetMenu(fileName = "GeneralQuestions", menuName = "GenerateQuestionBox/GeneralQuestions")]
public class SOGeneralQuestionsBox : ScriptableObject
{

    public QuestionsLevel questionLevel;

    public List<GeneralQuestionsData> questionsData;

}
