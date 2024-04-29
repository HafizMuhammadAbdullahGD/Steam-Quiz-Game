using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SteamQuestionData
{
    public string question;
    public List<string> options;
    public int answeInd;

}
[CreateAssetMenu(fileName = "SteamQuestions_Type", menuName = "GenerateQuestionBox/SteamQuestions")]

public class SOSteamQuestionsBox : ScriptableObject
{
    public QuestionsLevel questionDifficultyLevel;
    public List<SteamQuestionData> questionsData;
}
