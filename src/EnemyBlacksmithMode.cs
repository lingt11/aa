using System;
using UnityEngine;

// Token: 0x0200025E RID: 606
public class EnemyBlacksmithMode : EnemyMeleeMode
{
	// Token: 0x06000B20 RID: 2848 RVA: 0x00039D48 File Offset: 0x00037F48
	public override void OnClientInitEnemy()
	{
		base.OnClientInitEnemy();
		RoleAttribute roleAttribute = Game.GameData.HeroAttributeDic[this.enemyBase.enemyType.ToString()];
		if (Mathf.Approximately(this.enemyBase.xiXue, 0f))
		{
			this.enemyBase.AddXiXue((float)roleAttribute.hp * 0.1f);
		}
	}
}
