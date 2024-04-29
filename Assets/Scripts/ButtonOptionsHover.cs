using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

public class ButtonOptionsHover : MonoBehaviour
{
    //     GraphicRaycaster m_Raycaster;
    //     PointerEventData m_PointerEventData;
    //     EventSystem m_EventSystem;

    //     void Start()
    //     {
    //         //Fetch the Raycaster from the GameObject (the Canvas)
    //         m_Raycaster = GetComponent<GraphicRaycaster>();
    //         //Fetch the Event System from the Scene
    //         m_EventSystem = GetComponent<EventSystem>();
    //     }
    //     public void Entered()
    //     {
    //         print(1);
    //     }
    //     void Update()
    //     {
    //         return;
    //         // //Check if the left Mouse button is clicked

    //         // //Set up the new Pointer Event
    //         // m_PointerEventData = new PointerEventData(m_EventSystem);
    //         // //Set the Pointer Event Position to that of the mouse position
    //         // m_PointerEventData.position = new Vector2(Screen.width / 2, Screen.height / 2);

    //         // //Create a list of Raycast Results
    //         // List<RaycastResult> results = new List<RaycastResult>();

    //         // //Raycast using the Graphics Raycaster and mouse click position
    //         // m_Raycaster.Raycast(m_PointerEventData, results);

    //         // //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
    //         // foreach (RaycastResult result in results)
    //         // {
    //         //     Debug.Log("Hit " + result.gameObject.name);
    //         // }
    //     }
    Button BtncurrHit;
    Button BtnprevHit;
    private void Start()
    {
        BtnprevHit = BtncurrHit;

    }
    private void Update()
    {

        GetHitTransform()?.TryGetComponent<Button>(out BtncurrHit);

        if (BtnprevHit != null && BtncurrHit != BtnprevHit)
        {
            GetOptionButtonEvents(BtnprevHit)?.OnHoverExit();
            BtnprevHit = BtncurrHit;
        }

        if (BtncurrHit)
        {
            var optionButtonEvents = GetOptionButtonEvents(BtncurrHit);

            if (Input.GetMouseButtonDown(0))
            {
                OnButtonClick(optionButtonEvents);
            }
            else
            {
                OnButtonHover(optionButtonEvents);
            }
            BtnprevHit = BtncurrHit;
        }
    }
    Transform GetHitTransform()
    {
        RaycastHit rayHit;
        Physics.Raycast(GetRay(), out rayHit);
        return rayHit.transform;

    }
    Ray GetRay()
    {

        //  Ray ray = new Ray(rayStartPoint.position, rayStartPoint.forward);
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        return ray;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(GetRay());
    }
    OptionButtonEvents GetOptionButtonEvents(Button btn)
    {
        OptionButtonEvents optionButtonEvents;
        btn.TryGetComponent<OptionButtonEvents>(out optionButtonEvents);
        return optionButtonEvents;
    }
    void OnButtonHover(OptionButtonEvents optionButtonEvents)
    {
        optionButtonEvents?.OnHoverEntered();
    }
    void OnButtonClick(OptionButtonEvents optionButtonEvents)
    {
        optionButtonEvents?.OnClick();
    }

}