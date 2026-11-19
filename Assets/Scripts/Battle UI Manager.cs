
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Unity.VisualScripting;

public class BattleUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private List<TextMeshProUGUI> powerCountText;
       public Button SkipButton;
       public Button CancelButton;
        [SerializeField] private AttackButtonScript[] attackButtons; 
 
   

    private BattleManagerScript bm;

    void Start()
    {
        bm = GetComponent<BattleManagerScript>();
        BattleManagerScript.onUIUpdate += PowerTextsDisplay;
         BattleManagerScript.onUIUpdate += NameTextChange;
          BattleManagerScript.EnemyTurnStart += NameTextChange;
          BattleManagerScript.Initialize += SubscribeEntities;
          BattleManagerScript.PlayerturnStart += PlayerTurnStart;
          BattleManagerScript.onUIUpdate += ButtonUpdate;
          BattleManagerScript.TargetSelection += TargetSelect;
          BattleManagerScript.Cancelled += Cancel;

        bm.end += UnSubscribe;
    }

    private void SubscribeEntities() // подписка на события
    {
    foreach(Entity enemy in bm.enemyteam)
        {
            if (enemy == null) continue;

          
            enemy.isBurning += IconUpdate;
            enemy.isStun += IconUpdate;
            enemy.isBreakArmor += IconUpdate;
        }
     foreach(PlayerEntity player in BattleManagerScript.playerteam)
        {
             if (player == null) continue;

            player.isBurning += IconUpdate;
            player.isStun += IconUpdate;
            player.isBreakArmor += IconUpdate;
        }
    }
    private void UnSubscribe() // отписка от событий 
    {
     foreach(Entity enemy in bm.enemyteam)
        {
         
            enemy.isBurning -= IconUpdate;
            enemy.isStun -= IconUpdate;
            enemy.isBreakArmor -= IconUpdate;
        }
     foreach(PlayerEntity player in BattleManagerScript.playerteam)
        {
            player.isBurning -= IconUpdate;
            player.isStun -= IconUpdate;
            player.isBreakArmor -= IconUpdate;
        }
    }
    private void OnDisable() // отписка от событий 
    {
         BattleManagerScript.onUIUpdate -= PowerTextsDisplay;
         BattleManagerScript.onUIUpdate -= NameTextChange;
          BattleManagerScript.EnemyTurnStart -= NameTextChange;
          BattleManagerScript.Initialize -= SubscribeEntities;
            BattleManagerScript.PlayerturnStart -= PlayerTurnStart;
          BattleManagerScript.onUIUpdate -= ButtonUpdate;
               BattleManagerScript.TargetSelection -= TargetSelect;
          BattleManagerScript.Cancelled -= Cancel;
          if (bm != null)
          UnSubscribe();
    {
        bm.end -= UnSubscribe;
    }
    }
    public void NameTextChange(PlayerEntity player)
    {
        if (player == null){ nameText.text = "Enemy turn"; return;}
        nameText.text = player.unitName;
    }


     public void PowerTextsDisplay(PlayerEntity player)
    {
       

        List<int> powerCount =  player.powerDisplayCount;
        for (int i = 0; i < powerCountText.Count; i++)
        {
          

            powerCountText[i].text = "Power:" + powerCount[i].ToString();
        }
      
    }
    private void IconUpdate(Entity Entity)
    {
        if (Entity == null || Entity.transform == null || !Entity.gameObject.activeSelf) return;


        if (Entity.TryGetComponent<Burning>( out Burning burn)) // stun icon
        {
           Transform icon = Entity.transform.Find("Effect Status/Icon Manager/Fire Icon");
            if (icon != null) icon.gameObject.SetActive(true);

            TextMeshProUGUI text = icon.Find("Text").GetComponent<TextMeshProUGUI>();
         text.text = burn.fireCharge.ToString();
         if(burn.fireCharge == 0) icon.gameObject.SetActive(false);
           
        }



        if (Entity.TryGetComponent<Stunning>( out Stunning stun)) // stun icon
        {
           Transform icon = Entity.transform.Find("Effect Status/Icon Manager/Stun Icon");
            if (icon != null) icon.gameObject.SetActive(true);

            TextMeshProUGUI text = icon.Find("Text").GetComponent<TextMeshProUGUI>();
         text.text = stun.stunCharge.ToString();
         if(stun.stunCharge == 0) icon.gameObject.SetActive(false);
           
        }




         if (Entity.TryGetComponent<BreakArmor>( out BreakArmor Break)) // break icon
        {
          Transform icon =  Entity.transform.Find("Effect Status/Icon Manager/Break Icon");
          if (icon != null) icon.gameObject.SetActive(true);

           TextMeshProUGUI text = icon.Find("Text").GetComponent<TextMeshProUGUI>();
           text.text = Break.BreakCharge.ToString();
           if (Break.BreakCharge == 0) icon.gameObject.SetActive(false);
        }

    }
     

    private void ButtonUpdate(PlayerEntity currentPlayer)
    {
        
        SkipButton.gameObject.SetActive(true);
        CancelButton.gameObject.SetActive(false);
         currentPlayer.PowerDisplayUpdate();

         for (int i = 0; i < attackButtons.Length; i++)
        {
            
           if (i < currentPlayer.ButtonsColor.Length && i < currentPlayer.ButtonsTexts.Length)
            {
                
                attackButtons[i].Setup(currentPlayer.ButtonsTexts[i], currentPlayer.ButtonsColor[i]);
            }

            else attackButtons[i].Hide();

        }
    }
     private void PlayerTurnStart()
    {
        SkipButton.gameObject.SetActive(false);
       CancelButton.gameObject.SetActive(false);
    }
    private void TargetSelect()
    {
      SkipButton.gameObject.SetActive(false);
      CancelButton.gameObject.SetActive(true);
    }
    private void Cancel()
    {
         CancelButton.gameObject.SetActive(false);
         SkipButton.gameObject.SetActive(true);
    }

}
