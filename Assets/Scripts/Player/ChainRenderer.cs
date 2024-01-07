using UnityEngine;

public class ChainRenderer : MonoBehaviour
{
    [SerializeField] LineRenderer line;
    [SerializeField] Transform pos1;
    [SerializeField] Transform pos2;

    void Start()
    {
        line.enabled = false;
        line.positionCount = 2;
    }

    void Update()
    {
        if (line.enabled == true)
        {
            line.SetPosition(0, pos1.position);
            line.SetPosition(1, pos2.position);
        }
    }
}
