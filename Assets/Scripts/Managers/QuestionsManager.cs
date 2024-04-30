using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[Serializable]
public struct GeneralQuestionsResult
{
    public int TotalQuestions;
    public int TotalRightAnswers;
}
[Serializable]
public struct SteamQuestionResult
{
    public List<int> TotalQuestions;
    public List<int> TotalRightAnswers;


}
public class QuestionsManager : MonoBehaviour
{
    [SerializeField] private GeneralQuestionsResult generalQuestionsResult;
    [SerializeField] private SteamQuestionResult steamQuestionsResult;

    [SerializeField] private List<SOSteamQuestionsBox> steamSelectedAllQuizQuestions;
    [SerializeField] private List<SteamQuestionData> currSteamQuiz;

    // [SerializeField] private QuestionsLevel selectedPlayerLevel;

    [SerializeField] private SOGeneralQuestionsBox generalQuesitons;
    [SerializeField] private SOSteamQuestionsBox[] steamEasyQuestions;
    [SerializeField] private SOSteamQuestionsBox[] steamMediumQuestions;
    [SerializeField] private SOSteamQuestionsBox[] steamHighQuestions;

    public static event Action<GeneralQuestionsData> EventOnGiveGeneralAnswer;
    public static event Action<SteamQuestionData> EventOnGiveSteamAnswer;
    public static event Action<GeneralQuestionsData> EventGeneralQuizStart;
    public static event Action<SteamQuestionData> EventSteamQuizStart;

    public static event Action<GeneralQuestionsResult, SteamQuestionResult> EventGameOver;
    public static event Action EventGeneralQuizEnd;
    public static event Action EventSteamQuizEnd;



    private int currGenInd = 0;

    private int currSteamQuesInd = 0;
    private int currSteamInd = 0;

    private void Start()
    {
        Init();

        //Call Events When General Quiz Start
        EventGeneralQuizStart?.Invoke(generalQuesitons.questionsData[currGenInd]);


    }
    void Init()
    {
        // Init total Poitns
        int totalMaxPointOnEachRightAnswer = 3;
        generalQuestionsResult.TotalQuestions = generalQuesitons.questionsData.Count * totalMaxPointOnEachRightAnswer;


        //Init Total Steam Questions & Answers Data
        steamSelectedAllQuizQuestions = new List<SOSteamQuestionsBox>();
        currSteamQuiz = new List<SteamQuestionData>();
        //As All quizes have same length, so I assign easyQuestions Count
        // int totalSteamQuizQuestions = steamEasyQuestions.Length;

        // steamQuestionsResult.TotalQuestions = new List<int>(totalSteamQuizQuestions);
        // steamQuestionsResult.TotalRightAnswers = new List<int>(totalSteamQuizQuestions);

    }


    private void AnalyzeGeneralAnswer(int answerInd)
    {
        AnswerEfficencyLevel efficencyLevel = generalQuesitons.questionsData[currGenInd].answerEfficencyLevel[answerInd];
        switch (efficencyLevel)
        {
            case AnswerEfficencyLevel.Low:
                generalQuestionsResult.TotalRightAnswers += 1;

                break;

            case AnswerEfficencyLevel.Medium:
                generalQuestionsResult.TotalRightAnswers += 2;

                break;

            case AnswerEfficencyLevel.High:
                generalQuestionsResult.TotalRightAnswers += 3;

                break;
        }
        print(efficencyLevel);
    }
    void AssignSteamQuiz(SOSteamQuestionsBox[] allQuizes)
    {
        foreach (var quiz in allQuizes)
        {
            steamSelectedAllQuizQuestions.Add(quiz);
        }

        currSteamQuiz = steamSelectedAllQuizQuestions[currSteamInd].questionsData;
        EventSteamQuizStart?.Invoke(currSteamQuiz[currSteamQuesInd]);
    }
    private void AssignSteamQuestionsAccordingToEfficeny()
    {
        float mediumPercentage = 55.0f;
        float highPercentage = 70.0f;

        float resultPercentage = (generalQuestionsResult.TotalRightAnswers * 100) / generalQuestionsResult.TotalQuestions;
        print("Result" + resultPercentage);

        if (resultPercentage < mediumPercentage)
        {
            AssignSteamQuiz(steamEasyQuestions);
        }
        else if (resultPercentage > mediumPercentage && resultPercentage < highPercentage)
        {
            AssignSteamQuiz(steamMediumQuestions);
        }
        else
        {
            AssignSteamQuiz(steamHighQuestions);

        }




    }

    public void GiveGeneralAnswer(int answerInd)
    {
        AnalyzeGeneralAnswer(answerInd);

        if (++currGenInd >= generalQuesitons.questionsData.Count)
        {

            print("Genereal Quiz Completed");

            AssignSteamQuestionsAccordingToEfficeny();

            EventGeneralQuizEnd?.Invoke();

            return;
        }
        EventOnGiveGeneralAnswer?.Invoke(generalQuesitons.questionsData[currGenInd]);


    }
    public void GiveSteamAnswer(int answerInd)
    {

        if (!AnalyzeSteamAnswer(answerInd))
        {
            return;
        }
        if (++currSteamQuesInd >= currSteamQuiz.Count)
        {
            EventSteamQuizEnd?.Invoke();
            NextSteamQuiz();

            return;
        }
        //Broadcast Next Question Data
        EventOnGiveSteamAnswer?.Invoke(currSteamQuiz[currSteamQuesInd]);
    }
    private bool AnalyzeSteamAnswer(int answerInd)
    {
        if (currSteamQuiz[currSteamQuesInd].answeInd == answerInd)
        {
            steamQuestionsResult.TotalRightAnswers[currSteamInd]++;
            return true;
        }
        else
        {
            return false;
        }

    }
    private void NextSteamQuiz()
    {

        if (++currSteamInd >= steamSelectedAllQuizQuestions.Count)
        {

            EventGameOver?.Invoke(generalQuestionsResult, steamQuestionsResult);
            return;

        }

        currSteamQuesInd = 0;

        currSteamQuiz = steamSelectedAllQuizQuestions[currSteamInd].questionsData;

        EventSteamQuizStart?.Invoke(currSteamQuiz[currSteamQuesInd]);

    }

}
