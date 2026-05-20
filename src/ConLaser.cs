using System;
using UnityEngine;
using UnityEngine.InputSystem;

// Token: 0x020003FA RID: 1018
public class ConLaser : MonoBehaviour
{
	// Token: 0x06001769 RID: 5993 RVA: 0x00091F08 File Offset: 0x00090108
	private void Start()
	{
		this.globalProgress = 1f;
		this.lr = base.GetComponent<LineRenderer>();
		if (this.lr == null)
		{
			Debug.LogError("LineRenderer component is missing on the GameObject.");
			return;
		}
		this.lr.positionCount = this.segmentCount;
		this.resultVectors = new Vector3[this.segmentCount + 1];
		for (int i = 0; i < this.segmentCount + 1; i++)
		{
			this.resultVectors[i] = base.transform.forward;
		}
	}

	// Token: 0x0600176A RID: 5994 RVA: 0x00091F94 File Offset: 0x00090194
	private void Update()
	{
		for (int i = this.segmentCount - 1; i > 0; i--)
		{
			this.resultVectors[i] = this.resultVectors[i - 1];
		}
		this.resultVectors[0] = base.transform.forward;
		this.resultVectors[this.segmentCount] = this.resultVectors[this.segmentCount - 1];
		float num = this.maxLength / (float)this.segmentCount;
		this.currentPosition = new Vector3(0f, 0f, 0f);
		for (int j = 0; j < this.segmentCount; j++)
		{
			this.currentPosition = base.transform.position;
			for (int k = 0; k < j; k++)
			{
				this.currentPosition += this.resultVectors[k] * num;
			}
			this.lr.SetPosition(j, this.currentPosition);
		}
		for (int l = 0; l < this.segmentCount; l++)
		{
			this.currentPosition = base.transform.position;
			for (int m = 0; m < l; m++)
			{
				this.currentPosition += this.resultVectors[m] * num;
			}
			RaycastHit raycastHit;
			if (Physics.Raycast(this.currentPosition, this.resultVectors[l], out raycastHit, num))
			{
				this.hitPosition = this.currentPosition + this.resultVectors[l] * raycastHit.distance;
				this.hitPosition = Vector3.MoveTowards(this.hitPosition, base.transform.position, this.moveHitToSource);
				if (this.hitEffect != null)
				{
					this.hitEffect.transform.position = this.hitPosition;
				}
				this.dist = Vector3.Distance(this.hitPosition, base.transform.position);
				break;
			}
		}
		if (this.hitEffect != null && this.hitPsArray != null)
		{
			if (this.globalProgress < 0.75f)
			{
				foreach (ParticleSystem particleSystem in this.hitPsArray)
				{
					if (particleSystem != null)
					{
						this.pl.enabled = true;
						particleSystem.emission.enabled = true;
					}
				}
			}
			else
			{
				foreach (ParticleSystem particleSystem2 in this.hitPsArray)
				{
					if (particleSystem2 != null)
					{
						this.pl.enabled = false;
						particleSystem2.emission.enabled = false;
					}
				}
			}
		}
		base.GetComponent<Renderer>().material.SetFloat("_Distance", this.dist);
		base.GetComponent<Renderer>().material.SetVector("_Position", base.transform.position);
		Mouse current = Mouse.current;
		if (current != null)
		{
			if (current.leftButton.isPressed)
			{
				this.globalProgress = 0f;
			}
			if (current.leftButton.wasPressedThisFrame && this.hitEffect != null && this.hitPsArray.Length > 1 && this.hitPsArray[1] != null)
			{
				this.hitPsArray[1].Emit(100);
			}
		}
		if (this.globalProgress <= 1f)
		{
			this.globalProgress += Time.deltaTime * this.globalProgressSpeed;
		}
		if (this.pl != null)
		{
			this.pl.intensity = this.shaderProgressCurve.Evaluate(this.globalProgress) * 1.5f;
		}
		float value = this.shaderProgressCurve.Evaluate(this.globalProgress);
		base.GetComponent<Renderer>().material.SetFloat("_Progress", value);
		if (this.meshRenderer1 != null && this.meshRenderer2 != null)
		{
			this.meshRenderer1.material.SetFloat("_Progress", value);
			this.meshRenderer2.material.SetFloat("_Progress", value);
		}
		float widthMultiplier = this.lineWidthCurve.Evaluate(this.globalProgress);
		this.lr.widthMultiplier = widthMultiplier;
	}

	// Token: 0x04001620 RID: 5664
	public float maxLength = 16f;

	// Token: 0x04001621 RID: 5665
	public GameObject hitEffect;

	// Token: 0x04001622 RID: 5666
	public Renderer meshRenderer1;

	// Token: 0x04001623 RID: 5667
	public Renderer meshRenderer2;

	// Token: 0x04001624 RID: 5668
	public ParticleSystem[] hitPsArray;

	// Token: 0x04001625 RID: 5669
	public int segmentCount = 32;

	// Token: 0x04001626 RID: 5670
	public float globalProgressSpeed = 1f;

	// Token: 0x04001627 RID: 5671
	public AnimationCurve shaderProgressCurve;

	// Token: 0x04001628 RID: 5672
	public AnimationCurve lineWidthCurve;

	// Token: 0x04001629 RID: 5673
	public Light pl;

	// Token: 0x0400162A RID: 5674
	public float moveHitToSource;

	// Token: 0x0400162B RID: 5675
	private LineRenderer lr;

	// Token: 0x0400162C RID: 5676
	private Vector3[] resultVectors;

	// Token: 0x0400162D RID: 5677
	private float dist;

	// Token: 0x0400162E RID: 5678
	private float globalProgress;

	// Token: 0x0400162F RID: 5679
	private Vector3 hitPosition;

	// Token: 0x04001630 RID: 5680
	private Vector3 currentPosition;
}
