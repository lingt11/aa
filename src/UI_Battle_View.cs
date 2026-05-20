using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200030A RID: 778
public class UI_Battle_View : UGUIView
{
	// Token: 0x0600120A RID: 4618 RVA: 0x0006AEE0 File Offset: 0x000690E0
	public override void Init(Transform trans)
	{
		this.trans_EnemyEnterTip = trans.GetChild(0).GetComponent<Transform>();
		this.img_bossHead = trans.GetChild(0).GetChild(0).GetChild(3).GetComponent<Image>();
		this.ltext_bossTip = trans.GetChild(0).GetChild(0).GetChild(4).GetComponent<Text>();
		this.trans_pickUI = trans.GetChild(1).GetComponent<Transform>();
		this.ltext_pickItem = trans.GetChild(1).GetChild(0).GetChild(2).GetChild(0).GetChild(1).GetComponent<Text>();
		this.trans_keyPick = trans.GetChild(1).GetChild(0).GetChild(3).GetComponent<Transform>();
		this.trans_damageParent = trans.GetChild(2).GetComponent<Transform>();
		this.trans_damagePrefab = trans.GetChild(2).GetChild(0).GetComponent<Transform>();
		this.trans_teamUI = trans.GetChild(3).GetComponent<Transform>();
		this.trans_TeamHead = trans.GetChild(3).GetChild(4).GetChild(0).GetComponent<Transform>();
		this.trans_pickItemParent = trans.GetChild(4).GetComponent<Transform>();
		this.trans_pickItemPrefab = trans.GetChild(4).GetChild(0).GetComponent<Transform>();
		this.trans_YaliBar = trans.GetChild(5).GetComponent<Transform>();
		this.img_bg = trans.GetChild(5).GetChild(1).GetComponent<Image>();
		this.img_heart_bg = trans.GetChild(5).GetChild(2).GetChild(0).GetComponent<Image>();
		this.text_yalizhi_bg = trans.GetChild(5).GetChild(2).GetChild(1).GetComponent<TMP_Text>();
		this.img_yali = trans.GetChild(5).GetChild(2).GetChild(2).GetComponent<Image>();
		this.img_heart = trans.GetChild(5).GetChild(2).GetChild(2).GetChild(0).GetComponent<Image>();
		this.text_yalizhi = trans.GetChild(5).GetChild(2).GetChild(2).GetChild(1).GetComponent<TMP_Text>();
		this.ltext_enemyTipText = trans.GetChild(5).GetChild(3).GetComponent<Text>();
		this.text_time = trans.GetChild(6).GetComponent<TMP_Text>();
		this.trans_wavebg = trans.GetChild(8).GetComponent<Transform>();
		this.ltext_waveTip = trans.GetChild(8).GetChild(0).GetComponent<Text>();
		this.trans_eliteImg = trans.GetChild(8).GetChild(1).GetComponent<Transform>();
		this.trans_bossImg = trans.GetChild(8).GetChild(2).GetComponent<Transform>();
		this.btn_startGame = trans.GetChild(8).GetChild(3).GetComponent<Button>();
		this.ltext_startgame = trans.GetChild(8).GetChild(3).GetChild(0).GetChild(0).GetComponent<Text>();
		this.trans_readyTip = trans.GetChild(8).GetChild(3).GetChild(1).GetComponent<Transform>();
		this.trans_deadGo = trans.GetChild(9).GetComponent<Transform>();
		this.text_deadTime = trans.GetChild(9).GetChild(1).GetComponent<TMP_Text>();
		this.trans_deadmask = trans.GetChild(9).GetChild(2).GetComponent<Transform>();
		this.trans_countDown = trans.GetChild(10).GetComponent<Transform>();
		this.text_countDownTime = trans.GetChild(10).GetChild(0).GetComponent<TMP_Text>();
		this.img_normalMask = trans.GetChild(11).GetComponent<Image>();
	}

	// Token: 0x04001019 RID: 4121
	public Transform trans_EnemyEnterTip;

	// Token: 0x0400101A RID: 4122
	public Image img_bossHead;

	// Token: 0x0400101B RID: 4123
	public Text ltext_bossTip;

	// Token: 0x0400101C RID: 4124
	public Transform trans_pickUI;

	// Token: 0x0400101D RID: 4125
	public Text ltext_pickItem;

	// Token: 0x0400101E RID: 4126
	public Transform trans_keyPick;

	// Token: 0x0400101F RID: 4127
	public Transform trans_damageParent;

	// Token: 0x04001020 RID: 4128
	public Transform trans_damagePrefab;

	// Token: 0x04001021 RID: 4129
	public Transform trans_teamUI;

	// Token: 0x04001022 RID: 4130
	public Transform trans_TeamHead;

	// Token: 0x04001023 RID: 4131
	public Transform trans_pickItemParent;

	// Token: 0x04001024 RID: 4132
	public Transform trans_pickItemPrefab;

	// Token: 0x04001025 RID: 4133
	public Transform trans_YaliBar;

	// Token: 0x04001026 RID: 4134
	public Image img_bg;

	// Token: 0x04001027 RID: 4135
	public Image img_heart_bg;

	// Token: 0x04001028 RID: 4136
	public TMP_Text text_yalizhi_bg;

	// Token: 0x04001029 RID: 4137
	public Image img_yali;

	// Token: 0x0400102A RID: 4138
	public Image img_heart;

	// Token: 0x0400102B RID: 4139
	public TMP_Text text_yalizhi;

	// Token: 0x0400102C RID: 4140
	public Text ltext_enemyTipText;

	// Token: 0x0400102D RID: 4141
	public TMP_Text text_time;

	// Token: 0x0400102E RID: 4142
	public Transform trans_wavebg;

	// Token: 0x0400102F RID: 4143
	public Text ltext_waveTip;

	// Token: 0x04001030 RID: 4144
	public Transform trans_eliteImg;

	// Token: 0x04001031 RID: 4145
	public Transform trans_bossImg;

	// Token: 0x04001032 RID: 4146
	public Button btn_startGame;

	// Token: 0x04001033 RID: 4147
	public Text ltext_startgame;

	// Token: 0x04001034 RID: 4148
	public Transform trans_readyTip;

	// Token: 0x04001035 RID: 4149
	public Transform trans_deadGo;

	// Token: 0x04001036 RID: 4150
	public TMP_Text text_deadTime;

	// Token: 0x04001037 RID: 4151
	public Transform trans_deadmask;

	// Token: 0x04001038 RID: 4152
	public Transform trans_countDown;

	// Token: 0x04001039 RID: 4153
	public TMP_Text text_countDownTime;

	// Token: 0x0400103A RID: 4154
	public Image img_normalMask;
}
