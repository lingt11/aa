using System;
using UnityEngine;

// Token: 0x020003F3 RID: 1011
public class ControlParticlesSpawner : MonoBehaviour
{
	// Token: 0x0600174D RID: 5965 RVA: 0x00091434 File Offset: 0x0008F634
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag(this.bulletTag))
		{
			Object.Destroy(collision.gameObject);
			this.cps.transform.position = collision.transform.position;
			this.cps.Emit(1);
		}
	}

	// Token: 0x040015EF RID: 5615
	public ParticleSystem cps;

	// Token: 0x040015F0 RID: 5616
	public string bulletTag = "SineBullet";
}
