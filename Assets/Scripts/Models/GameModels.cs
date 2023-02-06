using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace GameModels 
{
    #region Tile
    [Serializable]
    public class Tile
    {
        public string name;
        public Sprite image;

        public int tileNumber;
        public int tileColor;

        public bool isExplode = false;
        public override string ToString()
        {
            return name;
        }


    }
    #endregion


    #region PlayArea
    [Serializable]
    public class PlayArea
    {
        
        public string name;

        public List<Tile> tileList;

        public PlayArea (string _name, List<Tile> _tileList)
        {
            this.name = _name;
            this.tileList = _tileList;

        }

        public override string ToString()
        {
            return name;
        }
    }
    #endregion
}
