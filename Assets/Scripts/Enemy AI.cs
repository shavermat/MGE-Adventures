using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    
public PlayerEntity SelectLowestHealth(List<PlayerEntity> targetTeam )
    {
        
     PlayerEntity target = targetTeam[0];
     for(int i = 0; i < targetTeam.Count; i++)
        {
            
            if (targetTeam[i].currentHealth < target.currentHealth)
            {
                
               target = targetTeam[i];


            }


        }
        return target;


    }
    public PlayerEntity RandomSelect(List<PlayerEntity> targetTeam)
    {
        int randomTargetIndex = Random.Range(0, targetTeam.Count);

       PlayerEntity target = targetTeam[randomTargetIndex];

       return target;

    }



}
