using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoValueChanger : MonoBehaviour {

	private PSXEffects psx;

	void Start() {
		psx = FindObjectOfType<PSXEffects>();
	}

	public void _ResolutionFactor(string input) {
		int val = int.Parse(input);
		psx.resolutionFactor = val;
	}

	public void _FrameSkip(string input) {
		int val = int.Parse(input);
		psx.skipFrames = val;
	}

	public void _AffineMapping(bool input) {
		psx.affineMapping = input;
	}

	public void _DrawDistance(string input) {
		float val = float.Parse(input);
		psx.polygonalDrawDistance = val;
	}

	public void _PolygonInaccuracy(string input) {
		int val = int.Parse(input);
		psx.polygonInaccuracy = val;
	}

	public void _VertexInaccuracy(string input) {
		int val = int.Parse(input);
		psx.vertexInaccuracy = val;
	}

	public void _WorldSpaceSnapping(bool input) {
		psx.worldSpaceSnapping = input;
	}

	public void _CameraBasedSnapping(bool input) {
		psx.camSnapping = input;
	}

	public void _SaturatedDiffuse(string input) {
		int val = int.Parse(input);
		psx.maxDarkness = val;
	}

	public void _PostProcessing(bool input) {
		psx.postProcessing = input;
	}

	public void _ColorDepth(string input) {
		int val = int.Parse(input);
		psx.colorDepth = val;
	}

	public void _SubtractionFade(float input) {
		psx.subtractFade = (int)input;
	}

	public void _FavorRed(string input) {
		float val = float.Parse(input);
		psx.favorRed = val;
	}

	public void _Scanlines(bool input) {
		psx.scanlines = input;
	}

	public void _Vertical(bool input) {
		psx.verticalScanlines = input;
	}

	public void _ScanlineIntensity(string input) {
		int val = int.Parse(input);
		psx.scanlineIntensity = val;
	}

	public void _Dithering(bool input) {
		psx.dithering = input;
	}

	public void _DitherThreshold(string input) {
		float val = float.Parse(input);
		psx.ditherThreshold = val;
	}

	public void _DitherIntensity(string input) {
		int val = int.Parse(input);
		psx.ditherIntensity = val;
	}
}
