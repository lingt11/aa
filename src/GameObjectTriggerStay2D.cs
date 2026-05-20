using System;
using UnityEngine;

// Token: 0x02000011 RID: 17
public class GameObjectTriggerStay2D : MonoBehaviour
{
	// Token: 0x06000035 RID: 53 RVA: 0x00002DCA File Offset: 0x00000FCA
	private void OnTriggerStay2D(Collider2D other)
	{
		this.triggerStay(other);
	}

	// Token: 0x0400003F RID: 63
	public GameObjectTriggerStay2D.TriggerStay triggerStay;

	// Token: 0x02000012 RID: 18
	// (Invoke) Token: 0x06000038 RID: 56
	public delegate void TriggerStay(Collider2D other);
}
