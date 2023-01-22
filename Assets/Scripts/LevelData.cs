using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom Menu/Level")]
public class LevelData : ScriptableObject
{
    [SerializeField]
    private Level[] _levels;
    public Level curentLevel 
    {
        get { return _levels[0]; }
    }

}

[Serializable]
public class Level
{
    [SerializeField]
    private GameObject prefabLevel;

    public GameObject PrefabLevel {get{ return prefabLevel; }}

    [SerializeField]
    private NumberEnemies numberEnemies;
    public NumberEnemies NumberEnemies { get { return numberEnemies; } }
}
[Serializable]
public struct NumberEnemies
{
    public int NumberLine;
    public int NumberRow;
}
