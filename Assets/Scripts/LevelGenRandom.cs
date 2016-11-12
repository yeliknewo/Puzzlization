using UnityEngine;

namespace LevelGenRandom {
	public class Core
	{
		public static TileType GetTileType(Point2<int> coords, TileData tileData) {
			switch (Mathf.FloorToInt (UnityEngine.Random.value * 5)) {
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

		public static UnitType GetUnitType(Point2<int> coords, UnitData unitData) {
			switch (Mathf.FloorToInt (UnityEngine.Random.value * 10)) {
			case 0:
				return UnitType.Axe;
			case 1:
				return UnitType.Horse;
			case 2:
				return UnitType.Spear;
			case 3:
				return UnitType.Sword;
			default:
				return UnitType.None;
			}
		}
	}

	public class TileData {

	}

	public class UnitData {

	}
}

