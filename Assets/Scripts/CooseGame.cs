using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CooseGame : MonoBehaviour
{
    public void GoToCoinGame()
    {
        SceneManager.LoadScene("Running Scene");
    }

    public void GoToShootingGame()
    {
        SceneManager.LoadScene("Shooting Scene");
    }
}
