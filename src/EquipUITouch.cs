using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000104 RID: 260
public class EquipUITouch : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
{
	// Token: 0x06000556 RID: 1366 RVA: 0x0001F1A3 File Offset: 0x0001D3A3
	public void OnPointerEnter(PointerEventData eventData)
	{
		UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
		if (ui == null)
		{
			return;
		}
		ui.ShowEquipInfo(true, base.transform.position, this.equipBase);
	}

	// Token: 0x06000557 RID: 1367 RVA: 0x0001F1CB File Offset: 0x0001D3CB
	public void OnPointerExit(PointerEventData eventData)
	{
		UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
		if (ui == null)
		{
			return;
		}
		ui.ShowEquipInfo(false, base.transform.position, this.equipBase);
	}

	// Token: 0x06000558 RID: 1368 RVA: 0x0001F1F4 File Offset: 0x0001D3F4
	public void OnPointerClick(PointerEventData eventData)
	{
		Action action = this.action;
		if (action != null)
		{
			action();
		}
		if (eventData.button == PointerEventData.InputButton.Right)
		{
			UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
			if (ui == null)
			{
				return;
			}
			ui.ShowEquipDetail(true, base.transform.position, this.equipBase);
		}
	}

	// Token: 0x04000493 RID: 1171
	public EquipBase equipBase;

	// Token: 0x04000494 RID: 1172
	public Action action;
}
