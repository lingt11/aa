using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000002 RID: 2
public class ParticleCollisionInstance : MonoBehaviour
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	public List<Vector3> CheckPosAry
	{
		get
		{
			return this.checkPosAry;
		}
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
	private void Start()
	{
		this.part = base.GetComponent<ParticleSystem>();
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00002066 File Offset: 0x00000266
	public void Init(float rangeValue)
	{
		this.range = rangeValue;
		this.checkPosAry.Clear();
	}

	// Token: 0x06000004 RID: 4 RVA: 0x0000207C File Offset: 0x0000027C
	private void OnParticleCollision(GameObject other)
	{
		int num = this.part.GetCollisionEvents(other, this.collisionEvents);
		for (int i = 0; i < num; i++)
		{
			Vector3 vector = this.collisionEvents[i].intersection + this.collisionEvents[i].normal * this.Offset;
			Transform transform = Game.EffectManager.PlayEffect(this.EffectsOnCollision, 2f, vector, 1f);
			transform.localScale = Vector3.one * this.range;
			this.checkPosAry.Add(vector);
			transform.rotation = Quaternion.identity;
			if (!this.UseWorldSpacePosition)
			{
				transform.parent = base.transform;
			}
			if (this.UseFirePointRotation)
			{
				transform.LookAt(base.transform.position);
			}
			else if (this.rotationOffset != Vector3.zero && this.useOnlyRotationOffset)
			{
				transform.rotation = Quaternion.Euler(this.rotationOffset);
			}
			else
			{
				transform.LookAt(this.collisionEvents[i].intersection + this.collisionEvents[i].normal);
				transform.rotation *= Quaternion.Euler(this.rotationOffset);
			}
		}
	}

	// Token: 0x04000001 RID: 1
	public string EffectsOnCollision;

	// Token: 0x04000002 RID: 2
	public float DestroyTimeDelay = 5f;

	// Token: 0x04000003 RID: 3
	public bool UseWorldSpacePosition;

	// Token: 0x04000004 RID: 4
	public float Offset;

	// Token: 0x04000005 RID: 5
	public Vector3 rotationOffset = new Vector3(0f, 0f, 0f);

	// Token: 0x04000006 RID: 6
	public bool useOnlyRotationOffset = true;

	// Token: 0x04000007 RID: 7
	public bool UseFirePointRotation;

	// Token: 0x04000008 RID: 8
	public bool DestoyMainEffect = true;

	// Token: 0x04000009 RID: 9
	private ParticleSystem part;

	// Token: 0x0400000A RID: 10
	private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

	// Token: 0x0400000B RID: 11
	private ParticleSystem ps;

	// Token: 0x0400000C RID: 12
	private List<Vector3> checkPosAry = new List<Vector3>();

	// Token: 0x0400000D RID: 13
	private float range;
}
