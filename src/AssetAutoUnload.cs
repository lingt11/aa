using System;
using UnityEngine;

// Token: 0x02000005 RID: 5
public class AssetAutoUnload : MonoBehaviour
{
	// Token: 0x0600000C RID: 12 RVA: 0x000028E3 File Offset: 0x00000AE3
	private void OnEnable()
	{
		this.useTime = 0f;
	}

	// Token: 0x0600000D RID: 13 RVA: 0x000028F0 File Offset: 0x00000AF0
	private void Update()
	{
		this.useTime += Time.deltaTime;
		if (this.useTime >= this.time)
		{
			base.gameObject.UnLoadPrefab();
		}
	}

	// Token: 0x04000025 RID: 37
	[SerializeField]
	private float time = 3f;

	// Token: 0x04000026 RID: 38
	private float useTime;
}
