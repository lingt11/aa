using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace Tiny
{
	// Token: 0x020004B8 RID: 1208
	public class Trail : MonoBehaviour
	{
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06001AC2 RID: 6850 RVA: 0x000A55B9 File Offset: 0x000A37B9
		// (set) Token: 0x06001AC3 RID: 6851 RVA: 0x000A55C1 File Offset: 0x000A37C1
		public Vector3[] Points
		{
			get
			{
				return this.points;
			}
			set
			{
				this.points = value;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06001AC4 RID: 6852 RVA: 0x000A55CA File Offset: 0x000A37CA
		public bool Loop
		{
			get
			{
				return this.loop && this.points.Length >= 3;
			}
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x000A55E4 File Offset: 0x000A37E4
		public void Clear()
		{
			if (!base.enabled || this.pointCount <= 1 || !this.trailGo)
			{
				return;
			}
			if (this.update != null)
			{
				base.StopCoroutine(this.update);
			}
			this.ClearVertices();
			this.update = base.StartCoroutine(this.PhysicsUpdate());
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x000A563C File Offset: 0x000A383C
		private void Start()
		{
			this.cacheTM = base.transform;
			this.trailGo = new GameObject(base.name + "Trail", new Type[]
			{
				typeof(MeshFilter),
				typeof(MeshRenderer)
			});
			Object.DontDestroyOnLoad(this.trailGo);
			this.mesh = new Mesh
			{
				name = "Trail Effect"
			};
			this.mesh.MarkDynamic();
			this.trailGo.GetComponent<MeshFilter>().sharedMesh = this.mesh;
			this.trailGo.layer = base.gameObject.layer;
			MeshRenderer component = this.trailGo.GetComponent<MeshRenderer>();
			component.material = this.material;
			component.shadowCastingMode = ShadowCastingMode.Off;
			this.Initialize((int)(this.duration / Time.fixedDeltaTime));
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x000A5718 File Offset: 0x000A3918
		private void OnDestroy()
		{
			if (this.mesh != null)
			{
				Object.DestroyImmediate(this.mesh);
			}
			this.mesh = null;
			if (this.trailGo != null)
			{
				Object.DestroyImmediate(this.trailGo);
			}
			this.trailGo = null;
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x000A5765 File Offset: 0x000A3965
		private void OnEnable()
		{
			if (this.trailGo == null)
			{
				return;
			}
			this.trailGo.SetActive(true);
			this.Initialize((int)(this.duration / Time.fixedDeltaTime));
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x000A5795 File Offset: 0x000A3995
		private void OnDisable()
		{
			if (this.trailGo)
			{
				this.trailGo.SetActive(false);
			}
			if (this.update != null)
			{
				base.StopCoroutine(this.update);
			}
			this.update = null;
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x000A57CC File Offset: 0x000A39CC
		private void SetVerticesAndCorner()
		{
			int num = this.pointCount + this.pointCount * this.corner;
			Array.Copy(this.vertices, 0, this.vertices, num, this.vertices.Length - num);
			this.TransformVertices();
			int num2 = num * 2;
			int num3 = num * 3;
			int num4 = -1;
			while (++num4 < this.pointCount)
			{
				Vector3 vector = this.vertices[num4];
				Vector3 vector2 = this.vertices[num4 + num];
				Vector3 vector3 = this.vertices[num4 + num2];
				Vector3 p = this.vertices[num4 + num3];
				int num5 = -1;
				int num6 = this.pointCount + num4;
				while (++num5 < this.corner)
				{
					float t = (float)(num5 + 1) * this.toCornerT;
					this.vertices[num6] = Trail.CatmullRomSpline(vector, vector, vector2, vector3, t);
					this.vertices[num6 + num] = Trail.CatmullRomSpline(vector, vector2, vector3, p, t);
					num6 += this.pointCount;
				}
			}
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x000A58DF File Offset: 0x000A3ADF
		private void SetVertices()
		{
			Array.Copy(this.vertices, 0, this.vertices, this.pointCount, this.vertices.Length - this.pointCount);
			this.TransformVertices();
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x000A590E File Offset: 0x000A3B0E
		private IEnumerator PhysicsUpdate()
		{
			YieldInstruction wait = new WaitForFixedUpdate();
			Action action = (this.corner > 0) ? new Action(this.SetVerticesAndCorner) : new Action(this.SetVertices);
			for (;;)
			{
				yield return wait;
				action();
				this.cacheTM.hasChanged = false;
			}
			yield break;
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x000A591D File Offset: 0x000A3B1D
		private void LateUpdate()
		{
			if (this.cacheTM.hasChanged)
			{
				this.TransformVertices();
			}
			this.mesh.vertices = this.vertices;
			this.mesh.RecalculateBounds();
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x000A5950 File Offset: 0x000A3B50
		private void TransformVertices()
		{
			Matrix4x4 localToWorldMatrix = this.cacheTM.localToWorldMatrix;
			int num = -1;
			while (++num < this.pointCount)
			{
				this.vertices[num] = localToWorldMatrix.MultiplyPoint3x4(this.points[num]);
			}
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x000A599C File Offset: 0x000A3B9C
		private void ClearVertices()
		{
			this.TransformVertices();
			for (int i = this.pointCount; i < this.vertices.Length; i += this.pointCount)
			{
				Array.Copy(this.vertices, 0, this.vertices, i, this.pointCount);
			}
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x000A59E8 File Offset: 0x000A3BE8
		private void Initialize(int segment)
		{
			int num = (segment >= 3) ? this.corner : 0;
			if (this.lastSegmentCount == segment && this.pointCount == this.points.Length && this.lastCorner == num)
			{
				this.ClearVertices();
				this.update = base.StartCoroutine(this.PhysicsUpdate());
				return;
			}
			this.pointCount = this.points.Length;
			this.lastCorner = num;
			this.lastSegmentCount = segment;
			if (this.pointCount <= 1)
			{
				this.mesh.Clear();
				return;
			}
			int num2 = segment + segment * num;
			Vector2[] array = new Vector2[this.pointCount * (num2 + 1)];
			bool flag = this.Loop;
			int[] array2 = new int[(flag ? this.pointCount : (this.pointCount - 1)) * 6 * num2];
			Vector2 vector = default(Vector2);
			int num3 = this.pointCount - 1;
			float num4 = 1f / (float)segment;
			float num5 = 1f / (float)num3;
			this.toCornerT = 1f / (float)(num + 1);
			int num6 = -1;
			int num7 = -1;
			while (++num6 <= segment)
			{
				vector.y = (float)num6 * num4;
				int num8 = -1;
				while (++num8 < this.pointCount)
				{
					vector.x = (float)num8 * num5;
					array[++num7] = vector;
				}
				if (num6 != segment)
				{
					int num9 = -1;
					while (++num9 < num)
					{
						vector.y = Mathf.Lerp((float)num6 * num4, (float)(num6 + 1) * num4, (float)(num9 + 1) * this.toCornerT);
						int num10 = -1;
						while (++num10 < this.pointCount)
						{
							vector.x = (float)num10 * num5;
							array[++num7] = vector;
						}
					}
				}
			}
			int num11 = 0;
			int num12 = flag ? (num3 + 1) : num3;
			int num13 = -1;
			while (++num13 < num2)
			{
				int num14 = num13 * this.pointCount;
				int num15 = num13 * this.pointCount;
				if (flag)
				{
					num14 += num3;
				}
				else
				{
					num15++;
				}
				int num16 = -1;
				while (++num16 < num12)
				{
					array2[num11] = num14;
					array2[num11 + 1] = num14 + this.pointCount;
					array2[num11 + 2] = num15;
					array2[num11 + 3] = num15;
					array2[num11 + 4] = num14 + this.pointCount;
					array2[num11 + 5] = num15 + this.pointCount;
					num11 += 6;
					num14 = num15++;
				}
			}
			this.vertices = new Vector3[array.Length];
			this.ClearVertices();
			this.mesh.vertices = this.vertices;
			this.mesh.uv = array;
			this.mesh.SetIndices(array2, MeshTopology.Triangles, 0);
			this.update = base.StartCoroutine(this.PhysicsUpdate());
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x000A5CA8 File Offset: 0x000A3EA8
		private static Vector3 CatmullRomSpline(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
		{
			float num = t * t;
			float d = num * t;
			return 0.5f * (2f * p1 + (-p0 + p2) * t + (2f * p0 - 5f * p1 + 4f * p2 - p3) * num + (-p0 + 3f * p1 - 3f * p2 + p3) * d);
		}

		// Token: 0x04001A2E RID: 6702
		[SerializeField]
		[Tooltip("The material to apply to the trail.")]
		private Material material;

		// Token: 0x04001A2F RID: 6703
		[SerializeField]
		[Tooltip("Define the lifetime of a point in the trail, in seconds.")]
		private float duration = 0.1f;

		// Token: 0x04001A30 RID: 6704
		[SerializeField]
		[Tooltip("Increase this value to make the trail corners appear rounder.")]
		private int corner = 1;

		// Token: 0x04001A31 RID: 6705
		[SerializeField]
		[Tooltip("Enable this to connect the first and last positions of the line, and form a closed loop.")]
		private bool loop;

		// Token: 0x04001A32 RID: 6706
		[SerializeField]
		[Tooltip("The array of Vector3 points to connect.")]
		private Vector3[] points = new Vector3[]
		{
			new Vector3(0f, 0f, -1f),
			new Vector3(0f, 0f, 1f)
		};

		// Token: 0x04001A33 RID: 6707
		[NonSerialized]
		private GameObject trailGo;

		// Token: 0x04001A34 RID: 6708
		[NonSerialized]
		private Mesh mesh;

		// Token: 0x04001A35 RID: 6709
		[NonSerialized]
		private Vector3[] vertices;

		// Token: 0x04001A36 RID: 6710
		[NonSerialized]
		private Transform cacheTM;

		// Token: 0x04001A37 RID: 6711
		[NonSerialized]
		private int lastSegmentCount = -1;

		// Token: 0x04001A38 RID: 6712
		[NonSerialized]
		private int lastCorner = -1;

		// Token: 0x04001A39 RID: 6713
		[NonSerialized]
		private int pointCount = -1;

		// Token: 0x04001A3A RID: 6714
		[NonSerialized]
		private float toCornerT;

		// Token: 0x04001A3B RID: 6715
		private Coroutine update;
	}
}
