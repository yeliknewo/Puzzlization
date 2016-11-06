using UnityEngine;
using System.Collections.Generic;

public class Map : MonoBehaviour {
	private Dictionary<Point2, Tile> _tiles = new Dictionary<Point2, Tile>();
	public Dictionary<Point2, Tile> tiles {
		get {
			return _tiles;
		}
	}
}
