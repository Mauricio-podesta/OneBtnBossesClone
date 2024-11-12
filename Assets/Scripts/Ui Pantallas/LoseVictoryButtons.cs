
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseVictoryButtons : MonoBehaviour
{
    
    public void SceneMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void SceneGame()
    {
        SceneManager.LoadScene("GameScene");
    }

}
