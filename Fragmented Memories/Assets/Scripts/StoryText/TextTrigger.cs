using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/*
 * Textmeshpro shares materials by default so when change color of one text mesh, another textmeshes color will also change
 * To get around this, you'll need to change the font asset under textmesh pro to a duplicated and seperate material
 * This means things will get really messy and annoying with lots of text...
 *
 * Also, needed to reuse fadeout here because coroutines were getting shared between the objects for some reason.
 */

namespace TMPro
{
    class TextTrigger : MonoBehaviour
    {
        [SerializeField] float TriggerDistance = 10f;
        private float TimePassed = 0f;
        private float FadeInTime = 2f;
        private float FadeTime = 2f;
        private TextMeshPro StoryText;
        private GameObject Player;
        private FadeText FadeTextEffect;
        private float OriginalFontSize;

        private bool TextVisible = false;
        private void Start()
        {
            // Fade text if there is text.
            this.StoryText = this.GetComponent<TextMeshPro>();
            this.FadeTextEffect = this.GetComponent<FadeText>();
            this.OriginalFontSize = this.StoryText.fontSize;
            this.StoryText.fontSize = 0f;
            this.Player = GameObject.FindGameObjectWithTag("Player");
        }

        private void Update()
        {
            // Debug.Log(Vector2.Distance(this.StoryText.transform.position, this.Player.transform.position));
            if (Vector2.Distance(this.StoryText.transform.position, this.Player.transform.position) < this.TriggerDistance)
            {
                if (!this.TextVisible)
                {
                    // Fade in and then fade out after determined fade time.
                    StoryText.fontSize = this.OriginalFontSize;
                    this.TextVisible = true;
                    StartCoroutine(FadeIn(true));
                }
            }
        }

        IEnumerator FadeIn(bool FadeOutAfter)
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
            if (FadeOutAfter)
            {
                this.StoryText.color = originalColor;
                StartCoroutine(FadeOut());
            }
        }

        IEnumerator FadeOut()
        {
            this.TimePassed = 0f;
            TextMeshPro text = GetComponent<TextMeshPro>();
            Color originalColor = text.color;
            while (this.TimePassed < this.FadeTime)
            {
                this.TimePassed += Time.deltaTime;
                this.StoryText.color = Color.Lerp(originalColor, Color.clear, this.TimePassed / this.FadeTime);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
