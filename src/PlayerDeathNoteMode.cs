using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

// Token: 0x02000299 RID: 665
public class PlayerDeathNoteMode : MeleePlayerMode
{
	// Token: 0x06000E9D RID: 3741 RVA: 0x0005369C File Offset: 0x0005189C
	public override void AttackUpdate()
	{
		float deltaTime = Time.deltaTime;
		this.playerBase.timer += deltaTime;
		float realOffsetInAttack = this.playerBase.GetRealOffsetInAttack();
		float num = this.playerBase.timer / realOffsetInAttack;
		if (this.playerBase.timer > realOffsetInAttack)
		{
			if (this.playerBase.hasAuthority)
			{
				int count = this.deathNoteDatas.Count;
				long playerNormalAttackPower = this.playerBase.GetPlayerNormalAttackPower();
				for (int i = 0; i < count; i++)
				{
					PlayerDeathNoteMode.DeathNoteData deathNoteData = this.deathNoteDatas[i];
					if (deathNoteData.targetRoleBase != null && !deathNoteData.targetRoleBase.IsDead())
					{
						bool isAttackWeek = this.playerBase.GetIsAttackWeek(AttackType.Normal);
						Util.OnLocalPlayerHit(this.playerBase, deathNoteData.targetRoleBase, (double)playerNormalAttackPower, this.playerBase.MyTransform.eulerAngles.y, AttackType.Normal, isAttackWeek);
						isAttackWeek = this.playerBase.GetIsAttackWeek(AttackType.Buff);
						Util.OnLocalPlayerHit(this.playerBase, deathNoteData.targetRoleBase, (double)deathNoteData.targetRoleBase.maxHp, this.playerBase.MyTransform.eulerAngles.y, AttackType.Buff, isAttackWeek);
					}
				}
			}
			if (this.playerBase.hasAuthority)
			{
				this.playerBase.UpdateRoleState(RoleState.Idle);
				return;
			}
		}
		else if (this.playerBase.hasAuthority)
		{
			if (num < 0.5f && num < 0.3f)
			{
				this.playerBase.TrackRotation(3f);
			}
			if (this.playerBase.CheckIsInputMove(num))
			{
				this.playerBase.timer = realOffsetInAttack;
			}
		}
	}

	// Token: 0x06000E9E RID: 3742 RVA: 0x00053844 File Offset: 0x00051A44
	public override void OnStartAttack()
	{
		base.OnStartAttack();
		this.ClearDeathNoteData();
		if (this.playerBase.hasAuthority)
		{
			if (this.playerBase.isLocalPlayer)
			{
				UI_ProgressBar ui_ProgressBar = Game.UI.OpenUI<UI_ProgressBar>(null) as UI_ProgressBar;
				float realOffsetInAttack = this.playerBase.GetRealOffsetInAttack();
				ui_ProgressBar.ShowProgress(realOffsetInAttack, Game.Language.Get("写字中", ""));
			}
			List<uint> list = new List<uint>();
			if (this.playerBase.trackRoleBase != null)
			{
				list.Add(this.playerBase.trackRoleBase.netId);
				if (this.playerBase.attackNum > 1)
				{
					List<RoleBase> canAttackRoleList = this.playerBase.GetCanAttackRoleList(base.GetAttackDistance(), this.playerBase.attackNum);
					if (canAttackRoleList.Count > 0)
					{
						int i = 0;
						int count = canAttackRoleList.Count;
						while (i < count)
						{
							list.Add(canAttackRoleList[i].netId);
							i++;
						}
					}
				}
			}
			this.playerBase.CmdAddAttackTarget(list);
		}
	}

	// Token: 0x06000E9F RID: 3743 RVA: 0x00053948 File Offset: 0x00051B48
	public void RpcAddAttackTarget(List<uint> roleList)
	{
		int count = roleList.Count;
		for (int i = 0; i < count; i++)
		{
			NetworkIdentity networkIdentity;
			if (NetworkClient.spawned.TryGetValue(roleList[i], out networkIdentity))
			{
				RoleBase component = networkIdentity.GetComponent<RoleBase>();
				PlayerDeathNoteMode.DeathNoteData item = default(PlayerDeathNoteMode.DeathNoteData);
				item.targetRoleBase = component;
				GameObject gameObject = AssetManager.LoadPrefab(EffectDefine.DeathNoteBuff, null, true);
				item.effect = gameObject;
				Transform transform = gameObject.transform;
				transform.SetParent(component.MyTransform);
				transform.localPosition = new Vector3(0f, 1f, 0f);
				transform.localScale = Vector3.one * (1f + component.RoleModeBase.addRange);
				this.deathNoteDatas.Add(item);
			}
		}
	}

	// Token: 0x06000EA0 RID: 3744 RVA: 0x00053A0E File Offset: 0x00051C0E
	public override void OnExitAttack()
	{
		base.OnExitAttack();
		this.ClearDeathNoteData();
		if (this.playerBase.isLocalPlayer && Game.UI.GetUI<UI_ProgressBar>().IsOpen())
		{
			Game.UI.CloseUI<UI_ProgressBar>();
		}
	}

	// Token: 0x06000EA1 RID: 3745 RVA: 0x00053A44 File Offset: 0x00051C44
	private void ClearDeathNoteData()
	{
		int count = this.deathNoteDatas.Count;
		for (int i = 0; i < count; i++)
		{
			PlayerDeathNoteMode.DeathNoteData deathNoteData = this.deathNoteDatas[i];
			if (deathNoteData.effect != null)
			{
				AssetManager.UnLoadPrefab(deathNoteData.effect, false);
			}
		}
		this.deathNoteDatas.Clear();
	}

	// Token: 0x06000EA2 RID: 3746 RVA: 0x00053A9B File Offset: 0x00051C9B
	public override void OnClearMode()
	{
		base.OnClearMode();
		this.ClearDeathNoteData();
	}

	// Token: 0x04000DA6 RID: 3494
	private List<PlayerDeathNoteMode.DeathNoteData> deathNoteDatas = new List<PlayerDeathNoteMode.DeathNoteData>();

	// Token: 0x0200029A RID: 666
	private struct DeathNoteData
	{
		// Token: 0x04000DA7 RID: 3495
		public RoleBase targetRoleBase;

		// Token: 0x04000DA8 RID: 3496
		public GameObject effect;
	}
}
