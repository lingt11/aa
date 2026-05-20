using System;
using UnityEngine;

// Token: 0x0200026F RID: 623
public class EnemyNPCKing : EnemyMeleeMode
{
	// Token: 0x06000B74 RID: 2932 RVA: 0x0003D10D File Offset: 0x0003B30D
	public override void OnClientInitEnemy()
	{
		base.OnClientInitEnemy();
		this.roleBase.deadMoveSpeed = 0f;
		this.showTimeIndex = 0;
		this.customDeadTime = 15f;
		this.isReward = false;
	}

	// Token: 0x06000B75 RID: 2933 RVA: 0x0003D140 File Offset: 0x0003B340
	public override void OnStartShowPose()
	{
		base.OnStartShowPose();
		Game.UI.GetUI<UI_PlayerState>().ShowTalkUI(this.roleBase, new string[]
		{
			Game.Language.Get("Talk_King_Start_1", ""),
			Game.Language.Get("Talk_King_Start_2", ""),
			Game.Language.Get("Talk_King_Start_3", ""),
			Game.Language.Get("Talk_King_Start_4", "")
		}, 5f);
	}

	// Token: 0x06000B76 RID: 2934 RVA: 0x0003D1D0 File Offset: 0x0003B3D0
	public override void OnStartDead()
	{
		if (!this.isReward)
		{
			this.roleBase.PlayAni(AnimDefine.Dead, 1f, 0.1f);
			Game.AudioManager.PlayDeadAudio(this.deadSound, this.roleBase.MyTransform.position);
			this.gamePlayerId = GameHelperClient.AddGamePlayItem(new GamePlayItemData
			{
				gamePlayItemType = GamePlayItemType.Help,
				pos = this.roleBase.MyTransform.position,
				targetRole = this.roleBase
			});
			this.showTimeIndex = 0;
			this.customDeadTime = 15f;
		}
	}

	// Token: 0x06000B77 RID: 2935 RVA: 0x0003D274 File Offset: 0x0003B474
	public override void UpdateDead()
	{
		base.UpdateDead();
		if (GameHelperClient.isReady && this.gamePlayerId > 0)
		{
			this.RemoveTalk();
			this.gamePlayerId = -1;
			if (this.roleBase.hasAuthority)
			{
				Util.ShowTipsNoLanguage(string.Format(ColorDefine.NormalColor, Game.Language.Get("monster_11", "")) + Game.Language.Get("任务失败", ""));
			}
			this.roleBase.timer = 99f;
			return;
		}
		if (this.gamePlayerId > 0)
		{
			this.roleBase.timer = 0f;
			this.customDeadTime -= Time.deltaTime;
			this.ShowTimeTalk();
			if (this.customDeadTime <= 0f)
			{
				this.roleBase.timer = 99f;
				if (this.roleBase.hasAuthority)
				{
					Util.ShowTipsNoLanguage(string.Format(ColorDefine.NormalColor, Game.Language.Get("monster_11", "")) + Game.Language.Get("任务失败", ""));
				}
			}
		}
	}

	// Token: 0x06000B78 RID: 2936 RVA: 0x0003D398 File Offset: 0x0003B598
	private void ShowTimeTalk()
	{
		int num = Mathf.CeilToInt(this.customDeadTime);
		if (num != this.showTimeIndex)
		{
			this.showTimeIndex = num;
			Game.UI.GetUI<UI_PlayerState>().ShowTalkUI(this.roleBase, new string[]
			{
				Game.Language.Get("Talk_King_Dead_1", "") + "（" + string.Format(Game.Language.Get("Talk_King_Dead_2", ""), this.showTimeIndex) + "）"
			}, 999f);
		}
	}

	// Token: 0x06000B79 RID: 2937 RVA: 0x0003D42B File Offset: 0x0003B62B
	public override void OnRemove()
	{
		base.OnRemove();
		this.RemoveTalk();
	}

	// Token: 0x06000B7A RID: 2938 RVA: 0x0003D439 File Offset: 0x0003B639
	private void RemoveTalk()
	{
		Game.UI.GetUI<UI_PlayerState>().RemoveTalkUI(this.roleBase);
		if (this.gamePlayerId > 0)
		{
			GameHelperClient.GamePlayItemDatas.Remove(this.gamePlayerId);
		}
	}

	// Token: 0x06000B7B RID: 2939 RVA: 0x0003D46C File Offset: 0x0003B66C
	public override void OnExitDead()
	{
		base.OnExitDead();
		if (this.gamePlayerId > 0)
		{
			GameHelperClient.GamePlayItemDatas.Remove(this.gamePlayerId);
			this.gamePlayerId = 0;
		}
		if (!this.isReward)
		{
			Game.EffectManager.PlayEffect(EffectDefine.HealingEffect, 1f, this.roleBase.GetAttackPos(), 1.2f);
			Game.UI.GetUI<UI_PlayerState>().ShowTalkUI(this.roleBase, new string[]
			{
				Game.Language.Get("Talk_King_Save_1", ""),
				Game.Language.Get("Talk_King_Save_2", ""),
				Game.Language.Get("Talk_King_Save_3", "")
			}, 5f);
			if (this.roleBase.hasAuthority)
			{
				this.roleBase.roleBuffManager.AddOneBuff<Buff无敌>("Buff无敌", 5f);
			}
		}
	}

	// Token: 0x06000B7C RID: 2940 RVA: 0x0003D560 File Offset: 0x0003B760
	public override void UpdateEvent()
	{
		base.UpdateEvent();
		if (GameHelperClient.isReady && !this.isReward && !this.roleBase.IsDead())
		{
			this.isReward = true;
			this.getReward = false;
			Game.UI.GetUI<UI_PlayerState>().ShowTalkUI(this.roleBase, new string[]
			{
				Game.Language.Get("Talk_King_Reward_1", ""),
				Game.Language.Get("Talk_King_Reward_2", "")
			}, 5f);
			if (this.roleBase.hasAuthority)
			{
				Game.TimerManager.AddTimer(6f, new Action(this.AfterReward));
			}
		}
	}

	// Token: 0x06000B7D RID: 2941 RVA: 0x0003D620 File Offset: 0x0003B820
	private void AfterReward()
	{
		if (this.roleBase != null && !this.getReward)
		{
			this.getReward = true;
			Util.ShowTipsNoLanguage(string.Format(ColorDefine.NormalColor, Game.Language.Get("monster_11", "")) + Game.Language.Get("任务完成", ""));
			ItemType itemType = (Random.value > 0.65f) ? ((Random.value > 0.5f) ? ItemType.Active_Book_S : ItemType.Passsive_Book_S) : ((Random.value > 0.5f) ? ItemType.Active_Book_A : ItemType.Passsive_Book_A);
			GameHelperClient.localPlayer.CmdCreateItemByPos(itemType, this.roleBase.MyTransform.position + this.roleBase.MyTransform.forward * 1.5f);
			MySystemEvent.Instance.DispatchMessage(37);
		}
	}

	// Token: 0x04000C5D RID: 3165
	private int gamePlayerId;

	// Token: 0x04000C5E RID: 3166
	private float customDeadTime;

	// Token: 0x04000C5F RID: 3167
	private int showTimeIndex;

	// Token: 0x04000C60 RID: 3168
	private bool isReward;

	// Token: 0x04000C61 RID: 3169
	private bool getReward;
}
