using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace TMPro
{
    class FadeText : MonoBehaviour
    {
        //Fade time in seconds
        private TextMeshPro StoryText;
        [SerializeField] float FadeTime = 5f;
        [SerializeField] float FadeInTime = 5f;
        private float TimePassed = 0f;

        private void Start()
        {
            // Fade text if there is text.
            StoryText = this.GetComponent<TextMeshPro>();
            if (!this.StoryText.text.Equals(""))
            {
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
