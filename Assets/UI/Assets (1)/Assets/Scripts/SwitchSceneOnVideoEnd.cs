using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SwitchSceneOnVideoEnd : MonoBehaviour
{
    // Reference to the VideoPlayer component
    public VideoPlayer videoPlayer;

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Load the next scene
        SceneManager.LoadScene("SampleScene");
        AudioManager.Instance.PlaySFX("sword");
        AudioManager.Instance.PlayMusic("fight");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Stop the video
            videoPlayer.Pause();

            // Optionally, you can add other actions here
            SceneManager.LoadScene("SampleScene");
            AudioManager.Instance.PlaySFX("click");
            AudioManager.Instance.PlaySFX("sword");
            AudioManager.Instance.PlayMusic("fight");
        }
    }
}
