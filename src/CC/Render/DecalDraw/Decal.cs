using System;
using UnityEngine;

namespace CC.Render.DecalDraw
{
	// Token: 0x02000484 RID: 1156
	[ExecuteInEditMode]
	public class Decal : MonoBehaviour
	{
		// Token: 0x060019BE RID: 6590 RVA: 0x0009E364 File Offset: 0x0009C564
		public void OnEnable()
		{
			this.props = new MaterialPropertyBlock();
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x0009E371 File Offset: 0x0009C571
		private void LateUpdate()
		{
			this.Draw(this.cam);
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x0009E380 File Offset: 0x0009C580
		private void Draw(Camera camera)
		{
			if (this.texture != null)
			{
				this.props.SetTexture("_MainTex", this.texture);
			}
			this.props.SetColor("_Tint", this.tinting);
			Graphics.DrawMesh(this.m_CubeMesh, base.transform.localToWorldMatrix, this.m_Material, 0, null, 0, this.props, false, true, false);
		}

		// Token: 0x0400191F RID: 6431
		public Material m_Material;

		// Token: 0x04001920 RID: 6432
		public Texture2D texture;

		// Token: 0x04001921 RID: 6433
		public Color tinting = Color.white;

		// Token: 0x04001922 RID: 6434
		[SerializeField]
		private Camera cam;

		// Token: 0x04001923 RID: 6435
		public Mesh m_CubeMesh;

		// Token: 0x04001924 RID: 6436
		private MaterialPropertyBlock props;
	}
}
