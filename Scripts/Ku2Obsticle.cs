using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ku2Obsticle : MonoBehaviour
{
    [SerializeField]
    GameObject obstaclePrefab;
    public List<Vector3> freeObstacles  = new List<Vector3>();
    private GameObject obstacleParent;


    // Start is called before the first frame update
    void Start()
    {
        obstacleParent = new GameObject("Obsticle");
        obstacleParent.transform.position = new Vector3(0,0);

        for(int i = 1;i < 6;i++)
        {
            AddObstacle();

            if(i==5)
            {
                
                Scan();
                StartAI();
            }
        }
    }
	

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAI()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<customAiMoveScript>().enabled = true;
        }
    }
   
    public Vector3 RandomPosition()
    {
        Vector3 pos;
        int xnode;
        int ynode;

        do
        {
            xnode = Random.Range(-49, 49);
            ynode = Random.Range(-49, 49);
            pos = new Vector3(xnode,ynode);
            
        }
        while(freeObstacles.Contains(new Vector3(xnode,ynode)));

        return pos;
    }
	
	public void AddObstacle()
    {
        Vector3 pos = RandomPosition();

        freeObstacles.Add(pos);

        GameObject obstacle = Instantiate(obstaclePrefab,pos,Quaternion.identity);

        obstacle.transform.SetParent(obstacleParent.transform);
    }
	public void Scan()
    {
        GameObject.Find("AStarGrid").GetComponent<AstarPath>().Scan();
    }
}

