using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    private List<Edge> edges = new List<Edge>();
    private List<Node> nodes = new List<Node>();
    public List<Node> pathList = new List<Node>();

    public Graph(){}

    public void AddNode(GameObject id){
        Node node = new Node(id);
        nodes.Add(node);
    }

    public void AddEdge(GameObject fromNode, GameObject toNode){
        Node from = FindNode(fromNode);
        Node to = FindNode(toNode);

        if(from != null && to != null){
            Edge e = new Edge(from, to);
            edges.Add(e);
            from.edgeList.Add(e);
        }


    }

    private Node FindNode(GameObject id){
        foreach (Node n in nodes)
        {
            if(n.getId() == id){
                return n;
            }
        }
        return null;
    }

    public bool AStar(GameObject startId, GameObject endId){
        if(startId == endId){
            pathList.Clear();
            return false;
        }

        Node start = FindNode(startId);
        Node end = FindNode(endId);

        if(start == null || end == null){
            return false;
        }

        List<Node> open = new List<Node>();
        List<Node> closed = new List<Node>();

        float tentative_g_score = 0;
        bool tentative_is_better;

        start.g = 0;
        start.h = CalculateNodeDistance(start,end);
        start.f = start.h;

        open.Add(start);
        while(open.Count > 0){
            int i = LowestF(open);
            Node thisNode = open[i];
            if(thisNode.getId() == endId){
                ReconstructPath(start,end);
                return true;
            }

            open.RemoveAt(i);
            closed.Add(thisNode);
            Node neighbour;
            foreach (Edge e in thisNode.edgeList){
                neighbour = e.endNode;

                if(closed.IndexOf(neighbour) > -1){
                    continue;
                }

                tentative_g_score = thisNode.g + CalculateNodeDistance(thisNode,neighbour);
                if(open.IndexOf(neighbour) == -1){
                    open.Add(neighbour);
                    tentative_is_better = true;
                }
                else if(tentative_g_score < neighbour.g){
                    tentative_is_better = true;
                }
                else{
                    tentative_is_better = false;
                }

                if(tentative_is_better){
                    neighbour.cameFrom = thisNode;
                    neighbour.g = tentative_g_score;
                    neighbour.h = CalculateNodeDistance(thisNode,end);
                    neighbour.f = neighbour.g + neighbour.h;
                }
            }

        }
        return false;

    }

    private void ReconstructPath(Node startId, Node endId){
        pathList.Clear();
        pathList.Add(endId);

        var p = endId.cameFrom;
        while(p != startId && p != null){
            pathList.Insert(0,p);
            p = p.cameFrom;
        }
        pathList.Insert(0, startId);
        

    }

    private float CalculateNodeDistance(Node a, Node b){
        return Vector3.SqrMagnitude(a.getId().transform.position - b.getId().transform.position);
    }

    private int LowestF(List<Node> nodeList){
        float lowestf = 0;
        int count = 0;
        int iteratorCount = 0;

        lowestf = nodeList[0].f;

        for (int i = 1; i < nodeList.Count; i++)
        {
            if(nodeList[i].f <= lowestf){
                lowestf = nodeList[i].f;
                iteratorCount = count;
            }
            count++;
        }
        return iteratorCount;


    }

}
