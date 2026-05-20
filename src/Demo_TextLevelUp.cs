using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000418 RID: 1048
public class Demo_TextLevelUp : MonoBehaviour
{
	// Token: 0x060017D2 RID: 6098 RVA: 0x00094A1E File Offset: 0x00092C1E
	private void Awake()
	{
		this.cam = Camera.main.transform;
		this.textMesh = base.transform.GetChild(0).transform.GetComponent<TextMesh>();
		base.StartCoroutine(this.Delay());
	}

	// Token: 0x060017D3 RID: 6099 RVA: 0x00094A59 File Offset: 0x00092C59
	private IEnumerator Delay()
	{
		this.textMesh.color = new Color(0f, 0f, 0f, 0f);
		yield return new WaitForSeconds(0.35f);
		this.start = true;
		yield break;
	}

	// Token: 0x060017D4 RID: 6100 RVA: 0x00094A68 File Offset: 0x00092C68
	private void Update()
	{
		base.transform.forward = this.cam.forward;
		if (this.start)
		{
			base.transform.position = Vector3.MoveTowards(base.transform.position, new Vector3(0f, 165f, 0f), this.speed * Time.deltaTime);
			this.curentTime -= Time.deltaTime;
			this.textMesh.color = new Color(this.textColor.r, this.textColor.g, this.textColor.b, this.curentTime);
			if (this.curentTime <= 0f)
			{
				Object.Destroy(base.gameObject);
			}
		}
	}

	// Token: 0x040016FC RID: 5884
	private Transform cam;

	// Token: 0x040016FD RID: 5885
	private TextMesh textMesh;

	// Token: 0x040016FE RID: 5886
	private bool start;

	// Token: 0x040016FF RID: 5887
	private float speed = 2.5f;

	// Token: 0x04001700 RID: 5888
	public Color textColor = Color.white;

	// Token: 0x04001701 RID: 5889
	private float curentTime = 1f;
}
