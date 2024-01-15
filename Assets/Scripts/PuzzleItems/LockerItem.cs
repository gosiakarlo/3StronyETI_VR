using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LockerItem : PuzzleItem
{
    public XRSimpleInteractable simpleInteractable; // Reference to your XR Simple Interactable
    public GameObject lockerClosed; // Reference to your closed locker GameObject
    public GameObject lockerOpened; // Reference to your opened locker GameObject
    public GameObject zipFolder;
    public int lockerNumber;

    private void OnEnable()
    {
        // Subscribe to the event
        simpleInteractable.activated.AddListener(HandleActivated);
    }

    private void OnDisable()
    {
        // Unsubscribe from the event
        simpleInteractable.activated.RemoveListener(HandleActivated);
    }

    private void HandleActivated(ActivateEventArgs arg)
    {
        OpenTheLocker();
    }

    public void OpenTheLocker()
    {
        switch(lockerNumber)
        {
            case (int)GameManager.Locker.Red:
                OpenRedLocker();
                break;
            case (int)GameManager.Locker.Coded1:
                OpenCodedLocker1();
                break;
            case (int)GameManager.Locker.Coded2:
                break;
        }
    }

    private void OpenRedLocker()
    {
        lockerOpened.SetActive(!lockerOpened.activeSelf);
        lockerClosed.SetActive(!lockerClosed.activeSelf);   //!lockerClosed.activeSelf
        // aktywowanie czêœci do anteny
    }

    private void OpenCodedLocker1()
    {
        if (GameManager.codedLocker1Opened)
        {
            lockerOpened.SetActive(!lockerOpened.activeSelf);
            lockerClosed.SetActive(!lockerClosed.activeSelf);   //!lockerClosed.activeSelf
            zipFolder.SetActive(true);
        }
    }
}
