using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAnchor : MonoBehaviour
{
    public Rigidbody rbAnchor;

    void Update()
    {
        if (Input.GetButtonDown("RightClic/R1"))
        {
            rbAnchor.AddForce(new Vector3(0,0,10000), ForceMode.Impulse);
            Debug.Log("shoote");
        }
    }
}
