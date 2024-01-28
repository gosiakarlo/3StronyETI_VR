using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractableObject : MonoBehaviour
{
    // Add variables for the correct position and a flag to track if the object is in the correct position
    public Vector3 correctPosition;
    private bool isCorrectPosition = false;

    // Update is called once per frame
    void Update()
    {
        // Implement logic to check if the object is in the correct position
        if (Vector3.Distance(transform.position, correctPosition) < 0.1f)
        {
            isCorrectPosition = true;
        }
        else
        {
            isCorrectPosition = false;
        }
    }

    public bool IsInCorrectPosition()
    {
        return isCorrectPosition;
    }
}

