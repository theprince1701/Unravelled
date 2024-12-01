using UnityEngine;

[CreateAssetMenu(menuName = "LevelStats", fileName = "LevelStats")]
public class LevelStats : ScriptableObject
{
    public string currentLevelName;
    public string nextLevelName;
    public int startingThreads;
    
    [Space]
    
    
    public int threadsBackForDismantle = 2;
    public int threadsCostForStretch = 1;
    public int threadsCostForLevitate = 1;

}
