using UnityEngine;
using System.Collections.Generic;
using System;

namespace LevelGenDiamondSquare {
	public class Gen: LevelGenerator {
		private Dictionary<Point2<int>, TileType> _tiles;
		public Dictionary<Point2<int>, TileType> tiles {
			private set {
				_tiles = value;
			}
			get {
				return _tiles;
			}
		}

		private Dictionary<Point2<int>, UnitType> _units;
		public Dictionary<Point2<int>, UnitType> units {
			private set {
				_units = value;
			}
			get {
				return _units;
			}
		}


		public Gen(Point2<int> min, Point2<int> max) {
			tiles = new Dictionary<Point2<int>, TileType> ();
			units = new Dictionary<Point2<int>, UnitType> ();

			Dictionary<Point2<int>, float> heights = new Dictionary<Point2<int>, float> ();

			List<Point2<Point2<int>>> squares = new List<Point2<Point2<int>>> ();
			List<Point2<Point2<int>>> diamonds = new List<Point2<Point2<int>>> ();
			{
				Point2<int> aa = new Point2<int> (min.a, min.b);
				heights.Add (aa, 0.0f);
				Point2<int> ab = new Point2<int> (min.a, max.b);
				heights.Add (ab, 0.0f);
				Point2<int> ba = new Point2<int> (max.a, min.b);
				heights.Add (ba, 0.0f);
				Point2<int> bb = new Point2<int> (max.a, max.b);
				heights.Add (bb, 0.0f);
				squares.Add (new Point2<Point2<int>>());
			}

			int size = max.a - min.a;
			for (int i = 0; i < size / 2; i++) {
				if (i % 2 == 0) {
					//diamond step
					diamonds.Clear();
					foreach (Point2<Point2<int>> square in squares) {
						Point2<int> mid = new Point2<int>((square.a.a + square.b.a) / 2, (square.a.a + square.a.b) / 2);
						heights.Add(mid, (heights[square.a] + heights[square.b] + heights[new Point2<int>(square.a.a, square.b.b)] + heights[new Point2<int>(square.b.a, square.a.b)]) / 4 + GetRandomShift(size, i));

						//left
						{
							Point2<int> left = null;
							if (square.a.a - mid.a >= min.a) {
								left = new Point2<int>(square.a.a - mid.a, mid.b);
							}
							Point2<int> right = mid;
							diamonds.Add (new Point2<Point2<int>> (left, right));
						}

						//top
						{
							Point2<int> left = square.a;
							Point2<int> right = new Point2<int> (square.b.a, square.a.b);
							diamonds.Add (new Point2<Point2<int>> (left, right));
						}

						//right
						{
							Point2<int> left = mid;
							Point2<int> right = null;
							if (square.b.a + mid.a <= max.a) {
								right = new Point2<int> (square.b.a + mid.a, mid.b);
							}
							diamonds.Add (new Point2<Point2<int>> (left, right));
						}

						//bot
						{
							Point2<int> left = new Point2<int>(square.a.a, square.b.b);
							Point2<int> right = square.b;
							diamonds.Add (new Point2<Point2<int>> (left, right));
						}
					}
				} else {
					//square step
					squares.Clear();
					foreach (Point2<Point2<int>> diamond in squares) {
						Point2<int> mid = new Point2<int> ((diamond.a.a + diamond.b.a) / 2, diamond.a.b);
						heights.Add (mid, (heights [diamond.a] + heights [diamond.b] + heights [new Point2<int> (diamond.a.a, diamond.b.b)] + heights [new Point2<int> (diamond.b.a, diamond.a.b)]) / 4 + GetRandomShift (size, i));
					
						//top left
						{

						}

						//top right
						{

						}

						//bot left
						{

						}

						//bot right
						{

						}
					}
				}
			}
		}

		private int GetRandomShift(int size, int iteration) {
			return Mathf.RoundToInt(UnityEngine.Random.value * (float)(size - iteration));
		}

		public TileType GetTileType(Point2<int> coords) {
			return TileType.Flat;
		}

		public UnitType GetUnitType(Point2<int> coords) {
			return UnitType.None;
		}
	}
}
