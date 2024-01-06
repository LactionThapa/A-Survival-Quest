using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip gameplayMusic;
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        audioSource.clip = gameplayMusic;
        audioSource.Play();
        StartCoroutine(TrackLoop(gameplayMusic.length));
    }

    private IEnumerator TrackLoop(float duration)
    {
        yield return new WaitForSeconds(duration);
        audioSource.Play();
    }

}
