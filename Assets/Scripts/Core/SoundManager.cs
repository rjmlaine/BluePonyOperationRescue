using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource source;
    private AudioSource musicsource;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        musicsource = transform.GetChild(0).GetComponent<AudioSource>();

        // Pidetään audio vaikka mennään uuteen skeneen ja deletoidaan kaksois kappaleet

        if (instance == null )
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
       else if (instance != null && instance != this)
            Destroy(gameObject);

        // Alku arvot
        changemusicvolume(0);
        changesoundvolume(0);
    }



    public void PlaySound(AudioClip _sound)
    {
        source.PlayOneShot(_sound);
    }

    // Äänien säätö
    public void changesoundvolume(float _change)
    {
        //Perus arvo
        float baseVolume = 1;
        
        // Otetaan perus arvo ja vaihdetaan sitä
        float currentVolume = PlayerPrefs.GetFloat("ChangeVolume", 1);  // Lataa viimisimmän tallenetun arvon
        currentVolume += _change;

        // Katsotaan ollaanko max tai min arvossa
        if (currentVolume > 1)
            currentVolume = 0;
        else if (currentVolume < 0)
            currentVolume = 1;

        // Lopullinen arvo
        float finalVolume = currentVolume * baseVolume;
        source.volume = finalVolume;

        // Tallennetaan lopullinen arvo player prefs
        PlayerPrefs.SetFloat("ChangeVolume", currentVolume);
    }

    public void changemusicvolume(float _change)
    {
        //Perus arvo
        float baseVolume = 0.3f;

        float currentVolume = PlayerPrefs.GetFloat("MusicVolume", 1);  // Lataa viimisimmän tallenetun arvon
        currentVolume += _change;

        // Katsotaan ollaanko max tai min arvossa
        if (currentVolume > 1)
            currentVolume = 0;
        else if (currentVolume < 0)
            currentVolume = 1;

        // Lopullinen arvo
        float finalVolume = currentVolume * baseVolume;
        musicsource.volume = finalVolume;

        // Tallennetaan lopullinen arvo player prefs
        PlayerPrefs.SetFloat("MusicVolume", currentVolume);
    }
}

