using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;




public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST, SELECTING_TARGET}
public enum ButtonClick { FirstButton, SecondButton, ThirdButton, None }

public class BattleManagerScript : MonoBehaviour
{


    public BattleState state;

    [Header("Buttons settings")]
    public ButtonClick SelectedButton;
    public GameObject attackButtonPanel;

     public TextMeshProUGUI[] ButtonTextsFields = new TextMeshProUGUI[3];
  



  
   static public List<PlayerEntity> playerteam = new List<PlayerEntity>();
 public List<Entity> enemyteam = new List<Entity>();


   
    
    private int currentPlayerIndex = 0;
     private int currentEnemyIndex = 0;  
    [NonSerialized] public bool Playerisattack = false;

   
  
    [SerializeField] private HealthBarScript healthBarManager;

    [SerializeField] private EnemyHealthBarScript enemyHealthBarManager;


           [Header("End battle settings")]
    [SerializeField] private GameObject EndScreen;
   [SerializeField] private TextMeshProUGUI endBattleText; 
   public event  Action end;

  static public event Action onEnemyturnEnd;
  static public event Action onPlayerturnEnd;
  static public event Action<PlayerEntity> onUIUpdate;
   static public event Action<PlayerEntity> EnemyTurnStart;
    static public event Action PlayerturnStart;
      static public event Action Initialize;
      static public event Action TargetSelection;
     static public event Action Cancelled;
  
 [NonSerialized] public PlayerEntity activePlayer;
  PlayerEntity.abilityType currentAbilityType;
private TargetSelector tg;
private BattleUIManager bmUI;
public bool playerInitialize;
private bool enemyInitialize;
private float timeForWait = 0.12f;


 private void Awake() 
 {

    bmUI = GetComponent<BattleUIManager>();
 }

    public void InitializeBattle(List<PlayerEntity> spawnedPlayers)
{
    TargetSelector.OnTargetClicked -= SetTarget; 
   
    TargetSelector.OnTargetClicked += SetTarget;
 
    playerteam = spawnedPlayers; 
    for (int i = 0; i < playerteam.Count; i ++)
        {
            playerteam[i].playerIndex = i;
        }
    state = BattleState.START;
   
    healthBarManager.HealthbarStart();
   

 // Подписываемся на смерть каждого игрока
    
    foreach (var player in playerteam)
    {
       player.OnDeath += HandleEntityDeath;
    }

   playerInitialize = true;
    SetupBattle();
   
}

public void InitializeEnemyTeam(List<Entity> spawnedEnemies)
    {
        enemyteam = spawnedEnemies;
         enemyHealthBarManager.HealthbarStart();

    // Подписываемся на смерть каждого врага
    foreach (var enemy in enemyteam)
    {
        enemy.OnDeath += HandleEntityDeath;
    }

 enemyInitialize = true;
    SetupBattle();
    }



  
    void SetupBattle()
    {
      
        Initialize?.Invoke();

        if (enemyInitialize && playerInitialize)
        {
      
        
        StartPlayerTurn();
        }
      
    }

    void StartPlayerTurn()
{
    currentPlayerIndex = 0; // Начинаем с самого первого персонажа в списке
    state = BattleState.PLAYERTURN;
   attackButtonPanel.gameObject.SetActive(true);
     attackButtonPanel.GetComponent<CanvasGroup>().interactable = true;
   
    ButtonsUpdate();
    
}


 void NextPlayerSubTurn()
    {
    
       
        ButtonsUpdate();

        attackButtonPanel.gameObject.SetActive(true);
     attackButtonPanel.GetComponent<CanvasGroup>().interactable = true;

    
    }
    public void ButtonsUpdate()
    {
        
         
        PlayerEntity currentPlayer = playerteam[currentPlayerIndex];
        onUIUpdate.Invoke(currentPlayer);
    }

    public void HandleEntityDeath(Entity deadEntity)
    {
        deadEntity.OnDeath -= HandleEntityDeath;
      if (deadEntity is PlayerEntity player)
        {
            if(playerteam.Contains(player))playerteam.Remove(player);
         
        }
        else if (enemyteam.Contains(deadEntity))
        {
            enemyteam.Remove(deadEntity);

          
        }
   

    }

   
   


    public void OnAttackButton(int ButtonIndex)
    {
         
       activePlayer = playerteam[currentPlayerIndex];

        currentAbilityType = activePlayer.AbilitiesTypes[ButtonIndex];

        if (state != BattleState.PLAYERTURN || Playerisattack == true ) return;

        attackButtonPanel.GetComponent<CanvasGroup>().interactable = false;

        SelectedButton = (ButtonClick)ButtonIndex;

       state = BattleState.SELECTING_TARGET;
        TargetSelection.Invoke();
       
       
   

    }

public void SetTarget(GameObject newtarget)
    {
       
       
      
         if (Playerisattack || state != BattleState.SELECTING_TARGET || newtarget == null) return;

        tg = newtarget.GetComponent<TargetSelector>();
       
       bool result = tg.FilterTargetByAbility(currentAbilityType);

     if (result == true )
     {
        StartCoroutine(PlayerAttackRoutine());

     }
      


    }
    public void SkipTurn()
    {
        SelectedButton = ButtonClick.None;
     StartCoroutine(PlayerAttackRoutine());
    }
     public void Cancel()
    {
        Cancelled?.Invoke();
     attackButtonPanel.GetComponent<CanvasGroup>().interactable = true;
     state = BattleState.PLAYERTURN;
     
    }



    IEnumerator PlayerAttackRoutine()
    {
       

        PlayerturnStart?.Invoke();
         
        Playerisattack = true;
      
      state =  BattleState.PLAYERTURN;
       

       if (enemyteam.Count > 0 && SelectedButton != ButtonClick.None)
        {

           List<Entity> targets = tg.GetTarget(); 
          
         
        
            switch(SelectedButton)
            {
                case ButtonClick.FirstButton:  timeForWait = activePlayer.FirstAbility(targets);
                break;
                case ButtonClick.SecondButton:  timeForWait = activePlayer.SecondAbility(targets);
                break;
                case ButtonClick.ThirdButton:  timeForWait = activePlayer.ThirdAbility(targets);
                break;

            }
           

             

         

        }
        
                
        yield return new WaitForSeconds(timeForWait);
         Playerisattack = false;

        if (enemyteam.Count == 0)  // если врагов не осталось
        {
             Playerisattack = false;
             state = BattleState.WON;
             EndBattle();
        
             yield break; 
        }


         currentPlayerIndex++; 


         if (currentPlayerIndex >= playerteam.Count) // если все персонажи походили
        {
              onPlayerturnEnd?.Invoke();
            state = BattleState.ENEMYTURN;
              attackButtonPanel.gameObject.SetActive(false);
            StartCoroutine(EnemyAttackRoutine());
            
        } 
         else  NextPlayerSubTurn(); // если еще не все персонажи походили

        

    }



    IEnumerator EnemyAttackRoutine()
    {

    if (enemyteam == null || enemyteam.Count <= 0)
    {
        Playerisattack = false;
        state = BattleState.WON;
        EndBattle();
        yield break;
    }
    if (currentEnemyIndex >= enemyteam.Count)
        {
              onEnemyturnEnd?.Invoke();

       
        currentEnemyIndex = 0;
       
        attackButtonPanel.GetComponent<CanvasGroup>().interactable = true;

        // Передаем ход обратно игроку
        StartPlayerTurn(); 
        yield break;
        }
       
       Entity activeEnemy = enemyteam[currentEnemyIndex];
         if(!activeEnemy.TryGetComponent<Stunning>(out Stunning stun))
         {
       
     
        EnemyTurnStart?.Invoke(null);
        yield return new WaitForSeconds(1.0f);

       if (playerteam.Count > 0)
    {
        
       PlayerEntity targetPlayer = null;
      foreach(PlayerEntity player in playerteam)
                {
                    if (!player.GetComponent<Invisibility>())
                    {
                        targetPlayer = player;
                        break;
                    } 
                }
              if(targetPlayer == null)
              {
               int randomPlayer = UnityEngine.Random.Range(0, playerteam.Count);
               targetPlayer = playerteam[randomPlayer];
              }


        // Наносим урон игроку
        activeEnemy.UseAbilities(targetPlayer);


        // Если наш персонаж погиб — убираем его из списка отряда
        if (targetPlayer.currentHealth <= 0)
        {
         

            playerteam.Remove(targetPlayer);
             
             
          
        }
    }

      yield return new WaitForSeconds(0.5f);

      if (playerteam.Count == 0)
    {
        state = BattleState.LOST;
        EndBattle();
        
        yield break; // Выходим, бой закончен
    }
    }
    currentEnemyIndex++;

    if (currentEnemyIndex >= enemyteam.Count)
    {
          onEnemyturnEnd?.Invoke();

        // Все враги сделали свой ход! Сбрасываем их счетчик на будущее
        currentEnemyIndex = 0;
       
        attackButtonPanel.GetComponent<CanvasGroup>().interactable = true;

        // Передаем ход обратно игроку
        if (enemyteam.Count > 0 && enemyteam != null) StartPlayerTurn(); // перепроверяем мертвы ли враги для урона в конце хода
        else{ state = BattleState.WON; EndBattle();}
    }
    else
    {
        // Если остались еще не походившие враги, запускаем эту же корутину КРУГОВОЙ СВЯЗЬЮ для следующего врага
        StartCoroutine(EnemyAttackRoutine());
        yield break;
    }
     
   }




 void EndBattle()
    {
        
        EndScreen.SetActive(true);
        
     Transform childTransform = EndScreen.transform.Find("Button Next");


       attackButtonPanel.SetActive(false);
    
        if (state == BattleState.WON)
        {
            childTransform.gameObject.SetActive(true);
            endBattleText.text = " Sector is clear";

          

        }
        else if (state == BattleState.LOST)
        {
              childTransform.gameObject.SetActive(false);
             endBattleText.text = "        You Loss";
           

        }
        end?.Invoke();
    }


    private void OnDisable()
    {
      onEnemyturnEnd = null;
      onPlayerturnEnd = null;
      onUIUpdate = null;
      EnemyTurnStart = null;
      Initialize = null;
    }





}
