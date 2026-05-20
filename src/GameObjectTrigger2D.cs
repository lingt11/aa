using System;
using UnityEngine;

// Token: 0x0200000F RID: 15
public class GameObjectTrigger2D : MonoBehaviour
{
	// Token: 0x0600002F RID: 47 RVA: 0x00002DBC File Offset: 0x00000FBC
	private void OnTriggerEnter2D(Collider2D other)
	{
		this.triggerEnterEvent(other);
	}

	// Token: 0x0400003E RID: 62
	public GameObjectTrigger2D.TriggerEnter triggerEnterEvent;

	// Token: 0x02000010 RID: 16
	// (Invoke) Token: 0x06000032 RID: 50
	public delegate void TriggerEnter(Collider2D other);
}
