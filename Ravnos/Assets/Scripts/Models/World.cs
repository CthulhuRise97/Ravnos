using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World{
    Tile[,] tiles;
    public int width, heigth;

    //generador del mundo
    public World(int width, int heigth){
        //mundo vacío
        this.width = width;
        this.heigth = heigth;

        tiles = new Tile[width,heigth];

        for(int x = 0; x < this.width; x++){
            for(int y = 0; y < this.heigth; y++){
                tiles[x,y] = new Tile(this,x,y);
            }
        }
        Debug.Log("World created with " + (width * heigth) + " tiles");
    }

    //generacion de tiles
    public void SetTiles(int[,] map, int width, int heigth){
        Debug.Log("Iniciando generación de mundos");
        for(int x = 0; x < width; x++){
            for(int y = 0; y < heigth; y++){
                if(map[x,y] == 0){
                    //oceano
                    tiles[x,y].Type = Tile.TileType.water;
                }else if(map[x,y] == 1){
                    tiles[x,y].Type = Tile.TileType.land;
                }
            }
        }
    }

    //obtener tile en (x,y) posicion
    public Tile GetTileAt(int x, int y){
        if (x > width || x < 0 || y > heigth || y < 0){
            Debug.LogError("Tile (" + x + "," + y + ") is out of range");
            return null;
        }
        return tiles[x, y];
    }

    //gets
    public int getWidth(){
        return this.width;
    }

    public int getHeigth(){
        return this.heigth;
    }
}
