using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200026E RID: 622
public class EnemyNPCGhost : EnemyMeleeMode
{
	// Token: 0x06000B69 RID: 2921 RVA: 0x0003CBF4 File Offset: 0x0003ADF4
	public override void OnClientInitEnemy()
	{
		base.OnClientInitEnemy();
		this.enemyBase.SetRotationY(225f);
		this.talkListIndex = 0;
		this.gamePlayerId = GameHelperClient.AddGamePlayItem(new GamePlayItemData
		{
			gamePlayItemType = GamePlayItemType.Talk,
			pos = this.roleBase.MyTransform.position,
			targetRole = this.roleBase,
			actionCallback = new Action(this.OnTalkAction)
		});
		this.talkList.Clear();
		this.otherTalkList.Clear();
		for (int i = 1; i < 3; i++)
		{
			this.talkList.Add("Talk_Ghost_Start_" + i.ToString() + "_" + Random.Range(0, 3).ToString());
		}
		this.otherTalkList.Add("Talk_Ghost_Other_" + Random.Range(0, 3).ToString());
		if (this.enemyBase.hasAuthority)
		{
			this.enemyBase.ShowTip();
		}
		this.isCheckDead = false;
	}

	// Token: 0x06000B6A RID: 2922 RVA: 0x0003CD06 File Offset: 0x0003AF06
	private void RemoveTalk()
	{
		Game.UI.GetUI<UI_PlayerState>().RemoveTalkUI(this.roleBase);
		if (this.gamePlayerId > 0)
		{
			GameHelperClient.GamePlayItemDatas.Remove(this.gamePlayerId);
		}
	}

	// Token: 0x06000B6B RID: 2923 RVA: 0x0003CD37 File Offset: 0x0003AF37
	public override void OnStartDead()
	{
		this.CheckDeadReward();
		this.RemoveTalk();
		Game.AudioManager.PlayAudioByPos("Audio/Battle_Audio/NPC/npc_ghost_laugh", this.enemyBase.MyTransform.position, 1f);
	}

	// Token: 0x06000B6C RID: 2924 RVA: 0x0003CD6C File Offset: 0x0003AF6C
	private void CheckDeadReward()
	{
		if (this.enemyBase.hasAuthority)
		{
			this.isCheckDead = true;
			if (GameHelperClient.localPlayer.noKillBossTime >= 3)
			{
				GameHelperClient.localPlayer.noKillBossTime = -999;
				GameHelperClient.localPlayer.CmdCreateItemByPos(ItemType.Remains_102, this.enemyBase.MyTransform.position);
				GameHelperClient.localPlayer.CmdChat(string.Format(ColorDefine.NormalColor, Game.Language.Get("pickitem_102", "")));
			}
		}
	}

	// Token: 0x06000B6D RID: 2925 RVA: 0x0003CDED File Offset: 0x0003AFED
	public override void OnRemove()
	{
		base.OnRemove();
		this.RemoveTalk();
	}

	// Token: 0x06000B6E RID: 2926 RVA: 0x0003CDFC File Offset: 0x0003AFFC
	private void OnTalkAction()
	{
		if (this.talkListIndex == 0)
		{
			Game.AudioManager.PlayAudioByPos("Audio/Battle_Audio/NPC/npc_ghost_laugh", this.enemyBase.MyTransform.position, 1f);
		}
		if (this.enemyBase.hasAuthority)
		{
			if (this.talkListIndex <= this.talkList.Count - 1)
			{
				Game.UI.GetUI<UI_PlayerState>().ShowTalkUI(this.enemyBase, new string[]
				{
					Game.Language.Get(this.talkList[this.talkListIndex], "")
				}, 9999f);
				this.talkListIndex++;
				return;
			}
			if (!Util.CheckCanRoguelike())
			{
				return;
			}
			this.RemoveTalk();
			(Game.UI.OpenUI<UI_Confirm>(null) as UI_Confirm).SetConfirmText(string.Format(Game.Language.Get("Talk_Ghost_Check", ""), string.Format(ColorDefine.NormalColor, this.enemyBase.roleName)), new Action(this.OnContract), new Action(this.OnCancel), null, "");
			Game.AudioManager.PlayAudioByPos("Audio/Battle_Audio/NPC/npc_ghost_laugh", this.enemyBase.MyTransform.position, 1f);
			return;
		}
		else
		{
			if (this.talkListIndex > this.otherTalkList.Count - 1)
			{
				this.RemoveTalk();
				return;
			}
			Game.UI.GetUI<UI_PlayerState>().ShowTalkUI(this.enemyBase, new string[]
			{
				Game.Language.Get(this.otherTalkList[this.talkListIndex], "")
			}, 9999f);
			this.talkListIndex++;
			return;
		}
	}

	// Token: 0x06000B6F RID: 2927 RVA: 0x0003CFB0 File Offset: 0x0003B1B0
	private void OnContract()
	{
		Util.OnDemonContract(delegate
		{
			if (this.enemyBase != null)
			{
				GameHelperClient.localPlayer.CmdChat(string.Format(Game.Language.Get("签订了契约", ""), string.Format(ColorDefine.NormalColor, this.enemyBase.roleName)));
				Game.UI.GetUI<UI_PlayerState>().ShowTalkUI(this.enemyBase, new string[]
				{
					Game.Language.Get("Talk_Ghost_Success_" + Random.Range(0, 3).ToString(), "")
				}, 5f);
			}
		});
	}

	// Token: 0x06000B70 RID: 2928 RVA: 0x0003CFC4 File Offset: 0x0003B1C4
	private void OnCancel()
	{
		this.CheckDeadReward();
		Game.UI.GetUI<UI_PlayerState>().ShowTalkUI(this.enemyBase, new string[]
		{
			Game.Language.Get("Talk_Ghost_Fail_" + Random.Range(0, 3).ToString(), "")
		}, 5f);
	}

	// Token: 0x06000B71 RID: 2929 RVA: 0x0003D022 File Offset: 0x0003B222
	public override void OnStartShowPose()
	{
		base.OnStartShowPose();
		Game.AudioManager.PlayAudioByPos("Audio/Battle_Audio/NPC/npc_ghost_appear", this.enemyBase.MyTransform.position, 1f);
	}

	// Token: 0x04000C54 RID: 3156
	private int gamePlayerId;

	// Token: 0x04000C55 RID: 3157
	private float customDeadTime;

	// Token: 0x04000C56 RID: 3158
	private int showTimeIndex;

	// Token: 0x04000C57 RID: 3159
	private bool isReward;

	// Token: 0x04000C58 RID: 3160
	private bool getReward;

	// Token: 0x04000C59 RID: 3161
	private List<string> talkList = new List<string>();

	// Token: 0x04000C5A RID: 3162
	private List<string> otherTalkList = new List<string>();

	// Token: 0x04000C5B RID: 3163
	private int talkListIndex;

	// Token: 0x04000C5C RID: 3164
	private bool isCheckDead;
}
