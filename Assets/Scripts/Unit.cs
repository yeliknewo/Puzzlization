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

	private Map map {
		get {
			return Object.FindObjectOfType<Map> ();
		}
	}

	public void Setup(Point2 coords, UnitType newType) {
		SetCoords (coords);
		SetType (newType);
	}

	public void SetTile(Tile newTile) {
		this.tile = newTile;
		transform.position = this.tile.transform.position + Vector3.back;
	}

	public void SetCoords(Point2 newCoords) {
		Tile temp;
		map.tiles.TryGetValue (newCoords, out temp);
		if (temp != null) {
			SetTile (temp);
		}
	}

	public void SetType(UnitType newType) {
		switch (newType) {
		case UnitType.Axe:
			SetTypeAxe ();
			break;
		case UnitType.Sword:
			SetTypeSword ();
			break;
		case UnitType.Spear:
			SetTypeSpear ();
			break;
		case UnitType.Horse:
			SetTypeHorse ();
			break;
		case UnitType.None:
			throw new UnityException ("Type was TileType.None");
		default:
			throw new UnityException ("Unhandled TileType");
		}
	}

	void SetTypeAxe() {

	}

	void SetTypeSword() {

	}

	void SetTypeSpear() {

	}

	void SetTypeHorse() {

	}
}

public enum UnitType {
	Axe,
	Sword,
	Spear,
	Horse,
	None,
}