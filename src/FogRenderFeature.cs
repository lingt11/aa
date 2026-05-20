using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

// Token: 0x020003F0 RID: 1008
public class FogRenderFeature : ScriptableRendererFeature
{
	// Token: 0x06001743 RID: 5955 RVA: 0x00090F19 File Offset: 0x0008F119
	public override void Create()
	{
		this.fogPass = new FogRenderFeature.FogPass(this.settings);
	}

	// Token: 0x06001744 RID: 5956 RVA: 0x00090F2C File Offset: 0x0008F12C
	public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
	{
		if (this.settings.material != null)
		{
			this.settings.material.SetFloat("_FogDensity", this.settings.fogDensity);
			this.settings.material.SetFloat("_DistanceOffset", this.settings.distanceOffset);
			this.settings.material.SetFloat("_HeightFade", this.settings.heightFade);
			this.settings.material.SetFloat("_MipCount", (float)this.settings.mipCount);
			renderer.EnqueuePass(this.fogPass);
		}
	}

	// Token: 0x040015E0 RID: 5600
	public FogRenderFeature.FogSettings settings = new FogRenderFeature.FogSettings();

	// Token: 0x040015E1 RID: 5601
	private FogRenderFeature.FogPass fogPass;

	// Token: 0x020003F1 RID: 1009
	[Serializable]
	public class FogSettings
	{
		// Token: 0x040015E2 RID: 5602
		public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingOpaques;

		// Token: 0x040015E3 RID: 5603
		[Range(0f, 1f)]
		public float fogDensity = 0.01f;

		// Token: 0x040015E4 RID: 5604
		public float heightFade = 1f;

		// Token: 0x040015E5 RID: 5605
		public float distanceOffset;

		// Token: 0x040015E6 RID: 5606
		[Range(0f, 5f)]
		public int mipCount;

		// Token: 0x040015E7 RID: 5607
		public Material material;
	}

	// Token: 0x020003F2 RID: 1010
	private class FogPass : ScriptableRenderPass
	{
		// Token: 0x06001747 RID: 5959 RVA: 0x00091018 File Offset: 0x0008F218
		public FogPass(FogRenderFeature.FogSettings settings)
		{
			base.renderPassEvent = settings.renderPassEvent;
			this.m_Material = settings.material;
			this.m_TemporaryColorTexture.Init("_TemporaryColorTexture");
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x00091048 File Offset: 0x0008F248
		public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
		{
			this.colorDescriptor = cameraTextureDescriptor;
			cmd.GetTemporaryRT(this.m_TemporaryColorTexture.id, this.colorDescriptor, FilterMode.Bilinear);
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x0009106C File Offset: 0x0008F26C
		public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			CommandBuffer commandBuffer = CommandBufferPool.Get("Fog Effect");
			this.m_Camera = renderingData.cameraData.camera;
			RenderTargetIdentifier cameraColorTarget = renderingData.cameraData.renderer.cameraColorTarget;
			this.CreateQuadMesh();
			this.SetupCameraVectors(commandBuffer);
			this.m_Material.SetMatrix("_InverseView", this.m_Camera.cameraToWorldMatrix);
			this.m_Material.SetMatrix("_InverseProjection", GL.GetGPUProjectionMatrix(this.m_Camera.projectionMatrix, true).inverse);
			base.Blit(commandBuffer, cameraColorTarget, this.m_TemporaryColorTexture.Identifier(), this.m_Material, 0);
			base.Blit(commandBuffer, this.m_TemporaryColorTexture.Identifier(), cameraColorTarget, null, 0);
			context.ExecuteCommandBuffer(commandBuffer);
			CommandBufferPool.Release(commandBuffer);
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x00091134 File Offset: 0x0008F334
		private void CreateQuadMesh()
		{
			this.m_QuadMesh = new Mesh();
			this.m_QuadMesh.vertices = new Vector3[]
			{
				new Vector3(-1f, -1f, 0f),
				new Vector3(1f, -1f, 0f),
				new Vector3(1f, 1f, 0f),
				new Vector3(-1f, 1f, 0f)
			};
			bool flag = SystemInfo.graphicsDeviceType == GraphicsDeviceType.Direct3D11 || SystemInfo.graphicsDeviceType == GraphicsDeviceType.Direct3D12;
			Mesh quadMesh = this.m_QuadMesh;
			Vector2[] uv;
			if (!flag)
			{
				Vector2[] array = new Vector2[4];
				array[0] = new Vector2(0f, 0f);
				array[1] = new Vector2(1f, 0f);
				array[2] = new Vector2(1f, 1f);
				uv = array;
				array[3] = new Vector2(0f, 1f);
			}
			else
			{
				Vector2[] array2 = new Vector2[4];
				array2[0] = new Vector2(0f, 1f);
				array2[1] = new Vector2(1f, 1f);
				array2[2] = new Vector2(1f, 0f);
				uv = array2;
				array2[3] = new Vector2(0f, 0f);
			}
			quadMesh.uv = uv;
			this.m_QuadMesh.triangles = new int[]
			{
				0,
				1,
				2,
				0,
				2,
				3
			};
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x000912C8 File Offset: 0x0008F4C8
		private void SetupCameraVectors(CommandBuffer cmd)
		{
			float nearClipPlane = this.m_Camera.nearClipPlane;
			float farClipPlane = this.m_Camera.farClipPlane;
			float fieldOfView = this.m_Camera.fieldOfView;
			float aspect = this.m_Camera.aspect;
			Matrix4x4 worldToCameraMatrix = this.m_Camera.worldToCameraMatrix;
			GL.GetGPUProjectionMatrix(this.m_Camera.projectionMatrix, true) * worldToCameraMatrix;
			Vector3[] array = new Vector3[4];
			this.m_Camera.CalculateFrustumCorners(new Rect(0f, 0f, 1f, 1f), nearClipPlane, Camera.MonoOrStereoscopicEye.Mono, array);
			for (int i = 0; i < 4; i++)
			{
				array[i] = this.m_Camera.transform.TransformVector(array[i]);
				array[i] = array[i].normalized * farClipPlane;
			}
			this.m_Material.SetVector("_FrustumCornerTL", array[1]);
			this.m_Material.SetVector("_FrustumCornerTR", array[2]);
			this.m_Material.SetVector("_FrustumCornerBR", array[3]);
			this.m_Material.SetVector("_FrustumCornerBL", array[0]);
		}

		// Token: 0x040015E8 RID: 5608
		private Material m_Material;

		// Token: 0x040015E9 RID: 5609
		private Mesh m_QuadMesh;

		// Token: 0x040015EA RID: 5610
		private Camera m_Camera;

		// Token: 0x040015EB RID: 5611
		private RenderTargetHandle m_TemporaryColorTexture;

		// Token: 0x040015EC RID: 5612
		internal RenderTextureDescriptor colorDescriptor;

		// Token: 0x040015ED RID: 5613
		private static readonly int CameraVectors = Shader.PropertyToID("_CameraVectors");

		// Token: 0x040015EE RID: 5614
		private static readonly int InverseProjectionMatrix = Shader.PropertyToID("_InverseProjectionMatrix");
	}
}
