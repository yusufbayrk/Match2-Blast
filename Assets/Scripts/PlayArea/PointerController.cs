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
                            int result = index / 9;

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
                            for (int i = 1; i < 10 - (result + 1) + 1; i++)
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

                        #region checkes
                        ////right check
                        //for (int i = 1; i < (9-(remainder+1)); i++)
                        //    {
                        //        //right check
                        //        if (tiles[index + i].tile.name == tiles[index].tile.name)
                        //        {


                        //            indexList.Add(index + i);

                        //        }
                        //        else
                        //        {
                        //        break;
                        //        }
                        //    }


                        ////left check
                        //for (int i = 1; i < remainder + 1; i++)
                        //{
                        //    //left check
                        //    if (tiles[index - i].tile.name == tiles[index].tile.name)
                        //    {


                        //        indexList.Add(index - i);
                        //    }
                        //    else
                        //    {
                        //        break;
                        //    }
                        //}


                        ////up check
                        //for (int i = 1; i < result+1; i++)
                        //{
                        //    //up check
                        //    if (tiles[index - (9*i)].tile.name == tiles[index].tile.name)
                        //    {


                        //        indexList.Add(index - (9 * i));
                        //    }
                        //    else
                        //    {
                        //        break;
                        //    }
                        //}

                        ////down check
                        //for (int i = 1; i < 10-(result+1) + 1; i++)
                        //{
                        //    //down check
                        //    if (tiles[index + (9 * i)].tile.name == tiles[index].tile.name)
                        //    {


                        //        indexList.Add(index + (9 * i));
                        //    }
                        //    else
                        //    {
                        //        break;
                        //    }
                        //}
                        #endregion


                        for (int i = 0; i < indexList.Count; i++)
                        {
                            int abc=indexList[i];
                            tiles[indexList[i]].ClearTile();
                        }

                        //for (int i = 0; i < indexList.Count; i++)
                        //{
                        //    if (tiles[indexList[i]-9].tile!=null)
                        //    {
                        //        tiles[indexList[i]].PlaceTile(tiles[indexList[i]-9].tile);

                        //        tiles[indexList[i]-9].ClearTile();
                        //    }
                            
                        //}
                        PlayAreaController.instance.RNGTileSetforNulls(indexList.Count);

                        
                        
                           // mouseTileObject.GetComponent<SpriteRenderer>().sprite = currentTile.image;


                       
                    }
                }
            }
        }


    }

    public void RightCheck(int remainder,int index,TileController[]tiles,List<int> indexList)
    {
        //right check
        for (int i = 1; i < (9 - (remainder + 1)); i++)
        {
            //right check
            if (tiles[index + i].tile.name == tiles[index].tile.name)
            {


                indexList.Add(index + i);

            }
            else
            {
                break;
            }
        }

    }

    public int Remainder(int index)
    {
        return index % 9;
    }

    public int Result(int index)
    {
        return index / 9;
    }

}
