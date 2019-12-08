using UnityEngine;
using System.Collections;

namespace TMPro
{
    public class TextFollow : MonoBehaviour
    {
        [SerializeField] float FollowTime = 0f;
        private float TimePassed;

        private TextMeshPro StoryText;
        private GameObject Player;

        private void Start()
        {
            // Fade text if there is text.
            StoryText = this.GetComponent<TextMeshPro>();
            Player = GameObject.FindGameObjectWithTag("Player");
        }

        // Update is called once per frame
        void Update()
        {
            if (this.FollowTime > 0f)
            {
                this.TimePassed += Time.deltaTime;
                if (this.TimePassed < this.FollowTime)
                {
                    this.StoryText.transform.position = new Vector2(this.Player.transform.position.x + 1, this.Player.transform.position.y + 4f);
                }
            }
            else
            {
                this.StoryText.transform.position = new Vector2(this.Player.transform.position.x + 1, this.Player.transform.position.y + 4f);
            }
        }
    }
}
