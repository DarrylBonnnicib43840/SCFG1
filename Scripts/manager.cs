using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour
{
	
	
	[SerializeField]
    GameObject obsticle;
	
    public List<Vector3> freeObsticles  = new List<Vector3>();

    [SerializeField]
    GameObject EnemyAI;

    private GameObject obsticleParent;
    private GameObject enemyParent;
	
	
    // Start is called before the first frame update
    void Start()
    {
		obsticleParent = new GameObject("Obsticles");
        obsticleParent.transform.position = new Vector3(0,0);

        enemyParent = new GameObject("Enemies");
        enemyParent.transform.position = new Vector3(0,0);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	public Vector3 RandomPos()
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
        while(freeObsticles.Contains(new Vector3(xnode,ynode)));

        return pos;
    }
	
	 public void Scan()
    {
        GameObject.Find("AStarGrid").GetComponent<AstarPath>().Scan();
    }
	
	public void AddAI()
    {
        GameObject enemy = Instantiate(EnemyAI,RandomPos(),Quaternion.identity);

        enemy.transform.SetParent(enemyParent.transform);
    }
	
	public void AddObsticles()
    {
        Vector3 pos = RandomPos();

        freeObsticles.Add(pos);

        GameObject obsticle2 = Instantiate(obsticle,pos,Quaternion.identity);
        obsticle.transform.SetParent(obsticleParent.transform);
    }
	
	public void startAI()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<customAiMoveScript>().enabled = true;
        }
    }
}
