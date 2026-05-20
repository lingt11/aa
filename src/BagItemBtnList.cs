using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000362 RID: 866
public class BagItemBtnList : MonoBehaviour
{
	// Token: 0x060013BF RID: 5055 RVA: 0x00079F24 File Offset: 0x00078124
	private void Awake()
	{
		this.btnSell.AddButtonEvent(new UnityAction(this.Sell));
		this.btnDiscard.AddButtonEvent(new UnityAction(this.Discard));
		this.btnCancel.AddButtonEvent(new UnityAction(this.Cancel));
		this.btnUse.AddButtonEvent(new UnityAction(this.Use));
		this.btnList.Add(this.btnUse);
		this.btnList.Add(this.btnSell);
		this.btnList.Add(this.btnDiscard);
		this.btnList.Add(this.btnCancel);
		this.isShowEquip = false;
	}

	// Token: 0x060013C0 RID: 5056 RVA: 0x00079FD8 File Offset: 0x000781D8
	public Button GetButton(int index)
	{
		return this.btnList[index];
	}

	// Token: 0x060013C1 RID: 5057 RVA: 0x00079FE6 File Offset: 0x000781E6
	private void Use()
	{
		GameHelperClient.localPlayer.playerAttribute.UseBook(this.bagItem);
		EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/购买药水和书", 1f, 3f);
		this.Cancel();
	}

	// Token: 0x060013C2 RID: 5058 RVA: 0x0007A020 File Offset: 0x00078220
	private void Cancel()
	{
		base.gameObject.SetActive(false);
		if (this.isShowEquip)
		{
			UI_PlayerState ui = Game.UI.GetUI<UI_PlayerState>();
			if (ui == null)
			{
				return;
			}
			ui.ShowEquipDetail(false, base.transform.position, this.equipBase);
			return;
		}
		else
		{
			UI_PlayerState ui2 = Game.UI.GetUI<UI_PlayerState>();
			if (ui2 == null)
			{
				return;
			}
			ui2.ShowBagItemDetail(false, base.transform.position, this.dic, this.bagItem);
			return;
		}
	}

	// Token: 0x060013C3 RID: 5059 RVA: 0x0007A094 File Offset: 0x00078294
	private void Discard()
	{
		EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/丢弃物品", 1f, 3f);
		GameHelperClient.localPlayer.playerAttribute.RemoveBook(this.bagItem);
		this.Cancel();
	}

	// Token: 0x060013C4 RID: 5060 RVA: 0x0007A0CC File Offset: 0x000782CC
	private void Sell()
	{
		EntityStatic.Get<AudioManager>().PlayAudio("Audio/Battle_Audio/UI/出售物品", 1f, 3f);
		if (this.isShowEquip)
		{
			GameHelperClient.localPlayer.playerAttribute.SellEquip(this.equipBase, false);
		}
		else
		{
			GameHelperClient.localPlayer.playerAttribute.SellBook(this.bagItem.id, this.bagItem);
		}
		this.Cancel();
	}

	// Token: 0x060013C5 RID: 5061 RVA: 0x0007A13C File Offset: 0x0007833C
	public void SetBagItem(BagItem bagItemValue, Dictionary<string, object> dicValue)
	{
		this.bagItem = bagItemValue;
		this.dic = dicValue;
		this.isShowEquip = false;
		this.btnList[0].gameObject.SetActive(true);
		this.btnList[2].gameObject.SetActive(true);
		this.arrowTransform.anchoredPosition = new Vector2(this.arrowTransform.anchoredPosition.x, 0f);
	}

	// Token: 0x060013C6 RID: 5062 RVA: 0x0007A1B4 File Offset: 0x000783B4
	public void SetEquipItem(EquipBase equipBaseValue)
	{
		this.equipBase = equipBaseValue;
		this.isShowEquip = true;
		this.btnList[0].gameObject.SetActive(false);
		this.btnList[2].gameObject.SetActive(false);
		this.arrowTransform.anchoredPosition = new Vector2(this.arrowTransform.anchoredPosition.x, -26f);
	}

	// Token: 0x04001254 RID: 4692
	public Button btnSell;

	// Token: 0x04001255 RID: 4693
	public Button btnDiscard;

	// Token: 0x04001256 RID: 4694
	public Button btnCancel;

	// Token: 0x04001257 RID: 4695
	public Button btnUse;

	// Token: 0x04001258 RID: 4696
	private Dictionary<string, object> dic;

	// Token: 0x04001259 RID: 4697
	private BagItem bagItem;

	// Token: 0x0400125A RID: 4698
	private EquipBase equipBase;

	// Token: 0x0400125B RID: 4699
	private List<Button> btnList = new List<Button>();

	// Token: 0x0400125C RID: 4700
	private bool isShowEquip;

	// Token: 0x0400125D RID: 4701
	public RectTransform arrowTransform;
}
