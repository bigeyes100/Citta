﻿using Citta_T1.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citta_T1.Core
{
    class CyclicDetector
    {
        private List<int> vertices;
        private Dictionary<int, bool> visited;
        private Dictionary<int, bool> recStack;
        private Dictionary<int, List<int>> graph;


        public CyclicDetector(ModelDocument doc, ModelRelation mr)
        {
            InitParams(doc, mr);
        }
        private void InitParams(ModelDocument doc, ModelRelation mr)
        {
            this.vertices = new List<int>();
            this.visited = new Dictionary<int, bool>();
            this.recStack = new Dictionary<int, bool>();
            foreach (ModelElement me in doc.ModelElements)
            {
                int ID = me.ID;
                vertices.Add(ID);
                visited[ID] = false;
                recStack[ID] = false;
            }
            this.graph = new Dictionary<int, List<int>>(doc.ModelLineDict);
            if (!this.graph.ContainsKey(mr.StartID))
                this.graph[mr.StartID] = new List<int>() { mr.EndID };
            else
                this.graph[mr.StartID].Add(mr.EndID);
        }
        public bool IsCyclic()
        {
            foreach (int vertex in this.vertices)
            {
                if (!this.visited[vertex] && IsCyclic(vertex, this.visited, this.recStack))
                    return true;
            }
            return false;
        }
        private bool IsCyclic(int v, Dictionary<int, bool> vst, Dictionary<int, bool> rs)
        {
            vst[v] = true;
            rs[v] = true;
            if (this.graph.ContainsKey(v))
            {
                foreach (int neighbour in this.graph[v])
                {
                    if ((!vst[neighbour] && IsCyclic(neighbour, vst, rs)) || rs[neighbour])
                        return true;
                }
            }
            rs[v] = false;
            return false;
        }

    }       

}

