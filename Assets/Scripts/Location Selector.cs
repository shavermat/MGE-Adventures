using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;


public class LocationSelector : MonoBehaviour
{
   public List<LocationConfig> AllLocation = new List<LocationConfig>();
  [NonSerialized] public  LocationConfig SelectedLocation;
   public TextMeshProUGUI locationText;



    void Awake()
    {
        
        
        foreach(LocationConfig location in AllLocation)
        {
             if (location == null) continue;

         bool isLocationCompleted = PlayerPrefs.GetInt(location.ID, 0) == 1;
            if (!isLocationCompleted)
            {
                SelectedLocation = location;
             
                break;
            }
        }
        
        
      if(SelectedLocation == null)
        SelectedLocation = AllLocation.LastOrDefault(location => location != null && PlayerPrefs.GetInt(location.ID, 0)== 1);


         locationText.text = SelectedLocation.locationNumber;
    }

}
