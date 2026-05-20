using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200008C RID: 140
public class BrotatoWeapon
{
	// Token: 0x06000316 RID: 790 RVA: 0x00014FBB File Offset: 0x000131BB
	public virtual void Clear()
	{
		AssetManager.UnLoadPrefab(this.weaponTransform.gameObject, false);
		Object.Destroy(this.nodeTransform.gameObject);
		this.weaponTransform = null;
		this.nodeTransform = null;
		this.trackRoleBase = null;
	}

	// Token: 0x06000317 RID: 791 RVA: 0x00014FF4 File Offset: 0x000131F4
	public static Vector3 GetWeaponPos(BrotatoWeaponData brotatoWeaponData, int allCount, int index, BrotatoShootType brotatoShootType, RoleBase roleBase, Transform nodeTransform = null)
	{
		Vector2 vector = Vector2.zero;
		Vector3 vector2 = roleBase.MyTransform.forward * (brotatoWeaponData.weaponPosZ + roleBase.RoleModeBase.addRange);
		vector = new Vector2(-vector2.x, -vector2.z);
		vector = Util.GetPointByRadian(vector.x, vector.y, Util.GetSymmetricValue(allCount, index, 45f));
		if (roleBase.RoleModeBase.myAnim != null && roleBase.RoleModeBase.myAnim.isHuman)
		{
			return roleBase.RoleModeBase.myAnim.GetBoneTransform(HumanBodyBones.Spine).position + new Vector3(vector.x, brotatoWeaponData.weaponPosY, vector.y);
		}
		return roleBase.GetAttackPos() + new Vector3(vector.x, brotatoWeaponData.weaponPosY, vector.y);
	}

	// Token: 0x06000318 RID: 792 RVA: 0x000150E0 File Offset: 0x000132E0
	public static float GetMeleeWeaponRotX(RoleBase roleBase)
	{
		if (roleBase.RoleModeBase.myAnim != null && roleBase.RoleModeBase.myAnim.isHuman)
		{
			return -90f - roleBase.RoleModeBase.myAnim.GetBoneTransform(HumanBodyBones.Spine).rotation.eulerAngles.z;
		}
		return 0f;
	}

	// Token: 0x06000319 RID: 793 RVA: 0x00015144 File Offset: 0x00013344
	public static RoleBase GetTrackRoleBase(Transform weaponTransform, float attackDistance, RoleBase roleBase)
	{
		if (roleBase.isLocalPlayer && GameHelperClient.ClickTrackRole != null && !GameHelperClient.ClickTrackRole.IsDead() && Util.GetV2Distance(weaponTransform.position, GameHelperClient.ClickTrackRole.MyTransform.position) < attackDistance)
		{
			if (GameHelperClient.isReady)
			{
				EnemyBase enemyBase = GameHelperClient.ClickTrackRole as EnemyBase;
				if (enemyBase == null || enemyBase.enemyType != EnemyType.Dummy)
				{
					return null;
				}
			}
			return GameHelperClient.ClickTrackRole;
		}
		float num = -9999f;
		List<RoleBase> attackRoles = roleBase.GetAttackRoles();
		RoleBase roleBase2 = null;
		int count = attackRoles.Count;
		Vector3 lhs = weaponTransform.position - roleBase.MyTransform.position;
		lhs.y = 0f;
		lhs.Normalize();
		for (int i = 0; i < count; i++)
		{
			RoleBase roleBase3 = attackRoles[i];
			if (roleBase3 != null && !roleBase3.IsDead() && Util.GetV2Distance(weaponTransform.position, roleBase3.MyTransform.position) < attackDistance)
			{
				Vector3 vector = roleBase3.MyTransform.position - weaponTransform.position;
				vector.y = 0f;
				float num2 = Vector3.Dot(lhs, vector.normalized);
				if (num2 > num)
				{
					roleBase2 = roleBase3;
					num = num2;
				}
			}
		}
		if (GameHelperClient.isReady && roleBase2 != null)
		{
			EnemyBase enemyBase2 = roleBase2 as EnemyBase;
			if (enemyBase2 == null || enemyBase2.enemyType != EnemyType.Dummy)
			{
				roleBase2 = null;
			}
		}
		return roleBase2;
	}

	// Token: 0x04000298 RID: 664
	public Transform nodeTransform;

	// Token: 0x04000299 RID: 665
	public Transform weaponTransform;

	// Token: 0x0400029A RID: 666
	public float attackCd;

	// Token: 0x0400029B RID: 667
	public float attackTime;

	// Token: 0x0400029C RID: 668
	public RoleBase trackRoleBase;

	// Token: 0x0400029D RID: 669
	public BrotatoWeaponData brotatoWeaponData;

	// Token: 0x0400029E RID: 670
	public float curAttackSpeed;

	// Token: 0x0400029F RID: 671
	public uint skillId;

	// Token: 0x040002A0 RID: 672
	public int baseDamage;

	// Token: 0x040002A1 RID: 673
	public float damageLevel = 0.5f;

	// Token: 0x040002A2 RID: 674
	public float overTime;
}
