using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002CB RID: 715
public class BlackHoleActiveSkill : CoAoeActiveSkill
{
	// Token: 0x060010C3 RID: 4291 RVA: 0x0005E384 File Offset: 0x0005C584
	protected override void UpdateSkill(float time)
	{
		base.UpdateSkill(time);
		List<RoleBase> attackRoles = this.attackRoleBase.GetAttackRoles();
		int count = attackRoles.Count;
		for (int i = 0; i < count; i++)
		{
			RoleBase roleBase = attackRoles[i];
			if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && roleBase.authorityId == GameHelperClient.localPlayer.authorityId && Util.NewCheckYuanXing(this.checkPos, roleBase.MyTransform.position, this.attackRange + roleBase.RoleModeBase.addRange, false))
			{
				Vector3 a = new Vector3(this.checkPos.x - roleBase.MyTransform.position.x, 0f, this.checkPos.z - roleBase.MyTransform.position.z);
				if (a.magnitude > this.attackRange / 3f)
				{
					PlayerBase playerBase = roleBase as PlayerBase;
					if (playerBase != null)
					{
						playerBase.CharacterController.Move(a * (time * 1.5f));
					}
					else
					{
						roleBase.MyTransform.position += a * (time * 2f);
					}
				}
			}
		}
	}

	// Token: 0x04000EBB RID: 3771
	private float timer;
}
