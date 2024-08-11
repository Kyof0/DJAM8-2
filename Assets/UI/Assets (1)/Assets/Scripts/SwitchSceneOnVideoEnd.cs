using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SwitchSceneOnVideoEnd : MonoBehaviour
{
    // Reference to the VideoPlayer component
    public VideoPlayer videoPlayer;

    void Start()
    {
        // Ensure the VideoPlayer component is assigned
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        // Subscribe to the video end event
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    // Method called when the video finishes playing
    void OnVideoEnd(VideoPlayer vp)
    {
        // Load the next scene
        SceneManager.LoadScene("SampleScene");
    }
}
