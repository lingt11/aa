using System;
using UnityEngine;

// Token: 0x020003FD RID: 1021
public class LaserLine : MonoBehaviour
{
	// Token: 0x06001772 RID: 6002 RVA: 0x00092768 File Offset: 0x00090968
	private void DrawLine()
	{
		float value = this.shaderProgressCurve.Evaluate(this.globalProgress);
		this.lr.material.SetFloat("_Progress", value);
		float widthMultiplier = this.lineWidthCurve.Evaluate(this.globalProgress);
		this.lr.widthMultiplier = widthMultiplier;
	}

	// Token: 0x06001773 RID: 6003 RVA: 0x000927BC File Offset: 0x000909BC
	private void CastLaserRay()
	{
		RaycastHit raycastHit;
		if (Physics.Raycast(base.transform.position, base.transform.forward, out raycastHit, this.maxLength))
		{
			this.HitLength = raycastHit.distance;
			this.positionForExplosion = Vector3.MoveTowards(raycastHit.point, base.transform.position, this.moveHitToSource);
			this.spawnExplosion = true;
			this.particleSpawnPositions = new Vector3[Mathf.RoundToInt(raycastHit.distance * 2f)];
			this.endPoint = raycastHit.point;
		}
		this.lr.SetPosition(0, base.transform.position);
		if (this.HitLength != 0f)
		{
			this.lr.SetPosition(1, base.transform.position + base.transform.forward * this.HitLength);
			return;
		}
		this.lr.SetPosition(1, base.transform.position + base.transform.forward * this.maxLength);
	}

	// Token: 0x06001774 RID: 6004 RVA: 0x000928D8 File Offset: 0x00090AD8
	private void Start()
	{
		this.spawnExplosion = false;
		this.lr = base.GetComponent<LineRenderer>();
		this.HitLength = 0f;
		this.CastLaserRay();
		this.DrawLine();
		if (this.spawnExplosion)
		{
			Object.Instantiate<GameObject>(this.explosionPrefab, this.positionForExplosion, new Quaternion(0f, 0f, 0f, 0f));
		}
		float num = 0f;
		for (int i = 0; i < this.particleSpawnPositions.Length; i++)
		{
			this.particleSpawnPositions[i] = Vector3.Lerp(base.transform.position, this.endPoint, num);
			this.psEmbers.transform.position = this.particleSpawnPositions[i];
			this.psEmbers.Emit(this.trailParticleCount);
			num += 1f / (float)this.particleSpawnPositions.Length;
		}
	}

	// Token: 0x06001775 RID: 6005 RVA: 0x000929BD File Offset: 0x00090BBD
	private void Update()
	{
		if (this.globalProgress < 1f)
		{
			this.globalProgress += Time.deltaTime * this.globalProgressSpeed;
		}
		this.DrawLine();
	}

	// Token: 0x0400163C RID: 5692
	public float maxLength = 1f;

	// Token: 0x0400163D RID: 5693
	public AnimationCurve shaderProgressCurve;

	// Token: 0x0400163E RID: 5694
	public AnimationCurve lineWidthCurve;

	// Token: 0x0400163F RID: 5695
	public float globalProgressSpeed = 0.1f;

	// Token: 0x04001640 RID: 5696
	public GameObject explosionPrefab;

	// Token: 0x04001641 RID: 5697
	public ParticleSystem psEmbers;

	// Token: 0x04001642 RID: 5698
	public int trailParticleCount = 5;

	// Token: 0x04001643 RID: 5699
	public float moveHitToSource = 0.5f;

	// Token: 0x04001644 RID: 5700
	private float AnimationProgress;

	// Token: 0x04001645 RID: 5701
	private float HitLength;

	// Token: 0x04001646 RID: 5702
	private LineRenderer lr;

	// Token: 0x04001647 RID: 5703
	private Vector3 positionForExplosion;

	// Token: 0x04001648 RID: 5704
	private bool spawnExplosion;

	// Token: 0x04001649 RID: 5705
	private Vector3[] particleSpawnPositions;

	// Token: 0x0400164A RID: 5706
	private Vector3 endPoint;

	// Token: 0x0400164B RID: 5707
	private float globalProgress;
}
