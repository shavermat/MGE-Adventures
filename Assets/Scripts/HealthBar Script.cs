using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class HealthBarScript : MonoBehaviour
{

 
  [SerializeField] private List<Slider> healthBars = new List<Slider>();
   [SerializeField] private List<TextMeshProUGUI> nametexts = new List<TextMeshProUGUI>();
   public List<GameObject> deathIcon = new List<GameObject>();
   public List<int> deathPlayersIndex;
    [SerializeField] BattleManagerScript bm;


private List<PlayerEntity> linkedEntities = new List<PlayerEntity>();


    public void HealthbarStart()
    {
      
    

     for (int i = 0; i < BattleManagerScript.playerteam.Count; i++)
        {
         if (i < healthBars.Count && i < nametexts.Count && healthBars[i] != null && nametexts[i] != null)
         {
            // запоминаем текущую команду
             PlayerEntity player = BattleManagerScript.playerteam[i];
                if (player == null) continue;
                 
                  linkedEntities.Add(player);


         // настраиваем UI элементы
          healthBars[i].gameObject.SetActive(true);
          
         

          healthBars[i].maxValue = BattleManagerScript.playerteam[i].GetComponent<Entity>().maxHealth;
           healthBars[i].value = BattleManagerScript.playerteam[i].GetComponent<Entity>().currentHealth;
         

        nametexts[i].text = CharactersManagerScript.characters[i].name;
         healthBars[i].name = BattleManagerScript.playerteam[i].name;
          player.onPlayerDamaged += TargetHealthUpdate;
          player.onPlayerHealing += TargetHealthUpdate;
           player.OnDeath += HealthBarControl;
           bm.end += HideHealthBars;
         




          }
          
          
        }


       
    }
     public void HealthBarControl(Entity entity)
    {
        
       PlayerEntity playerEntity = entity.gameObject.GetComponent<PlayerEntity>();

          healthBars[playerEntity.playerIndex].gameObject.SetActive(false);
           deathIcon[playerEntity.playerIndex].gameObject.SetActive(true);
           deathPlayersIndex.Add(playerEntity.playerIndex);

    }
      public void TargetHealthUpdate(Entity target)
    {
      PlayerEntity targetPlayerEntity =  target.gameObject.GetComponent<PlayerEntity>();
        if (target == null )
        {
            
        }
        int targetIndex = linkedEntities.IndexOf( targetPlayerEntity);
        if ( targetIndex == -1)
        {
           
            return;
        }
        if (healthBars[targetIndex] == null || !healthBars[targetIndex].gameObject.activeSelf) return;

        
         healthBars[targetIndex].value = target.currentHealth;
   

    }
    public void HideHealthBars()
  {
       gameObject.SetActive(false);
    for(int i = 0; i < healthBars.Count; i ++)
    {
      
      deathIcon[i].gameObject.SetActive(false);
    }
  }



}
  