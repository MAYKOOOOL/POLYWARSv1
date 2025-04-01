using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public Slider musicSlider;
    public Slider sfxSlider;

    public AudioClip buttonClickSound;
    public AudioClip boink;
    public AudioClip powerUpPick;
    public AudioClip scorePoint;
    public AudioClip BGMSounds;
    public AudioClip MainMenuSounds;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            if (musicSource == null)
            {
                musicSource = gameObject.AddComponent<AudioSource>();
                musicSource.loop = true;
            }

            if (sfxSource == null)
            {
                sfxSource = gameObject.AddComponent<AudioSource>();
            }
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
        ApplyVolume();

        PlayBGM(MainMenuSounds);
    }

    public void SetMusicVolume()
    {
        musicSource.volume = musicSlider.value;
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    public void SetSFXVolume()
    {
        sfxSource.volume = sfxSlider.value;
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
    }

    private void ApplyVolume()
    {
        musicSource.volume = musicSlider.value;
        sfxSource.volume = sfxSlider.value;
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void PlayBGM(AudioClip clip)
    {
        if (musicSource != null && clip != null)
        {
            Debug.Log("Playing BGM: " + clip.name);
            musicSource.Stop();
            musicSource.loop = true;
            musicSource.clip = clip;
            musicSource.Play();
        }
    }
}
