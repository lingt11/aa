using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace CC.Render.DecalDraw
{
	// Token: 0x02000485 RID: 1157
	[ExecuteInEditMode]
	public class DecalInstanceManager : MonoBehaviour
	{
		// Token: 0x060019C2 RID: 6594 RVA: 0x0009E402 File Offset: 0x0009C602
		public void OnEnable()
		{
			this.props = new MaterialPropertyBlock();
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x0009E40F File Offset: 0x0009C60F
		private void LateUpdate()
		{
			this.Init();
			this.Draw(this.cam);
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x0009E424 File Offset: 0x0009C624
		private void Init()
		{
			this.matrices = new Matrix4x4[this.transforms.Length];
			for (int i = 0; i < this.transforms.Length; i++)
			{
				this.matrices[i] = this.transforms[i].localToWorldMatrix;
			}
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x0009E470 File Offset: 0x0009C670
		private void Draw(Camera camera)
		{
			if (this.transforms.Length != 0)
			{
				if (this.texture != null)
				{
					this.props.SetTexture("_MainTex", this.texture);
				}
				this.props.SetFloatArray("_UVIndex", this.indexs);
				Graphics.DrawMeshInstanced(this.m_CubeMesh, 0, this.m_Material, this.matrices, this.matrices.Length, this.props, ShadowCastingMode.Off, true, this.decalLayer, camera);
			}
		}

		// Token: 0x04001925 RID: 6437
		public Material m_Material;

		// Token: 0x04001926 RID: 6438
		[Tooltip("贴花的图集")]
		public Texture2D texture;

		// Token: 0x04001927 RID: 6439
		[Tooltip("贴花的 Layer")]
		public int decalLayer;

		// Token: 0x04001928 RID: 6440
		[Tooltip("贴花的索引")]
		public float[] indexs;

		// Token: 0x04001929 RID: 6441
		[Tooltip("贴花的 Transform")]
		public Transform[] transforms;

		// Token: 0x0400192A RID: 6442
		private Matrix4x4[] matrices;

		// Token: 0x0400192B RID: 6443
		[SerializeField]
		private Camera cam;

		// Token: 0x0400192C RID: 6444
		public Mesh m_CubeMesh;

		// Token: 0x0400192D RID: 6445
		private MaterialPropertyBlock props;
	}
}
