using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandsManager : MonoBehaviour
{
    [SerializeField] GameObject island;
    [SerializeField] float spacingIsland = 500f;

    [SerializeField] List<GameObject> L1 = new List<GameObject>();
    [SerializeField] List<GameObject> L2 = new List<GameObject>();
    [SerializeField] List<GameObject> L3 = new List<GameObject>();
    [SerializeField] List<GameObject> L4 = new List<GameObject>();

    void Awake()
    {
        IslandRegister();
        CreateIslands(1, L3);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Quay"))
        {
            IslandRegister();
        }
    }

    // Stocke les iles instanciées dans des listes dans le but de trier ces dernières
    // et de détruire les bonnes iles au fur et à mesure que le niveau avance. 
    void IslandRegister()
    {
        if (L1.Count > 0)
        {
            foreach (GameObject islands in L1)
            {
                Destroy(islands);
            }
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

        CreateIslands(2, L4);
    }

    void CreateIslands(int distance, List<GameObject> Liste)
    {
        // Position de(s) (l')ile(s) que j'instancie
        Vector3 islandsPos = new(0, 0, (transform.position.z) + distance * spacingIsland);

        GameObject createdIsland = Instantiate(island, islandsPos, Quaternion.identity);
        Liste.Add(createdIsland);
    }
}
