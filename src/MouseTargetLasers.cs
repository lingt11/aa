using System;
using UnityEngine;
using UnityEngine.InputSystem;

// Token: 0x02000400 RID: 1024
public class MouseTargetLasers : MonoBehaviour
{
	// Token: 0x0600177F RID: 6015 RVA: 0x00092D63 File Offset: 0x00090F63
	private void Start()
	{
		this.anim = base.GetComponent<Animator>();
	}

	// Token: 0x06001780 RID: 6016 RVA: 0x00092D74 File Offset: 0x00090F74
	private void Update()
	{
		Mouse current = Mouse.current;
		if (current != null)
		{
			if (current.leftButton.wasPressedThisFrame)
			{
				this.startWavePS.Emit(1);
				this.startParticles.Emit(this.startParticlesCount);
			}
			this.anim.SetBool("Fire", current.leftButton.isPressed);
			if (current.rightButton.wasPressedThisFrame)
			{
				this.anim.SetBool("Fire", true);
				this.startWavePS.Emit(1);
				this.startParticles.Emit(this.startParticlesCount);
				Object.Instantiate<GameObject>(this.laserShotPrefab, this.laserShotPosition.position, base.transform.rotation);
			}
		}
	}

	// Token: 0x06001781 RID: 6017 RVA: 0x00092E30 File Offset: 0x00091030
	private void FixedUpdate()
	{
		Mouse current = Mouse.current;
		Vector3 pos = Vector3.zero;
		if (current != null)
		{
			pos = current.position.ReadValue();
		}
		else
		{
			pos = new Vector3((float)Screen.width / 2f, (float)Screen.height / 2f, 0f);
		}
		RaycastHit raycastHit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(pos), out raycastHit))
		{
			this.mouseWorldPosition = raycastHit.point;
		}
		Quaternion b = Quaternion.LookRotation(this.mouseWorldPosition - base.transform.position);
		base.transform.rotation = Quaternion.Lerp(base.transform.rotation, b, this.speed * Time.deltaTime);
	}

	// Token: 0x0400165E RID: 5726
	public Transform laserShotPosition;

	// Token: 0x0400165F RID: 5727
	public float speed = 1f;

	// Token: 0x04001660 RID: 5728
	public ParticleSystem startWavePS;

	// Token: 0x04001661 RID: 5729
	public ParticleSystem startParticles;

	// Token: 0x04001662 RID: 5730
	public int startParticlesCount = 100;

	// Token: 0x04001663 RID: 5731
	public GameObject laserShotPrefab;

	// Token: 0x04001664 RID: 5732
	private Vector3 mouseWorldPosition;

	// Token: 0x04001665 RID: 5733
	private Animator anim;
}
