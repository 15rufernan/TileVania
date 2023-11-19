using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPortal : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] float timeScale = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        Time.timeScale = timeScale;
        yield return new WaitForSecondsRealtime(loadDelay);
        Time.timeScale = 1f;
        FindObjectOfType<ScenePersist>().Delete();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
