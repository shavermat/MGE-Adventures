using System;
using System.Collections.Generic;

using UnityEngine;

public class TargetSelector : MonoBehaviour
{

 public static event Action<GameObject> OnTargetClicked;

 
 private BattleManagerScript battleManager;
  
  private Entity entity;
  private List<Entity> entities = new List<Entity>();


 private void Start()
  {

    entity = GetComponent<Entity>();
     battleManager = GameObject.FindWithTag("BattleManager").GetComponent<BattleManagerScript>();
     

  }

 public void OnMouseDown()
  {
  
  if (!TryGetComponent<Invisibility>(out Invisibility component)) OnTargetClicked?.Invoke(gameObject);
 }

 public bool FilterTargetByAbility(PlayerEntity.abilityType abilityType)
  {
    entities.Clear();
    switch (abilityType)
    {
      
      case PlayerEntity.abilityType.Enemy: return SetEnemy();
   
      case PlayerEntity.abilityType.Player: return SetPlayer();

      case PlayerEntity.abilityType.massEnemy: return SetMassEnemy();

      case PlayerEntity.abilityType.massPlayer: return SetMassPlayer();

    }

   return false;
  
  }





 private  bool  SetEnemy()
 {
        if (battleManager.state != BattleState.SELECTING_TARGET) return false;

        
        if (entity == null || !gameObject.CompareTag("Enemy")) return false;

        

        
      if (!entities.Contains(entity))
      {
       entities.Add(entity);
      }
       GetTarget();
       return true;
     
}
private bool SetPlayer()
 {
        if (battleManager.state != BattleState.SELECTING_TARGET) return false;

        
        if (entity == null || !gameObject.CompareTag("Player")) return false;

        

        

 
      if (!entities.Contains(entity) && gameObject.CompareTag("Player"))
      {
       entities.Add(entity);
      }
       GetTarget();
         return true;
        
}

private bool SetMassPlayer()
 {
        if (battleManager.state != BattleState.SELECTING_TARGET) return false;

        
        if (entity == null || !gameObject.CompareTag("Player")) return false;

        

        

 
       foreach (Entity target in BattleManagerScript.playerteam)
    {
      if (!entities.Contains(target))
      {
      entities.Add(target);
      }
    }
       GetTarget();
         return true;
        
}

private bool SetMassEnemy()
 {
        if (battleManager.state != BattleState.SELECTING_TARGET) return false;

        
        if (entity == null || !gameObject.CompareTag("Enemy")) return false;

        

       

 
       foreach (Entity target in battleManager.enemyteam)
    {
      if (!entities.Contains(target))
      {
      entities.Add(target);
      }
    }
       GetTarget();
         return true;
        
}

   public List<Entity> GetTarget()
  {
    

    return entities;
    
  }


}

