using System;
using System.Collections.Generic;
using System.Threading;

namespace RVO
{
	// Token: 0x02000490 RID: 1168
	public class Simulator
	{
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060019F9 RID: 6649 RVA: 0x000A11FE File Offset: 0x0009F3FE
		public static Simulator Instance
		{
			get
			{
				return Simulator.instance_;
			}
		}

		// Token: 0x060019FA RID: 6650 RVA: 0x000A1205 File Offset: 0x0009F405
		public void delAgent(int agentNo)
		{
			this.agents_[this.agentNo2indexDict_[agentNo]].needDelete_ = true;
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x000A1224 File Offset: 0x0009F424
		private void updateDeleteAgent()
		{
			bool flag = false;
			for (int i = this.agents_.Count - 1; i >= 0; i--)
			{
				if (this.agents_[i].needDelete_)
				{
					this.agents_.RemoveAt(i);
					flag = true;
				}
			}
			if (flag)
			{
				this.onDelAgent();
			}
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x000A1278 File Offset: 0x0009F478
		public int addAgent(Vector2 position)
		{
			if (this.defaultAgent_ == null)
			{
				return -1;
			}
			Agent agent = new Agent();
			agent.id_ = Simulator.s_totalID;
			Simulator.s_totalID++;
			agent.maxNeighbors_ = this.defaultAgent_.maxNeighbors_;
			agent.maxSpeed_ = this.defaultAgent_.maxSpeed_;
			agent.neighborDist_ = this.defaultAgent_.neighborDist_;
			agent.position_ = position;
			agent.radius_ = this.defaultAgent_.radius_;
			agent.timeHorizon_ = this.defaultAgent_.timeHorizon_;
			agent.timeHorizonObst_ = this.defaultAgent_.timeHorizonObst_;
			agent.velocity_ = this.defaultAgent_.velocity_;
			this.agents_.Add(agent);
			this.onAddAgent();
			return agent.id_;
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x000A1344 File Offset: 0x0009F544
		private void onDelAgent()
		{
			this.agentNo2indexDict_.Clear();
			this.index2agentNoDict_.Clear();
			for (int i = 0; i < this.agents_.Count; i++)
			{
				int id_ = this.agents_[i].id_;
				this.agentNo2indexDict_.Add(id_, i);
				this.index2agentNoDict_.Add(i, id_);
			}
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x000A13AC File Offset: 0x0009F5AC
		private void onAddAgent()
		{
			if (this.agents_.Count == 0)
			{
				return;
			}
			int num = this.agents_.Count - 1;
			int id_ = this.agents_[num].id_;
			this.agentNo2indexDict_.Add(id_, num);
			this.index2agentNoDict_.Add(num, id_);
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x000A1404 File Offset: 0x0009F604
		public int addAgent(Vector2 position, float neighborDist, int maxNeighbors, float timeHorizon, float timeHorizonObst, float radius, float maxSpeed, Vector2 velocity)
		{
			Agent agent = new Agent();
			agent.id_ = Simulator.s_totalID;
			Simulator.s_totalID++;
			agent.maxNeighbors_ = maxNeighbors;
			agent.maxSpeed_ = maxSpeed;
			agent.neighborDist_ = neighborDist;
			agent.position_ = position;
			agent.radius_ = radius;
			agent.timeHorizon_ = timeHorizon;
			agent.timeHorizonObst_ = timeHorizonObst;
			agent.velocity_ = velocity;
			this.agents_.Add(agent);
			this.onAddAgent();
			return agent.id_;
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x000A1484 File Offset: 0x0009F684
		public int addObstacle(IList<Vector2> vertices)
		{
			if (vertices.Count < 2)
			{
				return -1;
			}
			int count = this.obstacles_.Count;
			for (int i = 0; i < vertices.Count; i++)
			{
				Obstacle obstacle = new Obstacle();
				obstacle.point_ = vertices[i];
				if (i != 0)
				{
					obstacle.previous_ = this.obstacles_[this.obstacles_.Count - 1];
					obstacle.previous_.next_ = obstacle;
				}
				if (i == vertices.Count - 1)
				{
					obstacle.next_ = this.obstacles_[count];
					obstacle.next_.previous_ = obstacle;
				}
				obstacle.direction_ = RVOMath.normalize(vertices[(i == vertices.Count - 1) ? 0 : (i + 1)] - vertices[i]);
				if (vertices.Count == 2)
				{
					obstacle.convex_ = true;
				}
				else
				{
					obstacle.convex_ = (RVOMath.leftOf(vertices[(i == 0) ? (vertices.Count - 1) : (i - 1)], vertices[i], vertices[(i == vertices.Count - 1) ? 0 : (i + 1)]) >= 0f);
				}
				obstacle.id_ = this.obstacles_.Count;
				this.obstacles_.Add(obstacle);
			}
			return count;
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x000A15D0 File Offset: 0x0009F7D0
		public void Clear()
		{
			this.agents_ = new List<Agent>();
			this.agentNo2indexDict_ = new Dictionary<int, int>();
			this.index2agentNoDict_ = new Dictionary<int, int>();
			this.defaultAgent_ = null;
			this.kdTree_ = new KdTree();
			this.obstacles_ = new List<Obstacle>();
			this.globalTime_ = 0f;
			this.timeStep_ = 0.1f;
			this.SetNumWorkers(0);
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x000A1638 File Offset: 0x0009F838
		public float doStep()
		{
			this.updateDeleteAgent();
			if (this.workers_ == null)
			{
				this.workers_ = new Simulator.Worker[this.numWorkers_];
				this.doneEvents_ = new ManualResetEvent[this.workers_.Length];
				this.workerAgentCount_ = this.getNumAgents();
				for (int i = 0; i < this.workers_.Length; i++)
				{
					this.doneEvents_[i] = new ManualResetEvent(false);
					this.workers_[i] = new Simulator.Worker(i * this.getNumAgents() / this.workers_.Length, (i + 1) * this.getNumAgents() / this.workers_.Length, this.doneEvents_[i]);
				}
			}
			if (this.workerAgentCount_ != this.getNumAgents())
			{
				this.workerAgentCount_ = this.getNumAgents();
				for (int j = 0; j < this.workers_.Length; j++)
				{
					this.workers_[j].config(j * this.getNumAgents() / this.workers_.Length, (j + 1) * this.getNumAgents() / this.workers_.Length);
				}
			}
			this.kdTree_.buildAgentTree();
			for (int k = 0; k < this.workers_.Length; k++)
			{
				this.doneEvents_[k].Reset();
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.workers_[k].step));
			}
			WaitHandle[] waitHandles = this.doneEvents_;
			WaitHandle.WaitAll(waitHandles);
			for (int l = 0; l < this.workers_.Length; l++)
			{
				this.doneEvents_[l].Reset();
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.workers_[l].update));
			}
			waitHandles = this.doneEvents_;
			WaitHandle.WaitAll(waitHandles);
			this.globalTime_ += this.timeStep_;
			return this.globalTime_;
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x000A17F8 File Offset: 0x0009F9F8
		public int getAgentAgentNeighbor(int agentNo, int neighborNo)
		{
			return this.agents_[this.agentNo2indexDict_[agentNo]].agentNeighbors_[neighborNo].Value.id_;
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x000A1834 File Offset: 0x0009FA34
		public int getAgentMaxNeighbors(int agentNo)
		{
			return this.agents_[this.agentNo2indexDict_[agentNo]].maxNeighbors_;
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x000A1852 File Offset: 0x0009FA52
		public float getAgentMaxSpeed(int agentNo)
		{
			return this.agents_[this.agentNo2indexDict_[agentNo]].maxSpeed_;
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x000A1870 File Offset: 0x0009FA70
		public float getAgentNeighborDist(int agentNo)
		{
			return this.agents_[this.agentNo2indexDict_[agentNo]].neighborDist_;
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x000A188E File Offset: 0x0009FA8E
		public int getAgentNumAgentNeighbors(int agentNo)
		{
			return this.agents_[this.agentNo2indexDict_[agentNo]].agentNeighbors_.Count;
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x000A18B1 File Offset: 0x0009FAB1
		public int getAgentNumObstacleNeighbors(int agentNo)
		{
			return this.agents_[this.agentNo2indexDict_[agentNo]].obstacleNeighbors_.Count;
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x000A18D4 File Offset: 0x0009FAD4
		public int getAgentObstacleNeighbor(int agentNo, int neighborNo)
		{
			return this.agents_[this.agentNo2indexDict_[agentNo]].obstacleNeighbors_[neighborNo].Value.id_;
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x000A1910 File Offset: 0x0009FB10
		public IList<Line> getAgentOrcaLines(int agentNo)
		{
			return this.agents_[this.agentNo2indexDict_[agentNo]].orcaLines_;
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x000A192E File Offset: 0x0009FB2E
		public Vector2 getAgentPosition(int agentNo)
		{
			return this.agents_[this.agentNo2indexDict_[agentNo]].position_;
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x000A194C File Offset: 0x0009FB4C
		public Vector2 getAgentPrefVelocity(int agentNo)
		{
			return this.agents_[this.agentNo2indexDict_[agentNo]].prefVelocity_;
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x000A196A File Offset: 0x0009FB6A
		public float getAgentRadius(int agentNo)
		{
			return this.agents_[this.agentNo2indexDict_[agentNo]].radius_;
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x000A1988 File Offset: 0x0009FB88
		public float getAgentTimeHorizon(int agentNo)
		{
			return this.agents_[this.agentNo2indexDict_[agentNo]].timeHorizon_;
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x000A19A6 File Offset: 0x0009FBA6
		public float getAgentTimeHorizonObst(int agentNo)
		{
			return this.agents_[this.agentNo2indexDict_[agentNo]].timeHorizonObst_;
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x000A19C4 File Offset: 0x0009FBC4
		public Vector2 getAgentVelocity(int agentNo)
		{
			return this.agents_[this.agentNo2indexDict_[agentNo]].velocity_;
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x000A19E2 File Offset: 0x0009FBE2
		public float getGlobalTime()
		{
			return this.globalTime_;
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x000A19EA File Offset: 0x0009FBEA
		public int getNumAgents()
		{
			return this.agents_.Count;
		}

		// Token: 0x06001A13 RID: 6675 RVA: 0x000A19F7 File Offset: 0x0009FBF7
		public int getNumObstacleVertices()
		{
			return this.obstacles_.Count;
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x000A1A04 File Offset: 0x0009FC04
		public int GetNumWorkers()
		{
			return this.numWorkers_;
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x000A1A0C File Offset: 0x0009FC0C
		public Vector2 getObstacleVertex(int vertexNo)
		{
			return this.obstacles_[vertexNo].point_;
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x000A1A1F File Offset: 0x0009FC1F
		public int getNextObstacleVertexNo(int vertexNo)
		{
			return this.obstacles_[vertexNo].next_.id_;
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x000A1A37 File Offset: 0x0009FC37
		public int getPrevObstacleVertexNo(int vertexNo)
		{
			return this.obstacles_[vertexNo].previous_.id_;
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x000A1A4F File Offset: 0x0009FC4F
		public float getTimeStep()
		{
			return this.timeStep_;
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x000A1A57 File Offset: 0x0009FC57
		public void processObstacles()
		{
			this.kdTree_.buildObstacleTree();
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x000A1A64 File Offset: 0x0009FC64
		public bool queryVisibility(Vector2 point1, Vector2 point2, float radius)
		{
			return this.kdTree_.queryVisibility(point1, point2, radius);
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x000A1A74 File Offset: 0x0009FC74
		public int queryNearAgent(Vector2 point, float radius)
		{
			if (this.getNumAgents() == 0)
			{
				return -1;
			}
			return this.kdTree_.queryNearAgent(point, radius);
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x000A1A90 File Offset: 0x0009FC90
		public void setAgentDefaults(float neighborDist, int maxNeighbors, float timeHorizon, float timeHorizonObst, float radius, float maxSpeed, Vector2 velocity)
		{
			if (this.defaultAgent_ == null)
			{
				this.defaultAgent_ = new Agent();
			}
			this.defaultAgent_.maxNeighbors_ = maxNeighbors;
			this.defaultAgent_.maxSpeed_ = maxSpeed;
			this.defaultAgent_.neighborDist_ = neighborDist;
			this.defaultAgent_.radius_ = radius;
			this.defaultAgent_.timeHorizon_ = timeHorizon;
			this.defaultAgent_.timeHorizonObst_ = timeHorizonObst;
			this.defaultAgent_.velocity_ = velocity;
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x000A1B08 File Offset: 0x0009FD08
		public void setAgentMaxNeighbors(int agentNo, int maxNeighbors)
		{
			this.agents_[this.agentNo2indexDict_[agentNo]].maxNeighbors_ = maxNeighbors;
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x000A1B27 File Offset: 0x0009FD27
		public void setAgentMaxSpeed(int agentNo, float maxSpeed)
		{
			this.agents_[this.agentNo2indexDict_[agentNo]].maxSpeed_ = maxSpeed;
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x000A1B46 File Offset: 0x0009FD46
		public void setAgentNeighborDist(int agentNo, float neighborDist)
		{
			this.agents_[this.agentNo2indexDict_[agentNo]].neighborDist_ = neighborDist;
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x000A1B65 File Offset: 0x0009FD65
		public void setAgentPosition(int agentNo, Vector2 position)
		{
			this.agents_[this.agentNo2indexDict_[agentNo]].position_ = position;
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x000A1B84 File Offset: 0x0009FD84
		public void setAgentPrefVelocity(int agentNo, Vector2 prefVelocity)
		{
			this.agents_[this.agentNo2indexDict_[agentNo]].prefVelocity_ = prefVelocity;
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x000A1BA3 File Offset: 0x0009FDA3
		public void setAgentRadius(int agentNo, float radius)
		{
			this.agents_[this.agentNo2indexDict_[agentNo]].radius_ = radius;
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x000A1BC2 File Offset: 0x0009FDC2
		public void setAgentTimeHorizon(int agentNo, float timeHorizon)
		{
			this.agents_[this.agentNo2indexDict_[agentNo]].timeHorizon_ = timeHorizon;
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x000A1BE1 File Offset: 0x0009FDE1
		public void setAgentTimeHorizonObst(int agentNo, float timeHorizonObst)
		{
			this.agents_[this.agentNo2indexDict_[agentNo]].timeHorizonObst_ = timeHorizonObst;
		}

		// Token: 0x06001A25 RID: 6693 RVA: 0x000A1C00 File Offset: 0x0009FE00
		public void setAgentVelocity(int agentNo, Vector2 velocity)
		{
			this.agents_[this.agentNo2indexDict_[agentNo]].velocity_ = velocity;
		}

		// Token: 0x06001A26 RID: 6694 RVA: 0x000A1C1F File Offset: 0x0009FE1F
		public void setGlobalTime(float globalTime)
		{
			this.globalTime_ = globalTime;
		}

		// Token: 0x06001A27 RID: 6695 RVA: 0x000A1C28 File Offset: 0x0009FE28
		public void SetNumWorkers(int numWorkers)
		{
			this.numWorkers_ = numWorkers;
			if (this.numWorkers_ <= 0)
			{
				int num;
				ThreadPool.GetMinThreads(out this.numWorkers_, out num);
			}
			this.workers_ = null;
			this.workerAgentCount_ = 0;
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x000A1C60 File Offset: 0x0009FE60
		public void setTimeStep(float timeStep)
		{
			this.timeStep_ = timeStep;
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x000A1C69 File Offset: 0x0009FE69
		private Simulator()
		{
			this.Clear();
		}

		// Token: 0x0400195A RID: 6490
		internal IDictionary<int, int> agentNo2indexDict_;

		// Token: 0x0400195B RID: 6491
		internal IDictionary<int, int> index2agentNoDict_;

		// Token: 0x0400195C RID: 6492
		internal IList<Agent> agents_;

		// Token: 0x0400195D RID: 6493
		internal IList<Obstacle> obstacles_;

		// Token: 0x0400195E RID: 6494
		internal KdTree kdTree_;

		// Token: 0x0400195F RID: 6495
		internal float timeStep_;

		// Token: 0x04001960 RID: 6496
		private static Simulator instance_ = new Simulator();

		// Token: 0x04001961 RID: 6497
		private Agent defaultAgent_;

		// Token: 0x04001962 RID: 6498
		private ManualResetEvent[] doneEvents_;

		// Token: 0x04001963 RID: 6499
		private Simulator.Worker[] workers_;

		// Token: 0x04001964 RID: 6500
		private int numWorkers_;

		// Token: 0x04001965 RID: 6501
		private int workerAgentCount_;

		// Token: 0x04001966 RID: 6502
		private float globalTime_;

		// Token: 0x04001967 RID: 6503
		private static int s_totalID = 0;

		// Token: 0x02000491 RID: 1169
		private class Worker
		{
			// Token: 0x06001A2B RID: 6699 RVA: 0x000A1C89 File Offset: 0x0009FE89
			internal Worker(int start, int end, ManualResetEvent doneEvent)
			{
				this.start_ = start;
				this.end_ = end;
				this.doneEvent_ = doneEvent;
			}

			// Token: 0x06001A2C RID: 6700 RVA: 0x000A1CA6 File Offset: 0x0009FEA6
			internal void config(int start, int end)
			{
				this.start_ = start;
				this.end_ = end;
			}

			// Token: 0x06001A2D RID: 6701 RVA: 0x000A1CB8 File Offset: 0x0009FEB8
			internal void step(object obj)
			{
				for (int i = this.start_; i < this.end_; i++)
				{
					Simulator.Instance.agents_[i].computeNeighbors();
					Simulator.Instance.agents_[i].computeNewVelocity();
				}
				this.doneEvent_.Set();
			}

			// Token: 0x06001A2E RID: 6702 RVA: 0x000A1D14 File Offset: 0x0009FF14
			internal void update(object obj)
			{
				for (int i = this.start_; i < this.end_; i++)
				{
					Simulator.Instance.agents_[i].update();
				}
				this.doneEvent_.Set();
			}

			// Token: 0x04001968 RID: 6504
			private ManualResetEvent doneEvent_;

			// Token: 0x04001969 RID: 6505
			private int end_;

			// Token: 0x0400196A RID: 6506
			private int start_;
		}
	}
}
