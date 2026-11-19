
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCardSystem : MonoBehaviour
{
   [SerializeField] private GameObject Character;

   private Image buttonImage;

   private CharacterData characterData;
   public TextMeshProUGUI textname;

    void Start()
    {
      

      characterData = Character.GetComponent<CharacterData>();
       buttonImage = GetComponent<Image>();
        if (!characterData.IsUnlocked() && !characterData.isStartCharacter) GetComponent<Button>().interactable = false;
        
            
       buttonImage.sprite = Character.GetComponent<CharacterData>().Sprite;

     textname.text = characterData.characterName;
      
    }



}
