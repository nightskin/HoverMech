using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    public bool move = false;
    public Vector3 target;

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //----------Menu Specific-----------//
    public void ModeSelect()
    {
        move = true;
        target = new Vector3(760, 0, -100);
    }

    public void BackToTitle()
    {
        move = true;
        target = Vector3.zero;
    }

    void Update()
    {
        if(move)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, target, 20 * Time.deltaTime);
        }

    }

    //--------------------------------//

    public void NewGame()
    {
        SceneManager.LoadScene("1");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }
    
    public void LoadGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Lv"));
    }

    public void Retry()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Lv"));
    }
}
