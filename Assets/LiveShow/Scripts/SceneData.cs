using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "NewScene", menuName = "ScriptableObjects/SceneData")]
public class SceneData : ScriptableObject
{
    public string sceneName;
    public VideoClip videoIn;  // Path to input video
    public VideoClip videoOut; // Path to transition video
    public bool loop = true;
}
