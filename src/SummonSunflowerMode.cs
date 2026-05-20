using System;
using UnityEngine;

// Token: 0x02000302 RID: 770
public class SummonSunflowerMode : EnemyModeBase
{
	// Token: 0x060011C0 RID: 4544 RVA: 0x000684D0 File Offset: 0x000666D0
	public override void OnClientInitEnemy()
	{
		base.OnClientInitEnemy();
		this.myTransform = base.transform;
		this.myTransform.localEulerAngles = new Vector3(0f, 180f, 0f);
		this.myTransform.localPosition = new Vector3(0f, 0f, 0f);
		Game.EffectManager.PlayEffect(EffectDefine.SummonEffect, 2f, this.myTransform.position - new Vector3(0f, 1f, 0f), 1.75f);
	}

	// Token: 0x060011C1 RID: 4545 RVA: 0x0006856C File Offset: 0x0006676C
	public override void MoveUpdate()
	{
		if (this.roleBase.hasAuthority && !GameHelperClient.isReady)
		{
			this.createTimer += Time.deltaTime;
			if (this.createTimer > 5f)
			{
				this.createTimer = 0f;
				GameHelperClient.localPlayer.CmdCreateItemByPos(ItemType.Pick_Sun, this.myTransform.position);
				SkillBase skillByBookId = GameHelperClient.localPlayer.GetSkillByBookId(this.enemyBase.SkillBookId);
				if (skillByBookId != null)
				{
					skillByBookId.totals[0] += 25;
				}
			}
		}
	}

	// Token: 0x060011C2 RID: 4546 RVA: 0x000685FC File Offset: 0x000667FC
	public override void OnStartDead()
	{
		base.OnStartDead();
		Game.EffectManager.PlayEffect(EffectDefine.SummonEffect, 2f, base.transform.position - new Vector3(0f, 1f, 0f), 1.75f);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000FD9 RID: 4057
	private Transform myTransform;

	// Token: 0x04000FDA RID: 4058
	private float createTimer;
}
