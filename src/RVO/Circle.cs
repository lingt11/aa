using System;
using System.Collections.Generic;

namespace RVO
{
	// Token: 0x02000487 RID: 1159
	internal class Circle
	{
		// Token: 0x060019CD RID: 6605 RVA: 0x0009E9F0 File Offset: 0x0009CBF0
		private Circle()
		{
			this.goals = new List<Vector2>();
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x0009EA04 File Offset: 0x0009CC04
		private void setupScenario()
		{
			Simulator.Instance.setTimeStep(0.25f);
			Simulator.Instance.setAgentDefaults(15f, 10, 10f, 10f, 1.5f, 2f, new Vector2(0f, 0f));
			for (int i = 0; i < 250; i++)
			{
				Simulator.Instance.addAgent(200f * new Vector2((float)Math.Cos((double)((float)i * 2f) * 3.141592653589793 / 250.0), (float)Math.Sin((double)((float)i * 2f) * 3.141592653589793 / 250.0)));
				this.goals.Add(-Simulator.Instance.getAgentPosition(i));
			}
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x0009EAE4 File Offset: 0x0009CCE4
		private void updateVisualization()
		{
			Console.Write(Simulator.Instance.getGlobalTime());
			for (int i = 0; i < Simulator.Instance.getNumAgents(); i++)
			{
				Console.Write(" {0}", Simulator.Instance.getAgentPosition(i));
			}
			Console.WriteLine();
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x0009EB34 File Offset: 0x0009CD34
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
			}
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x0009EB94 File Offset: 0x0009CD94
		private bool reachedGoal()
		{
			for (int i = 0; i < Simulator.Instance.getNumAgents(); i++)
			{
				if (RVOMath.absSq(Simulator.Instance.getAgentPosition(i) - this.goals[i]) > Simulator.Instance.getAgentRadius(i) * Simulator.Instance.getAgentRadius(i))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x0009EBF4 File Offset: 0x0009CDF4
		public static void Main(string[] args)
		{
			Circle circle = new Circle();
			circle.setupScenario();
			do
			{
				circle.updateVisualization();
				circle.setPreferredVelocities();
				Simulator.Instance.doStep();
			}
			while (!circle.reachedGoal());
		}

		// Token: 0x04001930 RID: 6448
		private IList<Vector2> goals;
	}
}
