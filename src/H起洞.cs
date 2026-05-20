using System;
using UnityEngine;

// Token: 0x020001CA RID: 458
public class H起洞 : PasssiveSkill
{
	// Token: 0x0600086A RID: 2154 RVA: 0x00030134 File Offset: 0x0002E334
	public override void Enter()
	{
		this.totalName = Game.Language.Get(PathDefine.Concat("p_", this.skillId, StringDefine.Total), "");
		this.totals = new int[3];
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Combine(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x0600086B RID: 2155 RVA: 0x0003019E File Offset: 0x0002E39E
	public override void Exit()
	{
		PlayerBase roleBase = this.roleBase;
		roleBase.attackEnemyEvent = (RoleBase.AttackEnemy)Delegate.Remove(roleBase.attackEnemyEvent, new RoleBase.AttackEnemy(this.AttackEvent));
	}

	// Token: 0x0600086C RID: 2156 RVA: 0x000301C8 File Offset: 0x0002E3C8
	private float AttackEvent(RoleBase attackrole, RoleBase hurtrole, ref float damage)
	{
		if ((float)Random.Range(0, 100) < this.skillValues[0])
		{
			if (!GameHelperClient.isReady)
			{
				int num = Random.Range(1, 4);
				string a = Game.Language.Get("yuanshiren", "");
				int num2 = Random.Range(0, 3);
				if (num2 == 0)
				{
					attackrole.AddSTR(num);
					if (this.roleBase.isLocalPlayer)
					{
						UI_Msg ui = Game.UI.GetUI<UI_Msg>();
						if (ui != null)
						{
							ui.ShowMsg(PathDefine.Concat(a, num, Game.Language.Get("str", "")), false);
						}
					}
					this.totals[0] += num;
				}
				else if (num2 == 1)
				{
					attackrole.AddAGI(num);
					if (this.roleBase.isLocalPlayer)
					{
						UI_Msg ui2 = Game.UI.GetUI<UI_Msg>();
						if (ui2 != null)
						{
							ui2.ShowMsg(PathDefine.Concat(a, num, Game.Language.Get("dex", "")), false);
						}
					}
					this.totals[1] += num;
				}
				else if (num2 == 2)
				{
					attackrole.AddSTA(num);
					if (this.roleBase.isLocalPlayer)
					{
						UI_Msg ui3 = Game.UI.GetUI<UI_Msg>();
						if (ui3 != null)
						{
							ui3.ShowMsg(PathDefine.Concat(a, num, Game.Language.Get("sta", "")), false);
						}
					}
					this.totals[2] += num;
				}
			}
			float num3 = (float)attackrole.STR * this.skillValues[2] + this.skillValues[1];
			num3 = (float)Util.GetPassSkillDamage(this.roleBase, this.skillAttribute, (double)num3, false);
			GameHelperClient.AOEDamage(this.roleBase, num3, hurtrole.transform.position, base.Distance, EffectDefine.BlackHoleExplosionBlue, 1f + this.roleBase.skillRange);
		}
		return damage;
	}
}
