using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


                                                    
public class DataController : MonoBehaviour
{
    public GameObject mazeWall;
    public GameObject PickUps;
    public GameObject player;
    public GameObject Blackground;
    public GameObject Particle;
    private GameObject wall;
    public GameObject Plateform;
    public int size = 0;
    public int si = 5;
    public int sj = 5;
    public  int PickUp_number;
    private int count;

    public static int[] position = new int[3];
    private GameObject par;
    private BoxCollider box;
    public float BoxSize = 1.4f;
    void Start() {
        count = 0;
        short[,] laby;
        Maze maze = new Maze(si, sj);
        maze.setHauntandKill();
        laby = maze.getGrid();
        maze.setBall_position();
        maze.setPickUps(PickUp_number);
        setBlackground(si, sj);

        
        for (int j = 0; j < sj; j++) {
            for (int i = 0; i < si; i++) {
                if (laby[j, i] == 0) {
                 wall =  Instantiate(mazeWall, new Vector3(i, 0.5f, j), Quaternion.identity);
                  // box = wall.GetComponent<BoxCollider>();
                  // box.size = (new Vector3(BoxSize, BoxSize, BoxSize));
                }
                else if (laby[j, i] == 2) {
                    player.transform.position = new Vector3(i, 0.5f, j);
                }
                else if (laby[j, i] == 3) {
                  //  Instantiate(PickUps, new Vector3(i, 0.5f, j), Quaternion.identity);
                    par = Instantiate(Particle, new Vector3(i, 0, j), Quaternion.identity);
                    par.transform.localRotation = Quaternion.Euler(-90f, 90f, 0.1f);
                    count++;
                    PlayerController.amountCount = count;
                }
            }
        }
        middle(si, sj);
        Vector3 aux = new Vector3(DataController.position[0], 10f, DataController.position[1]);
        Plateform.transform.position = aux;
        Plateform.transform.localScale = new Vector3((float)si * 2f, (float)si * 2f, (float)si * 0.8f);

        Autowalk.amountCount = PickUp_number;

    }

    private void setBlackground(int si, int sj) {
        Blackground.transform.localScale = new Vector3((si) / 10.0f, 1, (sj) / 10.0f);
        Blackground.transform.position = new Vector3((float)(float)si/2.0f - 0.5f, 0, (float)(float)sj /2.0f - 0.5f);
    }

    public void middle(int si, int sj) {
        
        position[0] = (si - 2) / 2;
        position[1] = (sj - 2) / 2;
        position[2] = si;
    }
}