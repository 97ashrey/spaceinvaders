using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AlienController : MonoBehaviour
{
    public static event Action OnALienDeath;
    public event Action OnNextWave;

    private const float ONE_FIFTH = 5;

    [SerializeField]
    private float numberOfColumns = 2f;
    public float numberOfRows = 5f;
    public float distanceBetweenAliens = 1.28f;
    public float moveDownBy = 0.16f;
    [SerializeField]
    private GameObject[] alienTypes;
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private float minMoveAfter = 0.5f;
    [SerializeField]
    private float maxMoveAfter = 0.01f;
    [SerializeField]
    private float firstAlienFifth=0.4f;
    [SerializeField]
    private float secondAlienFifth = 0.3f;
    [SerializeField]
    private float chanceToShoot=10f;

    private float direction = 1;
    private List<AlienComponents> alienComps = new List<AlienComponents>();
    private int numberOfAliens;
    private float moveTimer = 0f;
    private float firstHeight;
    private float landPosY;
    private float distance;
    private float downSpeedInc;
    private float currentMoveAfter;
    private float moveY; //stores the next controller height
    private Vector3 firstPosition;

    void Start()
    {
        firstPosition = transform.position;
        //CalculateLandDistance();
        StartGame();
        numberOfAliens = alienComps.Count;       
    }

    void FixedUpdate()
    {
        if(GameController.state == GameController.GameState.play)
        {
            moveTimer += Time.deltaTime;
            if(moveTimer>=currentMoveAfter)
            {
                moveTimer = 0f;
                MoveAliens();
                RandomShoot();
            }
        }   
    }

    private void Update()
    {
        if (GameController.state == GameController.GameState.play)
        {
            SpeedUp();
            if (alienComps.Count == 0)
            {
                StartGame();
                if (OnNextWave != null)
                {
                    OnNextWave();
                }
            }
        }
    }


    void CreateAliens()
    {
        for (int i = 0; i < numberOfRows; i++)
        {  
            for (int j = 0; j < numberOfColumns; j++)
            {
                Vector3 spawnPosition = transform.position; //spawnposition gets the value from the position of the allien controller
                spawnPosition.x += distanceBetweenAliens * j; //x value gets incremented by multiplying the distance with a counter
                spawnPosition.y -= distanceBetweenAliens * i; //same for y value
                //Create a new allien game object
                GameObject newAlien =Instantiate(alienTypes[i], spawnPosition, Quaternion.identity,transform);
                if (newAlien.GetComponent<AlienComponents>() != null)
                {
                    alienComps.Add(newAlien.GetComponent<AlienComponents>());
                }
            }
        }
    }

    /* Delta time Movement
    void MoveAliens()
    {
        Vector2 newPosition = new Vector2(currentSpeed * Time.deltaTime, 0f); //we calculate the newposition
        ChangeDirection(newPosition); //checks to see if direction should change
        newPosition *= direction; //apply the direction to the newposition
        for (int i = 0; i < alienRb2ds.Count; i++) //apply the newposition to all aliens
        {
            Vector2 moveTo = alienRb2ds[i].position + newPosition;
            moveTo.x = ProjectMethods.PixelRound(moveTo.x);
            alienRb2ds[i].MovePosition(moveTo);
        }
    }
    */
    void MoveAliens()
    {
        Vector2 newPosition = new Vector2(speed, 0f); //we calculate the newposition
        ChangeDirection(newPosition); //checks to see if direction should change
        newPosition *= direction; //apply the direction to the newposition
        for (int i = 0; i < alienComps.Count; i++) //apply the newposition to all aliens
        {
            Vector2 moveTo = alienComps[i].rb2d.position + newPosition;
            moveTo.x = ProjectMethods.TwoDecimalRound(moveTo.x);
            if (alienComps[i].rb2d != null)
            {
                alienComps[i].rb2d.MovePosition(moveTo);
            }
            alienComps[i].animController.SetTick();
        }
    }

    void ChangeDirection(Vector2 moveBy)
    {
        //Here we look for the alien that is going to reach the edge of the screen
        for (int i = 0; i < alienComps.Count; i++) 
        {
            //we apply the current direction to the newposition 
            //and we add it to the current position of the allien
            Vector2 moveTo = alienComps[i].rb2d.position + (moveBy*direction);
            moveTo.x = ProjectMethods.TwoDecimalRound(moveTo.x); //rounds the position so it had 2 decimal points
            //then we check if we reached the edge of the screen
            if (Camera.main.WorldToScreenPoint(moveTo + (alienComps[i].bc2d.size / 2)).x > Screen.width || Camera.main.WorldToScreenPoint(moveTo - (alienComps[i].bc2d.size / 2)).x < 0)
            {
                direction *= -1; //direction gets changed
                MoveDown(); //and all aliens are moved down
                break; //we break from the loop because we changed direction
            }
        }
    }

    void MoveDown()
    {
        Vector3 moveTo = transform.position;
        moveTo.y -= moveDownBy;
        moveTo.y = ProjectMethods.TwoDecimalRound(moveTo.y);
        transform.position = moveTo;
        GoingDownSpeedUp();
    }

    public void KillAlien(GameObject alienToKill)
    {
        //removes references for the alien from the list
        for (int i = 0; i < alienComps.Count; i++)
        {
            if (alienComps[i].gameObject == alienToKill)
            {
                alienComps.RemoveAt(i);
                Destroy(alienToKill);
                if(OnALienDeath!=null)
                {
                    OnALienDeath();
                }
                break;
            }
        }

    }

    //Here the controller looks for aliens who can shoot
    private void AllowShoot()
    {
        if (alienComps.Count == 0)
        {
            return;
        }
        List<int> theyCanShoot = new List<int>();
        for (int i = 0; i < alienComps.Count; i++)
        {
            if (alienComps[i].alienShooting.CanShoot())
            {
                //if an alien can shoot his index from aliens list is stored in a new list
                theyCanShoot.Add(i);
            }
        }
        //a radnom nubmer is generated between 0 and the number of aliens who can shoot
        int rnd = UnityEngine.Random.Range(0, (theyCanShoot.Count));
        alienComps[theyCanShoot[rnd]].alienShooting.AllowShoot(); //then we allow that random alien to shoot
        theyCanShoot.Clear();
    }

    void RandomShoot()
    {
        int rnd = UnityEngine.Random.Range(1, 101);
        Debug.Log(rnd.ToString());
        if (rnd <=chanceToShoot)
        {
            AllowShoot();
        }
    }

    //used in gamecontroller to see if the aliens reached the ground
    public bool ComparePositions(Transform positionToCompare)
    {
        for (int i = 0; i < alienComps.Count; i++)
        {
            if (alienComps[i].rb2d.position.y <= positionToCompare.position.y)
            {
                return true;
            }
        }
        return false;
    }

    void StartGame()
    {
        ControllerPosition(); //for each new wave the aliens are moved down by moveDownBy
        CreateAliens();
        currentMoveAfter = minMoveAfter; //moveAfter is set to the minMoveAfter
        direction = 1; //direction is set to right
    }
    
    void ControllerPosition()
    {
        //moves the controller position from its original position
        Vector3 newPosition = firstPosition;
        newPosition.y -= moveY;
        transform.position = newPosition;
        moveY += moveDownBy;
    }

    void CalculateLandDistance()
    {
        //calucaltes the distance between the aliencontroller and the land
        firstHeight = transform.position.y;
        landPosY = GameObject.FindWithTag("Player").transform.position.y;
        distance = firstHeight - landPosY;
    }

    void SpeedUp()
    {
        float aliensKilled = numberOfAliens - alienComps.Count;
        if (aliensKilled == numberOfAliens * 1/ONE_FIFTH)
        {
            currentMoveAfter = firstAlienFifth;
        }
        else if (aliensKilled == numberOfAliens *2/ONE_FIFTH)
        {
            currentMoveAfter = secondAlienFifth;
            CalculateLandDistance();
            CalculateDownSpeedInc();
        }
    }

    void CalculateDownSpeedInc()
    {
        float toMaxSpeed = currentMoveAfter - maxMoveAfter; //we find how much we need to increase the currentMoveAfter till we reach the max value
        downSpeedInc = toMaxSpeed / distance; //then we check how much to increase the current moveAfter each time the aliens move down 
        downSpeedInc = ProjectMethods.TwoDecimalRound(downSpeedInc); 
    }

    void GoingDownSpeedUp()
    {
        currentMoveAfter -= downSpeedInc;
        if (currentMoveAfter < maxMoveAfter)
        {
            currentMoveAfter = maxMoveAfter;
        }
    }
}
