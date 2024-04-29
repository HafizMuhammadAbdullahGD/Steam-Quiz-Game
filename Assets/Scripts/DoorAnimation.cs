using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    public void OpenDoor()
    {
        this.GetComponent<Animator>().Play("DoorOpening");
    }
}