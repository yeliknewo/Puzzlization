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

		public Gen (Point2<int> min, Point2<int> max)
		{
			tiles = new TileType[max.a - min.a + 1, max.b - min.b + 1];
			units = new UnitType[max.a - min.a + 1, max.b - min.b + 1];

			float heightDelta = 0.5f;

			float oceanMin = -3.0f;
			float oceanMax = -2.0f;
			float coastMax = -1.0f;
			float flatMax = 0.5f;
			float hillMax = 1.5f;
			float peakMax = 2.5f;

			float absMin = oceanMin;
			float absMax = peakMax;

			this.minPos = min;

			int width = max.a - min.a + 1;
			int height = max.b - min.b + 1;

			float[,] heights = new float[width, height];
			bool[,] finished = new bool[width, height];

			List<Point2<int>> openPoints = new List<Point2<int>> ();
			{
				heights [0, 0] = GetNextHeight (0.0f, absMin, absMax);
				finished [0, 0] = true;
			}
			{
				int x = 1;
				int y = 0;
				openPoints.Add (new Point2<int> (x, y));
			}
			{
				int x = 0;
				int y = 1;
				openPoints.Add (new Point2<int> (x, y));
			}

			List<Point2<int>> deltas = new List<Point2<int>> ();
			{
				deltas.Add (new Point2<int> (-1, -1));
				deltas.Add (new Point2<int> (-1, 0));
				deltas.Add (new Point2<int> (-1, 1));
				deltas.Add (new Point2<int> (0, 1));
				deltas.Add (new Point2<int> (0, -1));
				deltas.Add (new Point2<int> (1, -1));
				deltas.Add (new Point2<int> (1, 0));
				deltas.Add (new Point2<int> (1, 1));
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
					UnitType unitType;

					Debug.Log (heights [x, y]);

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

					if (tileType != TileType.None && heights [x, y] >= -1.0f) {
						unitType = UnitType.Axe;
					} else {
						unitType = UnitType.None;
					}

					this.tiles [x, y] = tileType;
					this.units [x, y] = unitType;
				}
			}
		}

		private float GetNextHeight(float lastHeight, float min, float max) {
			return lastHeight + (Random.value * (max - min)) + min;
		}

		private float ScaleMin(float lastHeight, float min, float absMin) {
			//NEEDS WORK
			if (lastHeight - min < absMin) {
				return -min;
			} else {
				return min;
			}
		}

		private float ScaleMax(float lastHeight, float max, float absMax) {
			//NEEDS WORK
			if (lastHeight + max > absMax) {
				return -max;
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

