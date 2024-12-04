using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public Slider videoSlider;
    public Slider transitionSlider;

    public Button PlayButton;
    public Button PauseButton;
    public Button RewindButton;
    public Button FastForwardButton;
    public VideoPlayer videoPlayer { get { return VideoManager.Instance.videoPlayer; } }



    // Start is called before the first frame update
    void Start()
    {
        VideoManager.Instance.onTransitionLerp.AddListener(OnTransitionLerp);
        PlayButton.onClick.AddListener(Play);
        PauseButton.onClick.AddListener(Pause);
        RewindButton.onClick.AddListener(Rewind);
        FastForwardButton.onClick.AddListener(FastForward);
        
    }

    /// <summary>
    /// Fast-forwards the video by a specific time interval (default is 10 seconds).
    /// </summary>
    public void FastForward()
    {
        if (videoPlayer == null )
        {
            Debug.LogWarning("VideoPlayer is not assigned.");
            return;
        }

        double newTime = videoPlayer.length;
        videoPlayer.time = Mathf.Clamp((float)newTime, 0, (float)videoPlayer.length);
    }

    /// <summary>
    /// Rewinds the video by a specific time interval (default is 10 seconds).
    /// </summary>
    public void Rewind()
    {
        if (videoPlayer == null )
        {
            SceneActManager.Instance.SendStatusMessage("VideoPlayer is not assigned .");
            return;
        }

        double newTime = 0;
        videoPlayer.time = Mathf.Clamp((float)newTime, 0, (float)videoPlayer.length);
        SceneActManager.Instance.SendStatusMessage("VideoPlayer has been Rewound");

    }

    /// <summary>
    /// Pauses the video.
    /// </summary>
    public void Pause()
    {
        if (videoPlayer == null)
        {
            SceneActManager.Instance.SendStatusMessage("VideoPlayer is not assigned.");
            return;
        }
        if (!videoPlayer.isPlaying)
        {
            SceneActManager.Instance.SendStatusMessage("Video is not playing.");
            return;
        }

        videoPlayer.Pause();
        SceneActManager.Instance.SendStatusMessage("VideoPlayer has been Paused.");

    }

    /// <summary>
    /// Resumes or starts playing the video.
    /// </summary>
    public void Play()
    {
        if (videoPlayer == null)
        {
            SceneActManager.Instance.SendStatusMessage("VideoPlayer is not assigned.");
            return;
        }
        if(videoPlayer.isPlaying)
        {
            SceneActManager.Instance.SendStatusMessage("Video is already playing.");
            return;
        }

        videoPlayer.Play();
        SceneActManager.Instance.SendStatusMessage("VideoPlayer has started Playing.");

    }

    private void OnTransitionLerp(float arg0)
    {
        transitionSlider.SetValueWithoutNotify(arg0);
    }

    // Update is called once per frame
    void Update()
    {

        if(VideoManager.Instance.videoPlayer.isPlaying)
        {
            videoSlider.SetValueWithoutNotify(VideoPlayerUtilities.GetNormalizedPlayTime(VideoManager.Instance.videoPlayer));
        }
    }
}

public static class VideoPlayerUtilities
{
    /// <summary>
    /// Returns the normalized play time (0 - 1) of the given VideoPlayer.
    /// </summary>
    /// <param name="videoPlayer">The VideoPlayer to evaluate.</param>
    /// <returns>A float value between 0 and 1 representing the current play time.</returns>
    public static float GetNormalizedPlayTime(VideoPlayer videoPlayer)
    {
        if (videoPlayer == null || videoPlayer.frameCount <= 0)
            return 0f; // Return 0 if the VideoPlayer is null or uninitialized.

        // Get the current frame and total frame count
        double currentFrame = videoPlayer.frame;
        double totalFrames = videoPlayer.frameCount;

        // Calculate the normalized play time
        return Mathf.Clamp01((float)(currentFrame / totalFrames));
    }
}