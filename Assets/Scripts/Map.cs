using UnityEngine;
using System.Collections.Generic;

public class Map : MonoBehaviour {
	private Dictionary<Point2<int>, Tile> _tiles = new Dictionary<Point2<int>, Tile>();
	public Dictionary<Point2<int>, Tile> tiles {
		get {
			return _tiles;
		}
	}
}
