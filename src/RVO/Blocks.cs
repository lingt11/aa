using System;
using System.Collections.Generic;

namespace RVO
{
	// Token: 0x02000486 RID: 1158
	internal class Blocks
	{
		// Token: 0x060019C7 RID: 6599 RVA: 0x0009E4EF File Offset: 0x0009C6EF
		private Blocks()
		{
			this.goals = new List<Vector2>();
			this.random = new Random();
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x0009E510 File Offset: 0x0009C710
		private void setupScenario()
		{
			Simulator.Instance.setTimeStep(0.25f);
			Simulator.Instance.setAgentDefaults(15f, 10, 5f, 5f, 2f, 2f, new Vector2(0f, 0f));
			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < 5; j++)
				{
					Simulator.Instance.addAgent(new Vector2(55f + (float)i * 10f, 55f + (float)j * 10f));
					this.goals.Add(new Vector2(-75f, -75f));
					Simulator.Instance.addAgent(new Vector2(-55f - (float)i * 10f, 55f + (float)j * 10f));
					this.goals.Add(new Vector2(75f, -75f));
					Simulator.Instance.addAgent(new Vector2(55f + (float)i * 10f, -55f - (float)j * 10f));
					this.goals.Add(new Vector2(-75f, 75f));
					Simulator.Instance.addAgent(new Vector2(-55f - (float)i * 10f, -55f - (float)j * 10f));
					this.goals.Add(new Vector2(75f, 75f));
				}
			}
			IList<Vector2> list = new List<Vector2>();
			list.Add(new Vector2(-10f, 40f));
			list.Add(new Vector2(-40f, 40f));
			list.Add(new Vector2(-40f, 10f));
			list.Add(new Vector2(-10f, 10f));
			Simulator.Instance.addObstacle(list);
			IList<Vector2> list2 = new List<Vector2>();
			list2.Add(new Vector2(10f, 40f));
			list2.Add(new Vector2(10f, 10f));
			list2.Add(new Vector2(40f, 10f));
			list2.Add(new Vector2(40f, 40f));
			Simulator.Instance.addObstacle(list2);
			IList<Vector2> list3 = new List<Vector2>();
			list3.Add(new Vector2(10f, -40f));
			list3.Add(new Vector2(40f, -40f));
			list3.Add(new Vector2(40f, -10f));
			list3.Add(new Vector2(10f, -10f));
			Simulator.Instance.addObstacle(list3);
			IList<Vector2> list4 = new List<Vector2>();
			list4.Add(new Vector2(-10f, -40f));
			list4.Add(new Vector2(-10f, -10f));
			list4.Add(new Vector2(-40f, -10f));
			list4.Add(new Vector2(-40f, -40f));
			Simulator.Instance.addObstacle(list4);
			Simulator.Instance.processObstacles();
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x0009E850 File Offset: 0x0009CA50
		private void updateVisualization()
		{
			Console.Write(Simulator.Instance.getGlobalTime());
			for (int i = 0; i < Simulator.Instance.getNumAgents(); i++)
			{
				Console.Write(" {0}", Simulator.Instance.getAgentPosition(i));
			}
			Console.WriteLine();
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x0009E8A0 File Offset: 0x0009CAA0
		private void setPreferredVelocities()
		{
			for (int i = 0; i < Simulator.Instance.getNumAgents(); i++)
			{
				Vector2 vector = this.goals[i] - Simulator.Instance.getAgentPosition(i);
				if (RVOMath.absSq(vector) > 1f)
				{
					vector = RVOMath.normalize(vector);
				}
				Simulator.Instance.setAgentPrefVelocity(i, vector);
				float num = (float)this.random.NextDouble() * 2f * 3.1415927f;
				float scalar = (float)this.random.NextDouble() * 0.0001f;
				Simulator.Instance.setAgentPrefVelocity(i, Simulator.Instance.getAgentPrefVelocity(i) + scalar * new Vector2((float)Math.Cos((double)num), (float)Math.Sin((double)num)));
			}
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x0009E968 File Offset: 0x0009CB68
		private bool reachedGoal()
		{
			for (int i = 0; i < Simulator.Instance.getNumAgents(); i++)
			{
				if (RVOMath.absSq(Simulator.Instance.getAgentPosition(i) - this.goals[i]) > 400f)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x0009E9B8 File Offset: 0x0009CBB8
		public static void Main(string[] args)
		{
			Blocks blocks = new Blocks();
			blocks.setupScenario();
			do
			{
				blocks.updateVisualization();
				blocks.setPreferredVelocities();
				Simulator.Instance.doStep();
			}
			while (!blocks.reachedGoal());
		}

		// Token: 0x0400192E RID: 6446
		private IList<Vector2> goals;

		// Token: 0x0400192F RID: 6447
		private Random random;
	}
}
