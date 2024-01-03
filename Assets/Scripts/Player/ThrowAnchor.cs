using UnityEngine;

public class ThrowAnchor : MonoBehaviour
{
    [SerializeField] GameObject anchorPrefab;
    [SerializeField] Rigidbody rbPlayer;
    GameObject currentAnchor;
    Rigidbody rbAnchor;
    bool doOnce = false;

    void Update()
    {
        if (Input.GetButtonDown("RightClic/R1"))
        {
            // D�truire l'ancienne ancre s'il y en a une
            if (currentAnchor != null)
            {
                Destroy(currentAnchor);
                doOnce = false;
            }

            Vector3 cursorPosition = GetCursorPosition();
            Vector3 shootDirection = cursorPosition - transform.position;

            Quaternion rotation = Quaternion.LookRotation(shootDirection);
            currentAnchor = Instantiate(anchorPrefab, transform.position, rotation);

            rbAnchor = currentAnchor.GetComponent<Rigidbody>();
            rbAnchor.AddForce(shootDirection.normalized * 30000f, ForceMode.Impulse);
        }
    }

    void LateUpdate()
    {
        if ((currentAnchor != null) && (!doOnce))
        {
            float distance = Vector3.Distance(currentAnchor.transform.position, transform.position);
            // Debug.Log("distance :" + distance);

            if (distance >= 20f)
            {
                doOnce = true;
                ApplyVelocity();
            }
        }
    }

    void ApplyVelocity()
    {
        rbPlayer.velocity = Vector3.zero;
        rbPlayer.angularVelocity = Vector3.zero;

        // Dash
        rbPlayer.velocity *= 10;
    }

    void OnDrawGizmos()
    {
        if (currentAnchor != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, currentAnchor.transform.position);
        }
    }

    Vector3 GetCursorPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }

        // Si le rayon ne touche rien, utilise une position par d�faut
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
//            // D�truire l'ancienne ancre s'il y en a une
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

//        // Si le rayon ne touche rien, utilisez une position par d�faut
//        return ray.GetPoint(10f);
//    }
//}
