using System;
using UnityEngine.Events;

// Token: 0x0200031D RID: 797
public class UI_EquipStreng : UGUICtrl
{
	// Token: 0x06001263 RID: 4707 RVA: 0x0006DBA0 File Offset: 0x0006BDA0
	public UI_EquipStreng()
	{
		this.selfView = new UI_EquipStreng_View();
		base.OnCreate(this.selfView, "UI/Prefabs/ui_equipStreng", base.GetType());
		this.equipStrengUI = this.selfView.trans_equipStreng.gameObject.GetComponent<EquipStrengUI>();
	}

	// Token: 0x06001264 RID: 4708 RVA: 0x0006DBF0 File Offset: 0x0006BDF0
	protected override void ButtonAddClick()
	{
		this.selfView.btn_close.AddButtonEvent(new UnityAction(this.OnCloseBtnClick));
	}

	// Token: 0x06001265 RID: 4709 RVA: 0x0006DC10 File Offset: 0x0006BE10
	private void OnCloseBtnClick()
	{
		if (this.equipStrengUI.StrengItemType != ItemType.None)
		{
			(Game.UI.OpenUI<UI_Confirm>(null) as UI_Confirm).SetConfirmText(Game.Language.Get("强化退出提示", ""), new Action(this.CloseCreateItem), null, null, "");
			return;
		}
		base.CloseSelfPanel();
	}

	// Token: 0x06001266 RID: 4710 RVA: 0x0006DC6E File Offset: 0x0006BE6E
	private void CloseCreateItem()
	{
		GameHelperClient.localPlayer.CmdCreateItemByPos(this.equipStrengUI.StrengItemType, GameHelperClient.localPlayer.MyTransform.position);
		base.CloseSelfPanel();
	}

	// Token: 0x06001267 RID: 4711 RVA: 0x0006DC9C File Offset: 0x0006BE9C
	protected override void OpenPanel(object data)
	{
		base.OpenPanel(data);
		EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/强化装备界面", 1f, 3f);
		UI_Shop ui = Game.UI.GetUI<UI_Shop>();
		if (ui != null)
		{
			this.selfView.transform.SetSiblingIndex(ui.selfView.transform.GetSiblingIndex());
		}
	}

	// Token: 0x06001268 RID: 4712 RVA: 0x0006DCF8 File Offset: 0x0006BEF8
	public void SetStrengItemType(ItemType itemType)
	{
		if (itemType == ItemType.None)
		{
			this.selfView.ltext_title.text = Game.Language.Get("强化装备", "");
		}
		else
		{
			this.selfView.ltext_title.text = Util.GetItemName(itemType);
		}
		this.equipStrengUI.SetStrengItemType(itemType);
	}

	// Token: 0x040010A6 RID: 4262
	public UI_EquipStreng_View selfView;

	// Token: 0x040010A7 RID: 4263
	private EquipStrengUI equipStrengUI;
}
