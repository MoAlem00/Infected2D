using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance;
    
    public AudioSource musicSource;
    public AudioSource sFXSource;

    public AudioClip[] gameMusic;
    public AudioClip menuMusic;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }
    
    
    public void PlayMusic(AudioClip clip,float volume)
    {
        if (clip == null) return;
        
        musicSource.clip = clip;
        musicSource.loop = false;
        musicSource.volume = volume;
        musicSource.Play();
    }
    

    public void PlaySFX(AudioClip clip, float volume = 1.0f)
    {
        if (clip == null) return;
        
        sFXSource.loop = false;
        sFXSource.volume = volume;
        sFXSource.PlayOneShot(clip);
    }

    public void PlayMenuMusic()
    {
        PlayMusic(menuMusic, 0.5f);
    }

    public IEnumerator PlayShuffleMusic()
    {
        if (gameMusic == null || gameMusic.Length == 0) yield break;
        while (true)
        {
            int random = Random.Range(0, gameMusic.Length);
            PlayMusic(gameMusic[random],0.5f);
            yield return new WaitForSeconds(gameMusic[random].length);
        }
    }

}
