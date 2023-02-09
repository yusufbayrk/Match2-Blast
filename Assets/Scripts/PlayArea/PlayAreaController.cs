using GameModels;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[DefaultExecutionOrder(101)]


public class PlayAreaController : MonoBehaviour
{
    public static PlayAreaController instance;
    public PlayArea playArea;
    public TileController[] playAreaTiles;
    public TileSet_Scriptable tileSet;
    public int index = 0;
    public TextMeshProUGUI movement;
    public TextMeshProUGUI goal;

    void Awake()
    {
        instance = this;
        playAreaTiles=transform.Find("OpeningAreaTiles").GetComponentsInChildren<TileController>();
        RNGimageGoal();
    }

    // Start is called before the first frame update
    void Start()
    {
        RNGTileSet();
        RNGMovementNumber();
        RNGGoalNumber();
        

    }

    void RNGimageGoal()
    {
        var random = new System.Random();
        int index;
        var lowerbound = 0;
        var upperbound = 5;
        index = random.Next(lowerbound, upperbound);
       

        GameObject.FindGameObjectWithTag("Goal").GetComponent<Image>().sprite = tileSet.tiles[index].image;

    }

    public void  RNGMovementNumber()
    {
        var random = new System.Random();
        int index;

        var lowerbound = 23;
        var upperbound = 30;
        index = random.Next(lowerbound,upperbound);
        movement.text = index.ToString();

    }

    public void RNGGoalNumber()
    {
        var random = new System.Random();
        int index;

        var lowerbound = 23;
        var upperbound = 38;
        index = random.Next(lowerbound, upperbound);
        goal.text = index.ToString();

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
        //indexi 5

        var random = new System.Random();
        int index;
        var tileNames = new List<string>();
        
            index = random.Next(tileSet.tiles.Count);

            tileNames.Add(tileSet.tiles[index].name);
        

        InitTilesforNulls(tileNames,need);
    }

    public void InitTilesforNulls(List<string> tileString,int index)
    {
         
        for (int i = 0; i < tileString.Count; i++)
        {

            foreach (var item in tileSet.tiles)
            {

                if (item.name == tileString[i])
                {
                    playAreaTiles[index].PlaceTile(item);
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
