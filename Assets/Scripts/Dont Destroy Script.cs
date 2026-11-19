using UnityEngine;

public class DontDestroyScript : MonoBehaviour
{
    
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame

}
