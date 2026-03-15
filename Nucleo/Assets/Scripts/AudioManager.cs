using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Background Music")]
    public AudioClip backgroundMusic;
    [Range(0f, 1f)] public float musicVolume = 0.5f;

    [Header("SFX")]
    public AudioClip alphaShootSfx;
    public AudioClip betaShootSfx;
    public AudioClip gammaShootSfx;
    public AudioClip cardSelectSfx;
    public AudioClip buttonPressSfx;
    public AudioClip healthLossSfx;
    public AudioClip enemyKillSfx;
    [Range(0f, 1f)] public float sfxVolume = 1f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        EnsureAudioSources();
    }

    void Start()
    {
        PlayBackgroundMusic();
    }

    void EnsureAudioSources()
    {
        if (musicSource == null)
            musicSource = gameObject.AddComponent<AudioSource>();

        if (sfxSource == null)
            sfxSource = gameObject.AddComponent<AudioSource>();

        musicSource.loop = true;
        musicSource.playOnAwake = false;
        musicSource.volume = musicVolume;

        sfxSource.loop = false;
        sfxSource.playOnAwake = false;
        sfxSource.volume = sfxVolume;
    }

    public void PlayBackgroundMusic()
    {
        if (musicSource == null || backgroundMusic == null)
            return;

        if (musicSource.clip != backgroundMusic)
            musicSource.clip = backgroundMusic;

        musicSource.volume = musicVolume;

        if (!musicSource.isPlaying)
            musicSource.Play();
    }

    public void PlayAlphaShoot()
    {
        PlaySfx(alphaShootSfx);
    }

    public void PlayBetaShoot()
    {
        PlaySfx(betaShootSfx);
    }

    public void PlayGammaShoot()
    {
        PlaySfx(gammaShootSfx);
    }

    public void PlayCardSelect()
    {
        PlaySfx(cardSelectSfx);
    }

    public void PlayButtonPress()
    {
        PlaySfx(buttonPressSfx);
    }

    public void PlayHealthLoss()
    {
        PlaySfx(healthLossSfx);
    }

    public void PlayEnemyKill()
    {
        PlaySfx(enemyKillSfx);
    }

    void PlaySfx(AudioClip clip)
    {
        if (sfxSource == null || clip == null)
            return;

        sfxSource.PlayOneShot(clip, sfxVolume);
    }
}
