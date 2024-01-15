using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;


public class VirtualBoxItem : PuzzleItem //dial rotation & selected image updating
{
    public GameObject dial;
    public int numberOfAngles;
    public XRSimpleInteractable simpleInteractable;
    public GameObject debianImage;
    public GameObject linuxMintImage;
    public GameObject redhatImage;
    public GameObject ubuntuImage;
    private List<Vector3> currentRotations;
    private int currentRotationIndex = 0;
    private string currentRotationKey;


    private Dictionary<GameManager.VM_Socket, Vector3> imageRotations = new Dictionary<GameManager.VM_Socket, Vector3>
    {
        { GameManager.VM_Socket.Image2, new Vector3(90, 0, -90) },
        { GameManager.VM_Socket.Image3, new Vector3(160, 0, -90) },
        { GameManager.VM_Socket.Image4, new Vector3(225, 0, -90) },
        { GameManager.VM_Socket.Image1, new Vector3(30, 0, -90) }
    };

    private Dictionary<GameManager.VM_Ram, Vector3> ramRotations = new Dictionary<GameManager.VM_Ram, Vector3>
    {
        { GameManager.VM_Ram.Ram_2048MB, new Vector3(90, 0, -90) },
        { GameManager.VM_Ram.Ram_4096MB, new Vector3(150, 0, -90) },
        { GameManager.VM_Ram.Ram_8192MB, new Vector3(190, 0, -90) },
        { GameManager.VM_Ram.Ram_512MB, new Vector3(-15, 0, -90) },
        { GameManager.VM_Ram.Ram_1024MB, new Vector3(30, 0, -90) }
    };


    private void Start()
    {
        SetRotationList();
        UpdateRotation();
    }

    private void SetRotationList()
    {
        currentRotations = numberOfAngles == 4 ? new List<Vector3>(imageRotations.Values) : new List<Vector3>(ramRotations.Values);
    }

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
        RotateDial();
    }

    public void RotateDial()
    {
        currentRotationIndex = (currentRotationIndex + 1) % currentRotations.Count;
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        if (dial != null)
        {
            dial.transform.localEulerAngles = currentRotations[currentRotationIndex];
            UpdateCurrentRotationKey();
        }
    }

    private void UpdateCurrentRotationKey()
    {

        if (numberOfAngles == 4)    // image dial
        {
            foreach (var item in imageRotations)
            {
                if (item.Value == currentRotations[currentRotationIndex])
                {
                    currentRotationKey = item.Key.ToString();
                    GameManager.SelectedSocket = item.Key;
                    UpdateSelectedImage(item.Key);
                    break;
                }
            }
        }
        else    // ram dial
        {
            foreach (var item in ramRotations)
            {
                if (item.Value == currentRotations[currentRotationIndex])
                {
                    currentRotationKey = item.Key.ToString();
                    GameManager.SelectedRam = item.Key;
                    break;
                }
            }
        }
    }

    public void UpdateSelectedImage(GameManager.VM_Socket socket)
    {
        List<GameObject> images = new List<GameObject>()
        {
            debianImage, linuxMintImage, redhatImage, ubuntuImage
        };
        if (debianImage == null && linuxMintImage == null
            && redhatImage == null && ubuntuImage == null)
        {
            Debug.Log("Error! No images found by tag.");
            return;
        }

        float topY = 1.16f;
        float bottomY = 1.01f;
        float leftZ = 0.55f;
        float rightZ = 0.39f;
        /*
        Y 1.16	Y 1.16
        Z 0.55	Z 0.39

        Y 1.01 	Y 1.01 
        Z 0.55	Z 0.39
        */
        Vector3 imagePosition;
        bool imageFound = false;

        switch (socket)
        {
            case GameManager.VM_Socket.Image1:
                foreach (GameObject image in images)
                {
                    imagePosition = image.transform.position;
                    if (ImageInSocket(imagePosition, topY, leftZ))
                    {
                        GameManager.SelectedImage = GetImageNumber(image);
                        imageFound = true;
                        break;
                    }
                }
                break;
            case GameManager.VM_Socket.Image2:
                foreach (GameObject image in images)
                {
                    imagePosition = image.transform.position;
                    if (ImageInSocket(imagePosition, topY, rightZ))
                    {
                        GameManager.SelectedImage = GetImageNumber(image);
                        imageFound = true;
                        break;
                    }
                }
                break;
            case GameManager.VM_Socket.Image3:
                foreach (GameObject image in images)
                {
                    imagePosition = image.transform.position;
                    if (ImageInSocket(imagePosition, bottomY, leftZ))
                    {
                        GameManager.SelectedImage = GetImageNumber(image);
                        imageFound = true;
                        break;
                    }
                }
                break;
            case GameManager.VM_Socket.Image4:
                foreach (GameObject image in images)
                {
                    imagePosition = image.transform.position;
                    if (ImageInSocket(imagePosition, bottomY, rightZ))
                    {
                        GameManager.SelectedImage = GetImageNumber(image);
                        imageFound = true;
                        break;
                    }
                }
                break;
        }
        if (!imageFound)
        {
            GameManager.SelectedImage = GameManager.VM_Image.Empty;
        }
        Debug.Log(GameManager.SelectedImage);
    }

    private bool ImageInSocket(Vector3 imagePosition, float y, float z)
    {
        float tolerance = 0.01f;
        return Mathf.Abs(imagePosition.y - y) < tolerance
                        && Mathf.Abs(imagePosition.z - z) < tolerance;
    }

    private GameManager.VM_Image GetImageNumber(GameObject image)
    {
        if (image != null)
        {
            switch (image.tag)
            {
                case "debian":
                    return GameManager.VM_Image.Debian;
                case "linux_mint":
                    return GameManager.VM_Image.LinuxMint;
                case "redhat":
                    return GameManager.VM_Image.Redhat;
                case "ubuntu":
                    return GameManager.VM_Image.Ubuntu;
            }
        }
        return GameManager.VM_Image.Empty;
    }
}