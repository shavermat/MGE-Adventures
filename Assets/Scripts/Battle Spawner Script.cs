using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSpawnerScript : MonoBehaviour
{
   public List<GameObject> spawnPoints = new List<GameObject>();
  
   private BattleManagerScript bm;


  List<PlayerEntity> spawnedEntities = new List<PlayerEntity>();

    void Start()
    {

       bm = GetComponent<BattleManagerScript>();

       

        for (int i = 0; i < CharactersManagerScript.characters.Count; i ++)
        {

            if (i < spawnPoints.Count && spawnPoints[i] != null)
            {
               GameObject obj =  Instantiate(CharactersManagerScript.characters[i], spawnPoints[i].transform.position, transform.rotation);

            
           
           
             
             
               PlayerEntity entity = obj.GetComponent<PlayerEntity>();

               spawnedEntities.Add(entity);
            
           
              
            




          

           

            }
        }
        StartCoroutine(InitializeCoroutine());
    }

    // Update is called once per frame
IEnumerator InitializeCoroutine()
   {
      yield return null;

 bm.InitializeBattle(spawnedEntities);

 BattleManagerScript.playerteam[1].GetComponent<SpriteRenderer>().sortingOrder = 1;

   }
}
