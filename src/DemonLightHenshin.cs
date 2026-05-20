using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000281 RID: 641
public class DemonLightHenshin : TrajectoryPlayerMode
{
	// Token: 0x06000BFB RID: 3067 RVA: 0x0004205C File Offset: 0x0004025C
	public override void UpdateEvent()
	{
		for (int i = this.flyAttackList.Count - 1; i > -1; i--)
		{
			TrajectoryPlayerMode.FlyAttackData flyAttackData = this.flyAttackList[i];
			if (flyAttackData.trackBase == null || flyAttackData.trackBase.IsDead())
			{
				AssetManager.UnLoadPrefab(flyAttackData.myTransform.gameObject, false);
				flyAttackData.myTransform = null;
				this.flyAttackList.RemoveAt(i);
			}
			else
			{
				float deltaTime = Time.deltaTime;
				Vector3 attackPos = flyAttackData.trackBase.GetAttackPos();
				float num = Vector3.Distance(flyAttackData.myTransform.position, attackPos);
				if (num < 1.5f)
				{
					flyAttackData.myTransform.rotation = Quaternion.LookRotation(attackPos - flyAttackData.myTransform.position);
				}
				else
				{
					flyAttackData.myTransform.rotation = Quaternion.Lerp(flyAttackData.myTransform.rotation, Quaternion.LookRotation(attackPos - flyAttackData.myTransform.position), deltaTime * 20f);
				}
				flyAttackData.myTransform.position += flyAttackData.myTransform.forward * (deltaTime * 18f);
				if (num < 0.5f)
				{
					if (this.playerBase.hasAuthority)
					{
						bool flag = this.playerBase.roleType == RoleType.Enemy;
						List<RoleBase> attackRoles = this.playerBase.GetAttackRoles();
						int count = attackRoles.Count;
						Vector3 position = flyAttackData.trackBase.MyTransform.position;
						bool isAttackWeek = this.playerBase.GetIsAttackWeek(AttackType.Skill);
						for (int j = 0; j < count; j++)
						{
							RoleBase roleBase = attackRoles[j];
							if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && Util.NewCheckYuanXing(position, roleBase.MyTransform.position, 2f * (1f + this.playerBase.skillRange) + roleBase.RoleModeBase.addRange, false))
							{
								if (flag)
								{
									roleBase.OnHit(this.playerBase, (double)this.playerBase.FinalAttackPower * 1.25, Util.GetV2Angle(roleBase.MyTransform.position, position), AttackType.Skill, isAttackWeek);
								}
								else
								{
									double num2 = (double)this.playerBase.FinalAttackPower * 1.25;
									num2 = (double)Util.GetPassSkillDamage(this.playerBase, SkillAttribute.Lighting, num2, false);
									num2 = (double)ConstDefine.ClampBattleValue(num2 * (1.0 + (double)this.playerBase.addHenshin));
									Util.OnLocalPlayerHit(this.playerBase, roleBase, num2, Util.GetV2Angle(roleBase.MyTransform.position, position), AttackType.Skill, isAttackWeek);
								}
							}
						}
					}
					Game.EffectManager.PlayEffect(EffectDefine.LightBoom, 3f, flyAttackData.trackBase.GetAttackPos(), 3f * (1f + this.playerBase.skillRange));
					AssetManager.UnLoadPrefab(flyAttackData.myTransform.gameObject, false);
					flyAttackData.myTransform = null;
					flyAttackData.trackBase = null;
					this.flyAttackList.RemoveAt(i);
				}
			}
		}
	}
}
