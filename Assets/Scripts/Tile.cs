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
		case TileType.Purple:
			SetTypePurple ();
			break;
		case TileType.Red:
			SetTypeRed ();
			break;
		case TileType.Green:
			SetTypeGreen ();
			break;
		case TileType.Blue:
			SetTypeBlue ();
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

	void SetTypePurple() {
		this.type = TileType.Purple;
		this.spriteRenderer.color = Color.Lerp (Color.red, Color.blue, 0.5f);
	}

	void SetTypeRed() {
		this.type = TileType.Red;
		this.spriteRenderer.color = Color.red;
	}

	void SetTypeGreen() {
		this.type = TileType.Green;
		this.spriteRenderer.color = Color.green;
	}

	void SetTypeBlue() {
		this.type = TileType.Blue;
		this.spriteRenderer.color = Color.blue;
	}
}

public enum TileType {
	Purple,
	Red,
	Green,
	Blue,
	None,
}
