using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000065 RID: 101
public class UIDoubleClick : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	// Token: 0x060001DC RID: 476 RVA: 0x0000B0C0 File Offset: 0x000092C0
	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Right && this.rightClickEvent != null)
		{
			this.rightClickEvent();
			return;
		}
		float time = Time.time;
		if (time - this.lastClickTime < this.doubleClickThreshold)
		{
			this.OnDoubleClick();
		}
		this.lastClickTime = time;
	}

	// Token: 0x060001DD RID: 477 RVA: 0x0000B10D File Offset: 0x0000930D
	private void OnDoubleClick()
	{
		this.clickEvent();
	}

	// Token: 0x04000217 RID: 535
	public float doubleClickThreshold = 0.2f;

	// Token: 0x04000218 RID: 536
	private float lastClickTime;

	// Token: 0x04000219 RID: 537
	public Action clickEvent;

	// Token: 0x0400021A RID: 538
	public Action rightClickEvent;
}
