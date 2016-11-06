using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour {
	public float zoomSpeed = 5.0f, scrollSpeed = 1.0f;
	public float minSize = 5.0f, maxSize = 20.0f;

	private new Camera camera {
		get {
			return this.GetComponent<Camera> ();
		}
	}

	void Update () {
		transform.position += new Vector3 (Input.GetAxis (InputManager.AXIS_X), Input.GetAxis (InputManager.AXIS_Y)) * Time.deltaTime * scrollSpeed * camera.orthographicSize;
		camera.orthographicSize = Mathf.Clamp (camera.orthographicSize - Input.GetAxis(InputManager.AXIS_MOUSE_SCROLL_WHEEL) * zoomSpeed, minSize, maxSize);
	}
}
