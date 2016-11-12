using UnityEngine;
using System.Collections;

public class TileDriver : MonoBehaviour {
	private Map map {
		get {
			return Object.FindObjectOfType<Map> ();
		}
	}

	void Update() {
		if (Input.GetMouseButtonDown (InputManager.BUTTON_MOUSE_LEFT)) {
			this.Click ();
		}
		if (Input.GetKeyDown (InputManager.BUTTON_KEY_SPACE)) {
			this.Turn ();
		}
	}

	void Turn() {
		foreach (Tile tile in map.tiles.Values) {
			switch (tile.type) {
			case TileType.Coast:
				TurnCoast (tile);
				break;
			case TileType.Flat:
				TurnFlat (tile);
				break;
			case TileType.Hills:
				TurnHills (tile);
				break;
			case TileType.Ocean:
				TurnOcean (tile);
				break;
			case TileType.Peak:
				TurnPeak (tile);
				break;
			case TileType.None:
				break;
			default:
				throw new UnityException ("Unhandled TileType");
			}
		}
	}

	void TurnCoast(Tile tile) {
		tile.SetType (TileType.Peak);
	}

	void TurnFlat(Tile tile) {
		tile.SetType (TileType.Coast);
	}

	void TurnHills(Tile tile) {
		tile.SetType (TileType.Flat);
	}

	void TurnOcean(Tile tile) {
		tile.SetType (TileType.Hills);
	}

	void TurnPeak(Tile tile) {
		tile.SetType (TileType.Ocean);
	}

	void Click() {
		Vector2 mousePos = Object.FindObjectOfType<Camera>().ScreenToWorldPoint(Input.mousePosition);

		Tile tile;
		map.tiles.TryGetValue(new Point2<int> (Mathf.RoundToInt(mousePos.x), Mathf.RoundToInt(mousePos.y)), out tile);

		if (tile != null) {
			switch (tile.type) {
			case TileType.Coast:
				ClickCoast (tile);
				break;
			case TileType.Flat:
				ClickFlat (tile);
				break;
			case TileType.Hills:
				ClickHills (tile);
				break;
			case TileType.Ocean:
				ClickOcean (tile);
				break;
			case TileType.Peak:
				ClickPeak (tile);
				break;
			case TileType.None:
				break;
			default:
				throw new UnityException ("Unhandled TileType");
			}
		}
	}

	void ClickCoast(Tile tile) {
		tile.SetType (TileType.Flat);
	}

	void ClickFlat(Tile tile) {
		tile.SetType (TileType.Hills);
	}

	void ClickHills(Tile tile) {
		tile.SetType (TileType.Ocean);
	}

	void ClickOcean(Tile tile) {
		tile.SetType (TileType.Peak);
	}

	void ClickPeak(Tile tile) {
		tile.SetType (TileType.Coast);
	}
}
