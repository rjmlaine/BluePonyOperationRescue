
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Game over")]
    [SerializeField] private GameObject gameoverscreen;
    [SerializeField] private AudioClip gameoversound;

    [Header("Pause")]
    [SerializeField] private GameObject pausescreen;

    [Header("Continue")]
    [SerializeField] private GameObject continuescreen;


    private void Awake()
    {
        gameoverscreen.SetActive(false);                 // deaktivoidaan game over screen
        pausescreen.SetActive(false);                    // deaktivoidaan pause screen
    }

    public void Gameover()
    {
        gameoverscreen.SetActive(true);                 // aktivoidaan game over screen
        SoundManager.instance.PlaySound(gameoversound); // soitetaan game over efekti
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))            // Katsotaan onko esc painettu
        {
            if(pausescreen.activeInHierarchy)           // Jos pause screen on jo aktiivisena 
            PauseGame(false);
            else
            PauseGame(true);
        }
    }

    // Game over funktiot
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Mainmenu()
    {
        SceneManager.LoadScene(1);
    }

    // Intro continue, ladataan main menu
    public void Continue()
    {
        SceneManager.LoadScene(1);
    }

    // Cutscene next level
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Lataa seuraavan levelin
    }

    // Quit game
    public void Quit()
    {
        Application.Quit(); // Lopettaa pelin (toimii vain buildissa)

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // testi käyttöön unityssä
        #endif
    }

    // Pause game

    public void PauseGame(bool status)
    {
        // Jos status == true pause, jos status == false unpause
        pausescreen.SetActive(status);
        
        if(status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    // Äänien säätö

    public void ChangeVolume()
    {
        SoundManager.instance.changesoundvolume(0.2f);
    }

    public void MusicVolume()
    {
        SoundManager.instance.changemusicvolume(0.2f);
    }


}
