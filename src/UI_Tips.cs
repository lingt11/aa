using System;
using DG.Tweening;
using UnityEngine;

// Token: 0x020003BD RID: 957
public class UI_Tips : UGUICtrl
{
	// Token: 0x060015E2 RID: 5602 RVA: 0x00087F5E File Offset: 0x0008615E
	public UI_Tips()
	{
		this.selfView = new UI_Tips_View();
		base.OnCreate(this.selfView, "UI/Prefabs/ui_tips", base.GetType());
		this.rectBg = this.selfView.trans_bg.GetComponent<RectTransform>();
	}

	// Token: 0x060015E3 RID: 5603 RVA: 0x00002D1D File Offset: 0x00000F1D
	protected override void ButtonAddClick()
	{
	}

	// Token: 0x060015E4 RID: 5604 RVA: 0x00087FA0 File Offset: 0x000861A0
	protected override void OpenPanel(object data)
	{
		base.OpenPanel(data);
		this.selfView.ltext_info.text = data.ToString();
		this.time = 2f;
		UI_Msg ui = Game.UI.GetUI<UI_Msg>();
		if (ui != null)
		{
			ui.ShowMsg(data.ToString(), false);
		}
		this.selfView.trans_bg.localScale = new Vector3(1f, 0f, 1f);
		this.selfView.trans_bg.DOScaleY(1f, 0.2f);
		if (GameHelperClient.isReady)
		{
			this.rectBg.anchoredPosition = new Vector2(0f, -275f);
			return;
		}
		this.rectBg.anchoredPosition = new Vector2(0f, -190f);
	}

	// Token: 0x060015E5 RID: 5605 RVA: 0x0008806C File Offset: 0x0008626C
	public override void Update()
	{
		this.time -= Time.deltaTime;
		if (this.time <= 0f)
		{
			base.CloseSelfPanel();
		}
	}

	// Token: 0x0400149F RID: 5279
	public UI_Tips_View selfView;

	// Token: 0x040014A0 RID: 5280
	private float time;

	// Token: 0x040014A1 RID: 5281
	private RectTransform rectBg;
}
