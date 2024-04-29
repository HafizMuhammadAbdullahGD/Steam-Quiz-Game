using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtsEnabler : MonoBehaviour
{
    [SerializeField] Transform artsToEnable;
    [SerializeField] Material skyMaterial;

    private void OnTriggerEnter(Collider other)
    {
        RenderSettings.skybox = skyMaterial;
        CancelInvoke("OffArts");
        if (other.transform.CompareTag("Player"))
        {
            artsToEnable.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.transform.CompareTag("Player"))
        {
            Invoke("OffArts", 1f);
        }
    }
    void OffArts()
    {
        RenderSettings.skybox = null;

        artsToEnable.gameObject.SetActive(false);

    }

}
