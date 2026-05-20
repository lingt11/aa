using System;
using UnityEngine;

// Token: 0x020000C5 RID: 197
public class CardSkillManaVoid : CardSkillBase
{
	// Token: 0x06000383 RID: 899 RVA: 0x00016FF1 File Offset: 0x000151F1
	public override void Enter()
	{
		base.Enter();
		if (this.playerBase.roleType == RoleType.King)
		{
			this.addSkillDamage = (float)this.playerBase.maxMp * 0.04f * 0.01f;
		}
	}

	// Token: 0x06000384 RID: 900 RVA: 0x00017028 File Offset: 0x00015228
	public override void Update()
	{
		base.Update();
		if (this.checkTime < 0.3f)
		{
			this.checkTime += Time.deltaTime;
			return;
		}
		this.checkTime = 0f;
		this.playerBase.skillExDamage -= this.addSkillDamage;
		this.addSkillDamage = (float)this.playerBase.maxMp * 0.04f * 0.01f;
		this.playerBase.skillExDamage += this.addSkillDamage;
	}

	// Token: 0x06000385 RID: 901 RVA: 0x000170B4 File Offset: 0x000152B4
	public override void Exit()
	{
		base.Exit();
		this.playerBase.skillExDamage -= this.addSkillDamage;
	}

	// Token: 0x04000381 RID: 897
	private float addSkillDamage;

	// Token: 0x04000382 RID: 898
	private float checkTime;

	// Token: 0x04000383 RID: 899
	private const float Addlevel = 0.04f;
}
