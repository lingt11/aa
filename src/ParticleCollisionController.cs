using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000401 RID: 1025
public class ParticleCollisionController : MonoBehaviour
{
	// Token: 0x06001783 RID: 6019 RVA: 0x00092F03 File Offset: 0x00091103
	private void Start()
	{
		this.ps = base.GetComponent<ParticleSystem>();
		this.colEvents = new List<ParticleCollisionEvent>();
	}

	// Token: 0x06001784 RID: 6020 RVA: 0x00092F1C File Offset: 0x0009111C
	private void OnParticleCollision(GameObject other)
	{
		this.ps.GetCollisionEvents(other, this.colEvents);
		for (int i = 0; i < this.colEvents.Count; i++)
		{
			this.mac.EmitParticleExplosion(this.colEvents[i].intersection, this.big);
		}
		if (this.attackRole == null)
		{
			return;
		}
		Transform parent = other.transform.parent;
		RoleBase roleBase = (parent != null) ? parent.GetComponent<RoleBase>() : null;
		if (roleBase == null)
		{
			roleBase = other.transform.GetComponent<RoleBase>();
		}
		if (roleBase != null && !roleBase.IsDead())
		{
			bool isAttackWeek = this.attackRole.GetIsAttackWeek(AttackType.AttackEffect);
			roleBase.OnHit(this.attackRole, (double)this.attackRole.FinalAttackPower, 0f, AttackType.AttackEffect, isAttackWeek);
		}
	}

	// Token: 0x04001666 RID: 5734
	[HideInInspector]
	public RoleBase attackRole;

	// Token: 0x04001667 RID: 5735
	public MetaAudioController mac;

	// Token: 0x04001668 RID: 5736
	public bool big;

	// Token: 0x04001669 RID: 5737
	private ParticleSystem ps;

	// Token: 0x0400166A RID: 5738
	private List<ParticleCollisionEvent> colEvents;
}
