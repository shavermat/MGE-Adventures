
using UnityEngine;




public enum RarityState { Common, Rare, Epic, Legendary}


[DefaultExecutionOrder(100)]
public class CharacterData : MonoBehaviour
{
     public RarityState rarity;
    public string saveKey;   // ключ для доступа к playerpref
    public string characterName;  // Имя для отображения в UI
   public bool isStartCharacter;
    public Sprite Sprite;  // спрайт персонажа
   


    public bool IsUnlocked()
    {
        return PlayerPrefs.GetInt(saveKey, 0) == 1;

    }
    public void UnlockThisCharacter()
    {
        PlayerPrefs.SetInt(saveKey, 1);
        PlayerPrefs.Save();
    }

    [ContextMenu("заблокировать персонажа для выбора")]
     public void LockThisCharacter()
    {
        PlayerPrefs.SetInt(saveKey, 0);
        PlayerPrefs.Save();
    }
}
