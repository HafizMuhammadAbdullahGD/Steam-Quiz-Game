using UnityEngine;
using UnityEngine.UI;


public class ButtonOptionsHover : MonoBehaviour
{

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