using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;


public class customAiMoveScript : MonoBehaviour
{
    //the object that we are using to generate the path
    Seeker seeker;

    //path to follow stores the path
    Path pathToFollow;

    //a reference from the UI to the green box
    private GameObject target;

    //a reference to PointGraphObject
    GameObject graphParent;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(this.name);

        //the instance of the seeker attached to this game object
        seeker = GetComponent<Seeker>();

        target = GameObject.FindGameObjectWithTag("Player");
        

        //find the parent node of the point graph
        graphParent = GameObject.Find("AStarGrid");
        //we scan the graph to generate it in memory
        graphParent.GetComponent<AstarPath>().Scan();

        //generate the initial path
        pathToFollow = seeker.StartPath(transform.position, target.transform.position);      

        //move the red robot towards the green enemy
        StartCoroutine(moveTowardsEnemy(this.transform));
    }

    

    IEnumerator moveTowardsEnemy(Transform t)
    {

        while (true)
        {
            List<Vector3> posns = pathToFollow.vectorPath;
            Debug.Log("Positions Count: " + posns.Count);
            for (int counter = 0; counter < posns.Count; counter++)
            {
                // Debug.Log("Distance: " + Vector3.Distance(t.position, posns[counter]));
                while (Vector3.Distance(t.position, posns[counter]) >= 0.5f)
                {
                    t.position = Vector3.MoveTowards(t.position, posns[counter], 1f);
                    //since the enemy is moving, I need to make sure that I am following him
                    pathToFollow = seeker.StartPath(t.position, target.transform.position);
                    //wait until the path is generated
                    yield return seeker.IsDone();
                    //if the path is different, update the path that I need to follow
                    posns = pathToFollow.vectorPath;

                    GameObject.Find("AStarGrid").GetComponent<AstarPath>().Scan();

                    Debug.Log("@:" + t.position + " " + target.transform.position + " " + posns[counter]);
                    yield return new WaitForSeconds(0.2f);
                }
                //keep looking for a path because if we have arrived the enemy will anyway move away
                //This code allows us to keep chasing
                pathToFollow = seeker.StartPath(t.position, target.transform.position);
                yield return seeker.IsDone();
                posns = pathToFollow.vectorPath;
                //yield return null;

            }
            yield return null;
        }
    }


}