using GameModels;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;



public class PlayAreaController : MonoBehaviour
{
    public static PlayAreaController instance;
    public PlayArea playArea;
    public TileController[] playAreaTiles;
    public TileSet_Scriptable tileSet;
    public int index = 0;

    void Awake()
    {
        instance = this;
        playAreaTiles=transform.Find("OpeningAreaTiles").GetComponentsInChildren<TileController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        RNGTileSet();
    }


    public void RNGTileSet()
    {
        var random = new System.Random();
        int index;
        var tileNames = new List<string>();
        for (int i = 0; i < playAreaTiles.Length; i++)
        {
            index= random.Next(tileSet.tiles.Count);
            tileNames.Add(tileSet.tiles[index].name);
        }

        InitTiles(tileNames);
    }

    public void RNGTileSetforNulls(int need)
    {
        var random = new System.Random();
        int index;
        var tileNames = new List<string>();
        for (int i = 0; i < need; i++)
        {
            index = random.Next(tileSet.tiles.Count);
            tileNames.Add(tileSet.tiles[index].name);
        }

        InitTilesforNulls(tileNames);
    }

    public void InitTilesforNulls(List<string> tileString)
    {
         
        for (int i = 0; i < tileString.Count; i++)
        {

            foreach (var item in tileSet.tiles)
            {

                if (item.name == tileString[i])
                {
                    for (int j = 0; j < playAreaTiles.Length; j++)
                    {
                        if (playAreaTiles[j].tile == null)
                        {
                            playAreaTiles[j].PlaceTile(item);
                        }
                    }
                   

                    
                }


            }
        }
    }

    public void InitTiles(List<string> tileString)
    {

        for (int i = 0; i < tileString.Count; i++)
        {

            foreach (var item in tileSet.tiles)
            {

                if (item.name==tileString[i])
                {

                   

                    playAreaTiles[i].PlaceTile(item);
                }


            }
        }
    }
    public void GetCurrentPlayAreaTiles(TileController[] tiles)
    {
      tiles= transform.Find("OpeningAreaTiles").GetComponentsInChildren<TileController>();
    }
}
