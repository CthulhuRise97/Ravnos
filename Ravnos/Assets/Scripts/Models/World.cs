using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World
{
    Tile[,] tiles;
    int width;
    int heigth;

    //generador del mundo
    public World(int width = 100, int heigth = 100)
    {
        this.width = width;
        this.heigth = heigth;

        tiles = new Tile[width, heigth];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < heigth; y++)
            {
                tiles[x, y] = new Tile(this, x, y);
            }
        }
        Debug.Log("World created with " + (width * heigth) + " tiles");
    }

    //generacion aleatoria de los tiles
    public void RandomizeTiles(){
        Debug.Log("Randomized tiles");
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < heigth; y++)
            {
                //tipo de tile aleatorio
                if(Random.Range(0,2) == 0){
                    tiles[x,y].Type = Tile.TileType.Empty;
                }else{
                    tiles[x,y].Type = Tile.TileType.Floor;
                }
            }
        }
    }

    //obtener el tile en cierta posicion
    public Tile GetTileAt(int x, int y)
    {
        if (x > width || x < 0 || y > heigth || y < 0)
        {
            Debug.LogError("Tile (" + x + "," + y + ") is out of range");
            return null;
        }
        return tiles[x, y];
    }

    public int getWidth()
    {
        return this.width;
    }

    public int getHeigth()
    {
        return this.heigth;
    }
}
