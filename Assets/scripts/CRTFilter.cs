using UnityEngine;
using System.Collections;

public class CRTFilter : MonoBehaviour {

	public Shader shader;
	private Material _material;

	[Range(0, 1)] public float verts_force = 0.0f;
	[Range(0, 1)] public float verts_force_2 = 0.0f;
	[Range(1, 10)] public int verts_width = 1;
	[Range(0, 1)] public float blackwhite = 1;
	
	protected Material material
	{
		get
		{
			if (_material == null)
			{
				_material = new Material(shader);
				_material.hideFlags = HideFlags.HideAndDontSave;
			}
			return _material;
		}
	}
	
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (shader == null) return;
		Material mat = material;
		mat.SetFloat("_VertsColor", 1-verts_force);
		mat.SetFloat("_VertsColor2", 1-verts_force_2);
		mat.SetFloat("_VertsColorwidth", verts_width);
		mat.SetFloat("_bwBlend", blackwhite);
		Graphics.Blit(source, destination, mat);
	}
	
	void OnDisable()
	{
		if (_material)
		{
			DestroyImmediate(_material);
		}
	}

}
