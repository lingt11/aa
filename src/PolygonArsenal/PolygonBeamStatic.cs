using System;
using UnityEngine;

namespace PolygonArsenal
{
	// Token: 0x020004B2 RID: 1202
	public class PolygonBeamStatic : MonoBehaviour
	{
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06001A98 RID: 6808 RVA: 0x000A4C42 File Offset: 0x000A2E42
		public Transform BeamEndTransform
		{
			get
			{
				return this.beamEnd.transform;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06001A99 RID: 6809 RVA: 0x000A4C4F File Offset: 0x000A2E4F
		public GameObject BeamEnd
		{
			get
			{
				return this.beamEnd;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06001A9A RID: 6810 RVA: 0x000A4C57 File Offset: 0x000A2E57
		public GameObject BeamStart
		{
			get
			{
				return this.beamStart;
			}
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x00002D1D File Offset: 0x00000F1D
		private void Start()
		{
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x000A4C5F File Offset: 0x000A2E5F
		public void InitRadius(float radiusValue)
		{
			this.radius = radiusValue;
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x000A4C68 File Offset: 0x000A2E68
		private void OnEnable()
		{
			if (this.alwaysOn)
			{
				this.SpawnBeam();
			}
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x000A4C78 File Offset: 0x000A2E78
		private void OnDisable()
		{
			this.RemoveBeam();
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x000A4C80 File Offset: 0x000A2E80
		private void FixedUpdate()
		{
			if (this.beam)
			{
				this.line.SetPosition(0, base.transform.position);
				RaycastHit raycastHit;
				Vector3 vector;
				if ((this.beamCollides && this.layerMask == -1) ? Physics.SphereCast(base.transform.position - base.transform.forward, this.radius, base.transform.forward, out raycastHit) : Physics.SphereCast(base.transform.position - base.transform.forward, this.radius, base.transform.forward, out raycastHit, 100f, this.layerMask))
				{
					vector = raycastHit.point - base.transform.forward * this.beamEndOffset;
				}
				else
				{
					vector = base.transform.position + base.transform.forward * this.beamLength;
				}
				this.line.SetPosition(1, vector);
				if (this.beamStart)
				{
					this.beamStart.transform.position = base.transform.position;
					this.beamStart.transform.LookAt(vector);
				}
				if (this.beamEnd)
				{
					this.beamEnd.transform.position = vector;
					this.beamEnd.transform.LookAt(this.beamStart.transform.position);
				}
				float num = Vector3.Distance(base.transform.position, vector);
				this.line.material.mainTextureScale = new Vector2(num / this.textureLengthScale, 1f);
				this.line.material.mainTextureOffset -= new Vector2(Time.deltaTime * this.textureScrollSpeed, 0f);
			}
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x000A4E7C File Offset: 0x000A307C
		public void SpawnBeam()
		{
			if (this.beamLineRendererPrefab)
			{
				if (this.beamStartPrefab)
				{
					this.beamStart = Object.Instantiate<GameObject>(this.beamStartPrefab);
				}
				if (this.beamEndPrefab)
				{
					this.beamEnd = Object.Instantiate<GameObject>(this.beamEndPrefab);
				}
				this.beam = Object.Instantiate<GameObject>(this.beamLineRendererPrefab);
				this.beam.transform.position = base.transform.position;
				this.beam.transform.parent = base.transform;
				this.beam.transform.rotation = base.transform.rotation;
				this.line = this.beam.GetComponent<LineRenderer>();
				this.line.useWorldSpace = true;
				this.line.positionCount = 2;
				return;
			}
			MonoBehaviour.print("Add a hecking prefab with a line renderer to the SciFiBeamStatic script on " + base.gameObject.name + "! Heck!");
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x000A4F7C File Offset: 0x000A317C
		public void RemoveBeam()
		{
			if (this.beam)
			{
				Object.Destroy(this.beam);
			}
			if (this.beamStart)
			{
				Object.Destroy(this.beamStart);
			}
			if (this.beamEnd)
			{
				Object.Destroy(this.beamEnd);
			}
		}

		// Token: 0x04001A09 RID: 6665
		[Header("Prefabs")]
		public GameObject beamLineRendererPrefab;

		// Token: 0x04001A0A RID: 6666
		public GameObject beamStartPrefab;

		// Token: 0x04001A0B RID: 6667
		public GameObject beamEndPrefab;

		// Token: 0x04001A0C RID: 6668
		private GameObject beamStart;

		// Token: 0x04001A0D RID: 6669
		private GameObject beamEnd;

		// Token: 0x04001A0E RID: 6670
		public LayerMask layerMask = -1;

		// Token: 0x04001A0F RID: 6671
		private GameObject beam;

		// Token: 0x04001A10 RID: 6672
		private LineRenderer line;

		// Token: 0x04001A11 RID: 6673
		[Header("Beam Options")]
		public bool alwaysOn = true;

		// Token: 0x04001A12 RID: 6674
		public bool beamCollides = true;

		// Token: 0x04001A13 RID: 6675
		public float beamLength = 100f;

		// Token: 0x04001A14 RID: 6676
		public float beamEndOffset;

		// Token: 0x04001A15 RID: 6677
		public float textureScrollSpeed;

		// Token: 0x04001A16 RID: 6678
		public float textureLengthScale = 1f;

		// Token: 0x04001A17 RID: 6679
		private float radius;
	}
}
