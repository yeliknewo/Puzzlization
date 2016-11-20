using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class Building : MonoBehaviour {
	public Point2<int> coords {
		get {
			return tile.coords;
		}
	}

	private Tile _tile;
	public Tile tile {
		get {
			return _tile;
		}
		private set {
			_tile = value;
		}
	}

	private BuildingType _type = BuildingType.None;
	public BuildingType type {
		private set {
			_type = value;
		}
		get {
			return _type;
		}
	}

	public Unit unit {
		get {
			return tile.unit;
		}
	}

	public SpriteRenderer spriteRenderer {
		get {
			return this.GetComponent<SpriteRenderer> ();
		}
	}

	private Map map {
		get {
			return Object.FindObjectOfType<Map> ();
		}
	}

	public void Setup(Point2<int> coords, BuildingType newType) {
		SetCoords (coords);
		SetType (newType);
	}

	public void SetCoords(Point2<int> newCoords) {
		Tile temp;
		map.tiles.TryGetValue (newCoords, out temp);
		if (temp != null) {
			SetTile (temp);
		}
	}

	public void SetTile(Tile newTile) {
		this.tile = newTile;
		this.transform.position = this.tile.transform.position + Vector3.back * Depths.BUILDING_DEPTH;
	}

	public void SetType(BuildingType newType) {

	}
}

public enum BuildingType {
	None,
	Base,
}