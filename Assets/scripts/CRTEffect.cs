using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CRTEffect : MonoBehaviour
{
	public Material material;
	private Material _material;
	public Shader shader;
	
	protected Material test
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

	// Postprocess the image
	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (material) {
			Material mat = test;
			Graphics.Blit (source, destination, material);
		}
	}
}