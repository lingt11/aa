using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003BA RID: 954
public class UI_StartGame_View : UGUIView
{
	// Token: 0x060015DA RID: 5594 RVA: 0x00087A80 File Offset: 0x00085C80
	public override void Init(Transform trans)
	{
		this.trans_zhangjie = trans.GetChild(0).GetComponent<Transform>();
		this.btn_z1 = trans.GetChild(0).GetChild(0).GetComponent<Button>();
		this.btn_z2 = trans.GetChild(0).GetChild(1).GetComponent<Button>();
		this.btn_z3 = trans.GetChild(0).GetChild(2).GetComponent<Button>();
		this.trans_bg = trans.GetChild(1).GetComponent<Transform>();
		this.trans_main = trans.GetChild(1).GetChild(0).GetComponent<Transform>();
		this.btn_bendi = trans.GetChild(1).GetChild(0).GetChild(0).GetComponent<Button>();
		this.btn_steam = trans.GetChild(1).GetChild(0).GetChild(1).GetComponent<Button>();
		this.btn_rank = trans.GetChild(1).GetChild(0).GetChild(2).GetComponent<Button>();
		this.btn_guide = trans.GetChild(1).GetChild(0).GetChild(3).GetComponent<Button>();
		this.btn_set = trans.GetChild(1).GetChild(0).GetChild(4).GetComponent<Button>();
		this.btn_quit = trans.GetChild(1).GetChild(0).GetChild(5).GetComponent<Button>();
		this.trans_bendi = trans.GetChild(1).GetChild(1).GetComponent<Transform>();
		this.btn_host = trans.GetChild(1).GetChild(1).GetChild(0).GetComponent<Button>();
		this.btn_join = trans.GetChild(1).GetChild(1).GetChild(1).GetComponent<Button>();
		this.btn_back1 = trans.GetChild(1).GetChild(1).GetChild(2).GetComponent<Button>();
		this.trans_steam = trans.GetChild(1).GetChild(2).GetComponent<Transform>();
		this.btn_hostSteam = trans.GetChild(1).GetChild(2).GetChild(0).GetComponent<Button>();
		this.btn_joinSteam = trans.GetChild(1).GetChild(2).GetChild(1).GetComponent<Button>();
		this.btn_back2 = trans.GetChild(1).GetChild(2).GetChild(2).GetComponent<Button>();
		this.btn_selectHero = trans.GetChild(2).GetComponent<Button>();
		this.trans_wait = trans.GetChild(3).GetComponent<Transform>();
		this.ltext_lianjiTip = trans.GetChild(3).GetChild(1).GetComponent<Text>();
		this.btn_steam2 = trans.GetChild(4).GetComponent<Button>();
		this.trans_CNShow = trans.GetChild(5).GetComponent<Transform>();
		this.btn_unLock = trans.GetChild(5).GetChild(1).GetComponent<Button>();
		this.btn_addQQ = trans.GetChild(5).GetChild(2).GetComponent<Button>();
		this.btn_backMenu = trans.GetChild(6).GetComponent<Button>();
		this.trans_serverList = trans.GetChild(7).GetComponent<Transform>();
		this.btn_refresh = trans.GetChild(7).GetChild(1).GetComponent<Button>();
		this.trans_scroll = trans.GetChild(7).GetChild(2).GetComponent<Transform>();
		this.pool_serverList = trans.GetChild(7).GetChild(2).GetChild(0).GetChild(0).GetComponent<PoolView>();
		this.trans_createRoom = trans.GetChild(8).GetComponent<Transform>();
		this.btn_confirm = trans.GetChild(8).GetChild(0).GetChild(1).GetChild(1).GetComponent<Button>();
		this.btn_cancel = trans.GetChild(8).GetChild(0).GetChild(1).GetChild(2).GetComponent<Button>();
		this.trans_Toggle_0 = trans.GetChild(8).GetChild(0).GetChild(1).GetChild(3).GetChild(0).GetComponent<Transform>();
		this.trans_Toggle_1 = trans.GetChild(8).GetChild(0).GetChild(1).GetChild(3).GetChild(1).GetComponent<Transform>();
		this.trans_Toggle_2 = trans.GetChild(8).GetChild(0).GetChild(1).GetChild(3).GetChild(2).GetComponent<Transform>();
		this.trans_inputMessage = trans.GetChild(8).GetChild(0).GetChild(1).GetChild(4).GetComponent<Transform>();
		this.ltext_password = trans.GetChild(8).GetChild(0).GetChild(1).GetChild(4).GetChild(1).GetComponent<Text>();
	}

	// Token: 0x04001475 RID: 5237
	public Transform trans_zhangjie;

	// Token: 0x04001476 RID: 5238
	public Button btn_z1;

	// Token: 0x04001477 RID: 5239
	public Button btn_z2;

	// Token: 0x04001478 RID: 5240
	public Button btn_z3;

	// Token: 0x04001479 RID: 5241
	public Transform trans_bg;

	// Token: 0x0400147A RID: 5242
	public Transform trans_main;

	// Token: 0x0400147B RID: 5243
	public Button btn_bendi;

	// Token: 0x0400147C RID: 5244
	public Button btn_steam;

	// Token: 0x0400147D RID: 5245
	public Button btn_rank;

	// Token: 0x0400147E RID: 5246
	public Button btn_guide;

	// Token: 0x0400147F RID: 5247
	public Button btn_set;

	// Token: 0x04001480 RID: 5248
	public Button btn_quit;

	// Token: 0x04001481 RID: 5249
	public Transform trans_bendi;

	// Token: 0x04001482 RID: 5250
	public Button btn_host;

	// Token: 0x04001483 RID: 5251
	public Button btn_join;

	// Token: 0x04001484 RID: 5252
	public Button btn_back1;

	// Token: 0x04001485 RID: 5253
	public Transform trans_steam;

	// Token: 0x04001486 RID: 5254
	public Button btn_hostSteam;

	// Token: 0x04001487 RID: 5255
	public Button btn_joinSteam;

	// Token: 0x04001488 RID: 5256
	public Button btn_back2;

	// Token: 0x04001489 RID: 5257
	public Button btn_selectHero;

	// Token: 0x0400148A RID: 5258
	public Transform trans_wait;

	// Token: 0x0400148B RID: 5259
	public Text ltext_lianjiTip;

	// Token: 0x0400148C RID: 5260
	public Button btn_steam2;

	// Token: 0x0400148D RID: 5261
	public Transform trans_CNShow;

	// Token: 0x0400148E RID: 5262
	public Button btn_unLock;

	// Token: 0x0400148F RID: 5263
	public Button btn_addQQ;

	// Token: 0x04001490 RID: 5264
	public Button btn_backMenu;

	// Token: 0x04001491 RID: 5265
	public Transform trans_serverList;

	// Token: 0x04001492 RID: 5266
	public Button btn_refresh;

	// Token: 0x04001493 RID: 5267
	public Transform trans_scroll;

	// Token: 0x04001494 RID: 5268
	public PoolView pool_serverList;

	// Token: 0x04001495 RID: 5269
	public Transform trans_createRoom;

	// Token: 0x04001496 RID: 5270
	public Button btn_confirm;

	// Token: 0x04001497 RID: 5271
	public Button btn_cancel;

	// Token: 0x04001498 RID: 5272
	public Transform trans_Toggle_0;

	// Token: 0x04001499 RID: 5273
	public Transform trans_Toggle_1;

	// Token: 0x0400149A RID: 5274
	public Transform trans_Toggle_2;

	// Token: 0x0400149B RID: 5275
	public Transform trans_inputMessage;

	// Token: 0x0400149C RID: 5276
	public Text ltext_password;
}
