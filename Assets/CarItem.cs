using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarItem : PuzzleItem
{
    Rigidbody m_Rigidbody;
    float m_Speed;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Speed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();
    }

    public void MoveForward()
    {
        transform.position += transform.forward * Time.deltaTime * m_Speed;
    }
}
