using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.Networking;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class PSXEffects : MonoBehaviour {

	public System.Version version = new System.Version("1.15.5");
	public string cfuStatus = "PSXEffects";
	public bool[] sections = { true, true, true, false };

	public Vector2Int customRes = new Vector2Int(620, 480);
	public int resolutionFactor = 1;
	public int limitFramerate = -1;
	public int skipFrames = 0;
	public bool affineMapping = true;
	public float polygonalDrawDistance = 0f;
	public int vertexInaccuracy = 30;
	public int polygonInaccuracy = 2;
	public int colorDepth = 5;
	public bool scanlines = false;
	public int scanlineIntensity = 5;
	public Texture2D ditherTexture;
	public bool dithering = true;
	public float ditherThreshold = 1;
	public int ditherIntensity = 2;
	public int maxDarkness = 20;
	public int subtractFade = 0;
	public float favorRed = 1.0f;
	public bool worldSpaceSnapping = false;
	public bool postProcessing = true;
	public bool verticalScanlines = true;
	public float shadowIntensity = 0.5f;
	public bool downscale = false;
	public bool snapCamera = false;
	public float camInaccuracy = 0.05f;
	public bool camSnapping = false;
	public int shadowType = 0;
	public bool ditherSky = false;
	public int ditherType = 1;

	private Material postProcessingMat;
	private RenderTexture rt;
	private Vector2 prevCustomRes;

	private void Awake() {
		if (Application.isPlaying) {
			QualitySettings.vSyncCount = 0;
		}

		QualitySettings.antiAliasing = 0;
		cfuStatus = "PSXEffects v" + version.ToString();

		Camera cam = GetComponent<Camera>();
		cam.depthTextureMode = cam.depthTextureMode | DepthTextureMode.Depth;

		if (!downscale) {
			customRes = new Vector2Int(Screen.width / resolutionFactor, Screen.height / resolutionFactor);
		}
		rt = new RenderTexture(customRes.x, customRes.y, 16, RenderTextureFormat.ARGB32);
		rt.filterMode = FilterMode.Point;

		CheckForUpdates();
	}

	private void Update() {
		if (!downscale) {
			customRes = new Vector2Int(Screen.width / resolutionFactor, Screen.height / resolutionFactor);
		}

		if (prevCustomRes != customRes) {
			rt = new RenderTexture(customRes.x, customRes.y, 16, RenderTextureFormat.ARGB32);
			rt.filterMode = FilterMode.Point;
		}

		// Set mesh shader variables
		Shader.SetGlobalFloat("_AffineMapping", affineMapping ? 1.0f : 0.0f);
		Shader.SetGlobalFloat("_DrawDistance", polygonalDrawDistance);
		Shader.SetGlobalInt("_VertexSnappingDetail", vertexInaccuracy / 2);
		Shader.SetGlobalInt("_Offset", polygonInaccuracy);
		Shader.SetGlobalFloat("_DarkMax", (float)maxDarkness / 100);
		Shader.SetGlobalFloat("_SubtractFade", (float)subtractFade / 100);
		Shader.SetGlobalFloat("_WorldSpace", worldSpaceSnapping ? 1.0f : 0.0f);
		Shader.SetGlobalFloat("_CamPos", camSnapping ? 1.0f : 0.0f);
		Shader.SetGlobalFloat("_ShadowType", shadowType);


		if (postProcessing) {
			// Handles all post processing variables
			if (postProcessingMat == null) {
				postProcessingMat = new Material(Shader.Find("Hidden/PS1PostProcessing"));
			} else {
				postProcessingMat.SetFloat("_ColorDepth", colorDepth);
				postProcessingMat.SetFloat("_Scanlines", scanlines ? 1 : 0);
				postProcessingMat.SetFloat("_ScanlineIntensity", (float)scanlineIntensity / 100);
				postProcessingMat.SetTexture("_DitherTex", ditherTexture);
				postProcessingMat.SetFloat("_Dithering", dithering ? 1 : 0);
				postProcessingMat.SetFloat("_DitherThreshold", ditherThreshold);
				postProcessingMat.SetFloat("_DitherIntensity", (float)ditherIntensity / 100);
				postProcessingMat.SetFloat("_ResX", customRes.x);
				postProcessingMat.SetFloat("_ResY", customRes.y);
				postProcessingMat.SetFloat("_FavorRed", favorRed);
				postProcessingMat.SetFloat("_SLDirection", verticalScanlines ? 1 : 0);
				postProcessingMat.SetFloat("_DitherSky", ditherSky ? 1 : 0);
				postProcessingMat.SetFloat("_DitherType", ditherType);
			}
		}

		Application.targetFrameRate = limitFramerate;

		prevCustomRes = customRes;
	}

	void LateUpdate() {
		if (snapCamera && Application.isPlaying) {
			// Handles the camera position snapping
			if (transform.parent == null || !transform.parent.name.Contains("CameraRealPosition")) {
				GameObject newParent = new GameObject("CameraRealPosition");
				newParent.transform.position = transform.position;
				if (transform.parent)
					newParent.transform.SetParent(transform.parent);
				transform.SetParent(newParent.transform);
			}

			Vector3 snapPos = transform.parent.position;
			snapPos /= camInaccuracy;
			snapPos = new Vector3(Mathf.Round(snapPos.x), Mathf.Round(snapPos.y), Mathf.Round(snapPos.z));
			snapPos *= camInaccuracy;
			transform.position = snapPos;
		} else if(transform.parent != null && transform.parent.name.Contains("CameraRealPosition")) {
			Destroy(transform.parent.gameObject);
		}
	}

	public void SnapCamera() {
		Vector3 snapPos = transform.position;
		snapPos /= camInaccuracy;
		snapPos = new Vector3(Mathf.Round(snapPos.x), Mathf.Round(snapPos.y), Mathf.Round(snapPos.z));
		snapPos *= camInaccuracy;
		transform.position = snapPos;
	}

	// Draw a transparent red circle around the camera to show its
	// real position
	private void OnDrawGizmos() {
		if (snapCamera) {
			Gizmos.color = new Color(1, 0, 0, 0.5f);
			if(transform.parent != null)
				Gizmos.DrawSphere(transform.parent.position, 0.5f);
		}
	}

	private void OnRenderImage(RenderTexture src, RenderTexture dst) {
		if (postProcessing) {
			if (customRes.x > 2 && customRes.y > 2) {
				// Renders scene to downscaled render texture using the post processing shader
				if (src != null)
					src.filterMode = FilterMode.Point;
				if (skipFrames < 1 || Time.frameCount % skipFrames == 0)
					Graphics.Blit(src, rt);
				Graphics.Blit(rt, dst, postProcessingMat);
			} else {
				customRes.x = 2;
				customRes.y = 2;
			}
		} else {
			// Renders scene to downscaled render texture
			if (src != null)
				src.filterMode = FilterMode.Point;
			if(skipFrames < 1 || Time.frameCount % skipFrames == 0)
				Graphics.Blit(src, rt);
			Graphics.Blit(rt, dst);
		}
	}

	public void CheckForUpdates() {
		StartCoroutine("CheckForUpdate");
	}

	IEnumerator CheckForUpdate() {
		cfuStatus = "Checking for updates...";
		UnityWebRequest www = UnityWebRequest.Get("https://tripleaxis.net/test/psfxversion/");
		yield return www.SendWebRequest();

		if (www.isNetworkError || www.isHttpError) {
			Debug.Log(www.error);
		} else {
			System.Version onlineVer = new System.Version(www.downloadHandler.text);
			int comparison = onlineVer.CompareTo(version);

			if (comparison < 0) {
				cfuStatus = "PSXEffects v" + version.ToString() + " - version ahead?!";
			} else if (comparison == 0) {
				cfuStatus = "PSXEffects v" + version.ToString() + " - up to date.";
			} else {
				cfuStatus = "PSXEffects v" + version.ToString() + " - update available (click to update).";
			}
		}
	}
}
