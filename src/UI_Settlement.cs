using System;
using UnityEngine;

// Token: 0x020003A0 RID: 928
public class UI_Settlement : UGUICtrl
{
	// Token: 0x06001534 RID: 5428 RVA: 0x000831E7 File Offset: 0x000813E7
	public UI_Settlement()
	{
		this.selfView = new UI_Settlement_View();
		base.OnCreate(this.selfView, "UI/Prefabs/ui_settlement", base.GetType());
	}

	// Token: 0x06001535 RID: 5429 RVA: 0x00083211 File Offset: 0x00081411
	protected override void ButtonAddClick()
	{
		this.selfView.btn_back.AddButtonEvent(delegate
		{
			GameHelperClient.OnGameReset();
		});
	}

	// Token: 0x06001536 RID: 5430 RVA: 0x00083242 File Offset: 0x00081442
	protected override void ClosePanel()
	{
		MySystemEvent.Instance.UnregisterMessage(1, new Action<Body>(this.JoyA));
	}

	// Token: 0x06001537 RID: 5431 RVA: 0x0008325C File Offset: 0x0008145C
	protected override void OpenPanel(object data)
	{
		base.OpenPanel(data);
		if (data.ToString().Equals("win"))
		{
			this.selfView.ltext_info.text = Game.Language.Get("tip_win", "");
		}
		else
		{
			this.selfView.ltext_info.text = Game.Language.Get("tip_failed", "");
		}
		this.selfView.pool_player.RemoveAllView();
		int playerNum = GameHelperClient.PlayerNum;
		for (int i = 0; i < playerNum; i++)
		{
			RoleBase roleBase = Game.PlayerManagerClient.clientPlayerList[i];
			if (!(roleBase == null))
			{
				PlayerBase playerBase = roleBase as PlayerBase;
				if (playerBase != null)
				{
					if (GameHelperClient.IsExitGameOver)
					{
						if (roleBase != GameHelperClient.localPlayer)
						{
							goto IL_44E;
						}
						if (GameHelperClient.WaveNum == 0)
						{
							GameHelperClient.WaveNum = -1;
							playerBase.getGoldNum = 0;
							playerBase.getGemNum = 0;
						}
					}
					SettlementView component = this.selfView.pool_player.AddView().GetComponent<SettlementView>();
					component.name.text = roleBase.roleName;
					component.killNum.text = playerBase.killEnemyNum.ToString();
					component.damageText.text = playerBase.damageStatic.ToString();
					component.getGold.text = playerBase.getGoldNum.ToString();
					component.getGem.text = playerBase.getGemNum.ToString();
					component.dieText.text = playerBase.dieNum.ToString();
					component.bossText.text = playerBase.killBossNum.ToString();
					int num = (GameHelperClient.isWin ? 1000 : 0) + (GameHelperClient.WaveNum + 1) * 100 + Mathf.RoundToInt((float)playerBase.killEnemyNum * 0.2f) + playerBase.killBossNum * 100 + Mathf.RoundToInt((float)playerBase.getGoldNum * 0.005f) + playerBase.getGemNum * 5;
					component.score.text = num.ToString();
					if (roleBase == GameHelperClient.localPlayer)
					{
						string text = Game.Language.Get("评分", "") + StringDefine.ColonSpace;
						text += string.Format("({0}){1} + ", Game.Language.Get("通关", ""), GameHelperClient.isWin ? 1000 : 0);
						text += string.Format("({0}){1} * 100 + ", Game.Language.Get("波次", ""), GameHelperClient.WaveNum + 1);
						text += string.Format("({0}){1} * 20% + ", Game.Language.Get("击杀数量", ""), playerBase.killEnemyNum);
						text += string.Format("({0}){1} * 100 + ", Game.Language.Get("击杀BOSS", ""), playerBase.killBossNum);
						text += string.Format("({0}){1} * 0.5% + ", Game.Language.Get("金钱", ""), playerBase.getGoldNum);
						text += string.Format("({0}){1} * 5 = {2}", Game.Language.Get("骷髅币", ""), playerBase.getGemNum, num);
						int num2 = Mathf.RoundToInt((float)num * (1f + (float)(GameHelperClient.PlayerNum - 1) * 0.2f));
						text = string.Concat(new string[]
						{
							text,
							"\n",
							Game.Language.Get("get", ""),
							Game.Language.Get("记忆", ""),
							StringDefine.ColonSpace
						});
						text += string.Format("{0} * (1 + {1}:{2} * 20%) = {3}", new object[]
						{
							Game.Language.Get("评分", ""),
							Game.Language.Get("组队人数", ""),
							GameHelperClient.PlayerNum - 1,
							num2
						});
						this.selfView.ltext_result.text = text;
						if (GameHelperClient.isWin)
						{
							SaveLoadManager.OnCompLevel();
						}
						SaveLoadManager.SaveJiYi((long)num2);
					}
				}
			}
			IL_44E:;
		}
		MySystemEvent.Instance.RegisterMessage(1, new Action<Body>(this.JoyA));
	}

	// Token: 0x06001538 RID: 5432 RVA: 0x000836D9 File Offset: 0x000818D9
	private void JoyA(Body body)
	{
		this.selfView.btn_back.onClick.Invoke();
	}

	// Token: 0x040013E6 RID: 5094
	public UI_Settlement_View selfView;
}
