using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameModels;
using UnityEngine.UI;
public class TileController : MonoBehaviour
{
    public Tile tile;
    private Image imageUI;
    private void Awake()
    {
        imageUI = GetComponent<Image>();
    }

    public bool PlaceTile(Tile tileObject)
    {

            if (tileObject != null)
            {
                if (tileObject.image != null)
                {
                    tile = tileObject;

                    imageUI.sprite = tileObject.image;
                    imageUI.color = Color.white;

                    

                }
                else
                {
                    imageUI.color = Color.clear;
                    
                }
                 return true;
             }
           
        
        else
        {
            return false;
        }
    }

    public Tile PutTile()
    {
        var temptile = tile;

        tile.isExplode = true;
            
            

            return temptile;
    }
    public void ClearTile()
    {
        tile = null;
        imageUI.sprite = null;
        imageUI.color = Color.clear;
    }
}
