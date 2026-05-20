using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000339 RID: 825
public class UI_IllustratedGuide_View : UGUIView
{
	// Token: 0x060012E3 RID: 4835 RVA: 0x00070974 File Offset: 0x0006EB74
	public override void Init(Transform trans)
	{
		this.btn_hero = trans.GetChild(1).GetChild(0).GetChild(0).GetComponent<Button>();
		this.btn_skill = trans.GetChild(1).GetChild(0).GetChild(1).GetComponent<Button>();
		this.btn_relic = trans.GetChild(1).GetChild(0).GetChild(2).GetComponent<Button>();
		this.btn_equip = trans.GetChild(1).GetChild(0).GetChild(3).GetComponent<Button>();
		this.btn_monster = trans.GetChild(1).GetChild(0).GetChild(4).GetComponent<Button>();
		this.btn_workshopMod = trans.GetChild(1).GetChild(0).GetChild(5).GetComponent<Button>();
		this.btn_gamedec = trans.GetChild(1).GetChild(0).GetChild(6).GetComponent<Button>();
		this.btn_quit = trans.GetChild(1).GetChild(0).GetChild(7).GetComponent<Button>();
	}

	// Token: 0x0400112A RID: 4394
	public Button btn_hero;

	// Token: 0x0400112B RID: 4395
	public Button btn_skill;

	// Token: 0x0400112C RID: 4396
	public Button btn_relic;

	// Token: 0x0400112D RID: 4397
	public Button btn_equip;

	// Token: 0x0400112E RID: 4398
	public Button btn_monster;

	// Token: 0x0400112F RID: 4399
	public Button btn_workshopMod;

	// Token: 0x04001130 RID: 4400
	public Button btn_gamedec;

	// Token: 0x04001131 RID: 4401
	public Button btn_quit;
}
