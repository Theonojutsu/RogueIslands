using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandsGenerator : MonoBehaviour
{
    private Transform monTransfo;
    [SerializeField] GameObject prefabObjet;
    [SerializeField] float limiteGauche = -600;
    [SerializeField] float limiteDroite = 600;
    [SerializeField] float ecartPosZ = 100;
    [SerializeField] float distanceDeLimite = 50f;
    [SerializeField] float distanceMinEntreObjets = 50f;

    float positionX;
    float positionZ;
    Vector3 possiblePosition;

    void Awake()
    {
        InstancierObjets();
    }

    void InstancierObjets()
    {
        monTransfo = transform;

        int nombreObjetsAInstancier = Random.Range(1, 3);

        for (int i = 0; i < nombreObjetsAInstancier; i++)
        {
            // Calculer la position d'instanciation entre les limites gauche et droite
            PositionCalcul();

            // Instancier l'objet à la position calculée
            GameObject nouvelObjet = Instantiate(prefabObjet, possiblePosition, Quaternion.identity, monTransfo);

            // Vérifier la distance minimale entre les objets précédemment instanciés
            if (i > 0)
            {
                Vector3 positionActuelle = nouvelObjet.transform.position;
                Vector3 positionPrecedente = transform.GetChild(i - 1).position;

                while (Vector3.Distance(positionActuelle, positionPrecedente) < distanceMinEntreObjets)
                {              
                    PositionCalcul();
                    nouvelObjet.transform.position = possiblePosition;

                    positionActuelle = nouvelObjet.transform.position;
                }
            }
        }
    }

    void PositionCalcul()
    {
        positionX = Random.Range(limiteGauche + distanceDeLimite, limiteDroite - distanceDeLimite);
        positionZ = Random.Range(transform.position.z - ecartPosZ, transform.position.z + ecartPosZ);
        possiblePosition = new Vector3(positionX, transform.position.y, positionZ);
    }
}
