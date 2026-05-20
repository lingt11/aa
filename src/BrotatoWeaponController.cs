using System;
using System.Collections.Generic;
using Tiny;
using UnityEngine;

// Token: 0x0200028B RID: 651
public class BrotatoWeaponController
{
	// Token: 0x06000C28 RID: 3112 RVA: 0x00044D28 File Offset: 0x00042F28
	public void AddBrotatoWeapon(BrotatoWeaponType brotatoWeaponType, uint skillId, RoleBase roleBase, float[] skillValues, int grade)
	{
		this.attackRoleBase = roleBase;
		BrotatoWeaponData brotatoWeaponData = Util.GetSOBrotatoWeaponConfig().GetBrotatoWeaponData(brotatoWeaponType);
		if (brotatoWeaponData.brotatoShootType == BrotatoShootType.Melee)
		{
			this.AddMeleeBrotatoWeapon(brotatoWeaponData, skillId, skillValues, grade);
			return;
		}
		if (brotatoWeaponData.brotatoShootType == BrotatoShootType.Bullet)
		{
			this.AddBulletBrotatoWeapon(brotatoWeaponData, skillId, skillValues, grade);
			return;
		}
		if (brotatoWeaponData.brotatoShootType == BrotatoShootType.Thrower)
		{
			this.AddThrowerBrotatoWeapon(brotatoWeaponData, skillId, skillValues, grade);
		}
	}

	// Token: 0x06000C29 RID: 3113 RVA: 0x00044D88 File Offset: 0x00042F88
	private void AddMeleeBrotatoWeapon(BrotatoWeaponData brotatoWeaponData, uint skillId, float[] skillValues, int grade)
	{
		MeleeBrotatoWeapon meleeBrotatoWeapon = new MeleeBrotatoWeapon();
		meleeBrotatoWeapon.skillId = skillId;
		if (this.attackRoleBase.hasAuthority)
		{
			meleeBrotatoWeapon.baseDamage = Mathf.RoundToInt(skillValues[0]);
			meleeBrotatoWeapon.damageLevel = skillValues[1];
		}
		meleeBrotatoWeapon.brotatoWeaponData = brotatoWeaponData;
		meleeBrotatoWeapon.nodeTransform = new GameObject
		{
			name = PathDefine.Concat("BrotatoWeaponNode_", meleeBrotatoWeapon.brotatoWeaponData.brotatoWeaponType)
		}.transform;
		meleeBrotatoWeapon.weaponTransform = AssetManager.LoadPrefab(meleeBrotatoWeapon.brotatoWeaponData.weaponPrefabs[grade], null, true).transform;
		meleeBrotatoWeapon.trailTransform = meleeBrotatoWeapon.weaponTransform.GetComponentInChildren<Trail>().gameObject;
		meleeBrotatoWeapon.trailTransform.SetActive(false);
		meleeBrotatoWeapon.weaponTransform.SetParent(meleeBrotatoWeapon.nodeTransform);
		meleeBrotatoWeapon.weaponTransform.localPosition = new Vector3(0f, 0f, 0f);
		meleeBrotatoWeapon.weaponTransform.localRotation = Quaternion.identity;
		meleeBrotatoWeapon.curAttackSpeed = 1f;
		this.meleeBrotatoWeapons.Add(meleeBrotatoWeapon);
	}

	// Token: 0x06000C2A RID: 3114 RVA: 0x00044E98 File Offset: 0x00043098
	private void AddBulletBrotatoWeapon(BrotatoWeaponData brotatoWeaponData, uint skillId, float[] skillValues, int grade)
	{
		BulletBrotatoWeapon bulletBrotatoWeapon = new BulletBrotatoWeapon();
		bulletBrotatoWeapon.skillId = skillId;
		if (this.attackRoleBase.hasAuthority)
		{
			bulletBrotatoWeapon.baseDamage = Mathf.RoundToInt(skillValues[0]);
			bulletBrotatoWeapon.damageLevel = skillValues[1];
		}
		bulletBrotatoWeapon.brotatoWeaponData = brotatoWeaponData;
		bulletBrotatoWeapon.nodeTransform = new GameObject
		{
			name = PathDefine.Concat("BrotatoWeaponNode_", bulletBrotatoWeapon.brotatoWeaponData.brotatoWeaponType)
		}.transform;
		bulletBrotatoWeapon.weaponTransform = AssetManager.LoadPrefab(bulletBrotatoWeapon.brotatoWeaponData.weaponPrefabs[grade], null, true).transform;
		bulletBrotatoWeapon.weaponTransform.SetParent(bulletBrotatoWeapon.nodeTransform);
		bulletBrotatoWeapon.weaponTransform.localPosition = new Vector3(0f, 0f, 0f);
		bulletBrotatoWeapon.weaponTransform.localRotation = Quaternion.identity;
		bulletBrotatoWeapon.curAttackSpeed = 1f;
		this.bulletBrotatoWeapons.Add(bulletBrotatoWeapon);
	}

	// Token: 0x06000C2B RID: 3115 RVA: 0x00044F88 File Offset: 0x00043188
	private void AddThrowerBrotatoWeapon(BrotatoWeaponData brotatoWeaponData, uint skillId, float[] skillValues, int grade)
	{
		ThrowerBrotatoWeapon throwerBrotatoWeapon = new ThrowerBrotatoWeapon();
		throwerBrotatoWeapon.skillId = skillId;
		if (this.attackRoleBase.hasAuthority)
		{
			throwerBrotatoWeapon.baseDamage = Mathf.RoundToInt(skillValues[0]);
			throwerBrotatoWeapon.damageLevel = skillValues[1];
		}
		throwerBrotatoWeapon.brotatoWeaponData = brotatoWeaponData;
		throwerBrotatoWeapon.nodeTransform = new GameObject
		{
			name = PathDefine.Concat("BrotatoWeaponNode_", throwerBrotatoWeapon.brotatoWeaponData.brotatoWeaponType)
		}.transform;
		throwerBrotatoWeapon.weaponTransform = AssetManager.LoadPrefab(throwerBrotatoWeapon.brotatoWeaponData.weaponPrefabs[grade], null, true).transform;
		throwerBrotatoWeapon.weaponTransform.SetParent(throwerBrotatoWeapon.nodeTransform);
		throwerBrotatoWeapon.weaponTransform.localPosition = new Vector3(0f, 0f, 0f);
		throwerBrotatoWeapon.weaponTransform.localRotation = Quaternion.identity;
		throwerBrotatoWeapon.curAttackSpeed = 1f;
		this.throwerBrotatoWeapons.Add(throwerBrotatoWeapon);
	}

	// Token: 0x06000C2C RID: 3116 RVA: 0x00045078 File Offset: 0x00043278
	public void UpdateEvent()
	{
		if (this.meleeBrotatoWeapons.Count == 0 && this.bulletBrotatoWeapons.Count == 0 && this.throwerBrotatoWeapons.Count == 0)
		{
			return;
		}
		float deltaTime = Time.deltaTime;
		int count = this.meleeBrotatoWeapons.Count;
		int count2 = this.bulletBrotatoWeapons.Count;
		int count3 = this.throwerBrotatoWeapons.Count;
		int allCount = count + count2 + count3;
		int num = -1;
		for (int i = count - 1; i > -1; i--)
		{
			num++;
			MeleeBrotatoWeapon meleeBrotatoWeapon = this.meleeBrotatoWeapons[i];
			meleeBrotatoWeapon.nodeTransform.position = BrotatoWeapon.GetWeaponPos(meleeBrotatoWeapon.brotatoWeaponData, allCount, num, meleeBrotatoWeapon.brotatoWeaponData.brotatoShootType, this.attackRoleBase, null);
			if (meleeBrotatoWeapon.attackTime > 0f)
			{
				meleeBrotatoWeapon.attackTime -= deltaTime;
				if (meleeBrotatoWeapon.attackTime <= 0f)
				{
					meleeBrotatoWeapon.attackCd = meleeBrotatoWeapon.brotatoWeaponData.attackCd / meleeBrotatoWeapon.curAttackSpeed;
					if (this.attackRoleBase.hasAuthority && meleeBrotatoWeapon.trackRoleBase != null)
					{
						PlayerBase playerBase = this.attackRoleBase as PlayerBase;
						long num2 = (playerBase != null) ? playerBase.GetPlayerNormalAttackPower() : this.attackRoleBase.FinalAttackPower;
						bool isAttackWeek = this.attackRoleBase.GetIsAttackWeek(AttackType.Normal);
						PlayerBase playerBase2 = this.attackRoleBase as PlayerBase;
						float num3 = (playerBase2 != null) ? playerBase2.armedAdd : 0f;
						num2 = (long)Mathf.RoundToInt(((float)meleeBrotatoWeapon.baseDamage + (float)num2 * meleeBrotatoWeapon.damageLevel) * (1f + num3));
						if (this.attackRoleBase.hasAuthority)
						{
							if (this.attackRoleBase.roleType == RoleType.Enemy)
							{
								meleeBrotatoWeapon.trackRoleBase.OnHit(this.attackRoleBase, (double)num2, Util.GetV2Angle(meleeBrotatoWeapon.trackRoleBase.MyTransform.position, this.attackRoleBase.MyTransform.position), AttackType.Normal, isAttackWeek);
							}
							else
							{
								Util.OnLocalPlayerHit(this.attackRoleBase, meleeBrotatoWeapon.trackRoleBase, (double)num2, Util.GetV2Angle(meleeBrotatoWeapon.trackRoleBase.MyTransform.position, this.attackRoleBase.MyTransform.position), AttackType.Normal, isAttackWeek);
							}
						}
					}
					Quaternion endRotation = Quaternion.LerpUnclamped(meleeBrotatoWeapon.startRotation, meleeBrotatoWeapon.endRotation, 2f);
					meleeBrotatoWeapon.startRotation = meleeBrotatoWeapon.nodeTransform.rotation;
					meleeBrotatoWeapon.endRotation = endRotation;
					meleeBrotatoWeapon.overTime = meleeBrotatoWeapon.brotatoWeaponData.attackOverTime / meleeBrotatoWeapon.curAttackSpeed;
				}
				if (meleeBrotatoWeapon.trackRoleBase != null && !meleeBrotatoWeapon.trackRoleBase.IsDead())
				{
					float distanceV = this.attackRoleBase.GetDistanceV2(meleeBrotatoWeapon.trackRoleBase.MyTransform.position);
					meleeBrotatoWeapon.weaponTransform.localPosition = Vector3.Lerp(meleeBrotatoWeapon.weaponTransform.localPosition, new Vector3(0f, 0f, Mathf.Max(0f, distanceV - 0.5f - this.attackRoleBase.RoleModeBase.addRange)), deltaTime * 30f * meleeBrotatoWeapon.curAttackSpeed);
					float num4 = meleeBrotatoWeapon.brotatoWeaponData.attackTime / meleeBrotatoWeapon.curAttackSpeed;
					meleeBrotatoWeapon.nodeTransform.rotation = Quaternion.Lerp(meleeBrotatoWeapon.startRotation, meleeBrotatoWeapon.endRotation, (num4 - meleeBrotatoWeapon.attackTime) / num4);
				}
				else
				{
					meleeBrotatoWeapon.trackRoleBase = BrotatoWeapon.GetTrackRoleBase(meleeBrotatoWeapon.nodeTransform, meleeBrotatoWeapon.brotatoWeaponData.autoAttackDistance + this.attackRoleBase.exAttackDistance, this.attackRoleBase);
				}
				meleeBrotatoWeapon.weaponTransform.localRotation = Quaternion.Lerp(meleeBrotatoWeapon.weaponTransform.localRotation, Quaternion.Euler(0f, 0f, 0f), deltaTime * 30f * meleeBrotatoWeapon.curAttackSpeed);
			}
			else
			{
				meleeBrotatoWeapon.weaponTransform.localRotation = Quaternion.Lerp(meleeBrotatoWeapon.weaponTransform.localRotation, Quaternion.Euler(90f, 0f, 0f), deltaTime * 10f * meleeBrotatoWeapon.curAttackSpeed);
				if (this.attackRoleBase.IsDead())
				{
					meleeBrotatoWeapon.attackCd = meleeBrotatoWeapon.brotatoWeaponData.attackCd / meleeBrotatoWeapon.curAttackSpeed;
				}
				else
				{
					meleeBrotatoWeapon.attackCd -= deltaTime;
				}
				if (meleeBrotatoWeapon.attackCd < 0f && !this.attackRoleBase.IsDead())
				{
					RoleBase trackRoleBase = BrotatoWeapon.GetTrackRoleBase(meleeBrotatoWeapon.nodeTransform, meleeBrotatoWeapon.brotatoWeaponData.autoAttackDistance + this.attackRoleBase.exAttackDistance, this.attackRoleBase);
					if (trackRoleBase != null)
					{
						if (this.attackRoleBase.hasAuthority)
						{
							this.attackRoleBase.OnLocalStartAttack();
							Game.AudioManager.PlayAttackAudio(meleeBrotatoWeapon.brotatoWeaponData.attackHitSound);
						}
						meleeBrotatoWeapon.curAttackSpeed = 1f + (this.attackRoleBase.syncAttackSpeed - 1f) * meleeBrotatoWeapon.brotatoWeaponData.attackSpeedAdd;
						meleeBrotatoWeapon.attackTime = meleeBrotatoWeapon.brotatoWeaponData.attackTime / meleeBrotatoWeapon.curAttackSpeed;
						Vector3 vector = trackRoleBase.MyTransform.position - meleeBrotatoWeapon.nodeTransform.position;
						vector.y = 0f;
						meleeBrotatoWeapon.trackRoleBase = trackRoleBase;
						meleeBrotatoWeapon.startRotation = meleeBrotatoWeapon.nodeTransform.rotation;
						meleeBrotatoWeapon.endRotation = Quaternion.LookRotation(vector.normalized);
						meleeBrotatoWeapon.ShowTrail();
					}
					else if (meleeBrotatoWeapon.overTime > 0f)
					{
						meleeBrotatoWeapon.overTime -= deltaTime;
						float num5 = meleeBrotatoWeapon.brotatoWeaponData.attackOverTime / meleeBrotatoWeapon.curAttackSpeed;
						meleeBrotatoWeapon.nodeTransform.rotation = Quaternion.Lerp(meleeBrotatoWeapon.startRotation, meleeBrotatoWeapon.endRotation, (num5 - meleeBrotatoWeapon.attackTime) / num5);
					}
					else
					{
						Vector3 vector2 = meleeBrotatoWeapon.nodeTransform.position - this.attackRoleBase.MyTransform.position;
						vector2.y = 0f;
						Quaternion quaternion = Quaternion.LookRotation(vector2.normalized);
						meleeBrotatoWeapon.nodeTransform.rotation = Quaternion.Lerp(meleeBrotatoWeapon.nodeTransform.rotation, Quaternion.Euler(BrotatoWeapon.GetMeleeWeaponRotX(this.attackRoleBase), quaternion.eulerAngles.y, quaternion.eulerAngles.z), deltaTime * 15f * meleeBrotatoWeapon.curAttackSpeed);
					}
				}
				else
				{
					if (meleeBrotatoWeapon.overTime > 0f)
					{
						meleeBrotatoWeapon.overTime -= deltaTime;
						if (meleeBrotatoWeapon.overTime <= 0f)
						{
							meleeBrotatoWeapon.HideTrail();
						}
						float num6 = meleeBrotatoWeapon.brotatoWeaponData.attackOverTime / meleeBrotatoWeapon.curAttackSpeed;
						meleeBrotatoWeapon.nodeTransform.rotation = Quaternion.Lerp(meleeBrotatoWeapon.startRotation, meleeBrotatoWeapon.endRotation, (num6 - meleeBrotatoWeapon.attackTime) / num6);
					}
					else
					{
						Vector3 vector3 = meleeBrotatoWeapon.nodeTransform.position - this.attackRoleBase.MyTransform.position;
						vector3.y = 0f;
						Quaternion quaternion2 = Quaternion.LookRotation(vector3.normalized);
						meleeBrotatoWeapon.nodeTransform.rotation = Quaternion.Lerp(meleeBrotatoWeapon.nodeTransform.rotation, Quaternion.Euler(BrotatoWeapon.GetMeleeWeaponRotX(this.attackRoleBase), quaternion2.eulerAngles.y, quaternion2.eulerAngles.z), deltaTime * 15f * meleeBrotatoWeapon.curAttackSpeed);
					}
					meleeBrotatoWeapon.weaponTransform.localPosition = Vector3.Lerp(meleeBrotatoWeapon.weaponTransform.localPosition, new Vector3(0f, 0f, 0f), deltaTime * 10f * meleeBrotatoWeapon.curAttackSpeed);
				}
			}
		}
		for (int j = count2 - 1; j > -1; j--)
		{
			num++;
			BulletBrotatoWeapon bulletBrotatoWeapon = this.bulletBrotatoWeapons[j];
			bulletBrotatoWeapon.nodeTransform.position = BrotatoWeapon.GetWeaponPos(bulletBrotatoWeapon.brotatoWeaponData, allCount, num, bulletBrotatoWeapon.brotatoWeaponData.brotatoShootType, this.attackRoleBase, null);
			int count4 = bulletBrotatoWeapon.flyAttackList.Count;
			List<RoleBase> attackRoles = this.attackRoleBase.GetAttackRoles();
			int count5 = attackRoles.Count;
			for (int k = count4 - 1; k > -1; k--)
			{
				BulletBrotatoWeapon.FlyAttackData flyAttackData = bulletBrotatoWeapon.flyAttackList[k];
				flyAttackData.flyTime += deltaTime;
				if (flyAttackData.flyTime > bulletBrotatoWeapon.brotatoWeaponData.bulletFlyTime)
				{
					AssetManager.UnLoadPrefab(flyAttackData.myTransform.gameObject, false);
					flyAttackData.myTransform = null;
					bulletBrotatoWeapon.flyAttackList.RemoveAt(k);
				}
				else
				{
					flyAttackData.myTransform.position += flyAttackData.myTransform.forward * (deltaTime * bulletBrotatoWeapon.brotatoWeaponData.bulletSpeed);
					for (int l = 0; l < count5; l++)
					{
						RoleBase roleBase = attackRoles[l];
						if (roleBase != null && roleBase.gameObject.activeSelf && !roleBase.IsDead() && Util.NewCheckYuanXing(flyAttackData.myTransform.position, roleBase.MyTransform.position, 0.5f + roleBase.RoleModeBase.addRange, false))
						{
							if (bulletBrotatoWeapon.brotatoWeaponData.brotatoWeaponType == BrotatoWeaponType.RPG)
							{
								float boomRange = bulletBrotatoWeapon.brotatoWeaponData.boomRange;
								float num7 = 1f;
								PlayerBase playerBase3 = this.attackRoleBase as PlayerBase;
								float num8 = boomRange * (num7 + ((playerBase3 != null) ? playerBase3.skillRange : 0f));
								Game.EffectManager.PlayEffectByPos(bulletBrotatoWeapon.brotatoWeaponData.boomEffect, 1.5f, roleBase.MyTransform.position, num8 / 4f);
								if (this.attackRoleBase.hasAuthority)
								{
									bool flag = this.attackRoleBase.roleType == RoleType.Enemy;
									List<RoleBase> attackRoles2 = this.attackRoleBase.GetAttackRoles();
									int count6 = attackRoles2.Count;
									bool isAttackWeek2 = this.attackRoleBase.GetIsAttackWeek(AttackType.Skill);
									Vector3 position = roleBase.MyTransform.position;
									RoleBase roleBase2 = this.attackRoleBase;
									SkillAttribute skillAttribute = SkillAttribute.None;
									double num9 = (double)((float)bulletBrotatoWeapon.baseDamage + bulletBrotatoWeapon.damageLevel * (float)this.attackRoleBase.AGI);
									double num10 = (double)1f;
									PlayerBase playerBase4 = this.attackRoleBase as PlayerBase;
									long passSkillDamage = Util.GetPassSkillDamage(roleBase2, skillAttribute, num9 * (num10 + (double)((playerBase4 != null) ? playerBase4.armedAdd : 0f)), false);
									for (int m = 0; m < count6; m++)
									{
										RoleBase roleBase3 = attackRoles2[m];
										if (roleBase3 != null && roleBase3.gameObject.activeSelf && !roleBase3.IsDead() && Util.NewCheckYuanXing(position, roleBase3.MyTransform.position, num8 + roleBase3.RoleModeBase.addRange, false))
										{
											if (flag)
											{
												roleBase3.OnHit(this.attackRoleBase, (double)passSkillDamage, Util.GetV2Angle(roleBase3.MyTransform.position, position), AttackType.Skill, isAttackWeek2);
											}
											else
											{
												Util.OnLocalPlayerHit(this.attackRoleBase, roleBase3, (double)passSkillDamage, Util.GetV2Angle(roleBase3.MyTransform.position, position), AttackType.Skill, isAttackWeek2);
											}
										}
									}
								}
							}
							else if (this.attackRoleBase.hasAuthority)
							{
								PlayerBase playerBase5 = this.attackRoleBase as PlayerBase;
								long num11 = (playerBase5 != null) ? playerBase5.GetPlayerNormalAttackPower() : this.attackRoleBase.FinalAttackPower;
								bool isAttackWeek3 = this.attackRoleBase.GetIsAttackWeek(AttackType.Normal);
								if (this.attackRoleBase.hasAuthority)
								{
									float num12 = (float)bulletBrotatoWeapon.baseDamage + (float)num11 * bulletBrotatoWeapon.damageLevel;
									float num13 = 1f;
									PlayerBase playerBase6 = this.attackRoleBase as PlayerBase;
									int num14 = Mathf.RoundToInt(num12 * (num13 + ((playerBase6 != null) ? playerBase6.armedAdd : 0f)));
									if (this.attackRoleBase.roleType == RoleType.Enemy)
									{
										roleBase.OnHit(this.attackRoleBase, (double)num14, flyAttackData.myTransform.eulerAngles.y, AttackType.Normal, isAttackWeek3);
									}
									else
									{
										Util.OnLocalPlayerHit(this.attackRoleBase, roleBase, (double)num14, flyAttackData.myTransform.eulerAngles.y, AttackType.Normal, isAttackWeek3);
									}
								}
							}
							AssetManager.UnLoadPrefab(flyAttackData.myTransform.gameObject, false);
							flyAttackData.myTransform = null;
							bulletBrotatoWeapon.flyAttackList.RemoveAt(k);
							break;
						}
					}
				}
			}
			if (bulletBrotatoWeapon.attackTime > 0f)
			{
				bulletBrotatoWeapon.attackTime -= deltaTime;
				if (bulletBrotatoWeapon.attackTime <= 0f)
				{
					bulletBrotatoWeapon.attackCd = bulletBrotatoWeapon.brotatoWeaponData.attackCd / bulletBrotatoWeapon.curAttackSpeed;
					GameObject gameObject = AssetManager.LoadPrefab(bulletBrotatoWeapon.brotatoWeaponData.bulletPrefab, null, true);
					BulletBrotatoWeapon.FlyAttackData flyAttackData2 = new BulletBrotatoWeapon.FlyAttackData();
					flyAttackData2.myTransform = gameObject.transform;
					flyAttackData2.myTransform.position = bulletBrotatoWeapon.weaponTransform.position;
					flyAttackData2.myTransform.rotation = bulletBrotatoWeapon.nodeTransform.rotation;
					bulletBrotatoWeapon.flyAttackList.Add(flyAttackData2);
					bulletBrotatoWeapon.overTime = bulletBrotatoWeapon.brotatoWeaponData.attackOverTime / bulletBrotatoWeapon.curAttackSpeed;
					if (this.attackRoleBase.hasAuthority)
					{
						Game.AudioManager.PlayAttackAudio(bulletBrotatoWeapon.brotatoWeaponData.attackHitSound);
					}
				}
				if (bulletBrotatoWeapon.trackRoleBase != null && !bulletBrotatoWeapon.trackRoleBase.IsDead())
				{
					Vector3 vector4 = bulletBrotatoWeapon.trackRoleBase.MyTransform.position - bulletBrotatoWeapon.nodeTransform.position;
					vector4.y = 0f;
					bulletBrotatoWeapon.nodeTransform.rotation = Quaternion.Lerp(bulletBrotatoWeapon.nodeTransform.rotation, Quaternion.LookRotation(vector4.normalized), deltaTime * 30f * bulletBrotatoWeapon.curAttackSpeed);
				}
				else
				{
					bulletBrotatoWeapon.trackRoleBase = BrotatoWeapon.GetTrackRoleBase(bulletBrotatoWeapon.nodeTransform, bulletBrotatoWeapon.brotatoWeaponData.autoAttackDistance + this.attackRoleBase.exAttackDistance, this.attackRoleBase);
				}
			}
			else
			{
				if (bulletBrotatoWeapon.overTime > 0f)
				{
					bulletBrotatoWeapon.overTime -= deltaTime;
					bulletBrotatoWeapon.weaponTransform.localPosition = Vector3.Lerp(bulletBrotatoWeapon.weaponTransform.localPosition, new Vector3(0f, 0.15f, -0.35f), deltaTime * 50f * bulletBrotatoWeapon.curAttackSpeed);
					bulletBrotatoWeapon.weaponTransform.localRotation = Quaternion.Lerp(bulletBrotatoWeapon.weaponTransform.localRotation, Quaternion.Euler(-60f, 0f, 0f), deltaTime * 50f * bulletBrotatoWeapon.curAttackSpeed);
				}
				else
				{
					bulletBrotatoWeapon.weaponTransform.localRotation = Quaternion.Lerp(bulletBrotatoWeapon.weaponTransform.localRotation, Quaternion.identity, deltaTime * 10f * bulletBrotatoWeapon.curAttackSpeed);
					bulletBrotatoWeapon.weaponTransform.localPosition = Vector3.Lerp(bulletBrotatoWeapon.weaponTransform.localPosition, new Vector3(0f, 0f, 0f), deltaTime * 10f * bulletBrotatoWeapon.curAttackSpeed);
				}
				if (this.attackRoleBase.IsDead())
				{
					bulletBrotatoWeapon.attackCd = bulletBrotatoWeapon.brotatoWeaponData.attackCd / bulletBrotatoWeapon.curAttackSpeed;
				}
				else
				{
					bulletBrotatoWeapon.attackCd -= deltaTime;
				}
				if (bulletBrotatoWeapon.attackCd < 0f && !this.attackRoleBase.IsDead())
				{
					RoleBase trackRoleBase2 = BrotatoWeapon.GetTrackRoleBase(bulletBrotatoWeapon.nodeTransform, bulletBrotatoWeapon.brotatoWeaponData.autoAttackDistance + this.attackRoleBase.exAttackDistance, this.attackRoleBase);
					if (trackRoleBase2 != null)
					{
						if (this.attackRoleBase.hasAuthority)
						{
							this.attackRoleBase.OnLocalStartAttack();
						}
						bulletBrotatoWeapon.curAttackSpeed = 1f + (this.attackRoleBase.syncAttackSpeed - 1f) * bulletBrotatoWeapon.brotatoWeaponData.attackSpeedAdd;
						bulletBrotatoWeapon.attackTime = bulletBrotatoWeapon.brotatoWeaponData.attackTime / bulletBrotatoWeapon.curAttackSpeed;
						Vector3 vector5 = trackRoleBase2.MyTransform.position - bulletBrotatoWeapon.nodeTransform.position;
						vector5.y = 0f;
						bulletBrotatoWeapon.trackRoleBase = trackRoleBase2;
						bulletBrotatoWeapon.nodeTransform.rotation = Quaternion.Lerp(bulletBrotatoWeapon.nodeTransform.rotation, Quaternion.LookRotation(vector5.normalized), deltaTime * 30f * bulletBrotatoWeapon.curAttackSpeed);
					}
					else
					{
						bulletBrotatoWeapon.nodeTransform.rotation = Quaternion.Lerp(bulletBrotatoWeapon.nodeTransform.rotation, Quaternion.LookRotation(this.attackRoleBase.MyTransform.forward), deltaTime * 15f * bulletBrotatoWeapon.curAttackSpeed);
					}
				}
				else if (bulletBrotatoWeapon.overTime <= 0f)
				{
					bulletBrotatoWeapon.nodeTransform.rotation = Quaternion.Lerp(bulletBrotatoWeapon.nodeTransform.rotation, Quaternion.LookRotation(this.attackRoleBase.MyTransform.forward), deltaTime * 15f * bulletBrotatoWeapon.curAttackSpeed);
				}
			}
		}
		for (int n = count3 - 1; n > -1; n--)
		{
			num++;
			ThrowerBrotatoWeapon throwerBrotatoWeapon = this.throwerBrotatoWeapons[n];
			throwerBrotatoWeapon.nodeTransform.position = BrotatoWeapon.GetWeaponPos(throwerBrotatoWeapon.brotatoWeaponData, allCount, num, throwerBrotatoWeapon.brotatoWeaponData.brotatoShootType, this.attackRoleBase, throwerBrotatoWeapon.nodeTransform);
			if (throwerBrotatoWeapon.attackTime > 0f)
			{
				throwerBrotatoWeapon.attackTime -= deltaTime;
				if (throwerBrotatoWeapon.attackTime <= 0f)
				{
					throwerBrotatoWeapon.attackCd = throwerBrotatoWeapon.brotatoWeaponData.attackCd / throwerBrotatoWeapon.curAttackSpeed;
					GameObject gameObject2 = AssetManager.LoadPrefab(throwerBrotatoWeapon.brotatoWeaponData.bulletPrefab, null, true);
					throwerBrotatoWeapon.throwerEffect = gameObject2;
					Transform transform = gameObject2.transform;
					transform.SetParent(throwerBrotatoWeapon.weaponTransform);
					transform.localPosition = new Vector3(0f, 0.2f, 0.65f);
					transform.localRotation = Quaternion.identity;
					float num15 = 1f;
					PlayerBase playerBase7 = this.attackRoleBase as PlayerBase;
					transform.localScale = (num15 + ((playerBase7 != null) ? playerBase7.skillRange : 0f)) * Vector3.one;
					BrotatoWeapon brotatoWeapon = throwerBrotatoWeapon;
					float attackOverTime = throwerBrotatoWeapon.brotatoWeaponData.attackOverTime;
					float num16 = 1f;
					PlayerBase playerBase8 = this.attackRoleBase as PlayerBase;
					brotatoWeapon.overTime = attackOverTime * (num16 + ((playerBase8 != null) ? playerBase8.skillAddTime : 0f));
					throwerBrotatoWeapon.checkOffset = 0.1f;
				}
				if (throwerBrotatoWeapon.trackRoleBase != null && !throwerBrotatoWeapon.trackRoleBase.IsDead())
				{
					Vector3 vector6 = throwerBrotatoWeapon.trackRoleBase.MyTransform.position - throwerBrotatoWeapon.nodeTransform.position;
					vector6.y = 0f;
					throwerBrotatoWeapon.nodeTransform.rotation = Quaternion.Lerp(throwerBrotatoWeapon.nodeTransform.rotation, Quaternion.LookRotation(vector6.normalized), deltaTime * 30f * throwerBrotatoWeapon.curAttackSpeed);
				}
				else
				{
					throwerBrotatoWeapon.trackRoleBase = BrotatoWeapon.GetTrackRoleBase(throwerBrotatoWeapon.nodeTransform, throwerBrotatoWeapon.brotatoWeaponData.autoAttackDistance + this.attackRoleBase.exAttackDistance, this.attackRoleBase);
				}
			}
			else if (throwerBrotatoWeapon.overTime > 0f)
			{
				if (this.attackRoleBase.hasAuthority)
				{
					throwerBrotatoWeapon.checkOffset -= deltaTime;
					if (throwerBrotatoWeapon.checkOffset <= 0f)
					{
						throwerBrotatoWeapon.checkOffset = 0.25f;
						if (this.attackRoleBase.hasAuthority)
						{
							bool flag2 = this.attackRoleBase.roleType == RoleType.Enemy;
							List<RoleBase> attackRoles3 = this.attackRoleBase.GetAttackRoles();
							int count7 = attackRoles3.Count;
							bool isAttackWeek4 = this.attackRoleBase.GetIsAttackWeek(AttackType.Skill);
							Vector3 position2 = throwerBrotatoWeapon.weaponTransform.position;
							RoleBase roleBase4 = this.attackRoleBase;
							SkillAttribute skillAttribute2 = SkillAttribute.Fire;
							double num17 = (double)((float)throwerBrotatoWeapon.baseDamage + throwerBrotatoWeapon.damageLevel * (float)this.attackRoleBase.STR);
							double num18 = (double)1f;
							PlayerBase playerBase9 = this.attackRoleBase as PlayerBase;
							long passSkillDamage2 = Util.GetPassSkillDamage(roleBase4, skillAttribute2, num17 * (num18 + (double)((playerBase9 != null) ? playerBase9.armedAdd : 0f)), false);
							float num19 = 1f;
							PlayerBase playerBase10 = this.attackRoleBase as PlayerBase;
							float num20 = num19 + ((playerBase10 != null) ? playerBase10.skillRange : 0f);
							for (int num21 = 0; num21 < count7; num21++)
							{
								RoleBase roleBase5 = attackRoles3[num21];
								if (roleBase5 != null && roleBase5.gameObject.activeSelf && !roleBase5.IsDead() && Util.NewCheckJuXing(position2, throwerBrotatoWeapon.nodeTransform.eulerAngles.y, 2.25f * num20, 8f * num20, roleBase5.MyTransform.position, roleBase5.RoleModeBase.addRange, false, false) && (Vector3.Distance(roleBase5.MyTransform.position, position2) >= 3.5f * num20 || Util.NewCheckShanXing(position2 - throwerBrotatoWeapon.nodeTransform.forward * (1.5f * num20), roleBase5.MyTransform.position, 25f, 5f * num20 + roleBase5.RoleModeBase.addRange, throwerBrotatoWeapon.nodeTransform.eulerAngles.y, false)))
								{
									if (flag2)
									{
										roleBase5.OnHit(this.attackRoleBase, (double)passSkillDamage2, Util.GetV2Angle(roleBase5.MyTransform.position, position2), AttackType.Skill, isAttackWeek4);
									}
									else
									{
										Util.OnLocalPlayerHit(this.attackRoleBase, roleBase5, (double)passSkillDamage2, Util.GetV2Angle(roleBase5.MyTransform.position, position2), AttackType.Skill, isAttackWeek4);
									}
								}
							}
						}
					}
				}
				if (throwerBrotatoWeapon.trackRoleBase != null && !throwerBrotatoWeapon.trackRoleBase.IsDead())
				{
					Vector3 vector7 = throwerBrotatoWeapon.trackRoleBase.MyTransform.position - throwerBrotatoWeapon.nodeTransform.position;
					vector7.y = 0f;
					throwerBrotatoWeapon.nodeTransform.rotation = Quaternion.Lerp(throwerBrotatoWeapon.nodeTransform.rotation, Quaternion.LookRotation(vector7.normalized), deltaTime * 30f * throwerBrotatoWeapon.curAttackSpeed);
				}
				else
				{
					throwerBrotatoWeapon.trackRoleBase = BrotatoWeapon.GetTrackRoleBase(throwerBrotatoWeapon.nodeTransform, throwerBrotatoWeapon.brotatoWeaponData.autoAttackDistance + this.attackRoleBase.exAttackDistance, this.attackRoleBase);
				}
				throwerBrotatoWeapon.overTime -= deltaTime;
				if (throwerBrotatoWeapon.overTime <= 0f)
				{
					throwerBrotatoWeapon.ClearEffect();
				}
				throwerBrotatoWeapon.weaponTransform.localPosition = Vector3.Lerp(throwerBrotatoWeapon.weaponTransform.localPosition, new Vector3(0f, 0f, -0.35f), deltaTime * 50f * throwerBrotatoWeapon.curAttackSpeed);
			}
			else
			{
				throwerBrotatoWeapon.weaponTransform.localPosition = Vector3.Lerp(throwerBrotatoWeapon.weaponTransform.localPosition, new Vector3(0f, 0f, 0f), deltaTime * 10f * throwerBrotatoWeapon.curAttackSpeed);
				if (this.attackRoleBase.IsDead())
				{
					throwerBrotatoWeapon.attackCd = throwerBrotatoWeapon.brotatoWeaponData.attackCd / throwerBrotatoWeapon.curAttackSpeed;
				}
				else
				{
					throwerBrotatoWeapon.attackCd -= deltaTime;
				}
				if (throwerBrotatoWeapon.attackCd < 0f && !this.attackRoleBase.IsDead())
				{
					RoleBase trackRoleBase3 = BrotatoWeapon.GetTrackRoleBase(throwerBrotatoWeapon.nodeTransform, throwerBrotatoWeapon.brotatoWeaponData.autoAttackDistance + this.attackRoleBase.exAttackDistance, this.attackRoleBase);
					if (trackRoleBase3 != null)
					{
						if (this.attackRoleBase.hasAuthority)
						{
							this.attackRoleBase.OnLocalStartAttack();
							Game.AudioManager.PlayAttackAudio(throwerBrotatoWeapon.brotatoWeaponData.attackHitSound);
						}
						throwerBrotatoWeapon.curAttackSpeed = 1f + (this.attackRoleBase.syncAttackSpeed - 1f) * throwerBrotatoWeapon.brotatoWeaponData.attackSpeedAdd;
						throwerBrotatoWeapon.attackTime = throwerBrotatoWeapon.brotatoWeaponData.attackTime / throwerBrotatoWeapon.curAttackSpeed;
						Vector3 vector8 = trackRoleBase3.MyTransform.position - throwerBrotatoWeapon.nodeTransform.position;
						vector8.y = 0f;
						throwerBrotatoWeapon.trackRoleBase = trackRoleBase3;
						throwerBrotatoWeapon.nodeTransform.rotation = Quaternion.Lerp(throwerBrotatoWeapon.nodeTransform.rotation, Quaternion.LookRotation(vector8.normalized), deltaTime * 30f * throwerBrotatoWeapon.curAttackSpeed);
					}
					else
					{
						throwerBrotatoWeapon.nodeTransform.rotation = Quaternion.Lerp(throwerBrotatoWeapon.nodeTransform.rotation, Quaternion.LookRotation(this.attackRoleBase.MyTransform.forward), deltaTime * 15f * throwerBrotatoWeapon.curAttackSpeed);
					}
				}
				else if (throwerBrotatoWeapon.overTime <= 0f)
				{
					throwerBrotatoWeapon.nodeTransform.rotation = Quaternion.Lerp(throwerBrotatoWeapon.nodeTransform.rotation, Quaternion.LookRotation(this.attackRoleBase.MyTransform.forward), deltaTime * 15f * throwerBrotatoWeapon.curAttackSpeed);
				}
			}
		}
	}

	// Token: 0x06000C2D RID: 3117 RVA: 0x000469E4 File Offset: 0x00044BE4
	public void RemoveBrotatoWeapon(uint skillId)
	{
		int count = this.meleeBrotatoWeapons.Count;
		if (count > 0)
		{
			for (int i = count - 1; i > -1; i--)
			{
				MeleeBrotatoWeapon meleeBrotatoWeapon = this.meleeBrotatoWeapons[i];
				if (meleeBrotatoWeapon.skillId == skillId)
				{
					meleeBrotatoWeapon.Clear();
					this.meleeBrotatoWeapons.RemoveAt(i);
					return;
				}
			}
		}
		count = this.bulletBrotatoWeapons.Count;
		if (count > 0)
		{
			for (int j = count - 1; j > -1; j--)
			{
				BulletBrotatoWeapon bulletBrotatoWeapon = this.bulletBrotatoWeapons[j];
				if (bulletBrotatoWeapon.skillId == skillId)
				{
					bulletBrotatoWeapon.Clear();
					this.bulletBrotatoWeapons.RemoveAt(j);
					return;
				}
			}
		}
		count = this.throwerBrotatoWeapons.Count;
		if (count > 0)
		{
			for (int k = count - 1; k > -1; k--)
			{
				ThrowerBrotatoWeapon throwerBrotatoWeapon = this.throwerBrotatoWeapons[k];
				if (throwerBrotatoWeapon.skillId == skillId)
				{
					throwerBrotatoWeapon.Clear();
					this.throwerBrotatoWeapons.RemoveAt(k);
					return;
				}
			}
		}
	}

	// Token: 0x06000C2E RID: 3118 RVA: 0x00046AD4 File Offset: 0x00044CD4
	public void ClearBrotatoWeapons()
	{
		int count = this.meleeBrotatoWeapons.Count;
		if (count > 0)
		{
			for (int i = count - 1; i > -1; i--)
			{
				this.meleeBrotatoWeapons[i].Clear();
			}
		}
		this.meleeBrotatoWeapons.Clear();
		count = this.bulletBrotatoWeapons.Count;
		if (count > 0)
		{
			for (int j = count - 1; j > -1; j--)
			{
				this.bulletBrotatoWeapons[j].Clear();
			}
		}
		this.bulletBrotatoWeapons.Clear();
		count = this.throwerBrotatoWeapons.Count;
		if (count > 0)
		{
			for (int k = count - 1; k > -1; k--)
			{
				this.throwerBrotatoWeapons[k].Clear();
			}
		}
		this.throwerBrotatoWeapons.Clear();
	}

	// Token: 0x06000C2F RID: 3119 RVA: 0x00046B90 File Offset: 0x00044D90
	public int GetBrotatoWeaponCount()
	{
		int count = this.meleeBrotatoWeapons.Count;
		int count2 = this.bulletBrotatoWeapons.Count;
		int count3 = this.throwerBrotatoWeapons.Count;
		return count + count2 + count3;
	}

	// Token: 0x04000CF0 RID: 3312
	private RoleBase attackRoleBase;

	// Token: 0x04000CF1 RID: 3313
	private List<MeleeBrotatoWeapon> meleeBrotatoWeapons = new List<MeleeBrotatoWeapon>();

	// Token: 0x04000CF2 RID: 3314
	private List<BulletBrotatoWeapon> bulletBrotatoWeapons = new List<BulletBrotatoWeapon>();

	// Token: 0x04000CF3 RID: 3315
	private List<ThrowerBrotatoWeapon> throwerBrotatoWeapons = new List<ThrowerBrotatoWeapon>();
}
