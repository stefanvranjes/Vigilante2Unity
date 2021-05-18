using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public struct SCInformation {
	public int depth;
	public int ditherIntensity;
	public int pixelate;
	public bool alpha01;
	public string texUrl;
	public string extension;
	public string directory;
	public string name;

	public SCInformation(int dummy) {
		depth = 230;
		ditherIntensity = 6;
		pixelate = 1;
		alpha01 = false;
		texUrl = "";
		extension = "";
		directory = "";
		name = "";
	}
}

public class SCProcessor
{

	private static Color32 GetDither(int x, int y, Color32 color, float intensity, int scale) {
		int[] thresholds = {
			-4, 0, -3, 1,
			2, -2, 3, -1,
			-3, 1, -4, 0,
			3, -1, 2, -2
		};
		int[] lut = new int[256];
		for (int i = 0; i < 256; i++) {
			int value = (int)(i + thresholds[(x / scale) % 4 * 4 + (y / scale) % 4] * intensity);
			lut[i] = Mathf.Clamp(value, 0, 255);
		}
		Color32 newColor = new Color32();
		newColor.r = (byte)(lut[color.r]);
		newColor.g = (byte)(lut[color.g]);
		newColor.b = (byte)(lut[color.b]);
		newColor.a = color.a;
		return newColor;
	}

	public static Texture2D CreateCopyOfTexture(Texture2D tex, string newName) {
		string path = AssetDatabase.GetAssetPath(tex);
		string newPath = newName;
		if (AssetDatabase.CopyAsset(path, newPath)) {
			TextureImporter ti = AssetImporter.GetAtPath(newPath) as TextureImporter;
			if (ti != null) {
				ti.isReadable = true;
				ti.textureCompression = TextureImporterCompression.Uncompressed;
				ti.filterMode = FilterMode.Point;
				AssetDatabase.ImportAsset(newPath);
				AssetDatabase.Refresh();
			} else { 
				Debug.LogError("Failed to set texture import settings for \"" + newPath + "\".");
			}
			return AssetDatabase.LoadAssetAtPath<Texture2D>(newPath);
		}
		Debug.LogError("Failed to create a copy of \"" + path + "\".");
		return null;
	}

	public static Texture2D Posterize(Texture2D tex, int depth) {
		depth = 256 - depth;
		Color32[] colors = tex.GetPixels32();
		for (int y = 0; y < tex.height; y++) {
			for (int x = 0; x < tex.width; x++) {
				int index = y * tex.width + x;
				if (colors[index].r > 0)
					colors[index].r = (byte)((float)Mathf.Round(colors[index].r / depth) * depth);
				if (colors[index].g > 0)
					colors[index].g = (byte)((float)Mathf.Round(colors[index].g / depth) * depth);
				if (colors[index].b > 0)
					colors[index].b = (byte)((float)Mathf.Round(colors[index].b / depth) * depth);
				if (colors[index].a > 0 && colors[index].a < 255)
					colors[index].a = (byte)((float)Mathf.Round(colors[index].a / depth) * depth);
			}
		}
		tex.SetPixels32(colors);
		return tex;
	}

	public static Texture2D Dither(Texture2D tex, float intensity, int scale) {
		Color32[] colors = tex.GetPixels32();
		for (int y = 0; y < tex.height; y++) {
			for (int x = 0; x < tex.width; x++) {
				int index = y * tex.width + x;
				colors[index] = GetDither(x, y, colors[index], intensity, scale);
			}
		}
		tex.SetPixels32(colors);
		return tex;
	}

	public static Texture2D Pixelate(Texture2D tex, int factor) {
		Color32[] colors = tex.GetPixels32();
		for (int y = 0; y < tex.height; y++) {
			for (int x = 0; x < tex.width; x++) {
				int index = y * tex.width + x;
				int pixdex = (Mathf.FloorToInt(y / factor) * factor) * tex.width + (Mathf.FloorToInt(x / factor) * factor);
				colors[index] = colors[pixdex];
			}
		}
		tex.SetPixels32(colors);
		return tex;
	}

	public static Texture2D Alpha01(Texture2D tex, bool enabled) {
		if (enabled) {
			Color32[] colors = tex.GetPixels32();
			for (int y = 0; y < tex.height; y++) {
				for (int x = 0; x < tex.width; x++) {
					int index = y * tex.width + x;
					if (colors[index].a < 255 && colors[index].a > 0)
						colors[index].a = 255;
				}
			}
			tex.SetPixels32(colors);
		}
		return tex;
	}

	public static Texture2D ProcessTexture(Texture2D tex, SCInformation info) {
		tex = CreateCopyOfTexture(tex, info.texUrl);
		tex = Alpha01(tex, info.alpha01);
		tex = Pixelate(tex, info.pixelate);
		tex = Dither(tex, info.ditherIntensity, info.pixelate);
		tex = Posterize(tex, info.depth);
		tex.Apply();
		return tex;
	}
}
