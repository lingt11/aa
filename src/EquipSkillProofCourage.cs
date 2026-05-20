using System;
using UnityEngine;

// Token: 0x020000F3 RID: 243
public class EquipSkillProofCourage : EquipSkillBase
{
	// Token: 0x06000502 RID: 1282 RVA: 0x0001DD4E File Offset: 0x0001BF4E
	public override void Init()
	{
		base.Init();
		PlayerBase playerBase = this.playerBase;
		playerBase.killEnemyEvent = (RoleBase.KillEnemy)Delegate.Combine(playerBase.killEnemyEvent, new RoleBase.KillEnemy(this.KillEvent));
		this.equipBase.totals = new int[2];
	}

	// Token: 0x06000503 RID: 1283 RVA: 0x0001DD90 File Offset: 0x0001BF90
	private void KillEvent(RoleBase attackrole, RoleBase hurtrole)
	{
		EnemyBase enemyBase = hurtrole as EnemyBase;
		if (enemyBase != null && enemyBase.isBoss)
		{
			int num = Mathf.RoundToInt((float)this.equipNum * this.skillValueAry[0] + this.skillValueUpAry[0] * (float)this.strengLevel);
			attackrole.AddSTR(num);
			attackrole.AddSTA(num);
			attackrole.AddAGI(num);
			Util.ShowTipsNoLanguage(PathDefine.Concat(Game.Language.Get(PathDefine.Concat("equip_", this.equipIndex), ""), StringDefine.Colon, Game.Language.Get("全属性增加", ""), string.Format(ColorDefine.NormalColor, num)));
			int num2 = Mathf.RoundToInt((float)this.equipNum * this.skillValueAry[1] + this.skillValueUpAry[1] * (float)this.strengLevel);
			PlayerBase playerBase = attackrole as PlayerBase;
			if (playerBase != null)
			{
				num2 = playerBase.AddGold(attackrole.GetHeadUIPos(), num2, true);
			}
			this.equipBase.totals[0] += num;
			this.equipBase.totals[1] += num2;
		}
	}

	// Token: 0x06000504 RID: 1284 RVA: 0x0001DEAF File Offset: 0x0001C0AF
	public override void Clear()
	{
		base.Clear();
		PlayerBase playerBase = this.playerBase;
		playerBase.killEnemyEvent = (RoleBase.KillEnemy)Delegate.Remove(playerBase.killEnemyEvent, new RoleBase.KillEnemy(this.KillEvent));
	}
}
