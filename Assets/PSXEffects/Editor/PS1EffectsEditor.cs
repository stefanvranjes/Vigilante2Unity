using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;

[CustomEditor(typeof(PSXEffects))]
public class PSXEffectsEditor : Editor {

	public enum ShadowTypes {
		Default,
		PSX
	}

	public enum DitherTypes {
		PSX,
		Texture
	}

	SerializedProperty downscale;
	SerializedProperty resolutionVert;
	SerializedProperty resolutionFactor;
	SerializedProperty limitFramerate;
	SerializedProperty skipFrames;
	SerializedProperty affineMapping;
	SerializedProperty polygonalDrawDistance;
	SerializedProperty vertexInaccuracy;
	SerializedProperty polygonInaccuracy;
	SerializedProperty colorDepth;
	SerializedProperty scanlines;
	SerializedProperty scanlineIntensity;
	SerializedProperty dithering;
	SerializedProperty ditherTexture;
	SerializedProperty ditherThreshold;
	SerializedProperty ditherIntensity;
	SerializedProperty maxDarkness;
	SerializedProperty subtractFade;
	SerializedProperty favorRed;
	SerializedProperty postProcessing;
	SerializedProperty verticalScanlines;
	SerializedProperty snapCamera;
	SerializedProperty camInaccuracy;
	SerializedProperty worldSpaceSnapping;
	SerializedProperty camSnapping;
	SerializedProperty shadowType;
	SerializedProperty ditherSky;
	SerializedProperty ditherType;

	void OnEnable() {
		resolutionFactor = serializedObject.FindProperty("resolutionFactor");
		limitFramerate = serializedObject.FindProperty("limitFramerate");
		skipFrames = serializedObject.FindProperty("skipFrames");
		affineMapping = serializedObject.FindProperty("affineMapping");
		polygonalDrawDistance = serializedObject.FindProperty("polygonalDrawDistance");
		vertexInaccuracy = serializedObject.FindProperty("vertexInaccuracy");
		polygonInaccuracy = serializedObject.FindProperty("polygonInaccuracy");
		colorDepth = serializedObject.FindProperty("colorDepth");
		scanlines = serializedObject.FindProperty("scanlines");
		scanlineIntensity = serializedObject.FindProperty("scanlineIntensity");
		dithering = serializedObject.FindProperty("dithering");
		ditherTexture = serializedObject.FindProperty("ditherTexture");
		ditherThreshold = serializedObject.FindProperty("ditherThreshold");
		ditherIntensity = serializedObject.FindProperty("ditherIntensity");
		maxDarkness = serializedObject.FindProperty("maxDarkness");
		subtractFade = serializedObject.FindProperty("subtractFade");
		favorRed = serializedObject.FindProperty("favorRed");
		postProcessing = serializedObject.FindProperty("postProcessing");
		verticalScanlines = serializedObject.FindProperty("verticalScanlines");
		downscale = serializedObject.FindProperty("downscale");
		resolutionVert = serializedObject.FindProperty("customRes");
		snapCamera = serializedObject.FindProperty("snapCamera");
		camInaccuracy = serializedObject.FindProperty("camInaccuracy");
		worldSpaceSnapping = serializedObject.FindProperty("worldSpaceSnapping");
		camSnapping = serializedObject.FindProperty("camSnapping");
		shadowType = serializedObject.FindProperty("shadowType");
		ditherSky = serializedObject.FindProperty("ditherSky");
		ditherType = serializedObject.FindProperty("ditherType");
	}

	public override void OnInspectorGUI() {
		serializedObject.Update();

		PSXEffects psfx = (PSXEffects)target;

		GUIStyle foStyle = EditorStyles.foldout;
		FontStyle prevFoStyle = foStyle.fontStyle;
		foStyle.fontStyle = FontStyle.Bold;

		GUIStyle tinyStyle = EditorStyles.centeredGreyMiniLabel;
		GUIStyle prevTinyStyle = tinyStyle;

		psfx.sections[0] = EditorGUILayout.Foldout(psfx.sections[0], "Video Output");

		string[] ditherNames = { 
			"PSX (Accurate, Slow)",
			"Standard (Fast)",
			"Texture (Fastest)"
		};

		if (psfx.sections[0]) {
			downscale.boolValue = EditorGUILayout.Toggle("Custom Resolution", downscale.boolValue);
			if (downscale.boolValue) {
				resolutionVert.vector2IntValue = EditorGUILayout.Vector2IntField("Resolution", resolutionVert.vector2IntValue);
			} else {
				resolutionFactor.intValue = EditorGUILayout.IntField("Resolution Factor", resolutionFactor.intValue);
			}
			limitFramerate.intValue = EditorGUILayout.IntField("Target Framerate", limitFramerate.intValue);
			skipFrames.intValue = EditorGUILayout.IntField("Frame Skip", skipFrames.intValue);
			snapCamera.boolValue = EditorGUILayout.Toggle("Enable Camera Position Inaccuracy", snapCamera.boolValue);
			if (snapCamera.boolValue) {
				camInaccuracy.floatValue = EditorGUILayout.FloatField("Camera Inaccuracy", camInaccuracy.floatValue);
			}
			EditorGUILayout.Separator();
		}

		psfx.sections[1] = EditorGUILayout.Foldout(psfx.sections[1], "Mesh Settings");

		if (psfx.sections[1]) {
			affineMapping.boolValue = EditorGUILayout.Toggle("Affine Texture Mapping", affineMapping.boolValue);
			polygonalDrawDistance.floatValue = EditorGUILayout.FloatField("Polygonal Draw Distance", polygonalDrawDistance.floatValue);
			polygonInaccuracy.intValue = EditorGUILayout.IntField("Polygon Inaccuracy", polygonInaccuracy.intValue);
			vertexInaccuracy.intValue = EditorGUILayout.IntField("Vertex Inaccuracy", vertexInaccuracy.intValue);
			worldSpaceSnapping.boolValue = EditorGUILayout.Toggle("Use World Space Snapping", worldSpaceSnapping.boolValue);
			if (worldSpaceSnapping.boolValue) {
				camSnapping.boolValue = EditorGUILayout.Toggle("Camera-Based Snapping", camSnapping.boolValue);
			}
			maxDarkness.intValue = EditorGUILayout.IntSlider("Saturated Diffuse", maxDarkness.intValue, 0, 100);
			shadowType.intValue = EditorGUILayout.Popup("Shadow Type", shadowType.intValue, Enum.GetNames(typeof(ShadowTypes)));
			EditorGUILayout.Separator();
		}

		psfx.sections[2] = EditorGUILayout.Foldout(psfx.sections[2], "Post Processing");

		if (psfx.sections[2]) {
			postProcessing.boolValue = EditorGUILayout.Toggle("Enable Post Processing", postProcessing.boolValue);
			if (postProcessing.boolValue) {
				colorDepth.intValue = EditorGUILayout.IntSlider("Color Depth", colorDepth.intValue, 1, 24);
				subtractFade.intValue = EditorGUILayout.IntSlider("Subtractive Fade", subtractFade.intValue, 0, 100);
				favorRed.floatValue = EditorGUILayout.FloatField("Darken Darks/Favor Red", favorRed.floatValue);
				scanlines.boolValue = EditorGUILayout.Toggle("Scanlines", scanlines.boolValue);
				if (scanlines.boolValue) {
					verticalScanlines.boolValue = EditorGUILayout.Toggle("Vertical", verticalScanlines.boolValue);
					scanlineIntensity.intValue = EditorGUILayout.IntSlider("Scanline Intensity", scanlineIntensity.intValue, 0, 100);
				}
				dithering.boolValue = EditorGUILayout.Toggle("Enable Dithering", dithering.boolValue);
				if (dithering.boolValue) {
					ditherType.intValue = EditorGUILayout.Popup("Dithering Method", ditherType.intValue, ditherNames);
					if (ditherType.intValue == 2) {
						EditorGUILayout.BeginHorizontal();
						EditorGUILayout.PrefixLabel("Dither Texture");
						ditherTexture.objectReferenceValue = (Texture2D)EditorGUILayout.ObjectField(ditherTexture.objectReferenceValue, typeof(Texture2D), false);
						EditorGUILayout.EndHorizontal();
						ditherThreshold.floatValue = EditorGUILayout.FloatField("Dither Threshold", ditherThreshold.floatValue);
					} else {
						ditherTexture.objectReferenceValue = null;
					}
					ditherIntensity.intValue = EditorGUILayout.IntSlider("Dither Intensity", ditherIntensity.intValue, 0, 100);
					ditherSky.boolValue = EditorGUILayout.Toggle("Dither Skybox", ditherSky.boolValue);
				}
			}
			EditorGUILayout.Separator();
		}

		psfx.sections[3] = EditorGUILayout.Foldout(psfx.sections[3], "Miscellaneous", EditorStyles.foldout);

		if (psfx.sections[3]) {
			if (GUILayout.Button("Rate on the Asset Store!")) {
				Application.OpenURL("https://assetstore.unity.com/packages/vfx/shaders/psxeffects-132368");
			}
			if (GUILayout.Button("Triple Axis Website")) {
				Application.OpenURL("https://tripleaxis.net");
			}
			if (GUILayout.Button("Triple Axis on Twitter")) {
				Application.OpenURL("https://twitter.com/tripleaxis");
			}
			if (GUILayout.Button("Check for Updates")) {
				psfx.CheckForUpdates();
			}
		}
		if (GUILayout.Button(psfx.cfuStatus, tinyStyle)) {
			if (psfx.cfuStatus.Contains("update available")) {
				Application.OpenURL("https://assetstore.unity.com/packages/vfx/shaders/psxeffects-132368");
			}
		}

		serializedObject.ApplyModifiedProperties();
		foStyle.fontStyle = prevFoStyle;
		tinyStyle = prevTinyStyle;
	}
}
