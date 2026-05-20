using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000142 RID: 322
public class GamePlayItemManager : IUpdate
{
	// Token: 0x0600061F RID: 1567 RVA: 0x00025474 File Offset: 0x00023674
	public void Update()
	{
		if (GameHelperClient.localPlayer == null)
		{
			return;
		}
		this.isHasGamePlayItem = false;
		Vector3 position = GameHelperClient.localPlayer.MyTransform.position;
		for (int i = GameHelperClient.GamePlayItemDatas.Count - 1; i > -1; i--)
		{
			GamePlayItemData value = GameHelperClient.GamePlayItemDatas.ElementAt(i).Value;
			if (Util.GetV2Distance(position, value.pos) < 2.5f)
			{
				this.isHasGamePlayItem = true;
				this.currentGamePlayItemData = value;
				return;
			}
		}
	}

	// Token: 0x06000620 RID: 1568 RVA: 0x000254F4 File Offset: 0x000236F4
	public void StartAction()
	{
		GamePlayItemType gamePlayItemType = this.currentGamePlayItemData.gamePlayItemType;
		if (gamePlayItemType == GamePlayItemType.Help)
		{
			this.curPlayerActionId = this.currentGamePlayItemData.id;
			GameHelperClient.localPlayer.timer = 3.5f;
			GameHelperClient.localPlayer.SetActionCallBack(new Action(this.OnHelpSuccess), new Action(this.OnHelpFailure));
			GameHelperClient.localPlayer.UpdateRoleState(RoleState.Action);
			(Game.UI.OpenUI<UI_ProgressBar>(null) as UI_ProgressBar).ShowProgress(GameHelperClient.localPlayer.timer, Game.Language.Get("救援中", ""));
			return;
		}
		if (gamePlayItemType != GamePlayItemType.Talk)
		{
			return;
		}
		this.curPlayerActionId = this.currentGamePlayItemData.id;
		Action actionCallback = this.currentGamePlayItemData.actionCallback;
		if (actionCallback == null)
		{
			return;
		}
		actionCallback();
	}

	// Token: 0x06000621 RID: 1569 RVA: 0x000255C0 File Offset: 0x000237C0
	private void OnHelpSuccess()
	{
		if (Game.UI.GetUI<UI_ProgressBar>().IsOpen())
		{
			Game.UI.CloseUI<UI_ProgressBar>();
		}
		GamePlayItemData gamePlayItemData;
		if (GameHelperClient.GamePlayItemDatas.TryGetValue(this.curPlayerActionId, out gamePlayItemData) && gamePlayItemData.targetRole != null && gamePlayItemData.targetRole.IsDead())
		{
			GameHelperClient.localPlayer.CmdHelpNpc(gamePlayItemData.targetRole.netId);
		}
	}

	// Token: 0x06000622 RID: 1570 RVA: 0x0002562C File Offset: 0x0002382C
	private void OnHelpFailure()
	{
		if (Game.UI.GetUI<UI_ProgressBar>().IsOpen())
		{
			Game.UI.CloseUI<UI_ProgressBar>();
		}
	}

	// Token: 0x04000908 RID: 2312
	public GamePlayItemData currentGamePlayItemData;

	// Token: 0x04000909 RID: 2313
	private const float GamePlayItemDistance = 2.5f;

	// Token: 0x0400090A RID: 2314
	public bool isHasGamePlayItem;

	// Token: 0x0400090B RID: 2315
	private int curPlayerActionId;
}
