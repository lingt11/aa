using System;
using System.Collections.Generic;
using UnityEngine;

namespace PsychoticLab
{
	// Token: 0x020004AF RID: 1199
	public class CharacterRandomizer : MonoBehaviour
	{
		// Token: 0x06001A87 RID: 6791 RVA: 0x000A32E4 File Offset: 0x000A14E4
		private void OnGUI()
		{
			GUIStyle guistyle = new GUIStyle();
			guistyle.normal.textColor = Color.white;
			guistyle.fontStyle = FontStyle.Bold;
			guistyle.fontSize = 24;
			GUI.Label(new Rect(10f, 10f, 150f, 50f), "Hold Right Mouse Button Down\nor use W A S D To Rotate.", guistyle);
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x000A333C File Offset: 0x000A153C
		private void Start()
		{
			this.BuildLists();
			if (this.enabledObjects.Count != 0)
			{
				foreach (GameObject gameObject in this.enabledObjects)
				{
					gameObject.SetActive(false);
				}
			}
			this.enabledObjects.Clear();
			this.ActivateItem(this.male.headAllElements[0]);
			this.ActivateItem(this.male.eyebrow[0]);
			this.ActivateItem(this.male.facialHair[0]);
			this.ActivateItem(this.male.torso[0]);
			this.ActivateItem(this.male.arm_Upper_Right[0]);
			this.ActivateItem(this.male.arm_Upper_Left[0]);
			this.ActivateItem(this.male.arm_Lower_Right[0]);
			this.ActivateItem(this.male.arm_Lower_Left[0]);
			this.ActivateItem(this.male.hand_Right[0]);
			this.ActivateItem(this.male.hand_Left[0]);
			this.ActivateItem(this.male.hips[0]);
			this.ActivateItem(this.male.leg_Right[0]);
			this.ActivateItem(this.male.leg_Left[0]);
			Transform transform = Camera.main.transform;
			if (transform)
			{
				transform.position = base.transform.position + new Vector3(0f, 0.3f, 2f);
				transform.rotation = Quaternion.Euler(0f, -180f, 0f);
				this.camHolder = new GameObject().transform;
				this.camHolder.position = base.transform.position + new Vector3(0f, 1f, 0f);
				transform.LookAt(this.camHolder);
				transform.SetParent(this.camHolder);
			}
			if (this.repeatOnPlay)
			{
				base.InvokeRepeating("Randomize", this.shuffleSpeed, this.shuffleSpeed);
			}
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x000A35A8 File Offset: 0x000A17A8
		private void Update()
		{
			if (this.camHolder)
			{
				if (Input.GetKey(KeyCode.Mouse1))
				{
					this.x += 1f * Input.GetAxis("Mouse X");
					this.y -= 1f * Input.GetAxis("Mouse Y");
					Cursor.lockState = CursorLockMode.Locked;
					Cursor.visible = false;
					return;
				}
				this.x -= 1f * Input.GetAxis("Horizontal");
				this.y -= 1f * Input.GetAxis("Vertical");
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x000A3660 File Offset: 0x000A1860
		private void LateUpdate()
		{
			if (this.camHolder)
			{
				this.y = Mathf.Clamp(this.y, -45f, 15f);
				this.camHolder.eulerAngles = new Vector3(this.y, this.x, 0f);
			}
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x000A36B8 File Offset: 0x000A18B8
		private void Randomize()
		{
			Gender gender = Gender.Male;
			Race race = Race.Human;
			SkinColor skinColor = SkinColor.White;
			Elements elements = Elements.Yes;
			HeadCovering headCovering = HeadCovering.HeadCoverings_Base_Hair;
			FacialHair facialHair = FacialHair.Yes;
			if (this.enabledObjects.Count != 0)
			{
				foreach (GameObject gameObject in this.enabledObjects)
				{
					gameObject.SetActive(false);
				}
			}
			this.enabledObjects.Clear();
			if (!this.GetPercent(50))
			{
				gender = Gender.Female;
			}
			if (!this.GetPercent(70))
			{
				race = Race.Elf;
			}
			if (!this.GetPercent(50))
			{
				elements = Elements.No;
			}
			int num = Random.Range(0, 100);
			if (num <= 33)
			{
				headCovering = HeadCovering.HeadCoverings_Base_Hair;
			}
			if (num > 33 && num < 66)
			{
				headCovering = HeadCovering.HeadCoverings_No_FacialHair;
			}
			if (num >= 66)
			{
				headCovering = HeadCovering.HeadCoverings_No_Hair;
			}
			if (race != Race.Human)
			{
				if (race == Race.Elf)
				{
					skinColor = SkinColor.Elf;
				}
			}
			else
			{
				int num2 = Random.Range(0, 100);
				if (num2 <= 33)
				{
					skinColor = SkinColor.White;
				}
				if (num2 > 33 && num2 < 66)
				{
					skinColor = SkinColor.Brown;
				}
				if (num2 >= 66)
				{
					skinColor = SkinColor.Black;
				}
			}
			if (gender == Gender.Male)
			{
				if (!this.GetPercent(50))
				{
					facialHair = FacialHair.No;
				}
				this.RandomizeByVariable(this.male, gender, elements, race, facialHair, skinColor, headCovering);
				return;
			}
			if (gender != Gender.Female)
			{
				return;
			}
			facialHair = FacialHair.No;
			this.RandomizeByVariable(this.female, gender, elements, race, facialHair, skinColor, headCovering);
		}

		// Token: 0x06001A8C RID: 6796 RVA: 0x000A37F8 File Offset: 0x000A19F8
		private void RandomizeByVariable(CharacterObjectGroups cog, Gender gender, Elements elements, Race race, FacialHair facialHair, SkinColor skinColor, HeadCovering headCovering)
		{
			if (elements != Elements.Yes)
			{
				if (elements == Elements.No)
				{
					if (cog.headNoElements.Count != 0)
					{
						this.ActivateItem(cog.headNoElements[Random.Range(0, cog.headNoElements.Count)]);
					}
				}
			}
			else
			{
				if (cog.headAllElements.Count != 0)
				{
					this.ActivateItem(cog.headAllElements[Random.Range(0, cog.headAllElements.Count)]);
				}
				if (cog.eyebrow.Count != 0)
				{
					this.ActivateItem(cog.eyebrow[Random.Range(0, cog.eyebrow.Count)]);
				}
				if (cog.facialHair.Count != 0 && facialHair == FacialHair.Yes && gender == Gender.Male && headCovering != HeadCovering.HeadCoverings_No_FacialHair)
				{
					this.ActivateItem(cog.facialHair[Random.Range(0, cog.facialHair.Count)]);
				}
				switch (headCovering)
				{
				case HeadCovering.HeadCoverings_Base_Hair:
					if (this.allGender.all_Hair.Count != 0)
					{
						this.ActivateItem(this.allGender.all_Hair[1]);
					}
					if (this.allGender.headCoverings_Base_Hair.Count != 0)
					{
						this.ActivateItem(this.allGender.headCoverings_Base_Hair[Random.Range(0, this.allGender.headCoverings_Base_Hair.Count)]);
					}
					break;
				case HeadCovering.HeadCoverings_No_FacialHair:
					if (this.allGender.all_Hair.Count != 0)
					{
						this.ActivateItem(this.allGender.all_Hair[Random.Range(0, this.allGender.all_Hair.Count)]);
					}
					if (this.allGender.headCoverings_No_FacialHair.Count != 0)
					{
						this.ActivateItem(this.allGender.headCoverings_No_FacialHair[Random.Range(0, this.allGender.headCoverings_No_FacialHair.Count)]);
					}
					break;
				case HeadCovering.HeadCoverings_No_Hair:
					if (this.allGender.headCoverings_No_Hair.Count != 0)
					{
						this.ActivateItem(this.allGender.all_Hair[Random.Range(0, this.allGender.all_Hair.Count)]);
					}
					if (race != Race.Human && this.allGender.elf_Ear.Count != 0)
					{
						this.ActivateItem(this.allGender.elf_Ear[Random.Range(0, this.allGender.elf_Ear.Count)]);
					}
					break;
				}
			}
			if (cog.torso.Count != 0)
			{
				this.ActivateItem(cog.torso[Random.Range(1, cog.torso.Count)]);
			}
			if (cog.arm_Upper_Right.Count != 0)
			{
				this.RandomizeLeftRight(cog.arm_Upper_Right, cog.arm_Upper_Left, 15);
			}
			if (cog.arm_Lower_Right.Count != 0)
			{
				this.RandomizeLeftRight(cog.arm_Lower_Right, cog.arm_Lower_Left, 15);
			}
			if (cog.hand_Right.Count != 0)
			{
				this.RandomizeLeftRight(cog.hand_Right, cog.hand_Left, 15);
			}
			if (cog.hips.Count != 0)
			{
				this.ActivateItem(cog.hips[Random.Range(1, cog.hips.Count)]);
			}
			if (cog.leg_Right.Count != 0)
			{
				this.RandomizeLeftRight(cog.leg_Right, cog.leg_Left, 15);
			}
			if (this.allGender.chest_Attachment.Count != 0)
			{
				this.ActivateItem(this.allGender.chest_Attachment[Random.Range(0, this.allGender.chest_Attachment.Count)]);
			}
			if (this.allGender.back_Attachment.Count != 0)
			{
				this.ActivateItem(this.allGender.back_Attachment[Random.Range(0, this.allGender.back_Attachment.Count)]);
			}
			if (this.allGender.shoulder_Attachment_Right.Count != 0)
			{
				this.RandomizeLeftRight(this.allGender.shoulder_Attachment_Right, this.allGender.shoulder_Attachment_Left, 10);
			}
			if (this.allGender.elbow_Attachment_Right.Count != 0)
			{
				this.RandomizeLeftRight(this.allGender.elbow_Attachment_Right, this.allGender.elbow_Attachment_Left, 10);
			}
			if (this.allGender.hips_Attachment.Count != 0)
			{
				this.ActivateItem(this.allGender.hips_Attachment[Random.Range(0, this.allGender.hips_Attachment.Count)]);
			}
			if (this.allGender.knee_Attachement_Right.Count != 0)
			{
				this.RandomizeLeftRight(this.allGender.knee_Attachement_Right, this.allGender.knee_Attachement_Left, 10);
			}
			this.RandomizeColors(skinColor);
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x000A3CA0 File Offset: 0x000A1EA0
		private void RandomizeColors(SkinColor skinColor)
		{
			switch (skinColor)
			{
			case SkinColor.White:
				this.RandomizeAndSetHairSkinColors("White", this.whiteSkin, this.whiteHair, this.whiteStubble, this.whiteScar);
				break;
			case SkinColor.Brown:
				this.RandomizeAndSetHairSkinColors("Brown", this.brownSkin, this.brownHair, this.brownStubble, this.brownScar);
				break;
			case SkinColor.Black:
				this.RandomizeAndSetHairSkinColors("Black", this.blackSkin, this.blackHair, this.blackStubble, this.blackScar);
				break;
			case SkinColor.Elf:
				this.RandomizeAndSetHairSkinColors("Elf", this.elfSkin, this.elfHair, this.elfStubble, this.elfScar);
				break;
			}
			if (this.primary.Length != 0)
			{
				this.mat.SetColor("_Color_Primary", this.primary[Random.Range(0, this.primary.Length)]);
			}
			else
			{
				Debug.Log("No Primary Colors Specified In The Inspector");
			}
			if (this.secondary.Length != 0)
			{
				this.mat.SetColor("_Color_Secondary", this.secondary[Random.Range(0, this.secondary.Length)]);
			}
			else
			{
				Debug.Log("No Secondary Colors Specified In The Inspector");
			}
			if (this.metalPrimary.Length != 0)
			{
				this.mat.SetColor("_Color_Metal_Primary", this.metalPrimary[Random.Range(0, this.metalPrimary.Length)]);
			}
			else
			{
				Debug.Log("No Primary Metal Colors Specified In The Inspector");
			}
			if (this.metalSecondary.Length != 0)
			{
				this.mat.SetColor("_Color_Metal_Secondary", this.metalSecondary[Random.Range(0, this.metalSecondary.Length)]);
			}
			else
			{
				Debug.Log("No Secondary Metal Colors Specified In The Inspector");
			}
			if (this.leatherPrimary.Length != 0)
			{
				this.mat.SetColor("_Color_Leather_Primary", this.leatherPrimary[Random.Range(0, this.leatherPrimary.Length)]);
			}
			else
			{
				Debug.Log("No Primary Leather Colors Specified In The Inspector");
			}
			if (this.leatherSecondary.Length != 0)
			{
				this.mat.SetColor("_Color_Leather_Secondary", this.leatherSecondary[Random.Range(0, this.leatherSecondary.Length)]);
			}
			else
			{
				Debug.Log("No Secondary Leather Colors Specified In The Inspector");
			}
			if (this.bodyArt.Length != 0)
			{
				this.mat.SetColor("_Color_BodyArt", this.bodyArt[Random.Range(0, this.bodyArt.Length)]);
			}
			else
			{
				Debug.Log("No Body Art Colors Specified In The Inspector");
			}
			this.mat.SetFloat("_BodyArt_Amount", Random.Range(0f, 1f));
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x000A3F2C File Offset: 0x000A212C
		private void RandomizeAndSetHairSkinColors(string info, Color[] skin, Color[] hair, Color stubble, Color scar)
		{
			if (skin.Length != 0)
			{
				this.mat.SetColor("_Color_Skin", skin[Random.Range(0, skin.Length)]);
			}
			else
			{
				Debug.Log("No " + info + " Skin Colors Specified In The Inspector");
			}
			if (hair.Length != 0)
			{
				this.mat.SetColor("_Color_Hair", hair[Random.Range(0, hair.Length)]);
			}
			else
			{
				Debug.Log("No " + info + " Hair Colors Specified In The Inspector");
			}
			this.mat.SetColor("_Color_Stubble", stubble);
			this.mat.SetColor("_Color_Scar", scar);
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x000A3FD4 File Offset: 0x000A21D4
		private void RandomizeLeftRight(List<GameObject> objectListRight, List<GameObject> objectListLeft, int rndPercent)
		{
			int index = Random.Range(0, objectListRight.Count);
			this.ActivateItem(objectListRight[index]);
			if (this.GetPercent(rndPercent))
			{
				index = Random.Range(0, objectListLeft.Count);
			}
			this.ActivateItem(objectListLeft[index]);
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x000A401E File Offset: 0x000A221E
		private void ActivateItem(GameObject go)
		{
			go.SetActive(true);
			this.enabledObjects.Add(go);
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x000A4033 File Offset: 0x000A2233
		private Color ConvertColor(int r, int g, int b)
		{
			return new Color((float)r / 255f, (float)g / 255f, (float)b / 255f, 1f);
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x000A4058 File Offset: 0x000A2258
		private bool GetPercent(int pct)
		{
			bool result = false;
			if (Random.Range(0, 100) <= pct)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x000A4078 File Offset: 0x000A2278
		private void BuildLists()
		{
			this.BuildList(this.male.headAllElements, "Male_Head_All_Elements");
			this.BuildList(this.male.headNoElements, "Male_Head_No_Elements");
			this.BuildList(this.male.eyebrow, "Male_01_Eyebrows");
			this.BuildList(this.male.facialHair, "Male_02_FacialHair");
			this.BuildList(this.male.torso, "Male_03_Torso");
			this.BuildList(this.male.arm_Upper_Right, "Male_04_Arm_Upper_Right");
			this.BuildList(this.male.arm_Upper_Left, "Male_05_Arm_Upper_Left");
			this.BuildList(this.male.arm_Lower_Right, "Male_06_Arm_Lower_Right");
			this.BuildList(this.male.arm_Lower_Left, "Male_07_Arm_Lower_Left");
			this.BuildList(this.male.hand_Right, "Male_08_Hand_Right");
			this.BuildList(this.male.hand_Left, "Male_09_Hand_Left");
			this.BuildList(this.male.hips, "Male_10_Hips");
			this.BuildList(this.male.leg_Right, "Male_11_Leg_Right");
			this.BuildList(this.male.leg_Left, "Male_12_Leg_Left");
			this.BuildList(this.female.headAllElements, "Female_Head_All_Elements");
			this.BuildList(this.female.headNoElements, "Female_Head_No_Elements");
			this.BuildList(this.female.eyebrow, "Female_01_Eyebrows");
			this.BuildList(this.female.facialHair, "Female_02_FacialHair");
			this.BuildList(this.female.torso, "Female_03_Torso");
			this.BuildList(this.female.arm_Upper_Right, "Female_04_Arm_Upper_Right");
			this.BuildList(this.female.arm_Upper_Left, "Female_05_Arm_Upper_Left");
			this.BuildList(this.female.arm_Lower_Right, "Female_06_Arm_Lower_Right");
			this.BuildList(this.female.arm_Lower_Left, "Female_07_Arm_Lower_Left");
			this.BuildList(this.female.hand_Right, "Female_08_Hand_Right");
			this.BuildList(this.female.hand_Left, "Female_09_Hand_Left");
			this.BuildList(this.female.hips, "Female_10_Hips");
			this.BuildList(this.female.leg_Right, "Female_11_Leg_Right");
			this.BuildList(this.female.leg_Left, "Female_12_Leg_Left");
			this.BuildList(this.allGender.all_Hair, "All_01_Hair");
			this.BuildList(this.allGender.all_Head_Attachment, "All_02_Head_Attachment");
			this.BuildList(this.allGender.headCoverings_Base_Hair, "HeadCoverings_Base_Hair");
			this.BuildList(this.allGender.headCoverings_No_FacialHair, "HeadCoverings_No_FacialHair");
			this.BuildList(this.allGender.headCoverings_No_Hair, "HeadCoverings_No_Hair");
			this.BuildList(this.allGender.chest_Attachment, "All_03_Chest_Attachment");
			this.BuildList(this.allGender.back_Attachment, "All_04_Back_Attachment");
			this.BuildList(this.allGender.shoulder_Attachment_Right, "All_05_Shoulder_Attachment_Right");
			this.BuildList(this.allGender.shoulder_Attachment_Left, "All_06_Shoulder_Attachment_Left");
			this.BuildList(this.allGender.elbow_Attachment_Right, "All_07_Elbow_Attachment_Right");
			this.BuildList(this.allGender.elbow_Attachment_Left, "All_08_Elbow_Attachment_Left");
			this.BuildList(this.allGender.hips_Attachment, "All_09_Hips_Attachment");
			this.BuildList(this.allGender.knee_Attachement_Right, "All_10_Knee_Attachement_Right");
			this.BuildList(this.allGender.knee_Attachement_Left, "All_11_Knee_Attachement_Left");
			this.BuildList(this.allGender.elf_Ear, "Elf_Ear");
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x000A4438 File Offset: 0x000A2638
		private void BuildList(List<GameObject> targetList, string characterPart)
		{
			Transform[] componentsInChildren = base.gameObject.GetComponentsInChildren<Transform>();
			Transform transform = null;
			foreach (Transform transform2 in componentsInChildren)
			{
				if (transform2.gameObject.name == characterPart)
				{
					transform = transform2;
					break;
				}
			}
			targetList.Clear();
			for (int j = 0; j < transform.childCount; j++)
			{
				GameObject gameObject = transform.GetChild(j).gameObject;
				gameObject.SetActive(false);
				targetList.Add(gameObject);
				if (!this.mat && gameObject.GetComponent<SkinnedMeshRenderer>())
				{
					this.mat = gameObject.GetComponent<SkinnedMeshRenderer>().material;
				}
			}
		}

		// Token: 0x040019CA RID: 6602
		[Header("Demo Settings")]
		public bool repeatOnPlay;

		// Token: 0x040019CB RID: 6603
		public float shuffleSpeed = 0.7f;

		// Token: 0x040019CC RID: 6604
		[Header("Material")]
		public Material mat;

		// Token: 0x040019CD RID: 6605
		[Header("Gear Colors")]
		public Color[] primary = new Color[]
		{
			new Color(0.2862745f, 0.4f, 0.4941177f),
			new Color(0.4392157f, 0.1960784f, 0.172549f),
			new Color(0.3529412f, 0.3803922f, 0.2705882f),
			new Color(0.682353f, 0.4392157f, 0.2196079f),
			new Color(0.4313726f, 0.2313726f, 0.2705882f),
			new Color(0.5921569f, 0.4941177f, 0.2588235f),
			new Color(0.482353f, 0.4156863f, 0.3529412f),
			new Color(0.2352941f, 0.2352941f, 0.2352941f),
			new Color(0.2313726f, 0.4313726f, 0.4156863f)
		};

		// Token: 0x040019CE RID: 6606
		public Color[] secondary = new Color[]
		{
			new Color(0.7019608f, 0.6235294f, 0.4666667f),
			new Color(0.7372549f, 0.7372549f, 0.7372549f),
			new Color(0.1647059f, 0.1647059f, 0.1647059f),
			new Color(0.2392157f, 0.2509804f, 0.1882353f)
		};

		// Token: 0x040019CF RID: 6607
		[Header("Metal Colors")]
		public Color[] metalPrimary = new Color[]
		{
			new Color(0.6705883f, 0.6705883f, 0.6705883f),
			new Color(0.5568628f, 0.5960785f, 0.6392157f),
			new Color(0.5568628f, 0.6235294f, 0.6f),
			new Color(0.6313726f, 0.6196079f, 0.5568628f),
			new Color(0.6980392f, 0.6509804f, 0.6196079f)
		};

		// Token: 0x040019D0 RID: 6608
		public Color[] metalSecondary = new Color[]
		{
			new Color(0.3921569f, 0.4039216f, 0.4117647f),
			new Color(0.4784314f, 0.5176471f, 0.5450981f),
			new Color(0.3764706f, 0.3607843f, 0.3372549f),
			new Color(0.3254902f, 0.3764706f, 0.3372549f),
			new Color(0.4f, 0.4039216f, 0.3568628f)
		};

		// Token: 0x040019D1 RID: 6609
		[Header("Leather Colors")]
		public Color[] leatherPrimary;

		// Token: 0x040019D2 RID: 6610
		public Color[] leatherSecondary;

		// Token: 0x040019D3 RID: 6611
		[Header("Skin Colors")]
		public Color[] whiteSkin = new Color[]
		{
			new Color(1f, 0.8000001f, 0.682353f)
		};

		// Token: 0x040019D4 RID: 6612
		public Color[] brownSkin = new Color[]
		{
			new Color(0.8196079f, 0.6352941f, 0.4588236f)
		};

		// Token: 0x040019D5 RID: 6613
		public Color[] blackSkin = new Color[]
		{
			new Color(0.5647059f, 0.4078432f, 0.3137255f)
		};

		// Token: 0x040019D6 RID: 6614
		public Color[] elfSkin = new Color[]
		{
			new Color(0.9607844f, 0.7843138f, 0.7294118f)
		};

		// Token: 0x040019D7 RID: 6615
		[Header("Hair Colors")]
		public Color[] whiteHair = new Color[]
		{
			new Color(0.3098039f, 0.254902f, 0.1764706f),
			new Color(0.2196079f, 0.2196079f, 0.2196079f),
			new Color(0.8313726f, 0.6235294f, 0.3607843f),
			new Color(0.8901961f, 0.7803922f, 0.5490196f),
			new Color(0.8000001f, 0.8196079f, 0.8078432f),
			new Color(0.6862745f, 0.4f, 0.2352941f),
			new Color(0.5450981f, 0.427451f, 0.2156863f),
			new Color(0.8470589f, 0.4666667f, 0.2470588f)
		};

		// Token: 0x040019D8 RID: 6616
		public Color whiteStubble = new Color(0.8039216f, 0.7019608f, 0.6313726f);

		// Token: 0x040019D9 RID: 6617
		public Color[] brownHair = new Color[]
		{
			new Color(0.3098039f, 0.254902f, 0.1764706f),
			new Color(0.1764706f, 0.1686275f, 0.1686275f),
			new Color(0.3843138f, 0.2352941f, 0.0509804f),
			new Color(0.6196079f, 0.6196079f, 0.6196079f),
			new Color(0.6196079f, 0.6196079f, 0.6196079f)
		};

		// Token: 0x040019DA RID: 6618
		public Color brownStubble = new Color(0.6588235f, 0.572549f, 0.4627451f);

		// Token: 0x040019DB RID: 6619
		public Color[] blackHair = new Color[]
		{
			new Color(0.2431373f, 0.2039216f, 0.145098f),
			new Color(0.1764706f, 0.1686275f, 0.1686275f),
			new Color(0.1764706f, 0.1686275f, 0.1686275f)
		};

		// Token: 0x040019DC RID: 6620
		public Color blackStubble = new Color(0.3882353f, 0.2901961f, 0.2470588f);

		// Token: 0x040019DD RID: 6621
		public Color[] elfHair = new Color[]
		{
			new Color(0.9764706f, 0.9686275f, 0.9568628f),
			new Color(0.1764706f, 0.1686275f, 0.1686275f),
			new Color(0.8980393f, 0.7764707f, 0.6196079f)
		};

		// Token: 0x040019DE RID: 6622
		public Color elfStubble = new Color(0.8627452f, 0.7294118f, 0.6862745f);

		// Token: 0x040019DF RID: 6623
		[Header("Scar Colors")]
		public Color whiteScar = new Color(0.9294118f, 0.6862745f, 0.5921569f);

		// Token: 0x040019E0 RID: 6624
		public Color brownScar = new Color(0.6980392f, 0.5450981f, 0.4f);

		// Token: 0x040019E1 RID: 6625
		public Color blackScar = new Color(0.4235294f, 0.3176471f, 0.282353f);

		// Token: 0x040019E2 RID: 6626
		public Color elfScar = new Color(0.8745099f, 0.6588235f, 0.6313726f);

		// Token: 0x040019E3 RID: 6627
		[Header("Body Art Colors")]
		public Color[] bodyArt = new Color[]
		{
			new Color(0.0509804f, 0.6745098f, 0.9843138f),
			new Color(0.7215686f, 0.2666667f, 0.2666667f),
			new Color(0.3058824f, 0.7215686f, 0.6862745f),
			new Color(0.9254903f, 0.882353f, 0.8509805f),
			new Color(0.3098039f, 0.7058824f, 0.3137255f),
			new Color(0.5294118f, 0.3098039f, 0.6470588f),
			new Color(0.8666667f, 0.7764707f, 0.254902f),
			new Color(0.2392157f, 0.4588236f, 0.8156863f)
		};

		// Token: 0x040019E4 RID: 6628
		[HideInInspector]
		public List<GameObject> enabledObjects = new List<GameObject>();

		// Token: 0x040019E5 RID: 6629
		[HideInInspector]
		public CharacterObjectGroups male;

		// Token: 0x040019E6 RID: 6630
		[HideInInspector]
		public CharacterObjectGroups female;

		// Token: 0x040019E7 RID: 6631
		[HideInInspector]
		public CharacterObjectListsAllGender allGender;

		// Token: 0x040019E8 RID: 6632
		private Transform camHolder;

		// Token: 0x040019E9 RID: 6633
		private float x = 16f;

		// Token: 0x040019EA RID: 6634
		private float y = -30f;
	}
}
