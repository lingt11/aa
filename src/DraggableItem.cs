using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000348 RID: 840
public class DraggableItem : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	// Token: 0x06001320 RID: 4896 RVA: 0x00002D1D File Offset: 0x00000F1D
	public void OnBeginDrag(PointerEventData eventData)
	{
	}

	// Token: 0x06001321 RID: 4897 RVA: 0x00002D1D File Offset: 0x00000F1D
	public void OnDrag(PointerEventData eventData)
	{
	}

	// Token: 0x06001322 RID: 4898 RVA: 0x00073BEC File Offset: 0x00071DEC
	public void OnEndDrag(PointerEventData eventData)
	{
	}

	// Token: 0x06001323 RID: 4899 RVA: 0x00073BF9 File Offset: 0x00071DF9
	public void OnPointerEnter(PointerEventData eventData)
	{
		UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
		if (ui == null)
		{
			return;
		}
		ui.ShowBagItemInfo(true, base.transform.position, this.bagItem.id, false, this.bagItem.bagItemType);
	}

	// Token: 0x06001324 RID: 4900 RVA: 0x00073C32 File Offset: 0x00071E32
	public void OnPointerExit(PointerEventData eventData)
	{
		UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
		if (ui == null)
		{
			return;
		}
		ui.ShowBagItemInfo(false, base.transform.position, this.bagItem.id, false, this.bagItem.bagItemType);
	}

	// Token: 0x06001325 RID: 4901 RVA: 0x00073C6C File Offset: 0x00071E6C
	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Right)
		{
			this.ButtonRight();
			return;
		}
		if (eventData.button == PointerEventData.InputButton.Left)
		{
			UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
			if (ui != null)
			{
				ui.ShowBagItemInfo(false, base.transform.position, this.bagItem.id, false, this.bagItem.bagItemType);
			}
			GameHelperClient.localPlayer.playerAttribute.UseBook(this.bagItem);
			EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/购买药水和书", 1f, 3f);
		}
	}

	// Token: 0x06001326 RID: 4902 RVA: 0x00073CF8 File Offset: 0x00071EF8
	public void ButtonRight()
	{
		UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
		if (ui == null)
		{
			return;
		}
		ui.ShowBagItemDetail(true, base.transform.position, this.dic, this.bagItem);
	}

	// Token: 0x040011B3 RID: 4531
	private Vector3 originalPosition;

	// Token: 0x040011B4 RID: 4532
	private Transform originalParent;

	// Token: 0x040011B5 RID: 4533
	public CanvasGroup canvasGroup;

	// Token: 0x040011B6 RID: 4534
	public Dictionary<string, object> dic = new Dictionary<string, object>();

	// Token: 0x040011B7 RID: 4535
	public BagItem bagItem;
}
