using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020000B5 RID: 181
public class ButtonRightClickHandler : MonoBehaviour
{
	// Token: 0x0600035A RID: 858 RVA: 0x00016550 File Offset: 0x00014750
	public void AddLeftClickEvent(Action<object> ac, object x)
	{
		this.myButton.onClick.RemoveAllListeners();
		this.myButton.onClick.AddListener(delegate()
		{
			ac(x);
		});
	}

	// Token: 0x0600035B RID: 859 RVA: 0x0001659D File Offset: 0x0001479D
	public void AddRightClickEvent(Action<object> ac, object x)
	{
		this.rightEvent = ac;
		this.rightData = x;
	}

	// Token: 0x0600035C RID: 860 RVA: 0x000165B0 File Offset: 0x000147B0
	private void Update()
	{
		if (Input.GetMouseButtonDown(0) && this.IsPointerOverUIObject())
		{
			this.myButton.onClick.Invoke();
			Debug.Log("Left-click on button");
		}
		if (Input.GetMouseButtonDown(1) && this.IsPointerOverUIObject())
		{
			Debug.Log("Right-click on button");
			if (this.rightEvent != null)
			{
				this.rightEvent(this.rightData);
			}
		}
	}

	// Token: 0x0600035D RID: 861 RVA: 0x0001661C File Offset: 0x0001481C
	private bool IsPointerOverUIObject()
	{
		PointerEventData eventData = new PointerEventData(EventSystem.current)
		{
			position = Input.mousePosition
		};
		List<RaycastResult> list = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventData, list);
		return list.Count > 0;
	}

	// Token: 0x04000360 RID: 864
	public Button myButton;

	// Token: 0x04000361 RID: 865
	private Action<object> rightEvent;

	// Token: 0x04000362 RID: 866
	private object rightData;
}
