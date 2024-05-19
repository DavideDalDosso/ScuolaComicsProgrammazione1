using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Level", menuName = "GOFairies/Level", order = 0)]
public class LevelSO : ScriptableObject
{
    public string displayName;
    public int sceneIndex;
}
