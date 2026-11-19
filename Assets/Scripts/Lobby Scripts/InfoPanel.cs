using System;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    public GameObject infoPanel;

   public TextMeshProUGUI[] skillTexts;
   public GameObject[] ButtonColors;
   public TextMeshProUGUI RareText;
   public TextMeshProUGUI passiveText;

    void Start()
    {
               // Если панель не назначена в инспекторе, ищем ее автоматически
        if (infoPanel == null)
        {
            Transform panelTransform = transform.Find("InfoPanel");
            if (panelTransform != null)
            {
                infoPanel = panelTransform.gameObject;
            }
            else
            {
               
                return;
            }
        }
        
        CharactersManagerScript.characterRemove += InfoPanelOff;
        CharactersManagerScript.characterAdd += InfoPanelOn;
    }
    public void InfoPanelOn(GameObject character)
    {
        CharacterData characterData = character.GetComponent<CharacterData>();

         
          infoPanel.SetActive(true);
       
          infoPanel.transform.Find("Player Image").GetComponent<Image>().sprite = characterData.Sprite;
          infoPanel.transform.Find("CharacterName").GetComponent<TextMeshProUGUI>().text = characterData.characterName;
          for (int i =0; i < skillTexts.Length; i++)
          {
           skillTexts[i].text = character.GetComponent<PlayerEntity>().ButtonsTexts[i];
           ButtonColors[i].GetComponent<Image>().color = character.GetComponent<PlayerEntity>().ButtonsColor[i];
          }
          String rarity = character.GetComponent<CharacterData>().rarity.ToString();
          RareText.text = rarity;
          switch (rarity)
          {
            case "Common":
            RareText.color = Color.grey;
            break;

            case "Rare":
            RareText.color = Color.blue;
            break;

             case "Epic":
            RareText.color = new Color(0.6f, 0.2f, 0.8f); // фиолетовый
            break;

            case "Legendary":
            RareText.color = Color.yellow; 
            break;
            
          }


      string passive = passiveText.text = "Passive: " + character.GetComponent<PlayerEntity>().Passive;
     if (passive == null) passiveText.text = "Passive: None";

       
    }
    public void InfoPanelOff()
    {
       
      infoPanel.SetActive(false); 
    
    }
     void OnDestroy()
    {
        //  отписываемся от статических событий во избежание утечек памяти
        CharactersManagerScript.characterRemove -= InfoPanelOff;
        CharactersManagerScript.characterAdd -= InfoPanelOn;
    }
}
