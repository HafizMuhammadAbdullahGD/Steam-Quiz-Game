using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionButtonEvents : MonoBehaviour
{

    [SerializeField] Color normalColor;
    [SerializeField] Color hoverColor;
    [SerializeField] Color clickColor;


    private void Start()
    {
        normalColor = GetComponent<Button>().image.color;
    }

    public void OnHoverEntered()
    {
        this.GetComponent<Button>().image.color = hoverColor;
    }
    public void OnHoverExit()
    {
        this.GetComponent<Button>().image.color = normalColor;
    }

    public void OnClick()
    {
        print("Clicked!");

        this.GetComponent<Button>().image.color = clickColor;
        this.GetComponent<Button>().onClick.Invoke();
    }
}
