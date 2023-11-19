using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour
{
    int myScene;

    private void Awake()
    {
        var scenePersists = FindObjectsOfType<ScenePersist>();
        if (scenePersists.Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        myScene = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != myScene)
        {
            Destroy(gameObject);
        }
    }

    public void Delete()
    {
        Destroy(gameObject);
    }
}
