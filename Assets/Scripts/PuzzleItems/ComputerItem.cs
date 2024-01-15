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
    public GameObject imageDial;
    public XRSimpleInteractable simpleInteractable;
    private VirtualBoxItem virtualBoxItem;

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
            DeactivateAllDesktops();
            virtualBoxItem = imageDial.GetComponent<VirtualBoxItem>();
            virtualBoxItem.UpdateSelectedImage(GameManager.SelectedSocket);
            switch (GameManager.SelectedImage)
            {
                case GameManager.VM_Image.Debian:
                    if (GameManager.SelectedRam == GameManager.VM_Ram.Ram_1024MB)
                    {
                        desktopBckgDebianPassword.SetActive(true);
                    }
                    else
                    {
                        desktopBckgDebian.SetActive(true);
                    }
                    break;
                case GameManager.VM_Image.LinuxMint:
                    desktopBckgLinuxMint.SetActive(true);
                    break;
                case GameManager.VM_Image.Redhat:
                    desktopBckgRedhat.SetActive(true);
                    break;
                case GameManager.VM_Image.Ubuntu:
                    desktopBckgUbuntu.SetActive(true);
                    break;
                case GameManager.VM_Image.Empty:
                    break;
            }
        }
    }

    private void DeactivateAllDesktops()
    {
        desktopBckgDebian.SetActive(false);
        desktopBckgDebianPassword.SetActive(false);
        desktopBckgLinuxMint.SetActive(false);
        desktopBckgRedhat.SetActive(false);
        desktopBckgUbuntu.SetActive(false);
    }
}
