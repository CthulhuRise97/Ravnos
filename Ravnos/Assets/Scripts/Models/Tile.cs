using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Tile
{
    //Tipo de "terreno"
    public enum TileType { Empty, Floor };
    //Tile vacío por defecto
    TileType type = TileType.Empty;
    Action<Tile> cbtyleTypeChanged;

    public TileType Type
    {
        get
        {
            return type;
        }
        set
        {
            TileType oldTile = type;
            type = value;
            //call the call back
            if(cbtyleTypeChanged != null && oldTile != type){
                cbtyleTypeChanged(this);
            }
        }
    }

    LooseObject looseObject;            //fondo
    InstalledObject installedObject;    //frente

    //world gen
    World world;
    int x, y;

    public int X
    {
        get
        {
            return x;
        }
    }

    public int Y
    {
        get
        {
            return y;
        }
    }

    public Tile(World world, int x, int y)
    {
        this.world = world;
        this.x = x;
        this.y = y;
    }

    public void RegistrerTileTypeChangedCallback(Action<Tile> callback){
        cbtyleTypeChanged += callback;
    }

    public void UnregistrerTileTypeChangedCallback(Action<Tile> callback){
        cbtyleTypeChanged -= callback;
    }
}
