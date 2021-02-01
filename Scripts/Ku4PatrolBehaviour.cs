using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ku4PatrolBehaviour : MonoBehaviour
{
    public List <Transform> waypoints; 

    public Transform objectToMove;

    public List<Vector3> freeObstacles  = new List<Vector3>();

    [SerializeField]
    GameObject ObstacleAI;

    private GameObject obstacleParent;
    

    // Start is called before the first frame update
    void Start()
    {
        obstacleParent = new GameObject("Obstacles");
        obstacleParent.transform.position = new Vector3(0,0);

        for(int i = 1;i < 4;i++)
        {
            AddObstaclesAI();
            StartCoroutine(moveMe());

            if(i==3)
            {                
                Scan();
                StartObstacles();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
                
    }

    IEnumerator moveMe()
    {
        foreach(Transform mytransform in waypoints)
        {
            while(Vector3.Distance(objectToMove.position, mytransform.position) > 0.1f )
            {
                objectToMove.position = Vector3.MoveTowards(objectToMove.position, mytransform.position, 1f);
                                  
            }
            yield return new WaitForSeconds(2f);
        }


        yield return null;
    }


    public void StartObstacles()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obsticle");

        foreach (GameObject obstacle in obstacles)
        {
            obstacle.GetComponent<customAiMoveScript>().enabled = true;
        }
    }

    // A* Path Scanner
    public void Scan()
    {
        GameObject.Find("AStarGrid").GetComponent<AstarPath>().Scan();
    }

    public void AddObstaclesAI()
    {
        GameObject obstacle = Instantiate(ObstacleAI,RandomPosition(),Quaternion.identity);

        obstacle.transform.SetParent(obstacleParent.transform);
    }

     //calculate random spawn position
    public Vector3 RandomPosition()
    {
        Vector3 pos;
        int xCoordinate;
        int yCoordinate;

        do
        {
            xCoordinate = Random.Range(-49, 49);
            yCoordinate = Random.Range(-49, 49);
            pos = new Vector3(xCoordinate,yCoordinate);
            print("X: " + xCoordinate + " " + "Y: " + yCoordinate);
        }
        while(freeObstacles.Contains(new Vector3(xCoordinate,yCoordinate)));

        return pos;
    }

}
