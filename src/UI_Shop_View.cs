using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003AC RID: 940
public class UI_Shop_View : UGUIView
{
	// Token: 0x0600158C RID: 5516 RVA: 0x00086390 File Offset: 0x00084590
	public override void Init(Transform trans)
	{
		this.btn_shop = trans.GetChild(0).GetComponent<Button>();
		this.trans_bg = trans.GetChild(1).GetComponent<Transform>();
		this.btn_book = trans.GetChild(1).GetChild(1).GetComponent<Button>();
		this.btn_item = trans.GetChild(1).GetChild(2).GetComponent<Button>();
		this.btn_medicine = trans.GetChild(1).GetChild(3).GetComponent<Button>();
		this.trans_item = trans.GetChild(1).GetChild(4).GetComponent<Transform>();
		this.trans_sbg = trans.GetChild(1).GetChild(4).GetChild(0).GetComponent<Transform>();
		this.trans_scroll = trans.GetChild(1).GetChild(4).GetChild(1).GetComponent<Transform>();
		this.pool_buyItem = trans.GetChild(1).GetChild(4).GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<PoolView>();
		this.trans_info = trans.GetChild(1).GetChild(5).GetComponent<Transform>();
		this.ltext_info = trans.GetChild(1).GetChild(5).GetChild(0).GetChild(0).GetComponent<Text>();
		this.trans_buyItem = trans.GetChild(1).GetChild(5).GetChild(0).GetChild(1).GetComponent<Transform>();
		this.btn_buy = trans.GetChild(1).GetChild(5).GetChild(1).GetComponent<Button>();
		this.text_refresh = trans.GetChild(1).GetChild(6).GetChild(1).GetComponent<TMP_Text>();
		this.btn_close = trans.GetChild(1).GetChild(7).GetComponent<Button>();
		this.trans_ItemDetail = trans.GetChild(2).GetComponent<Transform>();
	}

	// Token: 0x04001442 RID: 5186
	public Button btn_shop;

	// Token: 0x04001443 RID: 5187
	public Transform trans_bg;

	// Token: 0x04001444 RID: 5188
	public Button btn_book;

	// Token: 0x04001445 RID: 5189
	public Button btn_item;

	// Token: 0x04001446 RID: 5190
	public Button btn_medicine;

	// Token: 0x04001447 RID: 5191
	public Transform trans_item;

	// Token: 0x04001448 RID: 5192
	public Transform trans_sbg;

	// Token: 0x04001449 RID: 5193
	public Transform trans_scroll;

	// Token: 0x0400144A RID: 5194
	public PoolView pool_buyItem;

	// Token: 0x0400144B RID: 5195
	public Transform trans_info;

	// Token: 0x0400144C RID: 5196
	public Text ltext_info;

	// Token: 0x0400144D RID: 5197
	public Transform trans_buyItem;

	// Token: 0x0400144E RID: 5198
	public Button btn_buy;

	// Token: 0x0400144F RID: 5199
	public TMP_Text text_refresh;

	// Token: 0x04001450 RID: 5200
	public Button btn_close;

	// Token: 0x04001451 RID: 5201
	public Transform trans_ItemDetail;
}
