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
        lockerOpened.SetActive(!lockerOpened.activeSelf);
        lockerClosed.SetActive(!lockerClosed.activeSelf);   //!lockerClosed.activeSelf
        zipFolder.SetActive(true);
    }
}
