using System;
using DG.Tweening;
using Mirror;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200030B RID: 779
public class UI_BattleSetting : UGUICtrl
{
	// Token: 0x0600120C RID: 4620 RVA: 0x0006B24F File Offset: 0x0006944F
	public UI_BattleSetting()
	{
		this.selfView = new UI_BattleSetting_View();
		base.OnCreate(this.selfView, "UI/Prefabs/ui_battleSetting", base.GetType());
	}

	// Token: 0x0600120D RID: 4621 RVA: 0x0006B27C File Offset: 0x0006947C
	protected override void ButtonAddClick()
	{
		this.selfView.btn_quit.AddButtonEvent(new UnityAction(this.OnQuitBtnClick));
		this.selfView.btn_return.AddButtonEvent(new UnityAction(this.OnReturnBtnClick));
		this.selfView.btn_set.AddButtonEvent(new UnityAction(this.OnSetBtnClick));
		this.selfView.btn_card.AddButtonEvent(new UnityAction(this.OnCardBtnClick));
		this.selfView.btn_battle.AddButtonEvent(new UnityAction(this.OnBattleBtnClick));
		this.selfView.btn_playerInfo.AddButtonEvent(new UnityAction(this.OnPlayerInfoBtnClick));
	}

	// Token: 0x0600120E RID: 4622 RVA: 0x00018CB6 File Offset: 0x00016EB6
	private void OnPlayerInfoBtnClick()
	{
		(Game.UI.OpenUI<UI_KingDec>(null) as UI_KingDec).SetPlayKingData(Util.GetLocalPlayerKingData());
	}

	// Token: 0x0600120F RID: 4623 RVA: 0x0006B334 File Offset: 0x00069534
	private void OnQuitBtnClick()
	{
		UI_Confirm ui_Confirm = Game.UI.OpenUI<UI_Confirm>(null) as UI_Confirm;
		if (GameHelperClient.WaveNum == 0 && GameHelperClient.isReady)
		{
			ui_Confirm.SetConfirmText(Game.Language.Get("是否返回主菜单", ""), new Action(this.OnQuitCallBack), null, null, "");
			return;
		}
		ui_Confirm.SetConfirmText(Game.Language.Get("结算提示", ""), new Action(this.OnQuitCallBack), null, null, "");
	}

	// Token: 0x06001210 RID: 4624 RVA: 0x0006B3BC File Offset: 0x000695BC
	private void OnQuitCallBack()
	{
		if (GameHelperClient.WaveNum == 0 && GameHelperClient.isReady)
		{
			AnalyticsManager analytics = Game.Analytics;
			if (analytics != null)
			{
				analytics.RecordEarlyResetAtWaveZero();
			}
			GameHelperClient.OnGameReset();
			return;
		}
		AnalyticsManager analytics2 = Game.Analytics;
		if (analytics2 != null)
		{
			analytics2.RecordGiveUpMidRun();
		}
		GameHelperClient.isWin = false;
		GameHelperClient.IsExitGameOver = true;
		(NetworkManager.singleton as MyServerNetworkManager).StartShowGameResult();
	}

	// Token: 0x06001211 RID: 4625 RVA: 0x0006B418 File Offset: 0x00069618
	private void OnReturnBtnClick()
	{
		this.CloseAnim();
	}

	// Token: 0x06001212 RID: 4626 RVA: 0x0006B420 File Offset: 0x00069620
	private void OnSetBtnClick()
	{
		Game.UI.OpenUI<UI_SelectLanguage>(null);
	}

	// Token: 0x06001213 RID: 4627 RVA: 0x0002B0F5 File Offset: 0x000292F5
	private void OnCardBtnClick()
	{
		Game.UI.OpenUI<UI_MyCard>(null);
	}

	// Token: 0x06001214 RID: 4628 RVA: 0x0006B42E File Offset: 0x0006962E
	private void OnBattleBtnClick()
	{
		UI_SelectLanguage ui_SelectLanguage = Game.UI.OpenUI<UI_SelectLanguage>(null) as UI_SelectLanguage;
		if (ui_SelectLanguage == null)
		{
			return;
		}
		ui_SelectLanguage.OnBattleClick();
	}

	// Token: 0x06001215 RID: 4629 RVA: 0x0006B44A File Offset: 0x0006964A
	protected override void OpenPanel(object data)
	{
		base.OpenPanel(data);
		this.OpenAnim();
	}

	// Token: 0x06001216 RID: 4630 RVA: 0x0006B45C File Offset: 0x0006965C
	public void OpenAnim()
	{
		if (!this.selfView.trans_bg.gameObject.activeSelf)
		{
			this.selfView.trans_bg.gameObject.SetActive(true);
		}
		this.isOpenSetting = true;
		this.selfView.trans_bg.GetComponent<RectTransform>().anchoredPosition = new Vector2(-500f, 0f);
		this.selfView.trans_bg.GetComponent<RectTransform>().DOAnchorPosX(0f, 0.2f, false);
		if (this.timer != null)
		{
			Game.TimerManager.CancelTimer(this.timer);
		}
	}

	// Token: 0x06001217 RID: 4631 RVA: 0x0006B4FC File Offset: 0x000696FC
	public void CloseAnim()
	{
		this.selfView.trans_bg.GetComponent<RectTransform>().DOAnchorPosX(-500f, 0.2f, false);
		this.isOpenSetting = false;
		Game.UI.CloseUI<UI_SelectLanguage>();
		this.timer = Game.TimerManager.AddTimer(0.2f, new Action(this.OnCloseEnd));
	}

	// Token: 0x06001218 RID: 4632 RVA: 0x0006B55C File Offset: 0x0006975C
	private void OnCloseEnd()
	{
		if (this.selfView.trans_bg.gameObject.activeSelf)
		{
			this.selfView.trans_bg.gameObject.SetActive(false);
		}
	}

	// Token: 0x0400103B RID: 4155
	public UI_BattleSetting_View selfView;

	// Token: 0x0400103C RID: 4156
	public bool isOpenSetting;

	// Token: 0x0400103D RID: 4157
	private Timer timer;
}
