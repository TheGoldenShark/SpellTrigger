using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadPersistentData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // If PersistentData is not loaded, load PersistentData
        if (!SceneManager.GetSceneByName("PersistentData").isLoaded)
        {
            SceneManager.LoadScene("PersistentData", LoadSceneMode.Additive);
        }
        // Always destroy this object after the code above has finished
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
