using GameModels;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileSet", menuName = "ScriptableObjects/Tile")]
public class TileSet_Scriptable : ScriptableObject
{
    [SerializeField]
    public List<Tile> tiles;
    
}
