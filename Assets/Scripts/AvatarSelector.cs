using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarSelector : MonoBehaviour
{

    private int currInd;

    [SerializeField] Transform[] avatars;
    public static event Action<int> EventSpawnPlayer;

    public void SelectAvatar(int ind)
    {
        EventSpawnPlayer?.Invoke(ind);
        this.gameObject.SetActive(false);
    }
    public void Next()
    {
        avatars[currInd].gameObject.SetActive(false);
        currInd = (currInd + 1) % avatars.Length;
        avatars[currInd].gameObject.SetActive(true);
    }
    public void Previous()
    {
        avatars[currInd].gameObject.SetActive(false);
        currInd = (currInd + avatars.Length - 1) % avatars.Length;
        avatars[currInd].gameObject.SetActive(true);
    }

}
