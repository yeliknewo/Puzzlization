using System.Collections.Generic;
using UnityEngine;

namespace LevelGenPather
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

		private Point2<int> minPos;

		private float heightDelta = 0.5f;

		private float oceanMin = -3.0f;
		private float oceanMax = -2.0f;
		private float coastMax = -1.0f;
		private float flatMax = 0.5f;
		private float hillMax = 1.5f;
		private float peakMax = 2.5f;

		private float absMin;
		private float absMax;

		public Gen (Point2<int> min, Point2<int> max)
		{
			this.tiles = new TileType[max.a - min.a + 1, max.b - min.b + 1];
			this.buildings = new BuildingType[max.a - min.a + 1, max.b - min.b + 1];
			this.units = new UnitType[max.a - min.a + 1, max.b - min.b + 1];

			this.minPos = min;

			this.absMax = peakMax;
			this.absMin = oceanMin;

			int width = max.a - min.a + 1;
			int height = max.b - min.b + 1;

			float[,] heights = new float[width, height];
			bool[,] finished = new bool[width, height];

			List<Point2<int>> openPoints = new List<Point2<int>> ();

			List<Point2<int>> deltas = new List<Point2<int>> ();
			{
				for (int i = 0; i < 3; i++) {
					deltas.Add (new Point2<int> (-1, -1));
					deltas.Add (new Point2<int> (-1, 1));
					deltas.Add (new Point2<int> (1, -1));
					deltas.Add (new Point2<int> (1, 1));
				}

				deltas.Add (new Point2<int> (-1, 0));
				deltas.Add (new Point2<int> (0, 1));
				deltas.Add (new Point2<int> (0, -1));
				deltas.Add (new Point2<int> (1, 0));
			}

			{
				List<Point2<int>> startingPoints = new List<Point2<int>> ();

				startingPoints.Add (new Point2<int> (0, 0));
				startingPoints.Add (new Point2<int> (width - 1, 0));
				startingPoints.Add (new Point2<int> (width - 1, height - 1));
				startingPoints.Add (new Point2<int> (0, height - 1));

				foreach (Point2<int> startingPoint in startingPoints) {
					heights [startingPoint.a, startingPoint.b] = GetNextHeight (0.0f, absMin, absMax);
					finished [startingPoint.a, startingPoint.b] = true;

					foreach (Point2<int> delta in deltas) {
						Point2<int> temp = new Point2<int> (startingPoint.a + delta.a, startingPoint.b + delta.b);
						if (temp.a >= 0 && temp.b >= 0 && temp.a < width && temp.b < height) {
							openPoints.Add (temp);
						}
					}
				}
			}
				
			while (openPoints.Count > 0) {
				Point2<int> open = openPoints[Random.Range(0, openPoints.Count - 1)];
				openPoints.Remove (open);
				if (finished [open.a, open.b]) {
					continue;
				}
				float lastHeight;
				{
					float sum = 0.0f;
					int count = 0;
					foreach (Point2<int> delta in deltas) {
						int x = open.a + delta.a;
						int y = open.b + delta.b;
						if (x >= 0 && x < width && y >= 0 && y < height) {
							if (finished [x, y]) {
								sum += heights [x, y];
								count++;
							} else {
								openPoints.Add (new Point2<int> (x, y));
							}
						}
					}
					if (count == 0) {
						continue;
					}
					lastHeight = sum / (float)count;
				}
				heights [open.a, open.b] = GetNextHeight (lastHeight, ScaleMin(lastHeight, -heightDelta, absMin), ScaleMax(lastHeight, heightDelta, absMax));
				finished [open.a, open.b] = true;
			}

			for (int y = 0; y < height; y++) {
				for (int x = 0; x < width; x++) {
					if (!finished [x, y]) {
						Debug.LogError ("Map Generation Ended before All Tiles were finished");
					}
					TileType tileType;
					BuildingType buildingType;
					UnitType unitType;

					//set tile type based on height
					if (heights [x, y] <= oceanMax) {
						tileType = TileType.Ocean;
					} else if (heights [x, y] <= coastMax) {
						tileType = TileType.Coast;
					} else if (heights [x, y] <= flatMax) {
						tileType = TileType.Flat;
					} else if (heights [x, y] <= hillMax) {
						tileType = TileType.Hills;
					} else if (heights [x, y] <= peakMax) {
						tileType = TileType.Peak;
					} else {
						tileType = TileType.None;
					}

					//set building type to none
					buildingType = BuildingType.None;

					//set unit type to none
					unitType = UnitType.None;

					this.tiles [x, y] = tileType;
					this.buildings [x, y] = buildingType;
					this.units [x, y] = unitType;
				}
			}
		}

		private float GetNextHeight(float lastHeight, float min, float max) {
			return lastHeight + (Random.value * (max - min)) + min;
		}

		private float ScaleMin(float lastHeight, float min, float absMin) {
			if (lastHeight + min < absMin) {
				return absMin - (lastHeight + min);
			} else {
				return min;
			}
		}

		private float ScaleMax(float lastHeight, float max, float absMax) {
			if (lastHeight + max > absMax) {
				return absMax - (lastHeight + max);
			} else {
				return max;
			}
		}

		public TileType GetTileType(Point2<int> coords) {
			int realX = coords.a - minPos.a;
			int realY = coords.b - minPos.b;
			if (realX >= 0 && realX < tiles.GetLength (0) && realY >= 0 && realY < tiles.GetLength (1)) {
				return this.tiles [realX, realY];
			}
			return TileType.None;
		}

		public BuildingType GetBuildingType(Point2<int> coords) {
			int realX = coords.a - minPos.a;
			int realY = coords.b - minPos.b;
			if (realX >= 0 && realX < units.GetLength (0) && realY >= 0 && realY < units.GetLength (1)) {
				return this.buildings [realX, realY];
			}
			return BuildingType.None;
		}

		public UnitType GetUnitType(Point2<int> coords) {
			int realX = coords.a - minPos.a;
			int realY = coords.b - minPos.b;
			if (realX >= 0 && realX < units.GetLength (0) && realY >= 0 && realY < units.GetLength (1)) {
				return this.units [realX, realY];
			}
			return UnitType.None;
		}
	}
}

