using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LockerButtonItem : MonoBehaviour
{
    public XRSimpleInteractable simpleInteractable; // Reference to your XR Simple Interactable
    public int lockerNumber;
    public string number;

    private Renderer objectRenderer;
    private Color originalColor;
    private int delay = 300;
    private int counter = 0;

    private void Start()
    {
        objectRenderer = gameObject.GetComponent<Renderer>();
    }

    private void Update()
    {
        counter++;
        Debug.Log(counter);

        switch (lockerNumber)
        {
            case (int)GameManager.Locker.Red:
                break;
            case (int)GameManager.Locker.Coded1:
                if (GameManager.codedLocker1Opened)
                {
                    objectRenderer.material.color = Color.green;
                }

                if (counter > 0.9 * delay && GameManager.resettingLockerColor1 && !originalColor.Equals(default(Color)))
                {
                    objectRenderer.material.color = originalColor;
                }

                if (counter == delay) { 
                    counter = 0;
                    GameManager.resettingLockerColor1 = false;
                }
                break;
            case (int)GameManager.Locker.Coded2:
                if (GameManager.codedLocker2Opened)
                {
                    objectRenderer.material.color = Color.green;
                }

                if (counter > 0.9 * delay && GameManager.resettingLockerColor2 && !originalColor.Equals(default(Color)))
                {
                    objectRenderer.material.color = originalColor;
                }

                if (counter == delay)
                {
                    counter = 0;
                    GameManager.resettingLockerColor2 = false;
                }
                break;
        }
        
    }

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
        PutInNumber();
    }

    public void PutInNumber()
    {
        switch (lockerNumber)
        {
            case (int)GameManager.Locker.Red:
                break;
            case (int)GameManager.Locker.Coded1:
                PutInNumberLocker1();
                break;
            case (int)GameManager.Locker.Coded2:
                PutInNumberLocker2();
                break;
        }
    }

    private void PutInNumberLocker1()
    {
        originalColor = objectRenderer.material.color;
        objectRenderer.material.color = Color.gray;
        GameManager.currentCombination1 += number;

        if (GameManager.currentCombination1.Length == 4)
        {
            if (GameManager.currentCombination1 == GameManager.code1)
            {
                GameManager.codedLocker1Opened = true;
            }
            GameManager.currentCombination1 = "";
            GameManager.resettingLockerColor1 = true;
        }
    }

    private void PutInNumberLocker2()
    {
        originalColor = objectRenderer.material.color;
        objectRenderer.material.color = Color.gray;
        GameManager.currentCombination2 += number;

        if (GameManager.currentCombination2.Length == 4)
        {
            if (GameManager.currentCombination2 == GameManager.code2)
            {
                GameManager.codedLocker2Opened = true;
            }
            GameManager.currentCombination2 = "";
            GameManager.resettingLockerColor2 = true;
        }
    }
}
