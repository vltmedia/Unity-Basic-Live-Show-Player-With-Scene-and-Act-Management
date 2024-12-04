using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Show", menuName = "ScriptableObjects/ShowData")]
public class ShowData : ScriptableObject
{
    public string showName;
    public List<ActData> acts;
}
