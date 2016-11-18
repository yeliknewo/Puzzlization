using UnityEngine;
using System.Collections.Generic;
using System;

namespace LevelGenDiamondSquare {
	public class Gen: LevelGenerator {
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

		private int minX;
		private int minY;

		public Gen(Point2<int> min, Point2<int> max) {
			this.minX = min.a;
			this.minY = min.b;

			tiles = new TileType[max.a - min.a + 1, max.b - min.b + 1];
			units = new UnitType[max.a - min.a + 1, max.b - min.b + 1];

			float[,] heights = new float[max.a - min.a + 1, max.b - min.b + 1];
			bool[,] used = new bool[max.a - min.a + 1, max.b - min.b + 1];

			List<Point2<Point2<int>>> squares = new List<Point2<Point2<int>>> ();
			List<Point2<Point2<int>>> diamonds = new List<Point2<Point2<int>>> ();
			{
				squares.Add (new Point2<Point2<int>> (min, max));
				heights[min.a - min.a, min.b - min.b] = 0.0f;
				used [min.a - min.a, min.b - min.b] = true;
				heights[min.a - min.a, max.b - min.b] = 0.0f;
				used [min.a - min.a, max.b - min.b] = true;
				heights[max.a - min.a, min.b - min.b] = 0.0f;
				used [max.a - min.a, min.b - min.b] = true;
				heights[max.a - min.a, max.b - min.b] = 0.0f;
				used [max.a - min.a, max.b - min.b] = true;
			}

			int size = (max.a - min.a);
			for (int i = 0; i < size; i++) {
				//Debug.Log ("Iteration: " + i);
				//Debug.Log ("Squares: " + squares.Count);
				//Debug.Log ("Diamonds: " + diamonds.Count);
				if (i % 2 == 0) {
					//diamond step
					diamonds.Clear();
					foreach (Point2<Point2<int>> square in squares) {
						Point2<int> mid = new Point2<int>((square.a.a + square.b.a) / 2, (square.a.b + square.b.b) / 2);
						if ((mid.a < min.a || mid.b < min.b) || (mid.a > max.a || mid.b > max.b)) {
							continue;
						}
						float sum = 0.0f;
						int count = 0;
						if (square.a.a >= min.a && square.a.b >= min.b && square.a.a <= max.a && square.a.b <= max.b) {
							sum += heights[square.a.a - min.a, square.a.b - min.b];
							count += 1;
						}
						if (square.b.a >= min.a && square.b.b >= min.b && square.b.a <= max.a && square.b.b <= max.b) {
							sum += heights[square.b.a - min.a, square.b.b - min.b];
							count += 1;
						}
						if (square.b.a >= min.a && square.a.b >= min.b && square.b.a <= max.a && square.a.b <= max.b) {
							sum += heights[square.b.a - min.a, square.a.b - min.b];
							count += 1;
						}
						if (square.a.a >= min.a && square.b.b >= min.b && square.a.a <= max.a && square.b.b <= max.b) {
							sum += heights[square.a.a - min.a, square.b.b - min.b];
							count += 1;
						}
						Debug.Log ("X: " + (mid.a - min.a) + ", Y: " + (mid.b - min.b));
						if (used [mid.a - min.a, mid.b - min.b]) {
							Debug.Log ("Already Used");
						}
						heights[mid.a - min.a, mid.b - min.b] = sum / count + this.GetRandomShift (size, i);
						used [mid.a - min.a, mid.b - min.b] = true;

						Point2<int> topRight = new Point2<int> (square.b.a, square.a.b);

						//left
						{
							Point2<int> top = square.a;
							Point2<int> right = mid;
							diamonds.Add (new Point2<Point2<int>> (top, right));
						}

						//right
						{
							Point2<int> top = topRight;
							Point2<int> right = new Point2<int>(mid.a + square.b.a - square.a.a, mid.b);
							diamonds.Add (new Point2<Point2<int>> (top, right));
						}

						//top
						{
							Point2<int> top = new Point2<int> (mid.a, mid.b + square.b.b - square.a.b);
							Point2<int> right = topRight;
							diamonds.Add (new Point2<Point2<int>> (top, right));
						}

						//bot
						{
							Point2<int> top = mid;
							Point2<int> right = square.b;
							diamonds.Add (new Point2<Point2<int>> (top, right));
						}
					}
				} else {
					//square step
					squares.Clear();
					foreach (Point2<Point2<int>> diamond in diamonds) {
						Point2<int> mid = new Point2<int> (diamond.a.a, diamond.b.b);
						if ((mid.a < min.a || mid.b < min.b) || (mid.a > max.a || mid.b > max.b)) {
							continue;
						}
						Point2<int> bot = new Point2<int> (mid.a, mid.b + diamond.b.b - diamond.a.b);
						Point2<int> left = new Point2<int> (mid.a + diamond.a.a - diamond.b.a, mid.b);
						float sum = 0.0f;
						int count = 0;
						if (diamond.a.a >= min.a && diamond.a.b >= min.b && diamond.a.a <= max.a && diamond.a.b <= max.b) {
							sum += heights[diamond.a.a - min.a, diamond.a.b - min.b];
							count += 1;
						}
						if (diamond.b.a >= min.a && diamond.b.b >= min.b && diamond.b.a <= max.a && diamond.b.b <= max.b) {
							sum += heights[diamond.b.a - min.a, diamond.b.b - min.b];
							count += 1;
						}
						if (bot.a >= min.a && bot.b >= min.b && bot.a <= max.a && bot.b <= max.b) {
							sum += heights[bot.a - min.a, bot.b - min.b];
							count += 1;
						}
						if (left.a >= min.a && left.b >= min.b && left.a <= max.a && left.b <= max.b) {
							sum += heights[left.a -  min.a, left.b - min.b];
							count += 1;
						}
						Debug.Log ("X: " + (mid.a - min.a) + ", Y: " + (mid.b - min.b));
						if (used [mid.a - min.a, mid.b - min.b]) {
							Debug.Log ("Already Used");
						}
						heights[mid.a - min.a, mid.b - min.b] = sum / count + this.GetRandomShift (size, i);
						used [mid.a - min.a, mid.b - min.b] = true;

						//topLeft
						{
							Point2<int> topLeft = new Point2<int> (left.a, diamond.a.b);
							Point2<int> botRight = mid;
							squares.Add (new Point2<Point2<int>> (topLeft, botRight));
						}

						//topRight
						{
							Point2<int> topLeft = diamond.a;
							Point2<int> botRight = diamond.b;
							squares.Add (new Point2<Point2<int>> (topLeft, botRight));
						}

						//botLeft
						{
							Point2<int> topLeft = left;
							Point2<int> botRight = bot;
							squares.Add (new Point2<Point2<int>> (topLeft, botRight));
						}

						//botRight
						{
							Point2<int> topLeft = mid;
							Point2<int> botRight = new Point2<int> (diamond.b.a, bot.b);
							squares.Add (new Point2<Point2<int>> (topLeft, botRight));
						}
					}
				}
			}

			for(int x = 0;x < heights.GetLength(0); x++) {
				for(int y = 0; y < heights.GetLength(1); y++) {
					float value = heights[x, y];
					//Debug.Log (value);

					TileType type = TileType.None;
					if(value < -1.0) {
						type = TileType.Ocean;
					}
					else if(value < -0.5) {
						type = TileType.Coast;
					}
					else if(value < 0.0) {
						type = TileType.Flat;
					}
					else if(value < 0.5) {
						type = TileType.Hills;
					}
					else {
						type = TileType.Peak;
					}

					UnitType unit = UnitType.None;

					tiles [x, y] = type;
					units [x, y] = unit;
				}
			}
		}

		private int GetRandomShift(int size, int iteration) {
			return Mathf.RoundToInt(UnityEngine.Random.value * (float)(size - iteration));
		}

		public TileType GetTileType(Point2<int> coords) {
			int realX = coords.a - minX;
			int realY = coords.b - minY;
			if (realX >= 0 && realX < tiles.GetLength (0) && realY >= 0 && realY < tiles.GetLength (1)) {
				return this.tiles [realX, realY];
			}
			return TileType.None;
		}

		public UnitType GetUnitType(Point2<int> coords) {
			int realX = coords.a - minX;
			int realY = coords.b - minY;
			if (realX >= 0 && realX < units.GetLength (0) && realY >= 0 && realY < units.GetLength (1)) {
				return this.units [realX, realY];
			}
			return UnitType.None;
		}
	}
}
