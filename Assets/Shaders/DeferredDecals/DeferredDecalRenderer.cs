using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using System.Collections.Generic;

// See _ReadMe.txt

public class DeferredDecalSystem
{
	static DeferredDecalSystem m_Instance;
	static public DeferredDecalSystem instance {
		get {
			if (m_Instance == null)
				m_Instance = new DeferredDecalSystem();
			return m_Instance;
		}
	}

	internal HashSet<Decal> m_DecalsDiffuse = new HashSet<Decal>();
	internal HashSet<Decal> m_DecalsNormals = new HashSet<Decal>();
	internal HashSet<Decal> m_DecalsBoth = new HashSet<Decal>();

	public void AddDecal (Decal d)
	{
		RemoveDecal (d);
		if (d.m_Kind == Decal.Kind.DiffuseOnly)
			m_DecalsDiffuse.Add (d);
		if (d.m_Kind == Decal.Kind.NormalsOnly)
			m_DecalsNormals.Add (d);
		if (d.m_Kind == Decal.Kind.Both)
			m_DecalsBoth.Add (d);
	}
	public void RemoveDecal (Decal d)
	{
		m_DecalsDiffuse.Remove (d);
		m_DecalsNormals.Remove (d);
		m_DecalsBoth.Remove (d);
	}
}

[ExecuteInEditMode]
public class DeferredDecalRenderer : MonoBehaviour
{
	public Mesh m_CubeMesh;
	private Dictionary<Camera,CommandBuffer> m_Cameras = new Dictionary<Camera,CommandBuffer>();

	public void OnDisable()
	{
		foreach (var cam in m_Cameras)
		{
			if (cam.Key)
			{
				cam.Key.RemoveCommandBuffer (CameraEvent.BeforeLighting, cam.Value);
			}
		}
	}

	public void OnWillRenderObject()
	{
		var act = gameObject.activeInHierarchy && enabled;
		if (!act)
		{
			OnDisable();
			return;
		}

		var cam = Camera.current;
		if (!cam)
			return;

		CommandBuffer buf = null;
		if (m_Cameras.ContainsKey(cam))
		{
			buf = m_Cameras[cam];
			buf.Clear ();
		}
		else
		{
			buf = new CommandBuffer();
			buf.name = "Deferred decals";
			m_Cameras[cam] = buf;

			// set this command buffer to be executed just before deferred lighting pass
			// in the camera
			cam.AddCommandBuffer (CameraEvent.BeforeLighting, buf);
		}

		//@TODO: in a real system should cull decals, and possibly only
		// recreate the command buffer when something has changed.

		var system = DeferredDecalSystem.instance;

		// copy g-buffer normals into a temporary RT
		var normalsID = Shader.PropertyToID("_NormalsCopy");
		buf.GetTemporaryRT (normalsID, -1, -1);
		buf.Blit (BuiltinRenderTextureType.GBuffer2, normalsID);
		// render diffuse-only decals into diffuse channel
		buf.SetRenderTarget (BuiltinRenderTextureType.GBuffer0, BuiltinRenderTextureType.CameraTarget);
		foreach (var decal in system.m_DecalsDiffuse)
		{
			buf.DrawMesh (m_CubeMesh, decal.transform.localToWorldMatrix, decal.m_Material);
		}
		// render normals-only decals into normals channel
		buf.SetRenderTarget (BuiltinRenderTextureType.GBuffer2, BuiltinRenderTextureType.CameraTarget);
		foreach (var decal in system.m_DecalsNormals)
		{
			buf.DrawMesh (m_CubeMesh, decal.transform.localToWorldMatrix, decal.m_Material);
		}
		// render diffuse+normals decals into two MRTs
		RenderTargetIdentifier[] mrt = {BuiltinRenderTextureType.GBuffer0, BuiltinRenderTextureType.GBuffer2};
		buf.SetRenderTarget (mrt, BuiltinRenderTextureType.CameraTarget);
		foreach (var decal in system.m_DecalsBoth)
		{
			buf.DrawMesh (m_CubeMesh, decal.transform.localToWorldMatrix, decal.m_Material);
		}
		// release temporary normals RT
		buf.ReleaseTemporaryRT (normalsID);
	}
}
