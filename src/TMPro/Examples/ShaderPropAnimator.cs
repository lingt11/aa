using System;
using System.Collections;
using UnityEngine;

namespace TMPro.Examples
{
	// Token: 0x02000430 RID: 1072
	public class ShaderPropAnimator : MonoBehaviour
	{
		// Token: 0x0600181A RID: 6170 RVA: 0x000967F4 File Offset: 0x000949F4
		private void Awake()
		{
			this.m_Renderer = base.GetComponent<Renderer>();
			this.m_Material = this.m_Renderer.material;
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x00096813 File Offset: 0x00094A13
		private void Start()
		{
			base.StartCoroutine(this.AnimateProperties());
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x00096822 File Offset: 0x00094A22
		private IEnumerator AnimateProperties()
		{
			this.m_frame = Random.Range(0f, 1f);
			for (;;)
			{
				float value = this.GlowCurve.Evaluate(this.m_frame);
				this.m_Material.SetFloat(ShaderUtilities.ID_GlowPower, value);
				this.m_frame += Time.deltaTime * Random.Range(0.2f, 0.3f);
				yield return new WaitForEndOfFrame();
			}
			yield break;
		}

		// Token: 0x0400176F RID: 5999
		private Renderer m_Renderer;

		// Token: 0x04001770 RID: 6000
		private Material m_Material;

		// Token: 0x04001771 RID: 6001
		public AnimationCurve GlowCurve;

		// Token: 0x04001772 RID: 6002
		public float m_frame;
	}
}
