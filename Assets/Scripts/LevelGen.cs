using UnityEngine;
using System.Collections.Generic;
using System;

public class LevelGen : MonoBehaviour {

	public GameObject tileObjPrefab;
	public GameObject unitObjPrefab;
	public GameObject buildingObjPrefab;

	public int minX = -8, maxX = 8, minY =-8, maxY = 8;

	void Start() {
		Point2<int> min = new Point2<int> (minX, minY);
		Point2<int> max = new Point2<int>(maxX, maxY);
		GenerateLevel<LevelGenPather.Gen> (
			min,
			max,
			new LevelGenPather.Gen(min, max)
		);
	}

	void GenerateLevel<G> (Point2<int> min, Point2<int> max, G gen) where G: LevelGenerator {
		for (int y = minY; y <= maxY; y++) {
			for (int x = minX; x <= maxX; x++) {
				Point2<int> coords = new Point2<int> (x, y);

				TileType tileType = gen.GetTileType (coords);
				if (tileType != TileType.None) {
					GameObject tileObj = Instantiate<GameObject> (tileObjPrefab);
					Tile tile = tileObj.GetComponent<Tile> ();
					tile.Setup (coords, tileType);

					BuildingType buildingType = gen.GetBuildingType (coords);
					if (buildingType != BuildingType.None) {
						GameObject buildingObj = Instantiate<GameObject> (buildingObjPrefab);
						Building building = buildingObj.GetComponent<Building> ();
						building.Setup (coords, buildingType);
					}

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
	TileType GetTileType(Point2<int> coords);
	BuildingType GetBuildingType(Point2<int> coords);
	UnitType GetUnitType(Point2<int> coords);
}
