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
    private List<PathMarker> close = new List<PathMarker>();

    public GameObject start;
    public GameObject end;
    public GameObject pathP;

    private PathMarker goalNode;
    private PathMarker startNode;

    private PathMarker lastPosition;
    private bool done = false;

    private void RemoveAllMarkers(){
        GameObject[] markers = GameObject.FindGameObjectsWithTag("marker");

        foreach (GameObject marker in markers)
        {
            Destroy(marker);
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


    }
    
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)){
            BeginSearch();
        }
    }
}
