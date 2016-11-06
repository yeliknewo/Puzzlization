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
	}

	void Click() {
		Vector2 mousePos = Object.FindObjectOfType<Camera>().ScreenToWorldPoint(Input.mousePosition);

		Tile tile;
		map.tiles.TryGetValue(new Point2 ((short)Mathf.RoundToInt(mousePos.x), (short)Mathf.RoundToInt(mousePos.y)), out tile);

		if (tile != null) {
			switch (tile.type) {
			case TileType.Purple:
				ClickPurple (tile);
				break;
			case TileType.Red:
				ClickRed (tile);
				break;
			case TileType.Green:
				ClickGreen (tile);
				break;
			case TileType.Blue:
				ClickBlue (tile);
				break;
			case TileType.None:
				throw new UnityException ("TileType is None");
			default:
				throw new UnityException ("Unhandled TileType");
			}
		}
	}

	void ClickPurple(Tile tile) {
		tile.SetType (TileType.Red);
		Point2 coords = tile.coords;
		for (short y = (short)(coords.y - 1); y <= coords.y + 1; y++) {
			for (short x = (short)(coords.x - 1); x <= coords.x + 1; x++) {
				Tile temp;
				map.tiles.TryGetValue (new Point2 (x, y), out temp);
				if (temp != null) {
					switch (temp.type) {
					case TileType.Purple:
						break;
					case TileType.Red:
						temp.SetType (TileType.Blue);
						break;
					case TileType.Green:
						temp.SetType (TileType.Blue);
						break;
					case TileType.Blue:
						temp.SetType (TileType.Red);
						break;
					case TileType.None:
						throw new UnityException ("TileType was None");
					default:
						throw new UnityException ("Unhandled TileType");
					}
				}
			}
		}
	}

	void ClickRed(Tile tile) {
		Point2 coords = tile.coords;
		for (short y = (short)(coords.y - 1); y <= coords.y + 1; y++) {
			for (short x = (short)(coords.x - 1); x <= coords.x + 1; x++) {
				Tile temp;
				map.tiles.TryGetValue (new Point2 (x, y), out temp);
				if (temp != null) {
					switch (temp.type) {
					case TileType.Purple:
						break;
					case TileType.Red:
						break;
					case TileType.Green:
						temp.SetType (TileType.Blue);
						break;
					case TileType.Blue:
						temp.SetType (TileType.Green);
						break;
					case TileType.None:
						throw new UnityException ("TileType was None");
					default:
						throw new UnityException ("Unhandled TileType");
					}
				}
			}
		}
	}

	void ClickGreen(Tile tile) {
		Point2 coords = tile.coords;
		for (short y = (short)(coords.y - 1); y <= coords.y + 1; y++) {
			for (short x = (short)(coords.x - 1); x <= coords.x + 1; x++) {
				Tile temp;
				map.tiles.TryGetValue (new Point2 (x, y), out temp);
				if (temp != null) {
					switch (temp.type) {
					case TileType.Purple:
						break;
					case TileType.Red:
						temp.SetType (TileType.Blue);
						break;
					case TileType.Green:
						break;
					case TileType.Blue:
						temp.SetType (TileType.Red);
						break;
					case TileType.None:
						throw new UnityException ("TileType was None");
					default:
						throw new UnityException ("Unhandled TileType");
					}
				}
			}
		}
	}

	void ClickBlue(Tile tile) {
		Point2 coords = tile.coords;
		for (short y = (short)(coords.y - 1); y <= coords.y + 1; y++) {
			for (short x = (short)(coords.x - 1); x <= coords.x + 1; x++) {
				Tile temp;
				map.tiles.TryGetValue (new Point2 (x, y), out temp);
				if (temp != null) {
					switch (temp.type) {
					case TileType.Purple:
						break;
					case TileType.Red:
						temp.SetType (TileType.Green);
						break;
					case TileType.Green:
						temp.SetType (TileType.Red);
						break;
					case TileType.Blue:
						break;
					case TileType.None:
						throw new UnityException ("TileType was None");
					default:
						throw new UnityException ("Unhandled TileType");
					}
				}
			}
		}
	}
}
