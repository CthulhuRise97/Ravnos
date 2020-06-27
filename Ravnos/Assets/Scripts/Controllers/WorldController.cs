using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    public Sprite oceanSprite, landSprite;
    public int x, y;
    World world;

    // Start is called before the first frame update
    void Start()
    {
        TilesGenerator tilesGenerator = new TilesGenerator(x,y);
        //crear el mundo con sprites vacíos
        this.world = new World(tilesGenerator.getX,tilesGenerator.getY);
        //crear gameObject de manera secuencial
        for (int x = 0; x < world.getWidth(); x++){
            for (int y = 0; y < world.getHeigth(); y++){
                Tile tile_data = world.GetTileAt(x, y);
                GameObject tile_go = new GameObject();
                tile_go.name = "Tile_" + x + "_" + y;
                tile_go.transform.position = new Vector3(tile_data.X, tile_data.Y, 0);

                //add a sprite renderer, but dont sett a sprite
                tile_go.AddComponent<SpriteRenderer>();
                tile_data.RegistrerTileTypeChangedCallback((tile) => {OnTileTypeChanged(tile, tile_go);});
            }
        }
        //establece tipo de terreno básico
        world.SetTiles(tilesGenerator.getMap, tilesGenerator.getX, tilesGenerator.getY);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTileTypeChanged(Tile tile_data, GameObject tile_go){
        if (tile_data.Type == Tile.TileType.water){
            tile_go.GetComponent<SpriteRenderer>().sprite = oceanSprite;
        }else if(tile_data.Type == Tile.TileType.land){
            tile_go.GetComponent<SpriteRenderer>().sprite = landSprite;
        }else{
            Debug.LogError("OnTileTypeChanged: Unrecognize tile type.");
        }
    }
}
