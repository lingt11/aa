using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020002EC RID: 748
public class PlantBombActiveSkill : ActiveSkillBase
{
	// Token: 0x0600114D RID: 4429 RVA: 0x00063EAC File Offset: 0x000620AC
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole, Vector3 pos, ActiveSkillData activeSkillData, int syncData, float duration, float attackRangeValue)
	{
		this.activeSkillEnum = activeSkillType;
		this.attackRoleBase = attackRole;
		this.activeSkillData = activeSkillData;
		float num = 7.5f;
		int num2 = 0;
		for (int i = 0; i < PlantBombActiveSkill.PlantBombSkillData.Length; i++)
		{
			if (syncData > PlantBombActiveSkill.PlantBombSkillData[i])
			{
				num2 = (i + 1) * 2;
			}
		}
		this.attackRange = (num + (float)num2 * 1.25f) * attackRangeValue;
		this.skillTime = duration;
		this.checkPos = pos;
		this.checkPos.y = 0f;
		this.effectGo = AssetManager.LoadPrefab(EffectDefine.PlantBombStart, null, true);
		Transform transform = this.effectGo.transform;
		transform.position = this.checkPos + new Vector3(0f, 0.35f, 0f);
		transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
	}

	// Token: 0x0600114E RID: 4430 RVA: 0x00002D1D File Offset: 0x00000F1D
	protected override void UpdateLocalSkill(float time)
	{
	}

	// Token: 0x0600114F RID: 4431 RVA: 0x00063F88 File Offset: 0x00062188
	public override void Clear(int clearData)
	{
		if (this.effectGo != null)
		{
			AssetManager.UnLoadPrefab(this.effectGo, false);
		}
		foreach (TipEffect tipEffect in this.tipEffects)
		{
			if (tipEffect.lifeTime > tipEffect.timer)
			{
				tipEffect.timer = tipEffect.lifeTime;
			}
		}
		Game.EffectManager.PlayEffect(EffectDefine.PlantBombExplosion, 3f, this.checkPos + new Vector3(0f, 0.15f, 0f), this.attackRange / 40f);
		if (this.attackRoleBase != null && this.attackRoleBase.hasAuthority)
		{
			bool flag = this.attackRoleBase.roleType == RoleType.Enemy;
			List<RoleBase> attackRoles = this.attackRoleBase.GetAttackRoles();
			int count = attackRoles.Count;
			bool isAttackWeek = this.attackRoleBase.GetIsAttackWeek(AttackType.Skill);
			long num = Util.GetSkillDamage(this.activeSkillData, this.attackRoleBase);
			if (clearData > 0)
			{
				PlayerBase playerBase = this.attackRoleBase as PlayerBase;
				if (playerBase != null && playerBase.playerAttribute.cardSkillListDic.ContainsKey(CardSkillType.ArtExplosion))
				{
					num = (long)Mathf.RoundToInt((float)num * (1f + (float)clearData * 0.45f));
				}
			}
			for (int i = 0; i < count; i++)
			{
				RoleBase roleBase = attackRoles[i];
				if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && !this.hitRoles.Contains(roleBase) && (Util.NewCheckJuXing(this.checkPos, 0f, this.attackRange / 6f, this.attackRange, roleBase.MyTransform.position, roleBase.RoleModeBase.addRange, true, false) || Util.NewCheckJuXing(this.checkPos, 90f, this.attackRange / 6f, this.attackRange, roleBase.MyTransform.position, roleBase.RoleModeBase.addRange, true, false)))
				{
					this.hitRoles.Add(roleBase);
					if (flag)
					{
						roleBase.OnHit(this.attackRoleBase, (double)num, Util.GetV2Angle(roleBase.MyTransform.position, this.checkPos), AttackType.Skill, isAttackWeek);
					}
					else
					{
						Util.OnLocalPlayerHit(this.attackRoleBase, roleBase, (double)num, Util.GetV2Angle(roleBase.MyTransform.position, this.checkPos), AttackType.Skill, isAttackWeek);
					}
				}
			}
			PlantBombActiveSkill.CheckConditionData checkConditionData = new PlantBombActiveSkill.CheckConditionData();
			checkConditionData.attackRole = this.attackRoleBase;
			checkConditionData.bombConditionData = clearData;
			checkConditionData.checkPos = this.checkPos;
			Game.TimerManager.AddTimer(0.1f, new Action(checkConditionData.CheckCondition));
		}
		base.Clear(clearData);
	}

	// Token: 0x04000F61 RID: 3937
	private Vector3 checkPos;

	// Token: 0x04000F62 RID: 3938
	private float attackRange;

	// Token: 0x04000F63 RID: 3939
	private GameObject effectGo;

	// Token: 0x04000F64 RID: 3940
	private int createWave;

	// Token: 0x04000F65 RID: 3941
	private List<RoleBase> hitRoles = new List<RoleBase>();

	// Token: 0x04000F66 RID: 3942
	private const float WidthLevel = 6f;

	// Token: 0x04000F67 RID: 3943
	private List<TipEffect> tipEffects = new List<TipEffect>();

	// Token: 0x04000F68 RID: 3944
	public static readonly int[] PlantBombSkillData = new int[]
	{
		50,
		150,
		300,
		500,
		1000,
		2000,
		5000
	};

	// Token: 0x020002ED RID: 749
	private class CheckConditionData
	{
		// Token: 0x06001152 RID: 4434 RVA: 0x000642B8 File Offset: 0x000624B8
		public void CheckCondition()
		{
			if (this.attackRole == null || !this.attackRole.hasAuthority)
			{
				return;
			}
			int count = Game.SkillManager.skills.Count;
			for (int i = 0; i < count; i++)
			{
				ActiveSkillBase value = Game.SkillManager.skills.ElementAt(i).Value;
				PlantBombActiveSkill plantBombActiveSkill = value as PlantBombActiveSkill;
				if (plantBombActiveSkill != null && value.attackRoleBase.netId == this.attackRole.netId && (Util.NewCheckJuXing(this.checkPos, 0f, plantBombActiveSkill.attackRange / 6f, plantBombActiveSkill.attackRange, plantBombActiveSkill.checkPos, 0.25f, true, false) || Util.NewCheckJuXing(this.checkPos, 90f, plantBombActiveSkill.attackRange / 6f, plantBombActiveSkill.attackRange, plantBombActiveSkill.checkPos, 0.25f, true, false)))
				{
					PlayerBase playerBase = this.attackRole as PlayerBase;
					if (playerBase != null && playerBase.playerAttribute.cardSkillListDic.ContainsKey(CardSkillType.ArtExplosion))
					{
						GameHelperClient.localPlayer.CmdClearSkillByData(value.skillId, Mathf.Max(1, this.bombConditionData + 1));
					}
					else
					{
						GameHelperClient.localPlayer.CmdClearSkill(value.skillId);
					}
				}
			}
		}

		// Token: 0x04000F69 RID: 3945
		public RoleBase attackRole;

		// Token: 0x04000F6A RID: 3946
		public int bombConditionData;

		// Token: 0x04000F6B RID: 3947
		public Vector3 checkPos;
	}
}
