using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text winText;
    public static int amountCount;

    private Rigidbody rb;
    private int count;

    // called once in the frame where the script starts
    private void Start(){
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
    }
    // called before creating a frame
    void Updtate(){

    }

    // called before any physics calculation
    void FixedUpdate()  {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    // called everytime we have a collision 
    private void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Pick Up")){
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    private void SetCountText(){
        countText.text = "Count: " + count.ToString();
        if (count >= amountCount) {
            winText.text = "You Win!!";
        }
    }
    public int getAmountCount() {
        return amountCount;   
    }

    public void setAmountCount(int value) {
        amountCount = value;
    }

}
