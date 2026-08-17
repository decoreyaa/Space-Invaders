using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioClip clickSound;
    public AudioClip laserSound;
    public AudioClip winSound;
    public AudioClip loseSound;

    private AudioSource sfxSource;

    void Awake()
    {
        Instance = this;
        sfxSource = GetComponent<AudioSource>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayClick()
    {
        sfxSource.PlayOneShot(clickSound);
    }

    public void PlayLaser()
    {
        sfxSource.PlayOneShot(laserSound);
    }

    public void PlayWin()
    {
        sfxSource.PlayOneShot(winSound);
    }

    public void PlayLose()
    {
        sfxSource.PlayOneShot(loseSound);
    }
}
