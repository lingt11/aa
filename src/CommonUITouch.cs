using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x020000CC RID: 204
public class CommonUITouch : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x060003A0 RID: 928 RVA: 0x000178AB File Offset: 0x00015AAB
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (this.touchPointerEnter != null)
		{
			this.touchPointerEnter();
		}
	}

	// Token: 0x060003A1 RID: 929 RVA: 0x000178C0 File Offset: 0x00015AC0
	public void OnPointerExit(PointerEventData eventData)
	{
		if (this.touchPointerExit != null)
		{
			this.touchPointerExit();
		}
	}

	// Token: 0x0400038C RID: 908
	public Action touchPointerEnter;

	// Token: 0x0400038D RID: 909
	public Action touchPointerExit;
}
