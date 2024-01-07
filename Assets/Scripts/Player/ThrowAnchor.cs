using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAnchor : MonoBehaviour
{
    [SerializeField] GameObject anchorPrefab;
    [SerializeField] Transform chainEndRight;
    [SerializeField] Transform chainEndLeft;
    [SerializeField] Rigidbody rbPlayer;
    [SerializeField] LineRenderer lineRight;
    [SerializeField] LineRenderer lineLeft;
    [SerializeField] int powerThrow = 30000;

    GameObject anchorRight;
    GameObject anchorLeft;
    Rigidbody rbAnchorRight;
    Rigidbody rbAnchorLeft;
    bool doOnceRight = false;
    bool doOnceLeft = false;

    void Update()
    {
        LaunchAnchor(ref anchorRight,ref rbAnchorRight, "RightClic/R1",ref doOnceRight, lineRight, chainEndRight);
        LaunchAnchor(ref anchorLeft,ref rbAnchorLeft, "LeftClic/L1",ref doOnceLeft, lineLeft, chainEndLeft);
    }

    void LaunchAnchor(ref GameObject currentAnchor, ref Rigidbody rbAnchor, string inputShoot, ref bool doOnce, LineRenderer line, Transform chainEnd)
    {
        if (Input.GetButtonDown(inputShoot))
        {
            // Détruire l'ancienne ancre s'il y en a une
            if ((currentAnchor != null) && (line.enabled == true))
            {
                Destroy(currentAnchor);
                doOnce = false;
            }

            // Target
            Vector3 cursorPosition = GetCursorPosition();
            Vector3 shootDirection = cursorPosition - transform.position;

            Quaternion rotation = Quaternion.LookRotation(shootDirection);
            currentAnchor = Instantiate(anchorPrefab, transform.position, rotation);

            // Chaine
            Transform chainHandleTransfo = currentAnchor.transform.GetChild(0);
            chainEnd.position = chainHandleTransfo.position;
            chainEnd.parent = chainHandleTransfo;


            // Shooter l'ancre
            rbAnchor = currentAnchor.GetComponent<Rigidbody>();
            rbAnchor.AddForce(shootDirection.normalized * powerThrow, ForceMode.Impulse);

            line.enabled = true;
        }
    }

    //void LateUpdate()
    //{
    //    //if ((anchorRight != null) && (!doOnce))
    //    //{
    //    //    float distance = Vector3.Distance(rbAnchor.position, transform.position);

    //    //    float anchorVel = rbAnchor.velocity.magnitude;

    //    //    if ((distance >= 15f) && (anchorVel >= 15))
    //    //    {
    //    //        doOnce = true;
    //    //        ApplyVelocity();
    //    //    }
    //    //}
    //}

    //void ApplyVelocity()
    //{
    //    //rbPlayer.velocity = Vector3.zero;
    //    //rbPlayer.angularVelocity = Vector3.zero;

    //    //// Dash
    //    //Vector3 dashDirection = rbAnchor.position - transform.position;

    //    //rbPlayer.AddForce(dashDirection.normalized * 15f, ForceMode.Impulse);
    //}

    Vector3 GetCursorPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        return ray.GetPoint(10f);
    }
}

//using UnityEngine;

//public class ThrowAnchor : MonoBehaviour
//{
//    [SerializeField] GameObject anchorPrefab;
//    [SerializeField] Rigidbody rbPlayer;
//    GameObject currentAnchor;
//    Rigidbody rbAnchor;
//    bool doOnce = false;

//    void Update()
//    {
//        if (Input.GetButtonDown("RightClic/R1"))
//        {
//            // Détruire l'ancienne ancre s'il y en a une
//            if (currentAnchor != null)
//            {
//                Destroy(currentAnchor);
//                doOnce = false;
//            }

//            Vector3 cursorPosition = GetCursorPosition();
//            Vector3 shoorDirection = cursorPosition - transform.position;

//            Quaternion rotation = Quaternion.LookRotation(shoorDirection);
//            currentAnchor = Instantiate(anchorPrefab, transform.position, rotation);

//            rbAnchor = currentAnchor.GetComponentInChildren<Rigidbody>();
//            rbAnchor.AddForce(shoorDirection.normalized * 45000f, ForceMode.Impulse);
//        } 
//    }

//    void LateUpdate()
//    {
//        if ((currentAnchor != null) && (doOnce == false))
//        {
//            float distance = Vector3.Distance(currentAnchor.transform.position, transform.position);
//            //Debug.Log("distance :" + distance);

//            if (distance >= 5f)
//            {
//                doOnce = true;
//                rbPlayer.velocity = rbAnchor.velocity;
//                rbPlayer.angularVelocity = rbAnchor.angularVelocity;
//            }
//        }
//    }

//    void OnDrawGizmos()
//    {
//        if (currentAnchor != null)
//        {
//            Gizmos.color = Color.red;
//            Gizmos.DrawLine(transform.position, currentAnchor.transform.position);
//        }
//    }

//    Vector3 GetCursorPosition()
//    {
//        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//        RaycastHit hit;

//        if (Physics.Raycast(ray, out hit))
//        {
//            return hit.point;
//        }

//        // Si le rayon ne touche rien, utilisez une position par défaut
//        return ray.GetPoint(10f);
//    }
//}
