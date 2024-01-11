using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZipFolderItem : PuzzleItem
{
    public GameObject endpoint1;
    public GameObject endpoint2;
    public GameObject zipper1;
    public GameObject zipper2;
    public GameObject picture;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == endpoint1) //open position
        {
            zipper1.transform.localEulerAngles = new Vector3(-90, 0, 180);
            zipper2.transform.localEulerAngles = new Vector3(-90, 180, 0);

            zipper1.transform.localPosition = new Vector3(0.01f, 0.15f, 0.221f);
            zipper2.transform.localPosition = new Vector3(-0.01f, 0.14f, 0.272f);

            //picture.transform.position = transform.position + new Vector3(0.483f, 0.031f, 0.003f);
            picture.SetActive(true);
            picture.transform.SetParent(null, true); ;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == endpoint1)
        {
            zipper1.transform.localEulerAngles = new Vector3(0, 0, 180);
            zipper2.transform.localEulerAngles = new Vector3(0, 180, 0);

            zipper1.transform.localPosition = new Vector3(0.01f, 0.15f, 0.25f);
            zipper2.transform.localPosition = new Vector3(-0.01f, 0.14f, 0.26f);
        }
    }
}
