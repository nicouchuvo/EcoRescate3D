using UnityEngine;

public class TextoParpadeo : MonoBehaviour
{
    CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup =
            GetComponent<CanvasGroup>();
    }

    void Update()
    {
        canvasGroup.alpha =
            Mathf.PingPong(
                Time.time,
                1f
            );
    }
}