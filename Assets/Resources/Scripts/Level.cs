using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Resources.Scripts
{
    public class Level : MonoBehaviour
    {
        //level objects contain all the info to create a level:
        public OVRPlayerController Player;                  //player object
        public int LevelNum;                                //level number

        public GameObject[] InteractableSpawnPoints;        //start positions of objects the player can pick up
        public GameObject[] TargetSpawnPoints;              //start positions  of objects the player aims for (hoops)
        public Text Hud;                                 //heads-up display text (the score)
        public Text level;
        public int Goal;                                    //the score goal
        public LevelManager Levelmanager;                   //reference to our scene's levelmanager
        public GameObject Confetti;                         //confetti animation
        public Camera Cam;                                  //player main camera
        public bool EndScreen;

        private int _score;                                 //current score
        private GameObject _nextposition;                   //array of player positions
        private readonly List<GameObject> _interactables = new List<GameObject>();    //list of current interactables (planes/boats/balloons)
        private float _timer;

        public Image leftArrow;
        public Image rightArrow;
        Score score;
        Achievements a;

        public Text diffText;

        public void Start()
        {

            diffText.text = "Difficulty: " + (Data.difficulty * 10).ToString("0.0") + "%";
            //set default score, set timer, set player positions, hide other levels' enclosures
            leftArrow.enabled = false;
            rightArrow.enabled = false;
            _score = 0;
            _timer = 0.0f;
            Data.planesHit = 0;
            Data.planesMissed = 0;
            Data.totalPlanes = 0; 
            score = GameObject.Find("Score").GetComponent<Score>();
            a = GameObject.Find("Achievements").GetComponent<Achievements>();
            //Debug.Log(Data.getLevelOfAssistance());
            _nextposition = Levelmanager.GetNextLevel(LevelNum);
            level.text = "LEVEL: " + Data.level;
            SetLevelEnclosure();

        }

      
        //set a new goal
        public void SetGoal(int newGoal)
        {
            Goal = newGoal;
        } 

        //trigger creation of new interactable objects
        public void SpawnInteractableObjects()
        {
            foreach (GameObject interactable in InteractableSpawnPoints)
            {
                _interactables.Add(interactable.GetComponent<Spawn>().SpawnPrefab());
                if (interactable.name.Equals("landing2"))
                {
                    GameObject.Find("ParticleRight").GetComponent<ParticleSystem>().Play();
                }
                else if (interactable.name.Equals("landing1"))
                {
                    GameObject.Find("ParticleLeft").GetComponent<ParticleSystem>().Play();
                }

                
            }
        }

        //update heads up display
        public void Update()
        {
            _timer += Time.deltaTime;
            string scoreString = "score: ";
            string slash = "/";
            // to be replaced
            /*
            if (EndScreen)
            {
                string hudtext = "\n\n\n\n\t\tPoints:           Time:\n";
                int [] goals = Levelmanager.GetGoals();
                float [] times = Levelmanager.GetTimes();
                for (int goal = 1; goal < goals.Length - 1; goal++)
                {
                    hudtext += "\t\t" + goals[goal] + "\t\t\t\t\t" + times[goal].ToString("0.00") + " sec\n";
                }
                Hud.text = hudtext;
            }
            else
            */
            {
                Hud.text = scoreString + _score + slash + Goal;  //score format: "score: xxxx/goal"
            }

            foreach (GameObject g in _interactables)
            {
                if (g == null)
                    return;
                Vector3 screenPos = Cam.WorldToScreenPoint(g.transform.position);
                //Debug.Log(g.gameObject.name + ": " + screenPos.x);
                if (g.gameObject.name.Equals("plane(Clone)"))
                {
                    if (CanSee(g))
                    {
                        rightArrow.enabled = false;
                    }
                    else
                    {
                        rightArrow.enabled = true; 
                    }
                }
                else if (g.gameObject.name.Equals("jet(Clone)"))
                {

                    if (CanSee(g))
                    {
                        leftArrow.enabled = false;
                        
                    }
                    else
                    {
                        leftArrow.enabled = true;
                    }
                }
               
            }

        }
        private bool CanSee(GameObject Obj)
        {
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Cam);
            if (GeometryUtility.TestPlanesAABB(planes, Obj.GetComponent<BoxCollider>().bounds))
                return true;
            else
                return false;
        }

        IEnumerator TransitionToNextLevel(int nextLevel)
        {
            yield return new WaitForSeconds(5);
            Cam.GetComponent<OVRScreenFade>().StartFade();
            //set back to default hand poses
            Player.GetComponentInChildren<OvrAvatarLeftHand>().SetDefaultHandPose();
            Player.GetComponentInChildren<OvrAvatarRightHand>().setDefaultHandPose();
            //move player to new position
            Levelmanager.SetPlayerPosition(nextLevel);
            Cam.GetComponent<OVRScreenFade>().OnLevelFinishedLoading(LevelNum-1);
            LevelNum++;
            Destroy(GameObject.FindGameObjectWithTag("Hoop"));
            Destroy(Confetti);
            Confetti = (GameObject) UnityEngine.Resources.Load("Prefabs&Objects/Confetti");
            _interactables.Clear();
            Data.incrementLevel();
            Data.level += 1;
            
            /*if (Data.level == Data.levelToOscillate)
            {
                Data.willOscillate = true;
            }
            if (Data.level > Data.maxLevel)
            {
                Data.resetValues();
            }*/
            score.level = Data.level;
            score.Save();
            a.checkAllAchievements(Data.level);
               SceneManager.LoadScene("PlaneGame");
            
            //Levelmanager.NextLevel();
        }

        void SetLevelEnclosure()
        {
            //set all previous enclosure to disabled, and enable this level's enclosure
            Levelmanager.GetPreviousLevel(LevelNum).gameObject.GetComponent<EnableEnclosure>().Disable();
            gameObject.GetComponent<EnableEnclosure>().Enable();
        }
    
        public void DecrementInteractables(GameObject interactable)
        {
            _interactables.Remove(interactable);
            if (_interactables.Count == 0 && _score != Goal)
            {
                SpawnInteractableObjects();
            }
        }
        public void IncrementScore()
        {
            _score += 100;
            Data.planesHit++;
            if (_score == Goal)
            {
                Confetti = Instantiate(Confetti, Cam.transform);
                //destroy interactables (plane/boat/balloon)
                foreach (GameObject interact in _interactables)
                {
                    Destroy(interact);
                }//destroy interactables (plane/boat/balloon)
                foreach (GameObject interact in _interactables)
                {
                    Destroy(interact);
                }
                // here we would UI prompt to continue to next level
                StartCoroutine(TransitionToNextLevel(LevelNum + 1));            
            }
        }

        // getters

        public int GetScore()
        {
            return _score;
        }

        public float GetTime()
        {
            return _timer;
        }
    }
}