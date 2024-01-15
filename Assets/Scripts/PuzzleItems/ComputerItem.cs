using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ComputerItem : PuzzleItem
{
    public GameObject runButton;
    public GameObject desktopBckgDebian;
    public GameObject desktopBckgDebianPassword;
    public GameObject desktopBckgLinuxMint;
    public GameObject desktopBckgRedhat;
    public GameObject desktopBckgUbuntu;
    public XRSimpleInteractable simpleInteractable;

    private GameObject currentDesktop;

    private void OnEnable()
    {
        simpleInteractable.activated.AddListener(HandleActivated);
    }

    private void OnDisable()
    {
        simpleInteractable.activated.RemoveListener(HandleActivated);
    }

    private void HandleActivated(ActivateEventArgs arg)
    {
        TurnOnVM();
    }

    public void TurnOnVM()
    {
        if (GameManager.isComputerOn)
        {
            desktopBckgUbuntu.SetActive(true);
            //if 
        }
    }
}
