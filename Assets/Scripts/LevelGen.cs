using UnityEngine;
using System.Collections.Generic;

public class LevelGen : MonoBehaviour {

	public GameObject tileObjPrefab;

	public short minX = -9, maxX = 9, minY =-9, maxY = 9;

	void Start() {
		GenerateLevel (minX, maxX, minY, maxY);
	}

	void GenerateLevel(short minX, short maxX, short minY, short maxY) {
		Map map = Object.FindObjectOfType<Map> ();

		for (short y = minY; y <= maxY; y++) {
			for (short x = minX; x <= maxX; x++) {
				GameObject tileObj = Instantiate<GameObject> (tileObjPrefab);
				Point2 coords = new Point2 (x, y);
				Tile tile = tileObj.GetComponent<Tile> ();
				tile.Setup (coords, GetTileType (x, y));
				map.tiles.Add (coords, tile);
			}
		}
	}

	TileType GetTileType(int x, int y) {
		switch (Mathf.FloorToInt (Random.value * 5)) {
		case 0:
			return TileType.Coast;
		case 1:
			return TileType.Flat;
		case 2:
			return TileType.Hills;
		case 3:
			return TileType.Ocean;
		case 4:
			return TileType.Peak;
		default:
			return TileType.None;
		}
	}
}
