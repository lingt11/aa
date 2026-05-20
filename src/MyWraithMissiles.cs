using System;
using UnityEngine;

// Token: 0x020002EB RID: 747
public class MyWraithMissiles : MonoBehaviour
{
	// Token: 0x06001144 RID: 4420 RVA: 0x00063CD1 File Offset: 0x00061ED1
	private void Start()
	{
		this.myTransform = base.transform;
		this.anim = base.GetComponent<Animator>();
	}

	// Token: 0x06001145 RID: 4421 RVA: 0x00063CEC File Offset: 0x00061EEC
	public void Init(RoleBase attackRole, RoleBase trackRoleValue)
	{
		this.trackRole = trackRoleValue;
		ParticleCollisionController particleCollisionController;
		if (this.smallMissiles.gameObject.TryGetComponent<ParticleCollisionController>(out particleCollisionController))
		{
			particleCollisionController.attackRole = attackRole;
		}
		ParticleCollisionController particleCollisionController2;
		if (this.bigMissileOne.gameObject.TryGetComponent<ParticleCollisionController>(out particleCollisionController2))
		{
			particleCollisionController2.attackRole = attackRole;
		}
	}

	// Token: 0x06001146 RID: 4422 RVA: 0x00063D38 File Offset: 0x00061F38
	public void StartSmall()
	{
		this.smallMissiles.emission.enabled = true;
		this.anim.SetBool("Fire", true);
	}

	// Token: 0x06001147 RID: 4423 RVA: 0x00063D6A File Offset: 0x00061F6A
	public void CreateSmall()
	{
		this.startWavePS.Emit(1);
		this.startParticles.Emit(this.smallMissilesCount);
	}

	// Token: 0x06001148 RID: 4424 RVA: 0x00063D8C File Offset: 0x00061F8C
	public void EndSmall()
	{
		this.smallMissiles.emission.enabled = false;
		this.anim.SetBool("Fire", false);
	}

	// Token: 0x06001149 RID: 4425 RVA: 0x00063DC0 File Offset: 0x00061FC0
	public void StartBig()
	{
		this.smallMissiles.emission.enabled = true;
		this.anim.SetBool("Fire", true);
	}

	// Token: 0x0600114A RID: 4426 RVA: 0x00002D1D File Offset: 0x00000F1D
	public void CreateBig()
	{
	}

	// Token: 0x0600114B RID: 4427 RVA: 0x00063DF4 File Offset: 0x00061FF4
	private void FixedUpdate()
	{
		if (this.trackRole != null)
		{
			Vector3 attackPos = this.trackRole.GetAttackPos();
			if (this.trackRole.IsDead())
			{
				attackPos.y = -0.1f;
			}
			this.ms.position = attackPos;
			Quaternion b = Quaternion.LookRotation(this.ms.position - this.myTransform.position);
			this.myTransform.rotation = Quaternion.Lerp(this.myTransform.rotation, b, this.speed * Time.deltaTime);
		}
	}

	// Token: 0x04000F53 RID: 3923
	public Transform ms;

	// Token: 0x04000F54 RID: 3924
	private RoleBase trackRole;

	// Token: 0x04000F55 RID: 3925
	public float speed = 1f;

	// Token: 0x04000F56 RID: 3926
	public ParticleSystem startWavePS;

	// Token: 0x04000F57 RID: 3927
	public ParticleSystem startParticles;

	// Token: 0x04000F58 RID: 3928
	public ParticleSystem smallMissiles;

	// Token: 0x04000F59 RID: 3929
	public int smallMissilesCount = 100;

	// Token: 0x04000F5A RID: 3930
	public ParticleSystem bigMissileOne;

	// Token: 0x04000F5B RID: 3931
	public ParticleSystem bigMissileTwo;

	// Token: 0x04000F5C RID: 3932
	public ParticleSystem bigMissileThree;

	// Token: 0x04000F5D RID: 3933
	public int bigMissileThreeCount = 6;

	// Token: 0x04000F5E RID: 3934
	private Vector3 mouseWorldPosition;

	// Token: 0x04000F5F RID: 3935
	private Animator anim;

	// Token: 0x04000F60 RID: 3936
	private Transform myTransform;
}
