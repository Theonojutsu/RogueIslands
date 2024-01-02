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
            Debug.Log("shoote");
            rbAnchor.AddRelativeForce(new Vector3(0, -10000, 0), ForceMode.Impulse);
        }
    }
}
