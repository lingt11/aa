using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000353 RID: 851
public class UI_MyCard_View : UGUIView
{
	// Token: 0x06001389 RID: 5001 RVA: 0x00078BA0 File Offset: 0x00076DA0
	public override void Init(Transform trans)
	{
		this.ltext_jiyi = trans.GetChild(2).GetChild(1).GetComponent<Text>();
		this.pool_cangku = trans.GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetComponent<PoolView>();
		this.trans_EquipCard = trans.GetChild(3).GetChild(1).GetComponent<Transform>();
		this.pool_equip = trans.GetChild(3).GetChild(1).GetChild(0).GetChild(0).GetComponent<PoolView>();
		this.btn_back = trans.GetChild(4).GetChild(0).GetComponent<Button>();
		this.trans_equipInfo = trans.GetChild(5).GetComponent<Transform>();
		this.trans_tipInfo = trans.GetChild(6).GetComponent<Transform>();
		this.ltext_tipTitle = trans.GetChild(6).GetChild(0).GetComponent<Text>();
		this.ltext_tipInfo = trans.GetChild(6).GetChild(1).GetComponent<Text>();
		this.trans_allDropdown = trans.GetChild(7).GetComponent<Transform>();
		this.trans_listDropdown = trans.GetChild(8).GetComponent<Transform>();
		this.trans_saveCardPreset = trans.GetChild(9).GetComponent<Transform>();
		this.trans_loadCardPreset = trans.GetChild(10).GetComponent<Transform>();
		this.trans_makeCard = trans.GetChild(11).GetComponent<Transform>();
		this.trans_cardMake = trans.GetChild(12).GetComponent<Transform>();
		this.trans_cardMakeeIInfo = trans.GetChild(12).GetChild(0).GetComponent<Transform>();
		this.btn_make = trans.GetChild(12).GetChild(1).GetComponent<Button>();
		this.btn_cancel = trans.GetChild(12).GetChild(2).GetComponent<Button>();
		this.trans_inputNum = trans.GetChild(12).GetChild(3).GetChild(0).GetComponent<Transform>();
		this.btn_addNum = trans.GetChild(12).GetChild(3).GetChild(1).GetComponent<Button>();
		this.btn_redNum = trans.GetChild(12).GetChild(3).GetChild(2).GetComponent<Button>();
		this.ltext_makeDec = trans.GetChild(12).GetChild(4).GetComponent<Text>();
		this.ltext_UseDec = trans.GetChild(12).GetChild(5).GetComponent<Text>();
		this.ltext_makeUseNum = trans.GetChild(12).GetChild(5).GetChild(1).GetComponent<Text>();
		this.btn_addRoom = trans.GetChild(13).GetChild(0).GetComponent<Button>();
		this.ltext_upLevel = trans.GetChild(13).GetChild(0).GetChild(0).GetComponent<Text>();
		this.ltext_gold = trans.GetChild(13).GetChild(3).GetComponent<Text>();
		this.ltext_dust = trans.GetChild(13).GetChild(4).GetComponent<Text>();
	}

	// Token: 0x04001203 RID: 4611
	public Text ltext_jiyi;

	// Token: 0x04001204 RID: 4612
	public PoolView pool_cangku;

	// Token: 0x04001205 RID: 4613
	public Transform trans_EquipCard;

	// Token: 0x04001206 RID: 4614
	public PoolView pool_equip;

	// Token: 0x04001207 RID: 4615
	public Button btn_back;

	// Token: 0x04001208 RID: 4616
	public Transform trans_equipInfo;

	// Token: 0x04001209 RID: 4617
	public Transform trans_tipInfo;

	// Token: 0x0400120A RID: 4618
	public Text ltext_tipTitle;

	// Token: 0x0400120B RID: 4619
	public Text ltext_tipInfo;

	// Token: 0x0400120C RID: 4620
	public Transform trans_allDropdown;

	// Token: 0x0400120D RID: 4621
	public Transform trans_listDropdown;

	// Token: 0x0400120E RID: 4622
	public Transform trans_saveCardPreset;

	// Token: 0x0400120F RID: 4623
	public Transform trans_loadCardPreset;

	// Token: 0x04001210 RID: 4624
	public Transform trans_makeCard;

	// Token: 0x04001211 RID: 4625
	public Transform trans_cardMake;

	// Token: 0x04001212 RID: 4626
	public Transform trans_cardMakeeIInfo;

	// Token: 0x04001213 RID: 4627
	public Button btn_make;

	// Token: 0x04001214 RID: 4628
	public Button btn_cancel;

	// Token: 0x04001215 RID: 4629
	public Transform trans_inputNum;

	// Token: 0x04001216 RID: 4630
	public Button btn_addNum;

	// Token: 0x04001217 RID: 4631
	public Button btn_redNum;

	// Token: 0x04001218 RID: 4632
	public Text ltext_makeDec;

	// Token: 0x04001219 RID: 4633
	public Text ltext_UseDec;

	// Token: 0x0400121A RID: 4634
	public Text ltext_makeUseNum;

	// Token: 0x0400121B RID: 4635
	public Button btn_addRoom;

	// Token: 0x0400121C RID: 4636
	public Text ltext_upLevel;

	// Token: 0x0400121D RID: 4637
	public Text ltext_gold;

	// Token: 0x0400121E RID: 4638
	public Text ltext_dust;
}
