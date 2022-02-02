using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawneurPneuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject PrefabPneu;
    [SerializeField]
    private GameObject SpawnPoint;
    private bool BoutonDejaPresse;

    // Start is called before the first frame update
    void Start()
    {
        // Cet attribut empêche l'utilisateur de faire apparaître plusieurs fois les pneus à chaque appui sur le bouton d'apparition
        // Il faut soit finir, soit arrêter le serious game, pour avoir à nouveau le droit de faire apparaître des pneus
        BoutonDejaPresse = false;
        EventManager.StartListening("BouttonRouge", ResetButton);
    }

    public void SpawnPneu()
    {
        if (!BoutonDejaPresse)
        {
            BoutonDejaPresse = true;
            StartCoroutine("CoroutineSpawnPneu");
            EventManager.TriggerEvent("startPneus", new EventParam());
        }
    }

    // Coroutine permettant de temporiser l'apparition des pneus
    IEnumerator CoroutineSpawnPneu()
    {
        for (int i = 0; i < 10; i++)
        {
            if (BoutonDejaPresse)
            {
                // On fait apparaître les pneus à la position de 'SpawnPoint'
                Instantiate(PrefabPneu, SpawnPoint.transform.position + new Vector3(Random.Range(-0.3f, 0.3f), 0, Random.Range(-0.3f, 0.3f)), Quaternion.identity);
                GetComponent<GestionSound>().PlaySon();
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    void ResetButton(EventParam evenement)
    {
        EventParamBool evenementBool = (EventParamBool)evenement;
        // 'evenementBool.Value' est toujours = True
        // On donne à nouveau le droit de faire apparaître des pneus
        BoutonDejaPresse = !evenementBool.Value;
    }
}
