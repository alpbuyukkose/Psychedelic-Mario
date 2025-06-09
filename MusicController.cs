using UnityEngine;

public class MusicController : MonoBehaviour
{
    private void Awake()
    {
        
        if (FindObjectsOfType<MusicController>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject); 
    }
}
