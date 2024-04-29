using System;
using UnityEngine;
// [Serializable]
// public struct PlayerDataToSpawn
// {
//     public Transform CameraMain;
//     public Transform AvatarPlayyer;
//     public Transform CameraCinemachine;
//     public Transform CanvasPlayer;

// }
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
        // Instantiate(playerDataToSpawns[ind].CameraMain);
        // Instantiate(playerDataToSpawns[ind].AvatarPlayyer);
        // Instantiate(playerDataToSpawns[ind].CameraCinemachine);
        // Instantiate(playerDataToSpawns[ind].CanvasPlayer);
        quizLevel.gameObject.SetActive(true);
        playersToSpawn[ind].gameObject.SetActive(true);
        Cursor.visible = false;
    }

}