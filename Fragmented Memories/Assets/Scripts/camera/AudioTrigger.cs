using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] float StartVolume;
    [SerializeField] float TargetVolume;
    [SerializeField] float FadeDuration;

    private float ElapsedTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!this.audioSource.isPlaying)
            {
                this.audioSource.Play();
                StartCoroutine(StartFade(this.FadeDuration, this.StartVolume, this.TargetVolume));
            }
            else
            {
                StartCoroutine(StartFade(this.FadeDuration, 1, 0));
            }
        }
    }

    IEnumerator StartFade(float fadeDuration, float startVolume, float targetVolume)
    { 
        while (this.ElapsedTime < fadeDuration)
        {
            this.audioSource.volume = Mathf.Lerp(startVolume, targetVolume, this.ElapsedTime / fadeDuration);
            this.ElapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }
}
