using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000003 RID: 3
public class HS_CameraHolder : MonoBehaviour
{
	// Token: 0x06000006 RID: 6 RVA: 0x0000223C File Offset: 0x0000043C
	private void Start()
	{
		if (Screen.dpi < 1f)
		{
			this.windowDpi = 1f;
		}
		if (Screen.dpi < 200f)
		{
			this.windowDpi = 1f;
		}
		else
		{
			this.windowDpi = Screen.dpi / 200f;
		}
		Vector3 eulerAngles = base.transform.eulerAngles;
		this.x = eulerAngles.y;
		this.y = eulerAngles.x;
		this.Counter(0);
	}

	// Token: 0x06000007 RID: 7 RVA: 0x000022B8 File Offset: 0x000004B8
	private void OnGUI()
	{
		if (GUI.Button(new Rect(5f * this.windowDpi, 5f * this.windowDpi, 110f * this.windowDpi, 35f * this.windowDpi), "Previous effect"))
		{
			this.Counter(-1);
		}
		if (GUI.Button(new Rect(120f * this.windowDpi, 5f * this.windowDpi, 110f * this.windowDpi, 35f * this.windowDpi), "Play again"))
		{
			this.Counter(0);
		}
		if (GUI.Button(new Rect(235f * this.windowDpi, 5f * this.windowDpi, 110f * this.windowDpi, 35f * this.windowDpi), "Next effect"))
		{
			this.Counter(1);
		}
		this.StartColor = this.HueColor;
		this.HueColor = GUI.HorizontalSlider(new Rect(5f * this.windowDpi, 45f * this.windowDpi, 340f * this.windowDpi, 35f * this.windowDpi), this.HueColor, 0f, 1f);
		GUI.DrawTexture(new Rect(5f * this.windowDpi, 65f * this.windowDpi, 340f * this.windowDpi, 15f * this.windowDpi), this.HueTexture, ScaleMode.StretchToFill, false, 0f);
		if (this.HueColor != this.StartColor)
		{
			int num = 0;
			ParticleSystem[] array = this.particleSystems;
			for (int i = 0; i < array.Length; i++)
			{
				ParticleSystem.MainModule main = array[i].main;
				Color color = Color.HSVToRGB(this.HueColor + this.H * 0f, this.svList[num].S, this.svList[num].V);
				main.startColor = new Color(color.r, color.g, color.b, this.svList[num].A);
				num++;
			}
		}
	}

	// Token: 0x06000008 RID: 8 RVA: 0x000024F0 File Offset: 0x000006F0
	private void Counter(int count)
	{
		this.Prefab += count;
		if (this.Prefab > this.Prefabs.Length - 1)
		{
			this.Prefab = 0;
		}
		else if (this.Prefab < 0)
		{
			this.Prefab = this.Prefabs.Length - 1;
		}
		if (this.Instance != null)
		{
			Object.Destroy(this.Instance);
		}
		this.Instance = Object.Instantiate<GameObject>(this.Prefabs[this.Prefab]);
		this.particleSystems = this.Instance.GetComponentsInChildren<ParticleSystem>();
		this.svList.Clear();
		ParticleSystem[] array = this.particleSystems;
		for (int i = 0; i < array.Length; i++)
		{
			Color color = array[i].main.startColor.color;
			HS_CameraHolder.SVA item = default(HS_CameraHolder.SVA);
			Color.RGBToHSV(color, out this.H, out item.S, out item.V);
			item.A = color.a;
			this.svList.Add(item);
		}
	}

	// Token: 0x06000009 RID: 9 RVA: 0x000025F8 File Offset: 0x000007F8
	private void LateUpdate()
	{
		if (this.currDistance < 2f)
		{
			this.currDistance = 2f;
		}
		this.currDistance -= Input.GetAxis("Mouse ScrollWheel") * 2f;
		if (this.Holder && (Input.GetMouseButton(0) || Input.GetMouseButton(1)))
		{
			Vector3 mousePosition = Input.mousePosition;
			if (Screen.dpi < 1f)
			{
			}
			float num;
			if (Screen.dpi < 200f)
			{
				num = 1f;
			}
			else
			{
				num = Screen.dpi / 200f;
			}
			if (mousePosition.x < 380f * num && (float)Screen.height - mousePosition.y < 250f * num)
			{
				return;
			}
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
			this.x += (float)((double)(Input.GetAxis("Mouse X") * this.xRotate) * 0.02);
			this.y -= (float)((double)(Input.GetAxis("Mouse Y") * this.yRotate) * 0.02);
			this.y = HS_CameraHolder.ClampAngle(this.y, this.yMinLimit, this.yMaxLimit);
			Quaternion rotation = Quaternion.Euler(this.y, this.x, 0f);
			Vector3 position = rotation * new Vector3(0f, 0f, -this.currDistance) + this.Holder.position + this.cameraPos;
			base.transform.rotation = rotation;
			base.transform.position = position;
		}
		else
		{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}
		if (this.prevDistance != this.currDistance)
		{
			this.prevDistance = this.currDistance;
			Quaternion rotation2 = Quaternion.Euler(this.y, this.x, 0f);
			Vector3 position2 = rotation2 * new Vector3(0f, 0f, -this.currDistance) + this.Holder.position + this.cameraPos;
			base.transform.rotation = rotation2;
			base.transform.position = position2;
		}
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00002839 File Offset: 0x00000A39
	private static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360f)
		{
			angle += 360f;
		}
		if (angle > 360f)
		{
			angle -= 360f;
		}
		return Mathf.Clamp(angle, min, max);
	}

	// Token: 0x0400000E RID: 14
	public Transform Holder;

	// Token: 0x0400000F RID: 15
	public Vector3 cameraPos = new Vector3(0f, 0f, 0f);

	// Token: 0x04000010 RID: 16
	public float currDistance = 5f;

	// Token: 0x04000011 RID: 17
	public float xRotate = 250f;

	// Token: 0x04000012 RID: 18
	public float yRotate = 120f;

	// Token: 0x04000013 RID: 19
	public float yMinLimit = -20f;

	// Token: 0x04000014 RID: 20
	public float yMaxLimit = 80f;

	// Token: 0x04000015 RID: 21
	public float prevDistance;

	// Token: 0x04000016 RID: 22
	private float x;

	// Token: 0x04000017 RID: 23
	private float y;

	// Token: 0x04000018 RID: 24
	[Header("GUI")]
	private float windowDpi;

	// Token: 0x04000019 RID: 25
	public GameObject[] Prefabs;

	// Token: 0x0400001A RID: 26
	private int Prefab;

	// Token: 0x0400001B RID: 27
	private GameObject Instance;

	// Token: 0x0400001C RID: 28
	private float StartColor;

	// Token: 0x0400001D RID: 29
	private float HueColor;

	// Token: 0x0400001E RID: 30
	public Texture HueTexture;

	// Token: 0x0400001F RID: 31
	private ParticleSystem[] particleSystems = new ParticleSystem[0];

	// Token: 0x04000020 RID: 32
	private List<HS_CameraHolder.SVA> svList = new List<HS_CameraHolder.SVA>();

	// Token: 0x04000021 RID: 33
	private float H;

	// Token: 0x02000004 RID: 4
	public struct SVA
	{
		// Token: 0x04000022 RID: 34
		public float S;

		// Token: 0x04000023 RID: 35
		public float V;

		// Token: 0x04000024 RID: 36
		public float A;
	}
}
