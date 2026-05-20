using System;
using UnityEngine;
using UnityEngine.InputSystem;

// Token: 0x020003FF RID: 1023
public class MouseTarget : MonoBehaviour
{
	// Token: 0x0600177B RID: 6011 RVA: 0x00092B4B File Offset: 0x00090D4B
	private void Start()
	{
		this.anim = base.GetComponent<Animator>();
	}

	// Token: 0x0600177C RID: 6012 RVA: 0x00092B5C File Offset: 0x00090D5C
	private void Update()
	{
		Mouse current = Mouse.current;
		if (current != null)
		{
			if (current.leftButton.wasPressedThisFrame)
			{
				this.startWavePS.Emit(1);
				this.startParticles.Emit(this.smallMissilesCount);
			}
			if (current.leftButton.isPressed)
			{
				this.smallMissiles.emission.enabled = true;
				this.anim.SetBool("Fire", true);
			}
			else
			{
				this.smallMissiles.emission.enabled = false;
				this.anim.SetBool("Fire", false);
			}
			if (current.rightButton.wasPressedThisFrame)
			{
				this.anim.SetBool("Fire", true);
				this.bigMissileOne.Emit(1);
				if (this.bigMissileTwo)
				{
					this.bigMissileTwo.Emit(1);
				}
				if (this.bigMissileThree)
				{
					this.bigMissileThree.Emit(this.bigMissileThreeCount);
				}
				this.startWavePS.Emit(1);
				this.startParticles.Emit(this.smallMissilesCount);
			}
		}
	}

	// Token: 0x0600177D RID: 6013 RVA: 0x00092C78 File Offset: 0x00090E78
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
		this.ms.position = this.mouseWorldPosition;
	}

	// Token: 0x04001652 RID: 5714
	public Transform ms;

	// Token: 0x04001653 RID: 5715
	public float speed = 1f;

	// Token: 0x04001654 RID: 5716
	public ParticleSystem startWavePS;

	// Token: 0x04001655 RID: 5717
	public ParticleSystem startParticles;

	// Token: 0x04001656 RID: 5718
	public ParticleSystem smallMissiles;

	// Token: 0x04001657 RID: 5719
	public int smallMissilesCount = 100;

	// Token: 0x04001658 RID: 5720
	public ParticleSystem bigMissileOne;

	// Token: 0x04001659 RID: 5721
	public ParticleSystem bigMissileTwo;

	// Token: 0x0400165A RID: 5722
	public ParticleSystem bigMissileThree;

	// Token: 0x0400165B RID: 5723
	public int bigMissileThreeCount = 6;

	// Token: 0x0400165C RID: 5724
	private Vector3 mouseWorldPosition;

	// Token: 0x0400165D RID: 5725
	private Animator anim;
}
