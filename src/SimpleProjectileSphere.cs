using System;
using UnityEngine;

// Token: 0x020003F8 RID: 1016
public class SimpleProjectileSphere : MonoBehaviour
{
	// Token: 0x06001761 RID: 5985 RVA: 0x00091D18 File Offset: 0x0008FF18
	private void Start()
	{
		this.rb = base.GetComponent<Rigidbody>();
		this.rb.AddForce(base.transform.forward * this.force);
		Object.Destroy(base.gameObject, 2.5f);
	}

	// Token: 0x06001762 RID: 5986 RVA: 0x00002D1D File Offset: 0x00000F1D
	private void Update()
	{
	}

	// Token: 0x04001613 RID: 5651
	public float force;

	// Token: 0x04001614 RID: 5652
	private Rigidbody rb;
}
