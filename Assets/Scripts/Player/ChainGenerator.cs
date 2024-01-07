using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainGenerator : MonoBehaviour
{
    public LineRenderer lineRenderer; // Le LineRenderer existant
    public GameObject maillonPrefab; // Le prefab du maillon de la chaîne
    public float distanceEntreMaillons = 1f; // La distance entre chaque maillon

    private List<GameObject> maillons = new List<GameObject>();

    void Update()
    {
        if (lineRenderer.enabled == true)
        {
            // Mettre à jour la position et l'orientation des maillons en fonction du LineRenderer
            UpdateChain();
        }
    }

    void UpdateChain()
    {
        // Vérifier si le LineRenderer est défini et s'il a au moins deux points
        if (lineRenderer == null || lineRenderer.positionCount < 2)
        {
            // Éventuellement, gérer le cas où le LineRenderer n'a pas assez de points.
            return;
        }

        // Calculer la tangente à la ligne une seule fois
        Vector3[] linePositions = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(linePositions);
        Vector3[] tangents = new Vector3[linePositions.Length - 1];
        for (int i = 0; i < tangents.Length; i++)
        {
            tangents[i] = (linePositions[i + 1] - linePositions[i]).normalized;
        }

        // Générer ou détruire les maillons en fonction de la distance entre eux
        int nombreMaillons = Mathf.CeilToInt(CalculateLineLength() / distanceEntreMaillons);

        // Supprimer les maillons excédents
        for (int i = maillons.Count - 1; i >= nombreMaillons; i--)
        {
            Destroy(maillons[i]);
            maillons.RemoveAt(i);
        }

        // Ajouter de nouveaux maillons si nécessaire
        for (int i = maillons.Count; i < nombreMaillons; i++)
        {
            float normalizedDistance = i / (float)(nombreMaillons - 1);
            Vector3 positionOnLine = Vector3.Lerp(linePositions[0], linePositions[linePositions.Length - 1], normalizedDistance);

            // Instancier le maillon
            GameObject maillon = Instantiate(maillonPrefab, positionOnLine, Quaternion.identity);
            maillons.Add(maillon);
        }

        // Mettre à jour la position et l'orientation des maillons en fonction du LineRenderer
        for (int i = 0; i < maillons.Count; i++)
        {
            float normalizedDistance = i / (float)(maillons.Count - 1);
            Vector3 positionOnLine = Vector3.Lerp(linePositions[0], linePositions[linePositions.Length - 1], normalizedDistance);

            // Mettre à jour la position du maillon
            maillons[i].transform.position = positionOnLine;

            // Optionnel : Orienting tous les maillons selon la tangente à la ligne
            int index = Mathf.FloorToInt(normalizedDistance * (linePositions.Length - 1));
            if (index < tangents.Length)
            {
                maillons[i].transform.rotation = Quaternion.LookRotation(tangents[index]);
            }
        }
    }

    float CalculateLineLength()
    {
        float lineLength = 0f;
        for (int i = 0; i < lineRenderer.positionCount - 1; i++)
        {
            lineLength += Vector3.Distance(lineRenderer.GetPosition(i), lineRenderer.GetPosition(i + 1));
        }
        return lineLength;
    }
}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ChainGenerator : MonoBehaviour
//{
//    public LineRenderer lineRenderer; // Le LineRenderer existant
//    public GameObject maillonPrefab; // Le prefab du maillon de la chaîne
//    public float distanceEntreMaillons = 5f; // La distance entre chaque maillon

//    bool canUpdate = false; //test

//    private List<GameObject> maillons = new List<GameObject>();

//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.K))
//        {
//            // Générer la chaîne initiale
//            GenerateChain();
//            canUpdate = true;
//        }

//        if (canUpdate)
//        {
//            // Mettre à jour la position et l'orientation des maillons en fonction du LineRenderer
//            UpdateChain();
//        }
//    }

//    void GenerateChain()
//    {
//        // Calculer la distance totale de la ligne
//        float lineLength = CalculateLineLength();

//        // Calculer le nombre de maillons nécessaires en fonction de la distance entre les maillons
//        int nombreMaillons = Mathf.CeilToInt(lineLength / distanceEntreMaillons);

//        // Créer les maillons le long de la ligne
//        for (int i = 0; i < nombreMaillons; i++)
//        {
//            float normalizedDistance = i / (float)(nombreMaillons - 1);
//            Vector3 positionOnLine = Vector3.Lerp(lineRenderer.GetPosition(0), lineRenderer.GetPosition(lineRenderer.positionCount - 1), normalizedDistance);

//            // Instancier le maillon
//            GameObject maillon = Instantiate(maillonPrefab, positionOnLine, Quaternion.identity);
//            maillons.Add(maillon);
//        }

//        // Mettre à jour la position et l'orientation initiales des maillons
//        UpdateChain();
//    }

//    void UpdateChain()
//    {
//        // Vérifier si le LineRenderer est défini
//        if (lineRenderer == null)
//        {
//            Debug.LogError("Veuillez attacher un LineRenderer à ce script.");
//            return;
//        }

//        // Mettre à jour la position et l'orientation des maillons en fonction du LineRenderer
//        for (int i = 0; i < maillons.Count; i++)
//        {
//            float normalizedDistance = i / (float)(maillons.Count - 1);
//            Vector3 positionOnLine = Vector3.Lerp(lineRenderer.GetPosition(0), lineRenderer.GetPosition(lineRenderer.positionCount - 1), normalizedDistance);

//            // Mettre à jour la position du maillon
//            maillons[i].transform.position = positionOnLine;

//            // Optionnel : Vous pouvez orienter chaque maillon selon la tangente à la ligne
//            if (i < maillons.Count - 1)
//            {
//                Vector3 tangent = (lineRenderer.GetPosition(i + 1) - lineRenderer.GetPosition(i)).normalized;
//                maillons[i].transform.rotation = Quaternion.LookRotation(tangent);
//            }
//        }
//    }

//    float CalculateLineLength()
//    {
//        float lineLength = 0f;
//        for (int i = 0; i < lineRenderer.positionCount - 1; i++)
//        {
//            lineLength += Vector3.Distance(lineRenderer.GetPosition(i), lineRenderer.GetPosition(i + 1));
//        }
//        return lineLength;
//    }
//}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ChainGenerator : MonoBehaviour
//{
//    [SerializeField] LineRenderer lineRenderer; // Le LineRenderer existant
//    [SerializeField] GameObject maillonPrefab; // Le prefab du maillon de la chaîne
//    [SerializeField] int nombreMaillons = 10; // Le nombre de maillons dans la chaîne

//    void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.K))
//        {
//            // Créer la chaîne avec plusieurs maillons
//            GenerateChain();
//        }
//    }

//    void GenerateChain()
//    {
//        // Vérifier si le LineRenderer est défini
//        if (lineRenderer == null)
//        {
//            Debug.LogError("Veuillez attacher un LineRenderer à ce script.");
//            return;
//        }

//        // Calculer la distance totale de la ligne
//        float lineLength = 0f;
//        for (int i = 0; i < lineRenderer.positionCount - 1; i++)
//        {
//            lineLength += Vector3.Distance(lineRenderer.GetPosition(i), lineRenderer.GetPosition(i + 1));
//        }

//        // Calculer la distance entre chaque maillon
//        float distanceBetweenMaillons = lineLength / (float)nombreMaillons;

//        // Créer les maillons le long de la ligne
//        for (int i = 0; i < nombreMaillons; i++)
//        {
//            float normalizedDistance = i / (float)(nombreMaillons - 1);
//            Vector3 positionOnLine = Vector3.Lerp(lineRenderer.GetPosition(0), lineRenderer.GetPosition(lineRenderer.positionCount - 1), normalizedDistance);

//            // Instancier le maillon
//            GameObject maillon = Instantiate(maillonPrefab, positionOnLine, Quaternion.identity);
//            // Optionnel : Vous pouvez orienter chaque maillon selon la tangente à la ligne
//            if (i < nombreMaillons - 1)
//            {
//                Vector3 tangent = (lineRenderer.GetPosition(i + 1) - lineRenderer.GetPosition(i)).normalized;
//                maillon.transform.rotation = Quaternion.LookRotation(tangent);
//            }
//        }
//    }
//}