using System;
using UnityEngine;

// Token: 0x02000408 RID: 1032
public class SineCameraControllerTopDownEffects : MonoBehaviour
{
	// Token: 0x0600179C RID: 6044 RVA: 0x000935E5 File Offset: 0x000917E5
	private void Start()
	{
		this.rotation = base.gameObject.transform.localRotation;
		this.mouseAxisToVector = new Vector3(0f, 0f, 0f);
	}

	// Token: 0x0600179D RID: 6045 RVA: 0x00093618 File Offset: 0x00091818
	private void Update()
	{
		if (Input.GetAxis("Mouse ScrollWheel") > 0f)
		{
			if (this.closeFar < 1f)
			{
				this.closeFar += 0.1f;
			}
			if (this.closeFar > 1f)
			{
				this.closeFar = 1f;
			}
		}
		else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
		{
			if (this.closeFar > 0f)
			{
				this.closeFar -= 0.1f;
			}
			if (this.closeFar < 0f)
			{
				this.closeFar = 0f;
			}
		}
		this.closeFarLerp = Mathf.Lerp(this.closeFarLerp, this.closeFar, Time.deltaTime * this.scrollSpeed);
		this.camera.transform.position = Vector3.Lerp(this.farPivot.position, this.basePivot.position, this.closeFarLerp);
		if (Input.GetMouseButton(0))
		{
			this.rotationPossible = true;
		}
		else
		{
			this.rotationPossible = false;
		}
		if (this.rotationPossible)
		{
			this.rotation = base.gameObject.transform.localRotation;
			this.x = this.rotation.eulerAngles.x + Input.GetAxis("Mouse Y") * this.rotationAmount;
			if (this.x > this.maximumAngle && this.x < 180f)
			{
				this.x = this.maximumAngle;
			}
			if (this.x < 340f && this.x > 180f)
			{
				this.x = 340f;
			}
			this.y = this.rotation.eulerAngles.y + Input.GetAxis("Mouse X") * this.rotationAmount;
			this.mouseAxisToVector.Set(this.x, this.y, 0f);
			this.rotation.eulerAngles = this.mouseAxisToVector;
			base.gameObject.transform.localRotation = this.rotation;
		}
	}

	// Token: 0x04001687 RID: 5767
	public Camera camera;

	// Token: 0x04001688 RID: 5768
	public Transform basePivot;

	// Token: 0x04001689 RID: 5769
	public Transform farPivot;

	// Token: 0x0400168A RID: 5770
	public float scrollSpeed = 10f;

	// Token: 0x0400168B RID: 5771
	public float rotationSpeed = 10f;

	// Token: 0x0400168C RID: 5772
	public float rotationAmount = 2f;

	// Token: 0x0400168D RID: 5773
	[Range(10f, 40f)]
	public float maximumAngle = 20f;

	// Token: 0x0400168E RID: 5774
	private float closeFar = 0.5f;

	// Token: 0x0400168F RID: 5775
	private float closeFarLerp = 0.5f;

	// Token: 0x04001690 RID: 5776
	private Vector3 mouseAxisToVector;

	// Token: 0x04001691 RID: 5777
	private float x;

	// Token: 0x04001692 RID: 5778
	private float y;

	// Token: 0x04001693 RID: 5779
	private Quaternion rotation;

	// Token: 0x04001694 RID: 5780
	private bool rotationPossible;
}
