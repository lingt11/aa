using System;
using UnityEngine;

// Token: 0x020001C9 RID: 457
public class H赛亚人之血 : PasssiveSkill
{
	// Token: 0x06000866 RID: 2150 RVA: 0x0002FF4C File Offset: 0x0002E14C
	public override void Enter()
	{
		this.totalName = Game.Language.Get(PathDefine.Concat("p_", this.skillId, StringDefine.Total), "");
		this.totals = new int[2];
		this.isTotalsPercent = new bool[]
		{
			default(bool),
			true
		};
	}

	// Token: 0x06000867 RID: 2151 RVA: 0x0002FFA0 File Offset: 0x0002E1A0
	public override void Update()
	{
		base.Update();
		if (this.checkTime < 0.1f)
		{
			this.checkTime += Time.deltaTime;
			return;
		}
		this.roleBase.addDamagePercent -= this.addDamage;
		this.roleBase.AddArmor(-this.addArmor);
		this.addDamage = 0f;
		this.addArmor = 0;
		float num = (float)this.roleBase.hp / (float)this.roleBase.maxHp;
		if (num < 0.1f)
		{
			this.addDamage = 2.5f;
			this.addArmor = 100;
		}
		else if (num < 0.2f)
		{
			this.addDamage = 1f;
			this.addArmor = 50;
		}
		else if (num < 0.35f)
		{
			this.addDamage = 0.5f;
			this.addArmor = 25;
		}
		else if (num < 0.5f)
		{
			this.addDamage = 0.2f;
			this.addArmor = 10;
		}
		this.roleBase.AddArmor(this.addArmor);
		this.roleBase.addDamagePercent += this.addDamage;
		this.checkTime = 0f;
		this.totals[0] = this.addArmor;
		this.totals[1] = Mathf.RoundToInt(this.addDamage * 100f);
	}

	// Token: 0x06000868 RID: 2152 RVA: 0x000300F6 File Offset: 0x0002E2F6
	public override void Exit()
	{
		this.roleBase.addDamagePercent -= this.addDamage;
		this.roleBase.AddArmor(-this.addArmor);
		this.addDamage = 0f;
		this.addArmor = 0;
	}

	// Token: 0x04000B88 RID: 2952
	private float addDamage;

	// Token: 0x04000B89 RID: 2953
	private int addArmor;

	// Token: 0x04000B8A RID: 2954
	private float checkTime;
}
