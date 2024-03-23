using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathMarker{
    public MapLocation location;
    public float G;
    public float H;
    public float F;
    public GameObject marker;
    public PathMarker parent;
    public PathMarker(MapLocation l,float g, float h, float f, GameObject marker, PathMarker p){
        location = l;
        G = g;
        H = h;
        F = f;
        this.marker = marker;
        parent = p;
    }

    public override bool Equals(object obj)
    {
        if ((obj == null) || !GetType().Equals(obj.GetType()))
            return false;
        else
            return location.Equals(((PathMarker) obj).location);
    }

    public override int GetHashCode()
    {
        return 0;
    }
    
}

public class FindPathStar : MonoBehaviour
{
    public Maze maze;
    public Material closeMaterial;
    public Material openMaterial;

    private List<PathMarker> open = new List<PathMarker>();
    private List<PathMarker> closed = new List<PathMarker>();

    public GameObject start;
    public GameObject end;
    public GameObject pathP;

    private PathMarker goalNode;
    private PathMarker startNode;

    private PathMarker lastPosition;
    private bool done = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)){ //Begins to search the path
            BeginSearch();
        }
        else if(Input.GetKeyDown(KeyCode.C) && !done){ //Makes a move to the Maplocation wit hthe lowest F value.
            Search(lastPosition);
        }
        else if(Input.GetKeyDown(KeyCode.M) && done){ //Once the path is found deletes all other ones except the one that reached the goal first.
            GetPath();
        }
    }

    private void BeginSearch(){
        done = false;
        RemoveAllMarkers();
        
        List<MapLocation> locations = new List<MapLocation>();

        for(int i = 1; i < maze.depth - 1; i++){
            for (int j=1; j < maze.width - 1; j++){
                if(maze.map[i,j] != 1){
                    locations.Add(new MapLocation(i,j));
                }
            }
        }

        locations.Shuffle();

        Vector3 startLocation = new Vector3(locations[0].x * maze.scale, 0, locations[0].z * maze.scale);
        startNode = new PathMarker(new MapLocation(locations[0].x,locations[0].z),0,0,0,
                                                    Instantiate(start, startLocation,Quaternion.identity), null);

        Vector3 goalLocation = new Vector3(locations[1].x * maze.scale, 0, locations[1].z * maze.scale);
        goalNode = new PathMarker(new MapLocation(locations[1].x,locations[1].z),0,0,0,
                                                    Instantiate(end, goalLocation,Quaternion.identity), null);

        open.Clear();
        closed.Clear();
        open.Add(startNode);
        lastPosition = startNode;

    }

    private void RemoveAllMarkers(){
        GameObject[] markers = GameObject.FindGameObjectsWithTag("marker");

        foreach (GameObject marker in markers)
        {
            Destroy(marker);
        } 
    }

    private void Search(PathMarker thisNode){
        if(thisNode == null) return;
        if(thisNode.Equals(goalNode)){
            done = true;
            return; // goal has been found
        }

        foreach (MapLocation dir in maze.directions)
        {
            MapLocation neighbour = dir + thisNode.location;

            if(maze.map[neighbour.x,neighbour.z] == 1) continue;

            if(neighbour.x < 1 || neighbour.x >= maze.width || neighbour.z < 1 || neighbour.z >= maze.depth) continue;

            if(IsClosed(neighbour)) continue;

            float G = Vector2.Distance(thisNode.location.ToVector(),neighbour.ToVector()) + thisNode.G;
            float H = Vector2.Distance(neighbour.ToVector(), goalNode.location.ToVector());
            float F = G + H;

            GameObject pathBlock = Instantiate(pathP, new Vector3(neighbour.x * maze.scale,0,neighbour.z * maze.scale), Quaternion.identity);

            TextMesh[] values = pathBlock.GetComponentsInChildren<TextMesh>();
            values[0].text = "G: " + G.ToString("0.00");
            values[1].text = "H: " + H.ToString("0.00");
            values[2].text = "F: " + F.ToString("0.00");

            if(!UpdateMarker(neighbour, G, H, F, thisNode)){
                open.Add(new PathMarker(neighbour, G, H, F, pathBlock, thisNode));
            }

        }

        //Orders the open nodes list by its F value in a way that if they have the same F value the one with the biggest distance "H" comes first
        open = open.OrderBy(p => p.F).ThenBy(n => n.H).ToList<PathMarker>(); 
        PathMarker pm = (PathMarker) open.ElementAt(0);
        closed.Add(pm);

        open.RemoveAt(0);
        pm.marker.GetComponent<Renderer>().material = closeMaterial;

        lastPosition = pm;

    }

    private bool UpdateMarker(MapLocation pos, float g, float h, float f, PathMarker pmr){
        foreach (PathMarker p in open)
        {
            if(p.location.Equals(pos)){
                p.G = g;
                p.H = h;
                p.F = f;
                p.parent = pmr;
                return true;
            }
        }
        return false;
    }

    private bool IsClosed(MapLocation marker){
        foreach (PathMarker pm in closed)
        {
            if(pm.location.Equals(marker)) return true;
        }
        return false;
    }

    private void GetPath(){
        RemoveAllMarkers();
        PathMarker begin = lastPosition;

        while(!startNode.Equals(begin) && begin != null){
            Instantiate(pathP, new Vector3(begin.location.x * maze.scale,0, begin.location.z * maze.scale), Quaternion.identity);
            begin = begin.parent;
        }

        Instantiate(pathP, new Vector3(startNode.location.x * maze.scale,0, startNode.location.z * maze.scale), Quaternion.identity);

    }

    
}
