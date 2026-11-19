
using UnityEngine;
using System.Collections.Generic;


public class MedicScript : PlayerEntity
{
    
  

   

    
    public override float FirstAbility(List<Entity> targets)
    {
     
        
    
        
       foreach(Entity target in targets)
        {
        
            
           target.Healing(Power);
       

        } 

       return firstAbilityDelay;
    }

    public override float SecondAbility(List<Entity> targets)
    {
     
        
    
        
       foreach(Entity target in targets)
        {
        
            
           target.PowerBuff(1.20f);
  

        } 
        return secondAbilityDelay;
       
    }
    public override float ThirdAbility(List<Entity> targets)
    {
     
        
    
       foreach (Entity target in targets)
        {
            target.TakeDamage(Power);
            GetComponent<Entity>().Healing(Power -10);
        }
        return thirdAbilityDelay;
       
    }

        public override void PowerDisplayUpdate()
    {
        
       powerDisplayCount[0] = Power;
      powerDisplayCount[1] = 20;
      powerDisplayCount[2] = Power;
    }
   

}
