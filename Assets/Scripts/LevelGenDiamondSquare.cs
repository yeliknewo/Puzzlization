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

			List<Point2<Point2<int>>> shapes = new List<Point2<Point2<int>>> ();
			{
				Point2<int> aa = new Point2<int> (min.a, min.b);
				Point2<int> ab = new Point2<int> (min.a, max.b);
				Point2<int> ba = new Point2<int> (max.a, min.b);
				Point2<int> bb = new Point2<int> (max.a, max.b);
				shapes.Add (new Point2<Point2<int>>());
			}

			int size = max.a - min.a;
			for (int i = 0; i < size / 2; i++) {
				if (i % 2 == 0) {
					//diamond step
					foreach (Point2<Point2<int>> square in shapes) {
						Point2<int> middle = new Point2<int>((square.a.a + square.b.a) / 2, (square.a.a + square.a.b) / 2);
						heights [middle] = GetRandomShift (size, i);

						//left
						{
							Point2<int> left;
							if (square.a.a - middle.a >= min.a) {
								left = new Point2<int>(square.a.a - middle.a, middle.b);
							}
						}
					}
				} else {
					//square step
					foreach (Point2<Point2<int>> diamond in shapes) {

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
