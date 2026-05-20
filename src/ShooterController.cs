using System;
using UnityEngine;

// Token: 0x020003F6 RID: 1014
public class ShooterController : MonoBehaviour
{
	// Token: 0x0600175C RID: 5980 RVA: 0x00002D1D File Offset: 0x00000F1D
	private void Start()
	{
	}

	// Token: 0x0600175D RID: 5981 RVA: 0x00091C20 File Offset: 0x0008FE20
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Object.Instantiate<GameObject>(this.projectile, base.gameObject.transform.position, base.gameObject.transform.rotation);
		}
		RaycastHit raycastHit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit))
		{
			this.mouseWorldPosition = raycastHit.point;
		}
		Quaternion b = Quaternion.LookRotation(this.mouseWorldPosition - base.transform.position);
		base.transform.rotation = Quaternion.Lerp(base.transform.rotation, b, this.rotationSpeed * Time.deltaTime);
	}

	// Token: 0x0400160F RID: 5647
	public GameObject projectile;

	// Token: 0x04001610 RID: 5648
	public float rotationSpeed = 15f;

	// Token: 0x04001611 RID: 5649
	private Vector3 mouseWorldPosition;
}
