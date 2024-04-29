using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public struct DoorsData
{
    public List<DoorAnimation> animDoors;

    [HideInInspector] public int currentDoor;
}
public class GameManager : MonoBehaviour
{
    [SerializeField] AudioSource audioDoorOpen;
    [SerializeField] private DoorsData quizDoorsData;
    [SerializeField] private DoorAnimation exitDoorsData;
    private void OnEnable()
    {
        QuestionsManager.EventGeneralQuizEnd += OnAnyQuizEnd;
        QuestionsManager.EventSteamQuizEnd += OnAnyQuizEnd;
    }
    void OnGerenalQuizEnd()
    {
        QuestionsManager.EventGeneralQuizEnd -= OnAnyQuizEnd;
        QuestionsManager.EventSteamQuizEnd -= OnAnyQuizEnd;

    }
    void OnAnyQuizEnd()
    {
        quizDoorsData.animDoors[quizDoorsData.currentDoor++].OpenDoor();
        audioDoorOpen.Play();
    }

}
