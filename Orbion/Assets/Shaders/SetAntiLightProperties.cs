using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]

public class SetAntiLightProperties : MonoBehaviour {
	public Projector ProjectorToUseForObjectDetectionAndRendering;
	public Shader AntiLightShader;
	public Color color = Color.white;
	public float range = 8;



	Material projectionMat;

	// Use this for initialization
	void Start () {
		projectionMat = new Material(AntiLightShader);
		ProjectorToUseForObjectDetectionAndRendering.material = projectionMat;
	}
	
	// Update is called once per frame
	void Update () 
	{
		projectionMat.color = color;
		projectionMat.SetVector("_LightCenterPos", new Vector4(transform.position.x, transform.position.y, transform.position.z, range));
	}
}
