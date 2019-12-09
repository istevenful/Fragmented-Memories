using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    [SerializeField] float StartVolume = 0f;
    [SerializeField] float TargetVolume = 1f;
    [SerializeField] float FadeDuration = 3f;
    [SerializeField] float Range = 15f;

    private AudioSource audioSource;
    private GameObject Player;
    private GameObject E1;

    private bool AudioStopped = false;
    private float ElapsedTime = 0.0f;
    private float Volume = 0.0f;

    private void Start()
    {
        this.audioSource = GetComponent<AudioSource>();
        this.Player = GameObject.FindGameObjectWithTag("Player");
        this.E1 = GameObject.FindGameObjectWithTag("E1");
    }

    // Update is called once per frame
    void Update()
    {
        if (TargetIsInRange())
        {
            if (!this.audioSource.isPlaying)
            {
                this.audioSource.Play();
                StartCoroutine(StartFade(this.FadeDuration, this.StartVolume, this.TargetVolume));
            }      
        }
        if (this.E1.GetComponent<EnemyAI>().IsDead() && !AudioStopped)
        {
            AudioStopped = true;
            StartFade(7f, 0f, this.Volume);
        }
    }

    private bool TargetIsInRange()
    {
        // Debug.Log(Vector2.Distance(this.audioSource.transform.position, this.Player.transform.position));
        return Vector2.Distance(this.audioSource.transform.position, this.Player.transform.position) < this.Range;
    }

    IEnumerator StartFade(float fadeDuration, float startVolume, float targetVolume)
    {
        this.ElapsedTime = 0;
        while (this.ElapsedTime < fadeDuration)
        {
            this.audioSource.volume = Mathf.Lerp(startVolume, targetVolume, this.ElapsedTime / fadeDuration);
            this.Volume = this.audioSource.volume;
            this.ElapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        if (Mathf.Approximately(this.TargetVolume, 0f))
        {
            this.audioSource.Stop();
        }

        yield break;
    }
}
