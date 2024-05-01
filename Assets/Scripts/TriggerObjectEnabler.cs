using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObjectEnabler : MonoBehaviour
{
    [SerializeField] Transform objectToEnable;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objectToEnable.gameObject.SetActive(true);
        }
    }
}
