using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000303 RID: 771
public class SummonTreeMode : EnemyModeBase
{
	// Token: 0x060011C4 RID: 4548 RVA: 0x0006865C File Offset: 0x0006685C
	public override void OnClientInitEnemy()
	{
		base.OnClientInitEnemy();
		this.myTransform = base.transform;
		Game.EffectManager.PlayEffect(EffectDefine.SummonEffect, 2f, this.myTransform.position, 1.75f);
		switch (this.enemyBase.enemyType)
		{
		case EnemyType.Summon_Tree_D:
			this.myTransform.localScale = Vector3.one;
			this.addHpLevel = 10;
			break;
		case EnemyType.Summon_Tree_C:
			this.myTransform.localScale = 1.3333334f * Vector3.one;
			this.addHpLevel = 20;
			break;
		case EnemyType.Summon_Tree_B:
			this.myTransform.localScale = 1.6666666f * Vector3.one;
			this.addHpLevel = 30;
			break;
		}
		this.range = this.myTransform.localScale.x * 3f;
	}

	// Token: 0x060011C5 RID: 4549 RVA: 0x00068744 File Offset: 0x00066944
	public override void MoveUpdate()
	{
		if (this.roleBase.hasAuthority)
		{
			this.checkTime += Time.deltaTime;
			if (this.checkTime > 0.5f)
			{
				this.checkTime = 0f;
				List<RoleBase> friendRoles = this.roleBase.GetFriendRoles();
				int count = friendRoles.Count;
				for (int i = 0; i < count; i++)
				{
					RoleBase roleBase = friendRoles[i];
					if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && Util.NewCheckYuanXing(this.myTransform.position, roleBase.MyTransform.position, this.range + roleBase.RoleModeBase.addRange, false))
					{
						this.roleBase.StartHealthHp((long)(50 + this.roleBase.STA * this.addHpLevel), roleBase);
					}
				}
			}
		}
	}

	// Token: 0x060011C6 RID: 4550 RVA: 0x00068825 File Offset: 0x00066A25
	public override void OnStartDead()
	{
		base.OnStartDead();
		Game.EffectManager.PlayEffect(EffectDefine.SummonEffect, 2f, base.transform.position, 1.75f);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000FDB RID: 4059
	private Transform myTransform;

	// Token: 0x04000FDC RID: 4060
	private float checkTime;

	// Token: 0x04000FDD RID: 4061
	private float range;

	// Token: 0x04000FDE RID: 4062
	private int addHpLevel;
}
