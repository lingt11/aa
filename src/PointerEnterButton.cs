using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x0200005A RID: 90
public class PointerEnterButton : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler
{
	// Token: 0x0600019C RID: 412 RVA: 0x00009CF9 File Offset: 0x00007EF9
	public void OnPointerEnter(PointerEventData eventData)
	{
		Action action = this.action;
		if (action == null)
		{
			return;
		}
		action();
	}

	// Token: 0x040001EC RID: 492
	public Action action;
}
