using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandsGenerator : MonoBehaviour
{
    [SerializeField] GameObject island;

    [SerializeField] List<GameObject> L1 = new List<GameObject>();
    [SerializeField] List<GameObject> L2 = new List<GameObject>();
    [SerializeField] List<GameObject> L3 = new List<GameObject>();
    [SerializeField] List<GameObject> L4 = new List<GameObject>();

    private void Awake()
    {
        SpawnIslands();
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.CompareTag("TrigQuay"))
    //    {
    //        Destroy(other);
    //        SpawnIslands();
    //    }
    //}

    void SpawnIslands()
    {
        // Position de(s) (l')ile(s) que j'instancie
        Vector3 islandsPos = new(0, 0, 500);

        if (L1.Count > 0)
        {
            L1.Clear();
        }

        if (L2.Count > 0)
        {
            L1.AddRange(L2);
            L2.Clear();
        }

        if (L3.Count > 0)
        {
            L2.AddRange(L3);
            L3.Clear();
        }

        if (L4.Count > 0)
        {
            L3.AddRange(L4);
            L4.Clear();
        }

        GameObject createdIsland = Instantiate(island, islandsPos, Quaternion.identity);
        L4.Add(createdIsland);
    }
}
