using UnityEngine;
using System.Collections.Generic;
using System;

public class LevelGen : MonoBehaviour {

	public GameObject tileObjPrefab;
	public GameObject unitObjPrefab;

	public short minX = -8, maxX = 8, minY =-8, maxY = 8;

	void Start() {
		Point2 min = new Point2 (minX, minY);
		Point2 max = new Point2(maxX, maxY);
		GenerateLevel<LevelGenDiamondSquare.Gen> (
			min,
			max,
			new LevelGenDiamondSquare.Gen(min, max)
		);
	}

	void GenerateLevel<G> (Point2 min, Point2 max, G gen) where G: LevelGenerator {
		for (short y = minY; y <= maxY; y++) {
			for (short x = minX; x <= maxX; x++) {
				Point2 coords = new Point2 (x, y);

				TileType tileType = gen.GetTileType (coords);
				if (tileType != TileType.None) {
					GameObject tileObj = Instantiate<GameObject> (tileObjPrefab);
					Tile tile = tileObj.GetComponent<Tile> ();
					tile.Setup (coords, tileType);

					UnitType unitType = gen.GetUnitType(coords);
					if (unitType != UnitType.None) {
						GameObject unitObj = Instantiate<GameObject> (unitObjPrefab);
						Unit unit = unitObj.GetComponent<Unit> ();
						unit.Setup (coords, unitType);
					}
				}
			}
		}
	}
}

public interface LevelGenerator {
	TileType GetTileType(Point2 coords);
	UnitType GetUnitType(Point2 coords);
}
