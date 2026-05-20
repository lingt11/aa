using System;
using UnityEngine;

// Token: 0x02000416 RID: 1046
public class Demo_FreeCam : MonoBehaviour
{
	// Token: 0x060017C3 RID: 6083 RVA: 0x00094355 File Offset: 0x00092555
	private void Start()
	{
		this.SavePosAndRot();
	}

	// Token: 0x060017C4 RID: 6084 RVA: 0x00094360 File Offset: 0x00092560
	private void Update()
	{
		if (!this.doFocus)
		{
			return;
		}
		if (this.cooldown > 0f && Input.GetKeyDown(KeyCode.Mouse0))
		{
			this.FocusObject();
		}
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			this.cooldown = this.doubleClickTime;
		}
		if (Input.GetKey(this.firstUndoKey) && Input.GetKeyDown(this.secondUndoKey))
		{
			this.GoBackToLastPosition();
		}
		this.cooldown -= Time.deltaTime;
	}

	// Token: 0x060017C5 RID: 6085 RVA: 0x000943E0 File Offset: 0x000925E0
	private void LateUpdate()
	{
		Vector3 vector = Vector3.zero;
		if (Input.GetKey(this.forwardKey))
		{
			vector += Vector3.forward * this.moveSpeed;
		}
		if (Input.GetKey(this.backKey))
		{
			vector += Vector3.back * this.moveSpeed;
		}
		if (Input.GetKey(this.leftKey))
		{
			vector += Vector3.left * this.moveSpeed;
		}
		if (Input.GetKey(this.rightKey))
		{
			vector += Vector3.right * this.moveSpeed;
		}
		if (Input.GetKey(this.flatMoveKey))
		{
			float y = base.transform.position.y;
			base.transform.Translate(vector);
			base.transform.position = new Vector3(base.transform.position.x, y, base.transform.position.z);
			return;
		}
		float axis = Input.GetAxis(this.mouseY);
		float axis2 = Input.GetAxis(this.mouseX);
		if (Input.GetKey(this.anchoredMoveKey))
		{
			vector += Vector3.up * axis * -this.moveSpeed;
			vector += Vector3.right * axis2 * -this.moveSpeed;
		}
		if (Input.GetKey(this.anchoredRotateKey))
		{
			base.transform.RotateAround(base.transform.position, base.transform.right, axis * -this.rotationSpeed);
			base.transform.RotateAround(base.transform.position, Vector3.up, axis2 * this.rotationSpeed);
		}
		base.transform.Translate(vector);
		float axis3 = Input.GetAxis(this.zoomAxis);
		base.transform.Translate(Vector3.forward * axis3 * this.zoomSpeed);
	}

	// Token: 0x060017C6 RID: 6086 RVA: 0x000945D8 File Offset: 0x000927D8
	private void FocusObject()
	{
		this.SavePosAndRot();
		RaycastHit raycastHit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, this.focusLimit))
		{
			GameObject gameObject = raycastHit.collider.gameObject;
			Vector3 position = gameObject.transform.position;
			Vector3 size = raycastHit.collider.bounds.size;
			base.transform.position = position + this.GetOffset(position, size);
			base.transform.LookAt(gameObject.transform);
		}
	}

	// Token: 0x060017C7 RID: 6087 RVA: 0x00094662 File Offset: 0x00092862
	private void SavePosAndRot()
	{
		this.prevRot = base.transform.rotation;
		this.prevPos = base.transform.position;
	}

	// Token: 0x060017C8 RID: 6088 RVA: 0x00094686 File Offset: 0x00092886
	private void GoBackToLastPosition()
	{
		base.transform.position = this.prevPos;
		base.transform.rotation = this.prevRot;
	}

	// Token: 0x060017C9 RID: 6089 RVA: 0x000946AC File Offset: 0x000928AC
	private Vector3 GetOffset(Vector3 targetPos, Vector3 targetSize)
	{
		Vector3 vector = targetPos - base.transform.position;
		float num = Mathf.Max(targetSize.x, targetSize.z);
		num = Mathf.Clamp(num, this.minFocusDistance, num);
		return -vector.normalized * num;
	}

	// Token: 0x040016D5 RID: 5845
	[Header("Focus Object")]
	[SerializeField]
	[Tooltip("Enable double-click to focus on objects?")]
	private bool doFocus;

	// Token: 0x040016D6 RID: 5846
	[SerializeField]
	private float focusLimit = 100f;

	// Token: 0x040016D7 RID: 5847
	[SerializeField]
	private float minFocusDistance = 5f;

	// Token: 0x040016D8 RID: 5848
	private float doubleClickTime = 0.15f;

	// Token: 0x040016D9 RID: 5849
	private float cooldown;

	// Token: 0x040016DA RID: 5850
	[Header("Undo - Only undoes the Focus Object - The keys must be pressed in order.")]
	[SerializeField]
	private KeyCode firstUndoKey = KeyCode.LeftControl;

	// Token: 0x040016DB RID: 5851
	[SerializeField]
	private KeyCode secondUndoKey = KeyCode.Z;

	// Token: 0x040016DC RID: 5852
	[Header("Movement")]
	[SerializeField]
	private float moveSpeed = 1f;

	// Token: 0x040016DD RID: 5853
	[SerializeField]
	private float rotationSpeed = 10f;

	// Token: 0x040016DE RID: 5854
	[SerializeField]
	private float zoomSpeed = 10f;

	// Token: 0x040016DF RID: 5855
	private Quaternion prevRot;

	// Token: 0x040016E0 RID: 5856
	private Vector3 prevPos;

	// Token: 0x040016E1 RID: 5857
	[Header("Axes Names")]
	[SerializeField]
	[Tooltip("Otherwise known as the vertical axis")]
	private string mouseY = "Mouse Y";

	// Token: 0x040016E2 RID: 5858
	[SerializeField]
	[Tooltip("AKA horizontal axis")]
	private string mouseX = "Mouse X";

	// Token: 0x040016E3 RID: 5859
	[SerializeField]
	[Tooltip("The axis you want to use for zoom.")]
	private string zoomAxis = "Mouse ScrollWheel";

	// Token: 0x040016E4 RID: 5860
	[Header("Move Keys")]
	[SerializeField]
	private KeyCode forwardKey = KeyCode.W;

	// Token: 0x040016E5 RID: 5861
	[SerializeField]
	private KeyCode backKey = KeyCode.S;

	// Token: 0x040016E6 RID: 5862
	[SerializeField]
	private KeyCode leftKey = KeyCode.A;

	// Token: 0x040016E7 RID: 5863
	[SerializeField]
	private KeyCode rightKey = KeyCode.D;

	// Token: 0x040016E8 RID: 5864
	[Header("Flat Move")]
	[Tooltip("Instead of going where the camera is pointed, the camera moves only on the horizontal plane (Assuming you are working in 3D with default preferences).")]
	[SerializeField]
	private KeyCode flatMoveKey = KeyCode.LeftShift;

	// Token: 0x040016E9 RID: 5865
	[Header("Anchored Movement")]
	[Tooltip("By default in scene-view, this is done by right-clicking for rotation or middle mouse clicking for up and down")]
	[SerializeField]
	private KeyCode anchoredMoveKey = KeyCode.Mouse2;

	// Token: 0x040016EA RID: 5866
	[SerializeField]
	private KeyCode anchoredRotateKey = KeyCode.Mouse1;
}
