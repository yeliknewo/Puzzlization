using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour {
	private Point2 _coords = new Point2();
	public Point2 coords {
		private set {
			_coords = value;
		}
		get {
			return _coords;
		}
	}

	private TileType _type = TileType.None;
	public TileType type {
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

	public void Setup(Point2 coords, TileType newType) {
		SetCoords (coords);
		SetType (newType);
	}

	public void SetType(TileType newType) {
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

	void SetCoords(Point2 newCoords) {
		this.coords = newCoords;
		transform.position = new Vector2 (this.coords.x, this.coords.y);
	}

	void SetTypeOcean() {
		this.type = TileType.Ocean;
		this.spriteRenderer.color = Color.blue;
	}

	void SetTypeFlat() {
		this.type = TileType.Flat;
		this.spriteRenderer.color = Color.green;
	}

	void SetTypeHills() {
		this.type = TileType.Hills;
		this.spriteRenderer.color = Color.magenta;
	}

	void SetTypePeak() {
		this.type = TileType.Peak;
		this.spriteRenderer.color = Color.red;
	}

	void SetTypeCoast() {
		this.type = TileType.Coast;
		this.spriteRenderer.color = Color.cyan;
	}
}

public enum TileType {
	Flat,
	Hills,
	Peak,
	Coast,
	Ocean,
	None,
}
