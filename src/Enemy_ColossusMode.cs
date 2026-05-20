using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000279 RID: 633
public class Enemy_ColossusMode : EnemyMeleeMode
{
	// Token: 0x06000BD0 RID: 3024 RVA: 0x00040962 File Offset: 0x0003EB62
	public override void OnStartSkill()
	{
		base.OnStartSkill();
		this.enemyBase.isCheckAttack = false;
		this.isPlayerEffect = false;
		this.skillIndex = 0;
		this.isCanRound = true;
	}

	// Token: 0x06000BD1 RID: 3025 RVA: 0x0004098C File Offset: 0x0003EB8C
	public override void UpdateSkill1()
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
		else
		{
			if (!this.enemyBase.isCheckAttack && this.enemyBase.timer > 2.227273f)
			{
				this.enemyBase.isCheckAttack = true;
				Game.CameraManager.ShakeCameraByPos(this.roleBase.MyTransform.position, 0.1f, 0.75f, 15, false);
				if (this.skillIndex < 3)
				{
					this.roleBase.timer = 0f;
					this.roleBase.ResetAnim();
					this.roleBase.PlayAni(AnimDefine.Skill, 1.5f, 0.1f);
					Game.AudioManager.PlaySkillAudio(this.skillSoundType, this.roleBase.MyTransform.position);
					this.enemyBase.isCheckAttack = false;
					this.isPlayerEffect = false;
				}
			}
			if (this.enemyBase.timer > 0.727273f)
			{
				if (!this.isPlayerEffect)
				{
					this.isPlayerEffect = true;
					if (this.enemyBase.hasAuthority)
					{
						float range = Game.GameData.ActiveSkillDataDic[ActiveSkillEnum.RockFall].range;
						int count = this.playerMoves.Count;
						Enemy_ColossusMode.RockFallSkillType rockFallSkillType = Enemy_ColossusMode.RockFallSkillType.Track;
						Vector3 position = this.roleBase.MyTransform.position;
						for (int i = 0; i < count; i++)
						{
							Enemy_ColossusMode.PlayerMoveData value = this.playerMoves.ElementAt(i).Value;
							if (value.roleBase != null)
							{
								float distanceV = value.roleBase.GetDistanceV2(position);
								float num = range * this.skillIndexMinDis[this.skillIndex];
								float num2 = num + range + range * this.skillIndexDisAdd[this.skillIndex];
								if (this.isCanRound && distanceV < num2 && distanceV > num)
								{
									rockFallSkillType = Enemy_ColossusMode.RockFallSkillType.Round;
									break;
								}
							}
						}
						if (rockFallSkillType == Enemy_ColossusMode.RockFallSkillType.Track)
						{
							for (int j = 0; j < count; j++)
							{
								Enemy_ColossusMode.PlayerMoveData value2 = this.playerMoves.ElementAt(j).Value;
								if (value2.roleBase != null && value2.roleBase.GetDistanceV2(position) <= 22.5f)
								{
									Vector3 position2 = value2.roleBase.MyTransform.position;
									Vector3 a = Vector3.zero;
									if (!Mathf.Approximately(position2.x, value2.lastPos.x) || !Mathf.Approximately(position2.z, value2.lastPos.z))
									{
										a = (position2 - value2.lastPos) / (Time.time - value2.lastTime);
									}
									float d = 1f;
									Vector3 pos = position2 + d * a;
									this.enemyBase.CmdPlayTipSector(pos, range, 1.5f);
									this.enemyBase.CmdCreateSkill(ActiveSkillEnum.RockFall, pos, 0f, -1, 0);
								}
							}
						}
						else if (rockFallSkillType == Enemy_ColossusMode.RockFallSkillType.Round)
						{
							int num3 = this.skillIndexNum[this.skillIndex];
							for (int k = 0; k < num3; k++)
							{
								Vector2 pointByRadian = Util.GetPointByRadian(0f, range * (float)(this.skillIndex + 1), 360f / (float)num3 * (float)k);
								Vector3 pos2 = new Vector3(position.x + pointByRadian.x, position.y, position.z + pointByRadian.y);
								this.enemyBase.CmdPlayTipSector(pos2, range, 1.5f);
								this.enemyBase.CmdCreateSkill(ActiveSkillEnum.RockFall, pos2, 0f, -1, 0);
							}
						}
						this.skillIndex++;
						return;
					}
				}
				else if (this.enemyBase.timer < 0.62727284f && this.enemyBase.hasAuthority)
				{
					this.enemyBase.TrackRotation(3f);
				}
			}
		}
	}

	// Token: 0x06000BD2 RID: 3026 RVA: 0x00040DA4 File Offset: 0x0003EFA4
	public override void UpdateEvent()
	{
		base.UpdateEvent();
		if (this.enemyBase.hasAuthority)
		{
			this.lastCheckTime -= Time.deltaTime;
			if (this.lastCheckTime < 0f)
			{
				this.lastCheckTime = 0.15f;
				List<RoleBase> attackRoles = this.enemyBase.GetAttackRoles();
				int count = attackRoles.Count;
				for (int i = 0; i < count; i++)
				{
					RoleBase roleBase = attackRoles[i];
					if (roleBase != null)
					{
						Enemy_ColossusMode.PlayerMoveData value = default(Enemy_ColossusMode.PlayerMoveData);
						value.lastTime = Time.time;
						value.lastPos = roleBase.MyTransform.position;
						value.roleBase = roleBase;
						this.playerMoves[roleBase.netId] = value;
					}
				}
			}
		}
	}

	// Token: 0x04000CA3 RID: 3235
	private const float Skill1Time = 3.327273f;

	// Token: 0x04000CA4 RID: 3236
	private const float Skill1MaxDistance = 22.5f;

	// Token: 0x04000CA5 RID: 3237
	private bool isPlayerEffect;

	// Token: 0x04000CA6 RID: 3238
	private Dictionary<uint, Enemy_ColossusMode.PlayerMoveData> playerMoves = new Dictionary<uint, Enemy_ColossusMode.PlayerMoveData>();

	// Token: 0x04000CA7 RID: 3239
	private float lastCheckTime;

	// Token: 0x04000CA8 RID: 3240
	private int skillIndex;

	// Token: 0x04000CA9 RID: 3241
	private readonly int[] skillIndexNum = new int[]
	{
		4,
		6,
		10
	};

	// Token: 0x04000CAA RID: 3242
	private readonly float[] skillIndexMinDis = new float[]
	{
		0f,
		1f,
		1.3f
	};

	// Token: 0x04000CAB RID: 3243
	private readonly float[] skillIndexDisAdd = new float[]
	{
		0.5f,
		1f,
		1f
	};

	// Token: 0x04000CAC RID: 3244
	private bool isCanRound;

	// Token: 0x0200027A RID: 634
	private struct PlayerMoveData
	{
		// Token: 0x04000CAD RID: 3245
		public float lastTime;

		// Token: 0x04000CAE RID: 3246
		public Vector3 lastPos;

		// Token: 0x04000CAF RID: 3247
		public RoleBase roleBase;
	}

	// Token: 0x0200027B RID: 635
	private enum RockFallSkillType
	{
		// Token: 0x04000CB1 RID: 3249
		Track,
		// Token: 0x04000CB2 RID: 3250
		Round
	}
}
