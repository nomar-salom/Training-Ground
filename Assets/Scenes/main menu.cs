using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayGame() {
        Debug.Log("PlayGame method called");
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame() {
        Application.Quit();
    }

    void Update() {
    if (Input.GetKeyDown(KeyCode.P))
    {
        Debug.Log("Loading SampleScene");
        SceneManager.LoadScene("SampleScene");
    }
    }
}
