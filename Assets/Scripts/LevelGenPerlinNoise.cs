using System;

namespace LevelGenPerlinNoise
{
	public class  Gen: LevelGenerator 
	{
		private TileType[,] _tiles;
		public TileType[,] tiles {
			private set {
				_tiles = value;
			}
			get {
				return _tiles;
			}
		}

		private BuildingType[,] _buildings;
		public BuildingType[,] buildings {
			private set {
				_buildings = value;
			}
			get {
				return _buildings;
			}
		}

		private UnitType[,] _units;
		public UnitType[,] units {
			private set {
				_units = value;
			}
			get {
				return _units;
			}
		}

		private Point2<int> min;

		public Gen (Point2<int> min, Point2<int> max)
		{
			tiles = new TileType[max.a - min.a + 1, max.b - min.b + 1];
			units = new UnitType[max.a - min.a + 1, max.b - min.b + 1];

			this.min = min;


		}
			
		public TileType GetTileType(Point2<int> coords) {
			int realX = coords.a - min.a;
			int realY = coords.b - min.b;
			if (realX >= 0 && realX < tiles.GetLength (0) && realY >= 0 && realY < tiles.GetLength (1)) {
				return this.tiles [realX, realY];
			}
			return TileType.None;
		}

		public BuildingType GetBuildingType(Point2<int> coords) {
			int realX = coords.a - min.a;
			int realY = coords.b - min.b;
			if (realX >= 0 && realX < units.GetLength (0) && realY >= 0 && realY < units.GetLength (1)) {
				return this.buildings [realX, realY];
			}
			return BuildingType.None;
		}

		public UnitType GetUnitType(Point2<int> coords) {
			int realX = coords.a - min.a;
			int realY = coords.b - min.b;
			if (realX >= 0 && realX < units.GetLength (0) && realY >= 0 && realY < units.GetLength (1)) {
				return this.units [realX, realY];
			}
			return UnitType.None;
		}
	}
}

