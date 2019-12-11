using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/*
 * Textmeshpro shares materials by default so when changing color of one text mesh, another textmesh's color will also change.
 * To get around this, you'll need to change the font asset under textmesh pro to a duplicated and seperate material
 */

namespace TMPro
{
    class TextTrigger : MonoBehaviour
    {
        [SerializeField] float TriggerDistance = 5f;

        [SerializeField] float FadeInTime = 2f;
        [SerializeField] float FadeOutTime = 4f;
        [SerializeField] float FollowTime = 2f;

        [SerializeField] float EnlargeTime = 0f;
        [SerializeField] float EnlargeSize = 0f;

        [SerializeField] float ShrinkTime = 0f;
        [SerializeField] float ShrinkSize = 0f;

        [SerializeField] GameObject StopTrigger;
        private bool StartFollowing = false;

        private float TimePassed = 0f;
        private bool TextStopped = false;

        private TextMeshPro StoryText;
        private GameObject Player;
        private bool TextVisible = false;
        private bool EnglargeStarted = false;
        private void Start()
        {
            // Fade text if there is text.
            this.StoryText = this.GetComponent<TextMeshPro>();
            this.Player = GameObject.FindGameObjectWithTag("Player");
        }

        private void Update()
        {
            if (this.StopTrigger != null && !this.TextStopped)
            {
                //Debug.Log(Vector2.Distance(this.StopTrigger.transform.position, this.Player.transform.position));

                if (Vector2.Distance(this.StopTrigger.transform.position, this.Player.transform.position) < 10f)
                {
                    this.TextStopped = true;
                    this.StoryText.color = Color.clear;
                }
            }
            if (Vector2.Distance(this.StoryText.transform.position, this.Player.transform.position) < this.TriggerDistance && !this.TextStopped)
            {
                if (!this.TextVisible)
                {
                    // Fade in and then fade out after determined fade time.
                    this.TextVisible = true;

                    this.StoryText.transform.position = new Vector3(this.StoryText.transform.position.x, this.StoryText.transform.position.y, this.Player.transform.position.z);
                    StartCoroutine(FadeIn());
                    if (FollowTime > 0f)
                    {
                        this.StartFollowing = true;
                    }
                    // Can't seem to figure out enlarge bug
                    if (EnlargeTime > 0f && !this.EnglargeStarted)
                    {
                        this.EnglargeStarted = true;
                        StartCoroutine(Enlarge());
                    }
                    if (this.ShrinkTime > 0f)
                    {
                        StartCoroutine(Shrink());
                    }

                }
            }
            if (this.StartFollowing)
            {
                Follow();
            }
        }

        private void Follow()
        {
            if (this.FollowTime > 0f && this.StoryText.fontSize > 0f && Vector2.Distance(this.StoryText.transform.position, this.Player.transform.position) < this.TriggerDistance)
            {
                this.TimePassed += Time.deltaTime;
                if (this.TimePassed < this.FollowTime)
                {
                    this.StoryText.transform.position = new Vector2(this.Player.transform.position.x + 1f, this.Player.transform.position.y + 2f);
                }
            }
            else
            {
                this.StoryText.transform.position = new Vector2(this.Player.transform.position.x + 1f, this.Player.transform.position.y + 2f);
            }
        }

        IEnumerator FadeIn()
        {
            this.TimePassed = 0f;
            TextMeshPro text = GetComponent<TextMeshPro>();
            Color originalColor = text.color;
            while (this.TimePassed < this.FadeInTime)
            {
                this.TimePassed += Time.deltaTime;
                this.StoryText.color = Color.Lerp(Color.clear, originalColor, this.TimePassed / this.FadeInTime);
                yield return new WaitForEndOfFrame();
            }
            if (this.FadeOutTime > 0f)
            {
                StartCoroutine(FadeOut());
            }
        }

        IEnumerator FadeOut()
        {
            this.TimePassed = 0f;
            TextMeshPro text = GetComponent<TextMeshPro>();
            Color originalColor = text.color;
            while (this.TimePassed < this.FadeOutTime)
            {
                this.TimePassed += Time.deltaTime;
                this.StoryText.color = Color.Lerp(originalColor, Color.clear, this.TimePassed / this.FadeOutTime);
                yield return new WaitForEndOfFrame();
            }
        }

        private IEnumerator Enlarge()
        {
            this.TimePassed = 0f;
            TextMeshPro text = GetComponent<TextMeshPro>();
            float OriginalSize = text.fontSize;
            while (this.TimePassed < this.EnlargeTime)
            {
                this.TimePassed += Time.deltaTime;
                this.StoryText.fontSize = Mathf.Lerp(OriginalSize, OriginalSize + this.EnlargeSize, this.TimePassed / this.EnlargeTime);
                yield return new WaitForEndOfFrame();
            }
        }

        private IEnumerator Shrink()
        {
            this.TimePassed = 0f;
            TextMeshPro text = GetComponent<TextMeshPro>();
            float OriginalSize = text.fontSize;
            while (this.TimePassed < this.EnlargeTime)
            {
                this.TimePassed += Time.deltaTime;
                this.StoryText.fontSize = Mathf.Lerp(OriginalSize, OriginalSize - this.EnlargeSize, this.TimePassed / this.EnlargeTime);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
