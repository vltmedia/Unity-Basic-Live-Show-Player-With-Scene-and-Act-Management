using System;
using System.Collections;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;         // The VideoPlayer component
    public RawImage videoDisplay;          // The UI RawImage to display the video
    public Image fadeOverlay;              // Fade overlay for transitions
    public float fadeDuration = 1.0f;      // Duration of fade transitions
    public float blackHoldDuration = 1.0f; // Duration to hold black after fade-out
    public int renderTextureWidth = 1920;  // Width of the RenderTexture
    public int renderTextureHeight = 1080; // Height of the RenderTexture
    public Button blackoutButton;
    public Button fadeToggleButton;
    public UnityEvent<float> onTransitionLerp = new UnityEvent<float>();

    private RenderTexture renderTexture;   // The RenderTexture to hold video frames

    private void Start()
    {
        if (blackoutButton)
        {
            blackoutButton.onClick.AddListener(FadeBlack);
        }
        if (fadeToggleButton)
        {
            fadeToggleButton.onClick.AddListener(FadeToggle);
        }
        SetupRenderTexture();
    }

    private void FadeToggle()
    {
        if(SceneActManager.Instance.sceneLoaded == false)
        {
            SceneActManager.Instance.SendStatusMessage("No scene loaded");
            return;
        }
        if(SceneActManager.Instance.isTransitioning)
        {
            SceneActManager.Instance.SendStatusMessage("Please wait for the transition to finish.");
            return;
        }
        switch(fadeState)
        {
            case 0:
                SceneActManager.Instance.SendStatusMessage("Fade Toggled: Fading In");

                StartCoroutine(Fade(1));
                break;
            case 1:
                SceneActManager.Instance.SendStatusMessage("Fade Toggled: Fading Out");

                StartCoroutine(Fade(0));
                break;
        }
    }

    public static VideoManager Instance;

        private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    /// <summary>
    /// Sets up a RenderTexture and assigns it to the VideoPlayer and RawImage.
    /// </summary>
    private void SetupRenderTexture()
    {
        // Create a new RenderTexture
        renderTexture = new RenderTexture(renderTextureWidth, renderTextureHeight, 0);
        renderTexture.Create();

        // Assign the RenderTexture to the VideoPlayer
        videoPlayer.targetTexture = renderTexture;

        // Assign the RenderTexture to the RawImage for display
        if (videoDisplay != null)
        {
            videoDisplay.texture = renderTexture;
        }
    }

    /// <summary>
    /// Plays a video with optional fade-in/out transitions and a black hold duration.
    /// </summary>
    public IEnumerator PlayVideo(string videoPath, string transitionVideo = null)
    {
        // Fade out the current video
        yield return Fade(1);

        // Hold on black for the specified duration
        yield return new WaitForSeconds(blackHoldDuration);

        // Play the transition video, if provided
        if (!string.IsNullOrEmpty(transitionVideo))
        {
            videoPlayer.url = transitionVideo;
            videoPlayer.Play();
            while (videoPlayer.isPlaying)
            {
                yield return null;
            }
        }

        // Load and prepare the main video
        videoPlayer.url = videoPath;
        videoPlayer.Prepare();
        while (!videoPlayer.isPrepared)
        {
            yield return null; // Wait until the video is ready
        }

        // Play the main video
        videoPlayer.Play();

        // Fade in the new video
        yield return Fade(0);
    }
    public void FadeBlack()
    {
        if (SceneActManager.Instance.sceneLoaded == false)
        {
            SceneActManager.Instance.SendStatusMessage("No scene loaded");
            return;
        }
        if (SceneActManager.Instance.isTransitioning)
        {
            SceneActManager.Instance.SendStatusMessage("Please wait for the transition to finish.");
            return;
        }
        SceneActManager.Instance.SendStatusMessage("Fading to black." );

        StartCoroutine(Fade(1));
    }
    public IEnumerator PlayVideo(VideoClip videoPath, VideoClip transitionVideo = null, bool loop = true)
    {
        // Fade out the current video
        if (fadeState != 1) {
        yield return Fade(1);
    }

        // Play the transition video, if provided
        //if (transitionVideo)
        //{
        //    videoPlayer.clip = transitionVideo;
        //    videoPlayer.Play();
        //    while (videoPlayer.isPlaying)
        //    {
        //        yield return null;
        //    }
        //}

        // Load and play the main video
        videoPlayer.isLooping = loop;
        videoPlayer.clip = videoPath;
        videoPlayer.Play();
        yield return new WaitForSeconds(blackHoldDuration);


        // Fade in the new video
        yield return Fade(0);
        SceneActManager.Instance.isTransitioning = false;
    }
    public float fadeState = 0;
    /// <summary>
    /// Fades the overlay in or out.
    /// </summary>
    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeOverlay.color.a;
        float elapsed = 0;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / fadeDuration);
            onTransitionLerp.Invoke(alpha);
            fadeOverlay.color = new Color(0, 0, 0, alpha);
            fadeState = alpha;
            yield return null;
        }
    }

    private void OnDestroy()
    {
        // Clean up the RenderTexture when the object is destroyed
        if (renderTexture != null)
        {
            renderTexture.Release();
        }
    }
}
