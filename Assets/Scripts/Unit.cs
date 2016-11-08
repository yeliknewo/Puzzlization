using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
	public Point2 coords {
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

	private UnitType _type = UnitType.None;
	public UnitType type {
		private set {
			_type = value;
		}
		get {
			return _type;
		}
	}

	private SpriteRenderer spriteRenderer {
		get {
			return this.GetComponent<SpriteRenderer> ();
		}
	}

	public void Setup(Point2 coords, UnitType newType) {
		SetCoords (coords);
		SetType (newType);
	}

	void SetCoords(Point2 newCoords) {
		this.coords = newCoords;
		transform.position = new Vector2 (this.coords.x, this.coords.y);
	}

	public void SetType(UnitType newType) {
		switch (newType) {
		case TileType.Ocean:
			SetTypeOcean ();
			break;
		case TileType.Flat:
			SetTypeFlat ();
			break;
		case TileType.Hills:
			SetTypeHills ();
			break;
		case TileType.Peak:
			SetTypePeak ();
			break;
		case TileType.Coast:
			SetTypeCoast ();
			break;
		case TileType.None:
			throw new UnityException ("Type was TileType.None");
		default:
			throw new UnityException ("Unhandled TileType");
		}
	}
}

public enum UnitType {
	None,
}