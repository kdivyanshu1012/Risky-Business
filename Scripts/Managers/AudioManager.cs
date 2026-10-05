using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Source")]
    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private AudioSource[] bgm;
    private int bgmIndex;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        InvokeRepeating("playMusic", 0, 2);
    }

    public void PlaySFX(int sfxIndex)
    {
        if(sfxIndex >= sfx.Length)
        {
            return;
        }
        sfx[sfxIndex].Play();
    }

    public void StopSFX(int sfxIndex)
    {
        sfx[sfxIndex].Stop();

    }

    public void PlayBgm(int localBgmIndex)
    {
        for(int i = 0; i<bgm.Length; i++)
        {
            bgm[i].Stop();
        }
        bgmIndex = localBgmIndex;
        bgm[localBgmIndex].Play();
    }

    public void playRandomBgm()
    {
        bgmIndex = Random.Range(0, bgm.Length);
        PlayBgm(bgmIndex);
    }

    public void playMusic()
    {
        if (bgm[bgmIndex].isPlaying == false)
        {
            playRandomBgm();
        }
    }





}
