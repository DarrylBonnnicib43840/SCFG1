using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAI : MonoBehaviour
{
    [SerializeField]
    GameObject EnemyAI;
    public List<Vector3> freeObstacles  = new List<Vector3>();
    private GameObject enemyParent;

    // Start is called before the first frame update
    void Start()
    {
        enemyParent = new GameObject("Enemies");
        enemyParent.transform.position = new Vector3(0,0);  

         for(int i = 1;i < 11;i++)
        {
            AddEnemyAI();

            if(i==10)
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

     // A* Path Scanner
    public void Scan()
    {
        GameObject.Find("AStarGrid").GetComponent<AstarPath>().Scan();
    }

    public void AddEnemyAI()
    {
        GameObject enemy = Instantiate(EnemyAI,RandomPosition(),Quaternion.identity);

        enemy.transform.SetParent(enemyParent.transform);
    }


    public void StartAI()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<customAiMoveScript>().enabled = true;
        }
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
