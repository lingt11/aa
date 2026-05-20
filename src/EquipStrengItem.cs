using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020003A3 RID: 931
public class EquipStrengItem : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x0600153E RID: 5438 RVA: 0x00083787 File Offset: 0x00081987
	private void Awake()
	{
		this.button.AddButtonEvent(new UnityAction(this.OnBtnClick));
	}

	// Token: 0x0600153F RID: 5439 RVA: 0x000837A0 File Offset: 0x000819A0
	private void OnBtnClick()
	{
		MySystemEvent.Instance.DispatchMessage<EquipBase>(43, this.equipBase);
	}

	// Token: 0x06001540 RID: 5440 RVA: 0x000837B4 File Offset: 0x000819B4
	public void UpdateUI(EquipBase equipBaseValue)
	{
		if (!this.image.gameObject.activeSelf)
		{
			this.image.gameObject.SetActive(true);
		}
		this.equipBase = equipBaseValue;
		this.image.sprite = Resources.Load<Sprite>("Bundles/UI/Icon/Shop/" + this.equipBase.iconName);
	}

	// Token: 0x06001541 RID: 5441 RVA: 0x00083810 File Offset: 0x00081A10
	public void HideSprite()
	{
		if (this.image.gameObject.activeSelf)
		{
			this.image.gameObject.SetActive(false);
		}
		this.equipBase = null;
	}

	// Token: 0x06001542 RID: 5442 RVA: 0x0006898B File Offset: 0x00066B8B
	public void Hide()
	{
		if (base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001543 RID: 5443 RVA: 0x0008383C File Offset: 0x00081A3C
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (this.equipBase != null)
		{
			UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
			if (ui == null)
			{
				return;
			}
			ui.ShowEquipInfo(true, base.transform.position, this.equipBase);
		}
	}

	// Token: 0x06001544 RID: 5444 RVA: 0x0008386C File Offset: 0x00081A6C
	public void OnPointerExit(PointerEventData eventData)
	{
		UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
		if (ui == null)
		{
			return;
		}
		ui.ShowEquipInfo(false, base.transform.position, this.equipBase);
	}

	// Token: 0x040013ED RID: 5101
	public Button button;

	// Token: 0x040013EE RID: 5102
	public Image image;

	// Token: 0x040013EF RID: 5103
	private EquipBase equipBase;
}
