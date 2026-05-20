using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000417 RID: 1047
public class Demo_LevelUpCustom_Manager : MonoBehaviour
{
	// Token: 0x060017CB RID: 6091 RVA: 0x000947CC File Offset: 0x000929CC
	private void Start()
	{
		this.m_SliderColor01.maxValue = 1f;
		this.m_SliderColor01.minValue = 0f;
		this.m_SliderColor02.maxValue = 1f;
		this.m_SliderColor02.minValue = 0f;
		this.PSarrows = this.levelUpCustom.transform.GetChild(1).gameObject;
		this.PSsparkles = this.levelUpCustom.transform.GetChild(2).gameObject;
		this.PSground = this.levelUpCustom.transform.GetChild(3).gameObject;
		Object.Instantiate<GameObject>(this.leveUpText, this.levelUpCustom.transform.position, this.levelUpCustom.transform.rotation);
	}

	// Token: 0x060017CC RID: 6092 RVA: 0x0009489C File Offset: 0x00092A9C
	private void Update()
	{
		this.m_Hue_01 = this.m_SliderColor01.value;
		this.m_Hue_02 = this.m_SliderColor02.value;
		Shader.SetGlobalColor("Color_01", Color.HSVToRGB(this.m_Hue_01, 0.9f, 1f));
		Shader.SetGlobalColor("Color_02", Color.HSVToRGB(this.m_Hue_02, 0.8f, 1f));
		this.currentTime -= Time.deltaTime;
		if (this.currentTime <= 0f)
		{
			this.Reset();
		}
	}

	// Token: 0x060017CD RID: 6093 RVA: 0x00094930 File Offset: 0x00092B30
	private void Reset()
	{
		this.levelUpCustom.Clear();
		this.levelUpCustom.Play();
		Object.Instantiate<GameObject>(this.leveUpText, this.levelUpCustom.transform.position, this.levelUpCustom.transform.rotation);
		this.currentTime = this.loopTime;
	}

	// Token: 0x060017CE RID: 6094 RVA: 0x0009498B File Offset: 0x00092B8B
	public void AddSparkles(bool value)
	{
		this.addSparkles = value;
		if (this.addSparkles)
		{
			this.PSsparkles.SetActive(true);
		}
		else
		{
			this.PSsparkles.SetActive(false);
		}
		this.Reset();
	}

	// Token: 0x060017CF RID: 6095 RVA: 0x000949BC File Offset: 0x00092BBC
	public void AddArrows(bool value)
	{
		this.addArrows = value;
		if (this.addArrows)
		{
			this.PSarrows.SetActive(true);
		}
		else
		{
			this.PSarrows.SetActive(false);
		}
		this.Reset();
	}

	// Token: 0x060017D0 RID: 6096 RVA: 0x000949ED File Offset: 0x00092BED
	public void AddGround(bool value)
	{
		this.addGround = value;
		if (this.addGround)
		{
			this.PSground.SetActive(true);
		}
		else
		{
			this.PSground.SetActive(false);
		}
		this.Reset();
	}

	// Token: 0x040016EB RID: 5867
	private float m_Hue_01;

	// Token: 0x040016EC RID: 5868
	private float m_Hue_02;

	// Token: 0x040016ED RID: 5869
	public ParticleSystem levelUpCustom;

	// Token: 0x040016EE RID: 5870
	public GameObject leveUpText;

	// Token: 0x040016EF RID: 5871
	public Slider m_SliderColor01;

	// Token: 0x040016F0 RID: 5872
	public Slider m_SliderColor02;

	// Token: 0x040016F1 RID: 5873
	public Toggle buttonSparkles;

	// Token: 0x040016F2 RID: 5874
	public Toggle buttonGround;

	// Token: 0x040016F3 RID: 5875
	public Toggle buttonArrows;

	// Token: 0x040016F4 RID: 5876
	private bool addSparkles;

	// Token: 0x040016F5 RID: 5877
	private bool addGround;

	// Token: 0x040016F6 RID: 5878
	private bool addArrows;

	// Token: 0x040016F7 RID: 5879
	private GameObject PSsparkles;

	// Token: 0x040016F8 RID: 5880
	private GameObject PSground;

	// Token: 0x040016F9 RID: 5881
	private GameObject PSarrows;

	// Token: 0x040016FA RID: 5882
	public float loopTime;

	// Token: 0x040016FB RID: 5883
	private float currentTime;
}
