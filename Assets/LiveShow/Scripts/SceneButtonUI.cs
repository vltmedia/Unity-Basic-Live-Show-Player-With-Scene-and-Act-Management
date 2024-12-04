using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneButtonUI : MonoBehaviour
{
    public TextMeshProUGUI sceneNameText;
    public Image indicatorImage;
    private SceneData sceneData;
    private System.Action<SceneData, SceneButtonUI> onSceneClicked;

    public void Initialize(SceneData data, System.Action<SceneData, SceneButtonUI> callback)
    {
        sceneData = data;
        sceneNameText.SetText( data.sceneName);
        onSceneClicked = callback;
        SetIndicator(false);
    }

    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    
    }
    public void OnClick()
    {
        onSceneClicked?.Invoke(sceneData, this);
    }

    public void SetIndicator(bool isActive)
    {
        indicatorImage.gameObject.SetActive(isActive);
    }
}
