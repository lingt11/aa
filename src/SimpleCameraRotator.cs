using System;
using UnityEngine;

// Token: 0x020003F7 RID: 1015
public class SimpleCameraRotator : MonoBehaviour
{
	// Token: 0x0600175F RID: 5983 RVA: 0x00091CDD File Offset: 0x0008FEDD
	private void Update()
	{
		base.gameObject.transform.Rotate(0f, Time.deltaTime * this.rotationSpeed, 0f);
	}

	// Token: 0x04001612 RID: 5650
	public float rotationSpeed = -15f;
}
