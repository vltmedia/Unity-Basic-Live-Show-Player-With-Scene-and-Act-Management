using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System;

public class ActButtonUI : MonoBehaviour
{
    public TextMeshProUGUI actNameText;            // Text for the Act's name
    public Image indicatorImage;                  // Indicator image for the Act
    public GameObject sceneButtonContainerPrefab; // Prefab for the floating scene container
    public GameObject sceneButtonPrefab; // Prefab for the floating scene container
    public Canvas canvas { get { return SceneActManager.Instance.sceneCanvas; } } // Reference to the UI canvas
    private GameObject sceneButtonContainer;      // Instance of the floating scene container

    private ActData actData;                      // Data for this act
    private System.Action<SceneData> onSceneClicked; // Callback for when a scene button is clicked
    private bool isContainerVisible = false;      // Tracks visibility state
    public SceneButtonUI currentSceneButton;
    /// <summary>
    /// Initializes the Act button with ActData and a callback for scene clicks.
    /// </summary>
    public void Initialize(ActData data, System.Action<SceneData> callback)
    {
        actData = data;
        actNameText.SetText(data.actName);
        onSceneClicked = callback;

        // Create a new floating scene button container
        CreateSceneButtonContainer();

        // Reset the indicator
        SetIndicator(false);
    }

    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(ToggleActClicked);
    }

    public void ToggleActClicked()
    {
        SceneActManager.Instance.ToggleActClicked(this);
    }

    /// <summary>
    /// Toggles the visibility of the floating scene button container.
    /// </summary>
    public void ToggleScenes()
    {
        isContainerVisible = !isContainerVisible;
        sceneButtonContainer.SetActive(isContainerVisible);

        if (isContainerVisible)
        {
            // Position the scene container below this act button
            Vector3 buttonPosition = transform.position;
            RectTransform containerRect = sceneButtonContainer.GetComponent<RectTransform>();
            containerRect.position = new Vector3(buttonPosition.x, buttonPosition.y - 50f, buttonPosition.z); // Adjust Y-offset as needed
        }
    }

    /// <summary>
    /// Creates the floating scene button container and populates it with buttons.
    /// </summary>
    private void CreateSceneButtonContainer()
    {
        // Instantiate the floating container as a sibling of the canvas
        sceneButtonContainer = Instantiate(sceneButtonContainerPrefab, SceneActManager.Instance.sceneContainerPosition);
        sceneButtonContainer.SetActive(false);

        // Populate the container with scene buttons
        foreach (var scene in actData.scenes)
        {
            var sceneButton = Instantiate(sceneButtonPrefab, sceneButtonContainer.transform);
            sceneButton.transform.SetParent(sceneButtonContainer.transform);
            var buttonScript = sceneButton.GetComponent<SceneButtonUI>();
            buttonScript.Initialize(scene, HandleSceneClicked);
        }
    }

    private void HandleSceneClicked(SceneData data, SceneButtonUI scene)
    {
        try
        {
            currentSceneButton?.SetIndicator(false);
        }catch (Exception e) { 
        
        }
        currentSceneButton = scene;
        currentSceneButton.SetIndicator(true);

        onSceneClicked?.Invoke(data);
    }

    /// <summary>
    /// Sets the indicator image for the Act.
    /// </summary>
    public void SetIndicator(bool isActive)
    {
        indicatorImage.gameObject.SetActive(isActive);
    }
}
