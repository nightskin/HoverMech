using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    public List<Checkpoint> progress;
    public int checkPoints = 3;
    public GameObject Greeting;

    private void Start()
    {
        Greeting = GameObject.Find("Win Greeting");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(progress.Count == checkPoints)
            {
                Greeting.SetActive(true);
                NextLevel();
            }
        }
    }

    void NextLevel()
    {
        string nextScene = SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1).name;
        if(nextScene != "GameOver")
        {
            SceneManager.LoadScene(nextScene);
        }
        
    }

}
