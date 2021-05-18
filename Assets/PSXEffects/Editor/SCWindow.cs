using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class SCWindow : EditorWindow
{
	public Texture2D texture = null;

	private Texture2D selectedTexture = null;
	private Texture2D prevSelectedTexture = null;
	private Texture2D transparencyTexture = null;
	private SCInformation info = new SCInformation(0);
	private bool saved = false;
	private float zoom = 1.0f;

	[MenuItem("Window/PSXEffects/Texture Cruncher")]
	static void Init() {
		SCWindow window = (SCWindow)GetWindow(typeof(SCWindow), false, "Texture Cruncher");
		window.minSize = new Vector2(900, 600);
		window.Show();
	}

	void SaveAs() {
		string newUrl = EditorUtility.SaveFilePanel("Save new texture", info.directory, info.name, info.extension.Substring(1, info.extension.Length - 1));
		if (newUrl.Length != 0) {
			newUrl = newUrl.Substring(newUrl.IndexOf("Assets/"));
			info.texUrl = newUrl;
			texture = SCProcessor.ProcessTexture(selectedTexture, info);
			saved = true;
		}
	}

	void OnGUI() {
		if(transparencyTexture == null)
			transparencyTexture = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/PSXEffects/Textures/transparency.png");

		EditorGUILayout.BeginHorizontal();

		EditorGUILayout.BeginVertical();
		EditorGUILayout.LabelField("Preview:");
		GUILayout.BeginArea(new Rect(3, 20, 512, 512));
		float boxSize = 512;
		if (texture != null) boxSize = texture.width;
		while (boxSize < 512) boxSize *= 2;
		while (boxSize > 512) boxSize /= 2;
		boxSize *= zoom;
		Rect previewRect = EditorGUILayout.GetControlRect(false);
		if (texture != null) {
			float ratio = texture.width / texture.height;
			Vector2 texOffset;
			texOffset.x = (512 - boxSize) / 2;
			texOffset.y = (512 - boxSize / ratio) / 2;
			GUI.DrawTexture(new Rect(previewRect.x, previewRect.y, 512, 512), transparencyTexture);
			GUI.DrawTexture(new Rect(previewRect.x + texOffset.x, previewRect.y + texOffset.y, boxSize, boxSize / ratio), texture);
		} else {
			GUI.DrawTexture(new Rect(previewRect.x, previewRect.y, 512, 512), transparencyTexture);
		}
		GUILayout.EndArea();
		EditorGUILayout.Space(515);
		zoom = EditorGUILayout.Slider("Zoom", zoom, 0.01f, 10.0f);
		EditorGUILayout.EndVertical();

		EditorGUILayout.BeginVertical();
		EditorGUILayout.LabelField("Options:");
		EditorGUILayout.BeginHorizontal();
		EditorGUI.BeginChangeCheck();
		selectedTexture = (Texture2D)EditorGUILayout.ObjectField(selectedTexture, typeof(Texture2D), true);
		if (prevSelectedTexture != selectedTexture) {
			texture = selectedTexture;
			prevSelectedTexture = selectedTexture;
			string path = AssetDatabase.GetAssetPath(selectedTexture);
			string ext = Path.GetExtension(path);
			string dir = Path.GetDirectoryName(path);
			string name = Path.GetFileNameWithoutExtension(path);
			info.name = name + "_crunched" + ext;
			info.texUrl = dir + "/" + info.name;
			info.extension = ext;
			info.directory = dir;
			saved = false;
		}
		EditorGUILayout.EndHorizontal();
		info.depth = EditorGUILayout.IntSlider("Color Depth", info.depth, 0, 255);
		info.ditherIntensity = EditorGUILayout.IntSlider("Dither Intensity", info.ditherIntensity, 0, 100);
		info.pixelate = EditorGUILayout.IntField("Pixelate", info.pixelate);
		info.alpha01 = EditorGUILayout.Toggle("Normalize Alpha", info.alpha01);
		if (EditorGUI.EndChangeCheck()) {
			info.pixelate = Mathf.Clamp(info.pixelate, 1, 1024);
		}

		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button("Save As...")) {
			SaveAs();
		}
		if (GUILayout.Button("Apply")) {
			if (!saved)
				SaveAs();
			else
				texture = SCProcessor.ProcessTexture(selectedTexture, info);
		}
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.EndVertical();

		EditorGUILayout.EndHorizontal();
	}
}
