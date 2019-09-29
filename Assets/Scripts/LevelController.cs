using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameObject winLabel;
    private int numberOfAttackers = 0;
    private bool levelTimerFinished = false;
    private float waitToLoad = 4f;

    public void AttackerSpawned()
    {
        numberOfAttackers++;
    }
    
    public void AttackerDestroy()
    {
        numberOfAttackers--;
        if (numberOfAttackers <= 0 && levelTimerFinished)
            StartCoroutine(HandleWinCondition());
    }

    private void Start()
    {
        winLabel.SetActive(false);
    }

    private IEnumerator HandleWinCondition()
    {
        winLabel.SetActive(true);
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(waitToLoad);
        if (SceneManager.GetActiveScene().name == "Level5")
            FindObjectOfType<LevelLoader>().LoadScene("Level5");
        else FindObjectOfType<LevelLoader>().LoadNextScene();
        
    }

    public void LevelTimerFinished()
    {
        levelTimerFinished = true;
        StopSpawners();
    }

    private void StopSpawners()
    {
        AttackerSpawner[] spawnerArray = FindObjectsOfType<AttackerSpawner>();

        foreach (var spawner in spawnerArray)
        {
            spawner.StopSpawning();
        }
    }
}
