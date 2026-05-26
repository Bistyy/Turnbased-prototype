using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip playerHit;
    public AudioClip playerHurt;
    public AudioClip enemyHit;
    public AudioClip enemyHurt;

    public void PlayAudio(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

}
