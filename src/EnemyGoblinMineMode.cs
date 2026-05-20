using System;
using UnityEngine;

// Token: 0x02000268 RID: 616
public class EnemyGoblinMineMode : EnemyModeBase
{
	// Token: 0x06000B51 RID: 2897 RVA: 0x0003BDB8 File Offset: 0x00039FB8
	public override void MoveUpdate()
	{
		this.myTimer += Time.deltaTime;
		this.moveAngle = this.myTimer / 55f * 360f;
		float num = this.startMovePos.magnitude;
		if (Mathf.Abs(num - 19f) > 0.1f)
		{
			if (num < 19f)
			{
				num = Mathf.Min(19f, num + 2f * Time.deltaTime);
			}
			else
			{
				num = Mathf.Max(19f, num - 2f * Time.deltaTime);
			}
			this.startMovePos = this.startMovePos.normalized * num;
		}
		else
		{
			this.startMovePos = this.startMovePos.normalized * 19f;
		}
		Vector2 pointByRadian = Util.GetPointByRadian(this.startMovePos.x, this.startMovePos.y, this.moveAngle);
		Vector3 vector = new Vector3(pointByRadian.x, this.enemyBase.MyTransform.position.y, pointByRadian.y);
		float v2Angle = this.enemyBase.GetV2Angle(vector);
		this.enemyBase.oldRotation = this.enemyBase.MyTransform.localEulerAngles.y;
		this.enemyBase.PingHuaZhuanShen(v2Angle, 2f);
		this.enemyBase.MyTransform.position = Vector3.Lerp(vector, new Vector3(vector.x, 2.5f, vector.z), 5f * Time.deltaTime);
		if (this.myTimer > 55f)
		{
			this.enemyBase.CmdAutoDead();
		}
	}

	// Token: 0x06000B52 RID: 2898 RVA: 0x0003BF54 File Offset: 0x0003A154
	public override void OnClientInitEnemy()
	{
		base.OnClientInitEnemy();
		this.startMovePos = new Vector2(this.enemyBase.MyTransform.position.x, this.enemyBase.MyTransform.position.z);
		this.moveAngle = 0f;
		this.myTimer = 0f;
		this.enemyBase.deadMoveSpeed = 2f;
	}

	// Token: 0x04000C39 RID: 3129
	private Vector2 startMovePos;

	// Token: 0x04000C3A RID: 3130
	private const float MoveTime = 55f;

	// Token: 0x04000C3B RID: 3131
	private float moveAngle;

	// Token: 0x04000C3C RID: 3132
	private const float MoveRange = 19f;

	// Token: 0x04000C3D RID: 3133
	private float myTimer;
}
