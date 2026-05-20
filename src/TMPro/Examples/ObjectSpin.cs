using System;
using UnityEngine;

namespace TMPro.Examples
{
	// Token: 0x0200042E RID: 1070
	public class ObjectSpin : MonoBehaviour
	{
		// Token: 0x06001817 RID: 6167 RVA: 0x00096654 File Offset: 0x00094854
		private void Awake()
		{
			this.m_transform = base.transform;
			this.m_initial_Rotation = this.m_transform.rotation.eulerAngles;
			this.m_initial_Position = this.m_transform.position;
			Light component = base.GetComponent<Light>();
			this.m_lightColor = ((component != null) ? component.color : Color.black);
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x000966C0 File Offset: 0x000948C0
		private void Update()
		{
			if (this.Motion == ObjectSpin.MotionType.Rotation)
			{
				this.m_transform.Rotate(0f, this.SpinSpeed * Time.deltaTime, 0f);
				return;
			}
			if (this.Motion == ObjectSpin.MotionType.BackAndForth)
			{
				this.m_time += this.SpinSpeed * Time.deltaTime;
				this.m_transform.rotation = Quaternion.Euler(this.m_initial_Rotation.x, Mathf.Sin(this.m_time) * (float)this.RotationRange + this.m_initial_Rotation.y, this.m_initial_Rotation.z);
				return;
			}
			this.m_time += this.SpinSpeed * Time.deltaTime;
			float x = 15f * Mathf.Cos(this.m_time * 0.95f);
			float z = 10f;
			float y = 0f;
			this.m_transform.position = this.m_initial_Position + new Vector3(x, y, z);
			this.m_prevPOS = this.m_transform.position;
			this.frames++;
		}

		// Token: 0x04001761 RID: 5985
		public float SpinSpeed = 5f;

		// Token: 0x04001762 RID: 5986
		public int RotationRange = 15;

		// Token: 0x04001763 RID: 5987
		private Transform m_transform;

		// Token: 0x04001764 RID: 5988
		private float m_time;

		// Token: 0x04001765 RID: 5989
		private Vector3 m_prevPOS;

		// Token: 0x04001766 RID: 5990
		private Vector3 m_initial_Rotation;

		// Token: 0x04001767 RID: 5991
		private Vector3 m_initial_Position;

		// Token: 0x04001768 RID: 5992
		private Color32 m_lightColor;

		// Token: 0x04001769 RID: 5993
		private int frames;

		// Token: 0x0400176A RID: 5994
		public ObjectSpin.MotionType Motion;

		// Token: 0x0200042F RID: 1071
		public enum MotionType
		{
			// Token: 0x0400176C RID: 5996
			Rotation,
			// Token: 0x0400176D RID: 5997
			BackAndForth,
			// Token: 0x0400176E RID: 5998
			Translation
		}
	}
}
