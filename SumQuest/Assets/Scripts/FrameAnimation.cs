using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FrameAnimation : MonoBehaviour
{
    [SerializeField] private Image targetImage;
    [SerializeField] private Sprite[] frames;
    private float frameTime = 0.1f;
    public bool IsPlaying { get; private set; }

    public void Play()
    {
        IsPlaying = true;
        targetImage.gameObject.SetActive(true);
        targetImage.transform.SetAsLastSibling();
        StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation()
    {
        for (int i = 0; i < frames.Length; i++)
        {
            targetImage.sprite = frames[i];
            yield return new WaitForSeconds(frameTime);
        }
        IsPlaying = false;
        targetImage.gameObject.SetActive(false);
    }
}
