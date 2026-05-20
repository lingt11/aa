using System;
using UnityEngine;

// Token: 0x0200007C RID: 124
public class TrailRendererAutoClear : MonoBehaviour
{
	// Token: 0x06000272 RID: 626 RVA: 0x0000CC52 File Offset: 0x0000AE52
	private void Awake()
	{
		this.trailRenderer = base.GetComponent<TrailRenderer>();
	}

	// Token: 0x06000273 RID: 627 RVA: 0x0000CC60 File Offset: 0x0000AE60
	private void OnDisable()
	{
		this.trailRenderer.Clear();
	}

	// Token: 0x0400026A RID: 618
	private TrailRenderer trailRenderer;
}
