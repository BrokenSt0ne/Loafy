using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using BepInEx;

namespace Loafy
{
    [BepInPlugin("Pheonix.Loafy", "Loafy", "1.0.1")]
    public class Plugin : BaseUnityPlugin
    {
        private GameObject Loaf;
        private BoxCollider2D loafBoxCollider;
        private CircleCollider2D loafCircleCollider;
        private PlayerController playerController;

        private GUIStyle textStyle = new GUIStyle();
        private Scene scene;

        private bool advanceNextFrameWhileFrozen = false;


        private void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            Debug.Log("Hello World!!!!!!!!!");
            DontDestroyOnLoad(gameObject);
            SetupGUIStyle();
        }


        private void SetupGUIStyle()
        {
            textStyle.fontStyle = FontStyle.Bold;
            textStyle.normal.textColor = Color.yellow;
            textStyle.fontSize = 36;
        }


        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            try
            {
                Loaf = GameObject.Find("Loaf");
                playerController = Loaf.GetComponent<PlayerController>();
                loafBoxCollider = Loaf.GetComponent<BoxCollider2D>();
                loafCircleCollider = Loaf.GetComponent<CircleCollider2D>();
            }
            catch (NullReferenceException e)
            {
                Debug.LogError("Loaf isn't in this scene");
            }
        }


        private void SpeedyLoaf()
        {
            if (playerController == null)
                return;

            if (Input.GetKeyDown(KeyCode.L))
            {
                playerController.moveSpeed += 5f;
                playerController.jumpForce += 2f;
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                playerController.moveSpeed -= 5f;
                playerController.jumpForce -= 2f;
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                playerController.moveSpeed = 12f;
                playerController.jumpForce = 17f;
            }
        }


        private void TimeControl()
        {
            if (advanceNextFrameWhileFrozen)
            {
                advanceNextFrameWhileFrozen = false;
                UnityEngine.Time.timeScale = 0f;
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                UnityEngine.Time.timeScale *= 0.25f;
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                UnityEngine.Time.timeScale = 1f;
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                UnityEngine.Time.timeScale *= 1.25f;
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                UnityEngine.Time.timeScale = 0f;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (UnityEngine.Time.timeScale == 0f)
                {
                    advanceNextFrameWhileFrozen = true;
                    UnityEngine.Time.timeScale = 1f;
                }
            }
        }


        private void AirJump()
        {
            if (playerController == null || loafBoxCollider == null || loafCircleCollider == null)
                return;

            if (Input.GetKeyDown(KeyCode.I))
            {
                AirJumpActive(!airJumpEnabled);
            }
        }


        private void AirJumpActive(bool active)
        {
            airJumpEnabled = active;
            if (active)
            {
                playerController.groundCheckRadius = 9999f;
                loafBoxCollider.enabled = false;
                loafCircleCollider.enabled = false;
                return;
            }
            playerController.groundCheckRadius = 0.2f;
            loafBoxCollider.enabled = true;
            loafCircleCollider.enabled = true;
        }


        private bool airJumpEnabled = false;


        void Update()
        {
            //ignore horrid code tysm
            //i will not
            //i wont either its horrible

            TimeControl();
            SpeedyLoaf();
            AirJump();
        }


        void OnGUI() => GUI.Label(new Rect(250, 5, 100, 25), "LOAFY ENABLED", textStyle);
    }
}
