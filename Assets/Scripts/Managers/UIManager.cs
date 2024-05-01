using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
[Serializable]
public struct Canvas
{
    public Transform CanvasComplete;
    public Transform CanvasAfterQuizMessage;
    public TextMeshProUGUI TxtQestions;
    public List<TextMeshProUGUI> TxtOptions;

}
public class UIManager : MonoBehaviour
{
    [SerializeField] private Canvas canvasGeneralQuestion;
    [SerializeField] private Canvas[] canvasSteamQuestion;
    [SerializeField] private TextMeshProUGUI txtResult;


    // private List<GeneralQuestionsData> loadedGeneralQuesitons;
    // private List<SteamQuestionData> loadedSteamQuestions;
    // int currGenInd = 0;
    int currSteamInd = 0;

    private void OnEnable()
    {

        QuestionsManager.EventGeneralQuizStart += OnGeneralAnwerGiven;
        QuestionsManager.EventOnGiveGeneralAnswer += OnGeneralAnwerGiven;
        QuestionsManager.EventGeneralQuizEnd += OnGeneralQuizEnd;

        QuestionsManager.EventSteamQuizStart += OnSteamAnwerGiven;
        QuestionsManager.EventOnGiveSteamAnswer += OnSteamAnwerGiven;
        QuestionsManager.EventSteamQuizEnd += OnSteamQuizEnd;

        QuestionsManager.EventGameOver += OnGameOver;

    }

    private void OnDisable()
    {
        QuestionsManager.EventGeneralQuizStart -= OnGeneralAnwerGiven;
        QuestionsManager.EventOnGiveGeneralAnswer -= OnGeneralAnwerGiven;
        QuestionsManager.EventGeneralQuizEnd -= OnGeneralQuizEnd;

        QuestionsManager.EventSteamQuizStart -= OnSteamAnwerGiven;
        QuestionsManager.EventOnGiveSteamAnswer -= OnSteamAnwerGiven;
        QuestionsManager.EventSteamQuizEnd -= OnSteamQuizEnd;

        QuestionsManager.EventGameOver -= OnGameOver;

    }
    public void OnGeneralAnwerGiven(GeneralQuestionsData questionsData)
    {
        UpdateGeneralQuestionUI(canvasGeneralQuestion, questionsData);
    }
    private void UpdateGeneralQuestionUI(Canvas currentCanvas, GeneralQuestionsData questionData)
    {
        currentCanvas.TxtQestions.GetComponent<ArabicFixerTMPRO>().fixedText
        =
        currentCanvas.TxtQestions.text = questionData.question;
        for (int i = 0; i < currentCanvas.TxtOptions.Count; i++)
        {
            currentCanvas.TxtOptions[i].GetComponent<ArabicFixerTMPRO>().fixedText
            =
            currentCanvas.TxtOptions[i].text = questionData.options[i];

        }

    }
    public void OnGeneralQuizEnd()
    {
        canvasGeneralQuestion.CanvasComplete.gameObject.SetActive(false);
        canvasGeneralQuestion.CanvasAfterQuizMessage.gameObject.SetActive(true);
    }


    public void OnSteamAnwerGiven(SteamQuestionData questionsData)
    {
        UpdateSteamQuestionUI(canvasSteamQuestion[currSteamInd], questionsData);
    }
    private void UpdateSteamQuestionUI(Canvas currentCanvas, SteamQuestionData questionData)
    {
        currentCanvas.TxtQestions.GetComponent<ArabicFixerTMPRO>().fixedText
        =
        currentCanvas.TxtQestions.text = questionData.question;
        for (int i = 0; i < currentCanvas.TxtOptions.Count; i++)
        {
            currentCanvas.TxtOptions[i].GetComponent<ArabicFixerTMPRO>().fixedText
            =
            currentCanvas.TxtOptions[i].text = questionData.options[i];
        }
        print(currSteamInd);

    }
    public void OnSteamQuizEnd()
    {
        canvasSteamQuestion[currSteamInd].CanvasComplete.gameObject.SetActive(false);
        canvasSteamQuestion[currSteamInd].CanvasAfterQuizMessage.gameObject.SetActive(true);
        currSteamInd++;
    }
    void OnGameOver(GeneralQuestionsResult generalResult, SteamQuestionResult steamResult)
    {
        float steamTotalQuestions = steamResult.TotalQuestions.Count;
        int steamTotalRightAnswers = 0;

        for (int i = 0; i < steamTotalQuestions; i++)
        {
            steamTotalRightAnswers += steamResult.TotalRightAnswers[i];
        }
        float generalResultPercentage = (generalResult.TotalRightAnswers * 100) / generalResult.TotalQuestions;
        float steamResultPercentage = (steamTotalRightAnswers * 100) / steamTotalQuestions;

        //so general percentage should not cross the limit of 100%
        generalResultPercentage = Mathf.Min(generalResultPercentage, 100);

        float totalPercentage = ((generalResultPercentage + steamResultPercentage) * 100) / 200;
        print(totalPercentage);
        // txtResult.text = "لقد حصلت على نسبة " + totalPercentage.ToString() + "%";
        // txtResult.GetComponent<ArabicFixerTMPRO>().fixedText = "لقد حصلت على نسبة " + totalPercentage.ToString() + "%";
        txtResult.text = totalPercentage.ToString() + "%";
        txtResult.GetComponent<ArabicFixerTMPRO>().fixedText = totalPercentage.ToString() + "%";
    }
    // }
    // public void LoadNextGeneralQuestion()
    // {
    //     UpdateGeneralQuestionUI(generalQuestion, loadedGeneralQuesitons[currGenInd++]);
    // }

    // public void OnGeneralQuizStart(SOGeneralQuestionsBox generalQuestionsBox)
    // {
    //     loadedGeneralQuesitons = generalQuestionsBox.questionsData;
    //     LoadNextGeneralQuestion();

    // }


    // private void UpdateSteamQuestionUI(Screens currentScreen, SteamQuestionData questionData)
    // {
    //     currentScreen.TxtQestions.text = questionData.question;
    //     for (int i = 0; i < currentScreen.TxtOptions.Count; i++)
    //     {
    //         currentScreen.TxtOptions[i].text = questionData.options[i];
    //     }

    // }

}