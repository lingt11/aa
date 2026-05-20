using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000033 RID: 51
public class DevToolButton : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler
{
	// Token: 0x060000C2 RID: 194 RVA: 0x00005C54 File Offset: 0x00003E54
	public void OnPointerEnter(PointerEventData eventData)
	{
		Action action = this.action;
		if (action == null)
		{
			return;
		}
		action();
	}

	// Token: 0x040000F9 RID: 249
	public Action action;
}
