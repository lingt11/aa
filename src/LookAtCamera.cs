using System;
using UnityEngine;

// Token: 0x0200012B RID: 299
public class LookAtCamera : MonoBehaviour
{
	// Token: 0x060005A4 RID: 1444 RVA: 0x00002D1D File Offset: 0x00000F1D
	private void Start()
	{
	}

	// Token: 0x060005A5 RID: 1445 RVA: 0x00020C18 File Offset: 0x0001EE18
	private void Update()
	{
		if (EntityStatic.Get<CameraManager>() == null)
		{
			return;
		}
		if (EntityStatic.Get<CameraManager>().camera == null)
		{
			return;
		}
		if (Game.Camera.transform == null)
		{
			return;
		}
		base.transform.forward = Game.Camera.transform.forward;
	}

	// Token: 0x04000817 RID: 2071
	private bool lockVerticalRotation;

	// Token: 0x04000818 RID: 2072
	private Vector3 rotationOffset = Vector3.zero;
}
