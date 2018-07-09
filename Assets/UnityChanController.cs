using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour {

    public float speed = 1.0f;
    public float forwardForce = 800.0f;
    public float turnForce = 500.0f;
    public float upForce = 500.0f;
    public float movableRange = 3.4f;

    GameObject stateText;
    GameObject scoreText;
    GameObject generatorWall;
    Animator myAnimator;
    Rigidbody myRigidbody;

    bool moveL = false;
    bool moveR = false;
    bool jump = false;
    bool isEnd = false;
    bool isLButtonDown = false;
    bool isRButtonDown = false;
    int score = 0;
    float coefficient = 0.95f;

    void Start()
    {
        this.stateText = GameObject.Find("GameResultText");
        this.scoreText = GameObject.Find("ScoreText");
        this.myAnimator = GetComponent<Animator>();
        this.myAnimator.SetFloat("Speed", speed);
        this.myRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(this.isEnd)
        {
            this.forwardForce *= this.coefficient;
            this.turnForce *= this.coefficient;
            this.upForce *= this.coefficient;
            this.myAnimator.speed *= this.coefficient;
        }

      if((Input.GetKey(KeyCode.A) || this.isLButtonDown) && -this.movableRange < this.transform.position.x)
        {
            moveL = true;
        }  
      else if((Input.GetKey(KeyCode.D) || this.isRButtonDown) && this.transform.position.x < this.movableRange)
        {
            moveR = true;
        }
      if(Input.GetKeyDown(KeyCode.Space) && this.transform.position.y <0.5f)
        {
            jump = true;
        }
      if(this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool("Jump", false);
        }
    }

    void FixedUpdate()
    {
        this.myRigidbody.AddForce(this.transform.forward * this.forwardForce);

        if (moveL)
        {
            this.myRigidbody.AddForce(-this.turnForce, 0, 0);
            moveL = false;
        }
        if (moveR)
        {
            this.myRigidbody.AddForce(this.turnForce, 0, 0);
            moveR = false;
        }
        if(jump)
        {
            this.myAnimator.SetBool("Jump", true);
            this.myRigidbody.AddForce(this.transform.up * this.upForce);
            jump = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag")
        {
            this.isEnd = true;
            this.stateText.GetComponent<Text>().text = "GAME OVER";
        }

        if (other.gameObject.tag == "GoalTag")
        {
            this.isEnd = true;
            this.stateText.GetComponent<Text>().text = "CLEAR!!";
        }

        if (other.gameObject.tag == "CoinTag")
        {
            this.score += 10;
            this.scoreText.GetComponent<Text>().text = "Score" + this.score + "pt";
            GetComponent<ParticleSystem>().Play();

            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "Respawn")
        {
            var generator = other.GetComponent<ItemGenerator>();
            generator.GenerateItems();
        }
    }

    public void GetMyLeftButtonDown()
    {
        this.isLButtonDown = true;
    }

    public void GetMyLeftButtonUp()
    {
        this.isLButtonDown = false;
    }

    public void GetMyRightButtonDown()
    {
        this.isRButtonDown = true;
    }

    public void GetMyRightButtonUp()
    {
        this.isRButtonDown = false;
    }

    public void GetMyJumpButtonDown()
    {
        if (this.transform.position.y < 0.5f)
        {
            jump = true;
        }
    }
}