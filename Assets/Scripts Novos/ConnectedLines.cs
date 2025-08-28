using UnityEngine;
using System.Collections;

// Put this script on a Camera
public class ConnectedLines: MonoBehaviour {
	
	// Fill/drag these in from the editor
	
	// Choose the Unlit/Color shader in the Material Settings
	// You can change that color, to change the color of the connecting lines
	public Material lineMat;

	public bool[] LinhasAtivas;
    public GameObject[] StartObjects;
    public GameObject[] EndObjects;

    // Connect all of the `points` to the `mainPoint`
    void DrawConnectingLines() {
			// Loop through each point to connect to the mainPoint
		for (int i = 0; i < StartObjects.Length; i++) {
			if (LinhasAtivas[i]){
				Vector3 StartPosition = StartObjects[i].transform.position;
				Vector3 EndPosition = EndObjects[i].transform.position;;
				
				GL.Begin(GL.LINES);
				lineMat.SetPass(0);
				//GL.Color(new Color(lineMat.color.r, lineMat.color.g, lineMat.color.b, lineMat.color.a));
				GL.Vertex3(StartPosition.x, StartPosition.y, StartPosition.z);
				GL.Vertex3(EndPosition.x, EndPosition.y, EndPosition.z);
				GL.End();
			}
			}
	}
	
	// To show the lines in the game window whne it is running
	void OnRenderObject() {
		DrawConnectingLines();
	}
	
	// To show the lines in the editor
	void OnDrawGizmos() {
		DrawConnectingLines();
	}
}
