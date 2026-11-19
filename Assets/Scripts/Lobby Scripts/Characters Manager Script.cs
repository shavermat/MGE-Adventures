using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CharactersManagerScript : MonoBehaviour
{
  
  public GameObject selectedprefab;

  private Button buttonObj;
  public Sounds soundPlayer;
  public AudioClip sound;

  public static List<GameObject> characters = new List<GameObject>();
 public  List<Button> CharacterCard;
 public static event Action characterRemove;
 public InfoPanel infoPanel;

   public static event Action<GameObject> characterAdd;






  public void SelectCharacters(GameObject CharacterPref)
    {
     
        selectedprefab = CharacterPref;

       

          if (characters.Contains(CharacterPref))
        {
          infoPanel.InfoPanelOff();
            characters.Remove(CharacterPref); // Удаляем из отряда
             characterRemove?.Invoke();
         
           
            buttonObj.image.color = Color.white;
        }


          else if (characters.Count < 3)
        {
          infoPanel.InfoPanelOn(CharacterPref);
            characters.Add(CharacterPref);   // Добавляем в отряд
              characterAdd?.Invoke(CharacterPref);
             soundPlayer.PlaySound(sound);
           

             buttonObj.image.color = Color.green;
        }


        

      

    }
  

        

  public void SelectColor(Button button)
    {
      buttonObj = button;
    

    }


    
 
}
