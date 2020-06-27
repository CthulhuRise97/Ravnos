using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesGenerator{
    int x,y;

    int[,] map;
    
    public TilesGenerator(int x, int y){
        this.x = x;
        this.y = y;
        this.map = new int[x,y];
        mapping();
    }

    public int[,] mapping(){
        return this.map;
    }

    //getters
    public int getX{
        get{
            return x;
        }
    }

    public int getY{
        get{
            return y;
        }
    }

    public int[,] getMap{
        get{
            return map;
        }
    }
}
