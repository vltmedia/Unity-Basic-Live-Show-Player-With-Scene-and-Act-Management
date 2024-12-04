using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAct", menuName = "ScriptableObjects/ActData")]
public class ActData : ScriptableObject
{
    public string actName;
    public List<SceneData> scenes;
}
