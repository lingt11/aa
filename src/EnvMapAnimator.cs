using System;
using System.Collections;
using TMPro;
using UnityEngine;

// Token: 0x02000412 RID: 1042
public class EnvMapAnimator : MonoBehaviour
{
	// Token: 0x060017B8 RID: 6072 RVA: 0x0009425A File Offset: 0x0009245A
	private void Awake()
	{
		this.m_textMeshPro = base.GetComponent<TMP_Text>();
		this.m_material = this.m_textMeshPro.fontSharedMaterial;
	}

	// Token: 0x060017B9 RID: 6073 RVA: 0x00094279 File Offset: 0x00092479
	private IEnumerator Start()
	{
		Matrix4x4 matrix = default(Matrix4x4);
		for (;;)
		{
			matrix.SetTRS(Vector3.zero, Quaternion.Euler(Time.time * this.RotationSpeeds.x, Time.time * this.RotationSpeeds.y, Time.time * this.RotationSpeeds.z), Vector3.one);
			this.m_material.SetMatrix("_EnvMatrix", matrix);
			yield return null;
		}
		yield break;
	}

	// Token: 0x040016C6 RID: 5830
	public Vector3 RotationSpeeds;

	// Token: 0x040016C7 RID: 5831
	private TMP_Text m_textMeshPro;

	// Token: 0x040016C8 RID: 5832
	private Material m_material;
}
