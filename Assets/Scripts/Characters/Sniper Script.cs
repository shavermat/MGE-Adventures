using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Timeline;

public class SniperScript : PlayerEntity
{
   [Header("Character Settings")]
private BattleManagerScript bm;

protected override void Start()
  {
    bm = GameObject.FindWithTag("BattleManager").GetComponent<BattleManagerScript>();
      powerDisplayCount[0] = Power / bm.enemyteam.Count;
      powerDisplayCount[1] = Power;
      powerDisplayCount[2] = Power;
    base.Start();
    

  }


    public override float SecondAbility(List<Entity> targets)
    {
 
        foreach(Entity target in targets)
        {
           
            target.Stunning(2);
            target.TakeDamage(Power);
           
        
        }
        return secondAbilityDelay;
       

    }
    public override float ThirdAbility(List<Entity> targets)
    {
       StartCoroutine(BarrageIntervalRoutine(targets));
       return thirdAbilityDelay;

    }

    IEnumerator BarrageIntervalRoutine(List<Entity> targets)
    {

        
         foreach(Entity target in targets)
        {

      for (int i = 0; i < Power / 7; i ++)
        {
           yield return new WaitForSeconds(0.05f);
            target.TakeDamage(7);
            
       
            
        }
        }
    }
   public override void PowerDisplayUpdate()
    {
        
       powerDisplayCount[0] = Power / bm.enemyteam.Count;
      powerDisplayCount[1] = Power;
      powerDisplayCount[2] = Power;
    }

       
}
