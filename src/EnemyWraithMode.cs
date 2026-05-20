using System;
using UnityEngine;

// Token: 0x02000278 RID: 632
public class EnemyWraithMode : EnemyTrajectoryMode
{
	// Token: 0x06000BC9 RID: 3017 RVA: 0x00039DB8 File Offset: 0x00037FB8
	public override void OnClientInitEnemy()
	{
		base.OnClientInitEnemy();
	}

	// Token: 0x06000BCA RID: 3018 RVA: 0x000405B4 File Offset: 0x0003E7B4
	public override void OnStartSkill()
	{
		base.OnStartSkill();
		if (!this.enemyBase.hasAuthority)
		{
			return;
		}
		this.enemyBase.GetTrackRole(false, 17f, true, false);
		Vector3 pos = (this.enemyBase.trackRoleBase != null) ? this.enemyBase.trackRoleBase.MyTransform.position : (this.enemyBase.MyTransform.position + this.enemyBase.MyTransform.forward * 5f);
		pos.y = 0.15f;
		this.enemyBase.CmdCreateSkill(ActiveSkillEnum.IceGround, pos, 0f, -1, 0);
	}

	// Token: 0x06000BCB RID: 3019 RVA: 0x00040668 File Offset: 0x0003E868
	public override void UpdateSkill1()
	{
		if (!this.enemyBase.hasAuthority)
		{
			return;
		}
		float deltaTime = Time.deltaTime;
		this.enemyBase.timer += deltaTime;
		if (this.enemyBase.timer > 1.2f / this.enemyBase.AniSpeed)
		{
			this.enemyBase.UpdateRoleState(RoleState.Run);
		}
	}

	// Token: 0x06000BCC RID: 3020 RVA: 0x000406C8 File Offset: 0x0003E8C8
	public override void OnStartSkill2()
	{
		this.roleBase.timer = 0f;
		this.roleBase.PlayAni(AnimDefine.Skill2, 1.5f, 0.1f);
		this.roleBase.isCheckAttack = false;
		Game.AudioManager.PlaySkillAudio(this.skillSoundType, this.roleBase.MyTransform.position);
	}

	// Token: 0x06000BCD RID: 3021 RVA: 0x0004072C File Offset: 0x0003E92C
	public override void UpdateSkill2()
	{
		float deltaTime = Time.deltaTime;
		this.enemyBase.timer += deltaTime;
		if (this.enemyBase.timer > 3.327273f)
		{
			if (this.enemyBase.hasAuthority)
			{
				this.enemyBase.UpdateRoleState(RoleState.Run);
				return;
			}
		}
		else if (this.enemyBase.timer > 0.727273f)
		{
			if (!this.roleBase.isCheckAttack)
			{
				PlayerBase playerBase;
				if (Game.PlayerManagerClient.clientPlayerDic.TryGetValue(this.enemyBase.authorityId, out playerBase))
				{
					if (this.enemyBase.GetDistanceV2(playerBase.MyTransform.position) < 20f)
					{
						this.enemyBase.trackRoleBase = playerBase;
					}
					else
					{
						this.enemyBase.GetTrackRole(false, 20f, false, false);
					}
				}
				else
				{
					this.enemyBase.GetTrackRole(false, 20f, false, false);
				}
				this.roleBase.isCheckAttack = true;
				if (this.myWraithMissiles == null)
				{
					GameObject gameObject = AssetManager.LoadPrefab(EffectDefine.WraithMissiles, null, true);
					gameObject.transform.position = this.createTransform.position;
					gameObject.transform.rotation = this.enemyBase.MyTransform.rotation;
					gameObject.transform.localScale = new Vector3(2f, 2f, 2f);
					this.myWraithMissiles = gameObject.GetComponent<MyWraithMissiles>();
				}
				else
				{
					this.myWraithMissiles.gameObject.SetActive(true);
				}
				if (this.enemyBase.trackRoleBase != null)
				{
					this.myWraithMissiles.Init(this.enemyBase, this.enemyBase.trackRoleBase);
				}
			}
			if (this.createIndex < 5 && this.enemyBase.timer > 0.727273f + (float)this.createIndex * 0.15f + 0.15f)
			{
				this.createIndex++;
				if (this.myWraithMissiles != null)
				{
					this.myWraithMissiles.CreateBig();
					return;
				}
			}
		}
		else if (this.enemyBase.timer < 0.62727284f && this.enemyBase.hasAuthority)
		{
			this.enemyBase.TrackRotation(3f);
		}
	}

	// Token: 0x06000BCE RID: 3022 RVA: 0x0003A74E File Offset: 0x0003894E
	public override void OnExitSkill2()
	{
		base.OnExitSkill2();
	}

	// Token: 0x04000C9E RID: 3230
	private const float skill1Time = 3.327273f;

	// Token: 0x04000C9F RID: 3231
	private MyWraithMissiles myWraithMissiles;

	// Token: 0x04000CA0 RID: 3232
	private int createIndex;

	// Token: 0x04000CA1 RID: 3233
	private const int CreateNum = 5;

	// Token: 0x04000CA2 RID: 3234
	private const float CreateOffset = 0.15f;
}
