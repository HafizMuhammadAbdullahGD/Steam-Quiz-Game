using System;
using UnityEngine;
public class PlayerSpawnManager : MonoBehaviour
{
    [SerializeField] private Transform[] playersToSpawn;
    [SerializeField] private Transform quizLevel;
    private void OnEnable()
    {
        AvatarSelector.EventSpawnPlayer += OnPlayerSpawnCalled;
    }
    private void OnDisable()
    {
        AvatarSelector.EventSpawnPlayer -= OnPlayerSpawnCalled;

    }
    private void OnPlayerSpawnCalled(int ind)
    {

        quizLevel.gameObject.SetActive(true);
        playersToSpawn[ind].gameObject.SetActive(true);
        Cursor.visible = false;
    }

}