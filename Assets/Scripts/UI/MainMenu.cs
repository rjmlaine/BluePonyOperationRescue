using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1); // Lataa ensimm�isen scenen ja aloittaa pelin
    }

    public void Quit()
    {
        Application.Quit(); // Lopettaa pelin (toimii vain buildissa)

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // testi k�ytt��n unityss�
#endif
    }
}
