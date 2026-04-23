using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance;
    
    
    public AudioSource musicSource;
    
    [SerializeField] private AudioSource sFXSource;
    [SerializeField] private AudioClip[] gameMusic;
    [SerializeField] private AudioClip menuMusic;


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
    
    
    
    private void PlayMusic(AudioClip clip)//,float volume = 0.6f)
    {
        if (clip == null) return;
        
        musicSource.clip = clip;
        musicSource.loop = false;
        //musicSource.volume = volume;
        musicSource.Play();
    }
    

    public void PlaySFX(AudioClip clip, float volume)
    {
        if (clip == null) return;
        
        sFXSource.loop = false;
        sFXSource.volume = volume;
        sFXSource.PlayOneShot(clip);
    }

    public void PlayMenuMusic()
    {
        PlayMusic(menuMusic);
    }

    public IEnumerator PlayShuffleMusic()//play shuffle music
    {
        if (gameMusic == null || gameMusic.Length == 0) yield break; 
        while (true)
        {
            int random = Random.Range(0, gameMusic.Length);
            PlayMusic(gameMusic[random]);
            yield return new WaitForSeconds(gameMusic[random].length);
        }
    }
    
    public void StopSoundsEffects(AudioSource source)
    {
        source.Stop();
    }
}
