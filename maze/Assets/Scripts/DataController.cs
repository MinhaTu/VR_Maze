using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


                                                    
public class DataController : MonoBehaviour
{
    public GameObject mazeWall;
    public GameObject PickUps;
    public GameObject player;
    public GameObject Blackground;

    
    public int size = 0;
    public int si = 5;
    public int sj = 5;
    public int PickUp_number;
    private int count;

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
                    Instantiate(mazeWall, new Vector3(i, 0.5f, j), Quaternion.identity);
                }
                else if (laby[j, i] == 2) {
                    player.transform.position = new Vector3(i, 0.5f, j);
                }
                else if (laby[j, i] == 3) {
                    Instantiate(PickUps, new Vector3(i, 0.5f, j), Quaternion.identity);
                    count++;
                    PlayerController.amountCount = count;
                }
            }
        }

    }

    private void setBlackground(int si, int sj) {
        Blackground.transform.localScale = new Vector3((si) / 10.0f, 1, (sj) / 10.0f);
        Blackground.transform.position = new Vector3((float)(float)si/2.0f - 0.5f, 0, (float)(float)sj /2.0f - 0.5f);
    }


}