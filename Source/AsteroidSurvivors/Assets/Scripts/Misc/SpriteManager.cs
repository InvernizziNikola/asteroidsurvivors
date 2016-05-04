using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteManager : MonoBehaviour {
    

    public Texture2D AsteroidsTexture;
    public int AsteroidsTileSize = 128;
    public List<Sprite> AsteroidsSprites = new List<Sprite>();

    public Texture2D BuildingsTexture;
    public int BuildingsTileSize = 128;
    public List<Sprite> BuildingsSprites = new List<Sprite>();

    public Texture2D CharactersTexture;
    public int CharactersTileSize = 128;
    public List<Sprite> CharactersSprites = new List<Sprite>();

    // Use this for initialization
    void Start ()
    {

        GenerateSprites(AsteroidsSprites, AsteroidsTexture, AsteroidsTileSize);
        GenerateSprites(BuildingsSprites, BuildingsTexture, BuildingsTileSize);
        GenerateSprites(CharactersSprites, CharactersTexture, CharactersTileSize);

    }

    public Sprite GetAsteroidSprite(int pos)
    {
        return BuildingsSprites[pos];
    }

	void GenerateSprites(List<Sprite> spriteList, Texture2D texture, int tileSize)
    {
        if (texture != null)
        {
            float rows = texture.height / tileSize;
            float columns = texture.width / tileSize;

            for (float x = 0; x < columns; x++)
            {
                for (float y = 0; y < rows; y++)
                {
                    Sprite tempSprite = Sprite.Create(texture,
                        new Rect(
                            x * tileSize,
                            y * tileSize,
                            tileSize,
                            tileSize),
                        new Vector2(0.5f, 0.5f),
                        tileSize);

                    tempSprite.name = "( " + (x * tileSize) + ", " +(y * tileSize) + ") => ( " + (x * tileSize + tileSize) + ", " + (y * tileSize + tileSize) + ")";
                    spriteList.Add(tempSprite);
                }
            }
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
}
