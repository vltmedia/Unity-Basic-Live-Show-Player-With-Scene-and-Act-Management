using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneActManager : MonoBehaviour
{
    [Header("References")]
    public GameObject actButtonPrefab;      // Prefab for Act buttons
    public Transform actButtonContainer;   // Parent container for Act buttons
    public VideoManager videoManager;      // Reference to the VideoManager
    public Transform sceneContainerPosition;      // Reference to the VideoManager
    public Transform sceneCanvas;      // Reference to the VideoManager

    [Header("Data")]
    public ShowData showData;
    public List<ActData> acts { get { return showData.acts; } set { showData.acts = value; } }        // List of acts to populate the UI

    private SceneData currentScene;        // Tracks the currently active scene
    public ActButtonUI currentAct;
    public  bool isTransitioning = false;        // Tracks if the scene is transitioning
    public bool sceneLoaded { get { return currentScene != null; } }
    public UnityEvent<string> onMessage = new UnityEvent<string>();
    /// <summary>
    /// Initializes the Act and Scene UI at the start.
    /// </summary>
    private void Start()
    {
        InitializeUI();
    }
    public static SceneActManager Instance;

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
    public void ToggleActClicked(ActButtonUI act)
    {
        if (currentAct == act)
            return; // Do nothing if the clicked act is already active
        if (currentAct != null)
        {
            currentAct.SetIndicator(false);
            currentAct.ToggleScenes();
        }
        act.ToggleScenes();
        currentAct = act;
        currentAct.SetIndicator(true);

    }
    /// <summary>
    /// Dynamically creates Act buttons and their associated Scene buttons.
    /// </summary>
    private void InitializeUI()
    {
        foreach (var act in acts)
        {
            // Create Act Button
            var actButton = Instantiate(actButtonPrefab, actButtonContainer);
            var actButtonScript = actButton.GetComponent<ActButtonUI>();

            // Pass the Act data and callback for scene button clicks
            actButtonScript.Initialize(act, OnSceneButtonClicked);
        }
    }

    public void SendStatusMessage(string statusMessage)
    {
        onMessage?.Invoke(statusMessage);
    }

    /// <summary>
    /// Handles scene transitions when a scene button is clicked.
    /// </summary>
    /// <param name="scene">The new scene to transition to.</param>
    public void OnSceneButtonClicked(SceneData scene)
    {
        if (currentScene == scene || isTransitioning)
            return; // Do nothing if the clicked scene is already active
        isTransitioning = true;
        StartCoroutine(TransitionScene(scene));
    }

    /// <summary>
    /// Handles the transition sequence between scenes.
    /// </summary>
    /// <param name="newScene">The new scene to load.</param>
    private IEnumerator TransitionScene(SceneData newScene)
    {

        // Determine if there�s a transition video from the current scene
        var transitionVideo = currentScene != null ? currentScene.videoOut : null;
        // Update the current scene
        currentScene = newScene;

        // Play the new video through the VideoManager
        yield return videoManager.PlayVideo(newScene.videoIn, transitionVideo);
    }
}
