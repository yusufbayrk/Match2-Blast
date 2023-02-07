using GameModels;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// Author: Yusuf Bayrak
/// </summary>

public class PointerController : MonoBehaviour
{

    public static PointerController instance;
    public GameObject mouseTileObject;
    private TileController lastTileController;
    public List<Tile> beforeTiles = new List<Tile>();
    public TileController[] currentplayareaTiles;
    public Tile currentTile = null;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        currentTile = null;
    }
    public void Update()
    {
        PointerEventData pointer=new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();

        EventSystem.current.RaycastAll(pointer, raycastResults);

        if (Input.GetMouseButtonDown(0))
        {
           
            if (raycastResults.Count>0)
            {
                foreach (var check in raycastResults)
                {
                    if (check.gameObject.tag.Equals("Tile") && currentTile == null)
                    {

                        beforeTiles.Clear();

                        for (int i = 0; i < PlayAreaController.instance.playAreaTiles.Length; i++)
                        {
                            beforeTiles.Add(PlayAreaController.instance.playAreaTiles[i].tile);
                        }

                    }
                }



                foreach (var go in raycastResults)
                {
                    if (go.gameObject.tag.Equals("Tile") && currentTile == null)
                    {
                        lastTileController = go.gameObject.GetComponent<TileController>();
                        string name = go.gameObject.name;//tile(15)
                        string nameEmpty=string.Empty;
                        int index = 0;
                        int currentindex=0; 

                        for (int i = 0; i < name.Length; i++)
                        {
                            if (Char.IsDigit(name[i]))
                            {
                                nameEmpty+=name[i];
                            }
                        }

                        if (nameEmpty.Length > 0)
                        {
                            index=int.Parse(nameEmpty);
                            currentindex = index;
                        }

                       

                        

                            //currentTile = go.gameObject.GetComponent<TileController>().PutTile();
                            
                            
                            var explosionTiles=new List<Tile>();


                            var tiles = PlayAreaController.instance.playAreaTiles;

                            explosionTiles.Add(tiles[index].tile);

                            int remainder = index % 9;
                        int result;

                        if (index>=9)
                        {
                             result = index / 9;
                        }
                        else
                        {
                             result = 0;
                        }
                            

                            var indexList=new List<int>();
                            indexList.Add(index);
                            
                        for (int j = 0; j < indexList.Count; j++)
                        {
                            //right check
                            index=indexList[j];
                            for (int i = 1; i < (9 - (remainder + 1)); i++)
                            {
                                //right check
                                if (tiles[index + 1].tile.name == tiles[index].tile.name)
                                {
                                    bool check = true;
                                    foreach (var item in indexList)
                                    {
                                        if (item==index+1)
                                        {
                                            check = false;
                                        }
                                    }
                                    if (check)
                                    {
                                        indexList.Add(index + 1);

                                        index = index + 1;
                                        remainder = Remainder(index);
                                    }
                                    
                                }
                                else
                                {
                                    break;
                                }
                            }
                            index = indexList[j];
                            for (int i = 1; i < remainder + 1; i++)
                            {
                                //left check
                                if (tiles[index - 1].tile.name == tiles[index].tile.name)
                                {
                                    bool check = true;
                                    foreach (var item in indexList)
                                    {
                                        if (item == index - 1)
                                        {
                                            check = false;
                                        }
                                    }
                                    if (check)
                                    {
                                        indexList.Add(index - 1);
                                        index = index - 1;
                                        remainder = Remainder(index);
                                    }

                                    
                                }
                                else
                                {
                                    break;
                                }
                            }

                            index = indexList[j];
                           result= Result(index);
                            //up check
                            for (int i = 1; i < result + 1; i++)
                            {
                                //up check
                                if (tiles[index - (9 * 1)].tile.name == tiles[index].tile.name)
                                {
                                    bool check = true;
                                    foreach (var item in indexList)
                                    {
                                        if (item == index-(9 * 1))
                                        {
                                            check = false;
                                        }
                                    }
                                    if (check)
                                    {
                                        indexList.Add(index - (9 * 1));
                                        index = index - (9 * 1);
                                        result=Result(index);
                                    }

                                    
                                }
                                else
                                {
                                    break;
                                }
                            }

                            index = indexList[j];
                            result = Result(index);
                            for (int i = 1; i < 10 - (result + 1); i++)
                            {
                                //down check
                                if (tiles[index + (9 * 1)].tile.name == tiles[index].tile.name)
                                {
                                    bool check = true;
                                    foreach (var item in indexList)
                                    {
                                        if (item == index + (9 * 1))
                                        {
                                            check = false;
                                        }
                                    }
                                    if (check)
                                    {
                                        indexList.Add(index + (9 * 1));
                                        index=index + (9 * 1);
                                        result = Result(index);
                                    }

                                   
                                }
                                else
                                {
                                    break;
                                }
                            }


                        }

                        if (indexList.Count!=1)
                        {
                            for (int i = 0; i < indexList.Count; i++)
                            {
                                int abc = indexList[i];

                                tiles[indexList[i]].ClearTile();




                            }

                            for (int i = 0; i < 9; i++)
                            {
                                if (i == 2)
                                {
                                    Debug.Log("abc");
                                }
                                for (int j = 0; j < tiles.Length; j++)
                                {
                                    if (j <= 8)
                                    {
                                        if ((tiles[j].tile == null))
                                        {
                                            PlayAreaController.instance.RNGTileSetforNulls(j);
                                        }
                                        else
                                        {
                                            continue;
                                        }

                                    }

                                    else
                                    {
                                        if (tiles[j].tile == null)
                                        {
                                            tiles[j].tile = tiles[j - 9].tile;
                                            tiles[j - 9].ClearTile();
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }
                                }
                            }
                            Debug.Log(tiles);


                            for (int i = 0; i < tiles.Length; i++)
                            {
                                PlayAreaController.instance.playAreaTiles[i].PlaceTile(tiles[i].tile);
                            }
                        }
                        else
                        {
                            continue;
                        }

                        
                            
                    }
                }
            }
        }


    }

    public int Remainder(int index)
    {
        return index % 9;
    }

    public int Result(int index)
    {
        if (index>=9)
        {
            return index / 9;
        }
        else
        {
            return 0;
        }
        
    }

}
