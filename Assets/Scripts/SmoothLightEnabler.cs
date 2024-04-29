using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class SmoothLightEnabler : MonoBehaviour
{
    [SerializeField] Light currLight;

    [SerializeField] float t = 1;
    private void OnDisable()
    {
        currLight.intensity = 0;
    }
    private void Update()
    {
        if (currLight.intensity >= 0.95f) return;
        currLight.intensity = Mathf.Lerp(currLight.intensity, 1, t * Time.deltaTime);
    }


}
