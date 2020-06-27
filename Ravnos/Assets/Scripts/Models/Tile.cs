using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Tile{
    //tipo de terrenos
    public enum TileType{water,land};

    //oceano por defecto
    TileType type = TileType.water;
    //call back (cambio del Tile type)
    Action<Tile> cbtyleTypeChanged;

    public TileType Type{
        get{
            return type;
        }set{
            TileType oldTile = type;
            type = value;
            //call the call back
            if(cbtyleTypeChanged != null && oldTile != type){
                cbtyleTypeChanged(this);
            }
        }
    }

    BackObject backObject;          //fondo
    FrontObject frontObject;        //frente

    //generar el mundo
    World world;
    int x,y;
    
    //funcion variable X
    public int X{
        get{
            return x;
        }
    }

    //funcion variable Y
    public int Y{
        get{
            return y;
        }
    }

    //Contructor
    //crear de Tile en x,y (coordenadas de world)
    public Tile(World world, int x, int y){
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
