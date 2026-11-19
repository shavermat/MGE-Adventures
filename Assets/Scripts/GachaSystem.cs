using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine;

 public enum rarityState { Common, Rare, Epic, Legendary}
public class GachaSystem : MonoBehaviour
{
   
    public List<CharacterData> allCharacters;

      private List<CharacterData> CommonCharacters = new List<CharacterData>();

       private List<CharacterData> RareCharacters = new List<CharacterData>();

        private List<CharacterData> EpicCharacters = new List<CharacterData>();

        private List<CharacterData> LegendaryCharacters = new List<CharacterData>();



    public CharacterData selectedCharacter;
    public rarityState rarity;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI rarityText;
     public Image characterSprite;
     public Sprite trashCan;
     public Wallet_UI walletUI;
     public Wallet wallet;
    [SerializeField] private int legendaryChange;
    [SerializeField] private int epicChange;   
    [SerializeField] private int rareChange;
  
     [SerializeField] private float cooldown;


    private bool isCooldown;


    private void Start()
    {
       
      
        CommonCharacters.Clear();
        RareCharacters.Clear();
        EpicCharacters.Clear();
        LegendaryCharacters.Clear();
                
                foreach(CharacterData character in allCharacters)
        {
               if (character.rarity == RarityState.Legendary)
            {
                 LegendaryCharacters.Add(character); // добавляем в список легендарных
            }
            else if (character.rarity == RarityState.Epic)
            {
                EpicCharacters.Add(character);   // добавляем в список эпических
            }
             else if (character.rarity == RarityState.Rare)
            {
                RareCharacters.Add(character);   // добавляем в список редких
            }
            else if (character.rarity == RarityState.Common)
            {
                CommonCharacters.Add(character);   // добавляем в список обычных
            }


        }
        
    }
public void Rolling(bool forceRun)
    {
        if ( !forceRun && isCooldown) return;
     int currentMoney = PlayerPrefs.GetInt("Money", 0);
    if (!forceRun && currentMoney < 50)
    { 
    
        return;
    }
      if (!forceRun)wallet.ChangeMoney(-50);
          walletUI.MoneyUpdate();
        StartCoroutine(CooldownRoutine());
         characterSprite.color = Color.white;
         characterSprite.rectTransform.sizeDelta = new Vector2(280,280);

       
      int randomRarity = Random.Range(0, 100);

       if (randomRarity <= legendaryChange) rarity = rarityState.Legendary;
       else if (randomRarity <= epicChange) rarity = rarityState.Epic;
       else if (randomRarity <= rareChange) rarity = rarityState.Rare;
       else rarity = rarityState.Common;
       

       List<CharacterData> targetList = new List<CharacterData>();

       if (rarity == rarityState.Legendary) targetList = LegendaryCharacters;
       else if (rarity == rarityState.Epic) targetList = EpicCharacters;
       else if (rarity == rarityState.Rare) targetList = RareCharacters;
       else if (rarity == rarityState.Common) targetList = CommonCharacters;

      
       
    

       CharacterCelector(targetList);
     


    }



    
    public void CharacterCelector(List<CharacterData> targetList)
    {
        if (targetList.Count == 0) 
       {

        Rolling(true);
       
        return;
       }

      
         
         
    if (targetList.Count != 0 && targetList.Count > 0)  
   {

    int roll = Random.Range(0, targetList.Count); 
    
    
    selectedCharacter = targetList[roll]; 
    
   
    nameText.GetComponent<TextMeshProUGUI>().text = selectedCharacter.name;
    characterSprite.sprite = selectedCharacter.Sprite;
    rarityText.GetComponent<TextMeshProUGUI>().text = selectedCharacter.rarity.ToString();
    selectedCharacter.UnlockThisCharacter();
   }
    }

    IEnumerator CooldownRoutine()
    {
       isCooldown = true;

        yield return new WaitForSeconds(cooldown);

       isCooldown = false;

    }
}
