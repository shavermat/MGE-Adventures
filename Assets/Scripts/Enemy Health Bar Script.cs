using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class EnemyHealthBarScript : MonoBehaviour
{

    Entity entity;
  [SerializeField] private List<Slider> healthBars = new List<Slider>();
   [SerializeField] private List<TextMeshProUGUI> nametexts = new List<TextMeshProUGUI>();
   public BattleManagerScript battleManager;
  

private List<Entity> linkedEntities = new List<Entity>();

    public void Start()
    {
        battleManager.end += linkedEntitiesClean;
         
    }
    public void HealthbarStart()
    {
      
   
     for (int i = 0; i < healthBars.Count; i++)
        {
         if (i < battleManager.enemyteam.Count)
         {
            // запоминаем текущую команду
             Entity enemy = battleManager.enemyteam[i];

                if (enemy == null) continue;
                 
                  linkedEntities.Add(enemy);

if (healthBars[i] != null && i < nametexts.Count && nametexts[i] != null)
{
 // настраиваем UI элементы
          healthBars[i].gameObject.SetActive(true);
          

          healthBars[i].maxValue = battleManager.enemyteam[i].GetComponent<Entity>().maxHealth;
           healthBars[i].value = battleManager.enemyteam[i].GetComponent<Entity>().maxHealth;

        
         
        nametexts[i].text = battleManager.enemyteam[i].unitName;
         healthBars[i].name = battleManager.enemyteam[i].name;
}


        
         
        

          
          enemy.onPlayerDamaged += TargetHealthUpdate;
          enemy.onPlayerHealing += TargetHealthUpdate;
          enemy.OnDeath += HealthBarControl;
         
       


          }
         
          
          
        }


       
    }
    
      public void HealthBarControl(Entity entity)
    {
        
    

    int deathEnemyIndex = linkedEntities.IndexOf(entity);

   
    

    // Твой оригинальный код
    if (deathEnemyIndex >= 0 && deathEnemyIndex < healthBars.Count)
    {
        healthBars[deathEnemyIndex].gameObject.SetActive(false);
    }

        

    }
      public void TargetHealthUpdate(Entity target)
    {
        if (target == null) return;
        int targetIndex = linkedEntities.IndexOf(target);
        if ( targetIndex == -1)
        {
    
            return;
        }
        if (healthBars[targetIndex] == null || !healthBars[targetIndex].gameObject.activeSelf) return;

        
         healthBars[targetIndex].value = target.currentHealth;
         
   

    }

   private void linkedEntitiesClean()
  {
    linkedEntities.Clear();

  }


 






}
  