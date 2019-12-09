using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace TMPro
{
    class EnlargeText : MonoBehaviour
    {
        //Fade time in seconds
        private TextMeshPro StoryText;
        [SerializeField] float EnlargeTime = 5f;
        [SerializeField] float EnlargeSize = 5f;
        private float TimePassed = 0f;

        private void Start()
        {
            // Fade text if there is text.
            StoryText = this.GetComponent<TextMeshPro>();
            if (!this.StoryText.text.Equals(""))
            {
                StartCoroutine(Enlarge());
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
    }
}
