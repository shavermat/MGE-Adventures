using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class DemomanScript : PlayerEntity
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
public override float FirstAbility(List<Entity> targets)
    {
        
      
        foreach(Entity target in targets)
        {
         target.TakeDamage(Power / targets.Count);
       
        }
        return firstAbilityDelay;

    }

    public override float ThirdAbility(List<Entity> targets)
    {
       
       StartCoroutine(IntervalRoutine(targets));
       return thirdAbilityDelay;

    }

    IEnumerator IntervalRoutine(List<Entity> targets)
    {
        
        foreach(Entity target in targets)
      {
            for(int i = 0; i < 2; i++)
          {
            yield return new WaitForSeconds(0.2f);
           target.TakeDamage(Power / 2);
         
           
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
