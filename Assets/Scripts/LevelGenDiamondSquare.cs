using UnityEngine;
using System.Collections.Generic;
using System;

namespace LevelGenDiamondSquare {
	public class Gen: LevelGenerator {
		private Dictionary<Point2, TileType> _tiles;
		public Dictionary<Point2, TileType> tiles {
			private set {
				_tiles = value;
			}
			get {
				return _tiles;
			}
		}

		private Dictionary<Point2, UnitType> _units;
		public Dictionary<Point2, UnitType> units {
			private set {
				_units = value;
			}
			get {
				return _units;
			}
		}


		public Gen(Point2 min, Point2 max) {
			tiles = new Dictionary<Point2, TileType> ();
			units = new Dictionary<Point2, UnitType> ();

			List<Square> squares = new List<Square> ();
			squares.Add (new Square (min, max));

			for (short i = 0; i < (max.x - min.x) / 2; i++) {
				if (i % 2 == 0) {
					//diamond step
					List<Square> temp = new List<Square>();
					foreach (Square square in squares) {
						temp.Add(new Square(
					}
				} else {
					//square step
					foreach (Square square in squares) {

					}
				}
			}
		}

		public TileType GetTileType(Point2 coords) {
			return TileType.Flat;
		}

		public UnitType GetUnitType(Point2 coords) {
			return UnitType.None;
		}
	}

	internal class Square {
		private Point2 _a;
		public Point2 a {
			private set {
				_a = value;
			}
			get {
				return _a;
			}
		}

		private Point2 _b;
		public Point2 b {
			private set {
				_b = value;
			}
			get {
				return _b;
			}
		}

		public Square(Point2 min, Point2 max) {
			this.a = min;
			this.b = max;
		}
	}
}
