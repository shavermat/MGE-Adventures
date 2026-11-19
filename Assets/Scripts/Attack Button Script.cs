
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class AttackButtonScript : MonoBehaviour
{
   public Button attackButton;
 [SerializeField] private TextMeshProUGUI textField;


private void Start()
   {
     
   }
   public void Setup(string text, Color color)
    {
        if (!gameObject.activeSelf) gameObject.SetActive(true);

        textField.text = text;
        ColorBlock cb = attackButton.colors ;
        cb.normalColor = color;
        attackButton.colors = cb;
        
       

            

    }
    public void Hide()
    {
         if (gameObject.activeSelf) gameObject.SetActive(false);
    }
}
