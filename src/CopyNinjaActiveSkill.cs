using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002D3 RID: 723
public class CopyNinjaActiveSkill : ActiveSkillBase
{
	// Token: 0x060010E0 RID: 4320 RVA: 0x0005F18B File Offset: 0x0005D38B
	public void InitSkill(ActiveSkillEnum activeSkillType, RoleBase attackRole)
	{
		this.activeSkillEnum = activeSkillType;
		this.activeSkillData = Game.GameData.ActiveSkillDataDic[activeSkillType];
		this.attackRoleBase = attackRole;
		this.skillTime = 0.8f;
	}

	// Token: 0x060010E1 RID: 4321 RVA: 0x0005F1BC File Offset: 0x0005D3BC
	public override void Clear(int clearData)
	{
		if (this.attackRoleBase.HasAuthority && this.attackRoleBase.isLocalPlayer)
		{
			this.attackRoleBase.UpdateRoleState(RoleState.Idle);
			float num = 99999f;
			List<RoleBase> clientEnemyList = Game.EnemyManagerClient.clientEnemyList;
			int count = clientEnemyList.Count;
			Vector3 position = this.attackRoleBase.MyTransform.position;
			ActiveSkillEnum activeSkillEnum = ActiveSkillEnum.None;
			for (int i = 0; i < count; i++)
			{
				EnemyBase enemyBase = clientEnemyList[i] as EnemyBase;
				if (enemyBase != null && enemyBase.enemyModeBase != null && enemyBase.enemyModeBase.activeSkillAry != null)
				{
					float v2Distance = Util.GetV2Distance(position, enemyBase.MyTransform.position);
					if (v2Distance < num)
					{
						int num2 = enemyBase.enemyModeBase.activeSkillAry.Length;
						if (num2 > 0)
						{
							num = v2Distance;
							activeSkillEnum = (ActiveSkillEnum)enemyBase.enemyModeBase.activeSkillAry[Random.Range(0, num2)];
						}
					}
				}
			}
			List<RoleBase> clientPlayerList = Game.PlayerManagerClient.clientPlayerList;
			count = clientPlayerList.Count;
			for (int j = 0; j < count; j++)
			{
				RoleBase roleBase = clientPlayerList[j];
				if (roleBase != null && roleBase.roleType == RoleType.Player && roleBase != this.attackRoleBase)
				{
					float v2Distance2 = Util.GetV2Distance(position, roleBase.MyTransform.position);
					PlayerBase playerBase = roleBase as PlayerBase;
					if (v2Distance2 < num && playerBase.syncActiveSkillEnum != ActiveSkillEnum.None)
					{
						num = v2Distance2;
						activeSkillEnum = playerBase.syncActiveSkillEnum;
					}
				}
			}
			if (activeSkillEnum != ActiveSkillEnum.None)
			{
				this.attackRoleBase.AddHeroSkill(activeSkillEnum, GameHelperClient.localPlayer.roleSkillList[0]);
				GameHelperClient.AddShowBuff(Game.Language.Get(PathDefine.Concat("a_", this.activeSkillData.id), ""), SkillBase.GetActiveSkillTip(this.activeSkillEnum), "Skill/" + this.activeSkillData.icon, 60f);
				string b = Game.Language.Get(PathDefine.Concat("a_", Game.GameData.ActiveSkillDataDic[activeSkillEnum].id), "");
				Util.ShowTipsNoLanguage(PathDefine.Concat(Game.Language.Get("成功偷取技能：", ""), b));
				CopyNinjaActiveSkill.MyCallBack myCallBack = new CopyNinjaActiveSkill.MyCallBack();
				myCallBack.roleBase = this.attackRoleBase;
				Game.TimerManager.AddTimer(60f, new Action(myCallBack.OnCallBack));
			}
			else
			{
				Util.ShowTipsNoLanguage(Game.Language.Get("没有可以偷取的技能！", ""));
			}
		}
		base.Clear(clearData);
	}

	// Token: 0x020002D4 RID: 724
	private class MyCallBack
	{
		// Token: 0x060010E3 RID: 4323 RVA: 0x0005F458 File Offset: 0x0005D658
		public void OnCallBack()
		{
			if (this.roleBase != null)
			{
				this.roleBase.AddHeroSkill(ActiveSkillEnum.CopyNinja, GameHelperClient.localPlayer.roleSkillList[0]);
				Util.ShowTipsNoLanguage(Game.Language.Get("偷取的技能已失效！", ""));
				this.roleBase = null;
			}
		}

		// Token: 0x04000ED5 RID: 3797
		public RoleBase roleBase;
	}
}
