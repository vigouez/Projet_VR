using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawneurPneuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject PrefabPneu;
    [SerializeField]
    private GameObject SpawnPoint1;
    private bool BoutonDejaPresse;


    // Start is called before the first frame update
    void Start()
    {
        BoutonDejaPresse = false;
        EventManager.StartListening("BouttonRouge", ResetButton);
    }

    // Update is called once per frame
    void Update()
    {
        
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

    IEnumerator CoroutineSpawnPneu()
    {
        for (int i = 0; i < 10; i++)
        {
            if (BoutonDejaPresse)
            {
                Instantiate(PrefabPneu, SpawnPoint1.transform.position + new Vector3(Random.Range(-0.3f, 0.3f), 0, Random.Range(-0.3f, 0.3f)), Quaternion.identity);
                GetComponent<GestionSound>().PlaySon();
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    void ResetButton(EventParam evenement)
    {
        EventParamBool evenementBool = (EventParamBool)evenement;
        BoutonDejaPresse = !evenementBool.Value;
    }
}
