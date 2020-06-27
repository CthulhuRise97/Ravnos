using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    public Sprite floorSprite;
    World world;

    void Start()
    {
        //create a world with empty tiles
        this.world = new World();

        //create a gameObject for each of our tiles, so they show visually
        for (int x = 0; x < world.getWidth(); x++)
        {
            for (int y = 0; y < world.getHeigth(); y++)
            {
                Tile tile_data = world.GetTileAt(x, y);
                GameObject tile_go = new GameObject();
                tile_go.name = "Tile_" + x + "_" + y;
                tile_go.transform.position = new Vector3(tile_data.X, tile_data.Y, 0);

                //add a sprite renderer, but dont sett a sprite
                tile_go.AddComponent<SpriteRenderer>();
                tile_data.RegistrerTileTypeChangedCallback((tile) => {OnTileTypeChanged(tile, tile_go);});
            }
        }
        world.RandomizeTiles();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTileTypeChanged(Tile tile_data, GameObject tile_go)
    {
        if (tile_data.Type == Tile.TileType.Floor)
        {
            tile_go.GetComponent<SpriteRenderer>().sprite = floorSprite;
        }
        else if (tile_data.Type == Tile.TileType.Empty)
        {
            tile_go.GetComponent<SpriteRenderer>().sprite = null;
        }
        else
        {
            Debug.LogError("OnTileTypeChanged: Unrecognize tile type.");
        }
    }
}
