using System;
using UnityEngine;

// Token: 0x02000403 RID: 1027
public class SelfDestroy : MonoBehaviour
{
	// Token: 0x06001788 RID: 6024 RVA: 0x00093052 File Offset: 0x00091252
	private void Start()
	{
		Object.Destroy(base.gameObject, this.timeToDestroy);
	}

	// Token: 0x0400166D RID: 5741
	public float timeToDestroy = 2f;
}
