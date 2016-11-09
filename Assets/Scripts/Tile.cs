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

	private Map map {
		get {
			return Object.FindObjectOfType<Map> ();
		}
	}

	public void Setup(Point2 coords, TileType newType) {
		SetCoords (coords);
		SetType (newType);
	}
		
	void SetCoords(Point2 newCoords) {
		this.coords = newCoords;
		transform.position = new Vector2 (this.coords.x, this.coords.y);
		map.tiles.Add (coords, this);
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

	void SetTypeOcean() {
		this.type = TileType.Ocean;
		this.spriteRenderer.color = Color.Lerp(Color.blue, Color.black, 0.1f);
	}

	void SetTypeFlat() {
		this.type = TileType.Flat;
		this.spriteRenderer.color = Color.Lerp(Color.green, Color.black, 0.2f);
	}

	void SetTypeHills() {
		this.type = TileType.Hills;
		this.spriteRenderer.color = Color.Lerp(Color.magenta, Color.black, 0.5f);
	}

	void SetTypePeak() {
		this.type = TileType.Peak;
		this.spriteRenderer.color = Color.Lerp(Color.red, Color.black, 0.7f);
	}

	void SetTypeCoast() {
		this.type = TileType.Coast;
		this.spriteRenderer.color = Color.Lerp(Color.cyan, Color.black, 0.3f);
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
