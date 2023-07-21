using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using BepInEx;
using System.Threading.Tasks;
using HarmonyLib;
using System.Reflection;

namespace Loafy
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]

    public class Plugin : BaseUnityPlugin
    {
        private GameObject Loaf;
        private BoxCollider2D loafBoxCollider;
        private CircleCollider2D loafCircleCollider;
        private PlayerController playerController;

        private GUIStyle textStyle = new GUIStyle();
        private Scene scene;

        private bool advanceNextFrameWhileFrozen = false;
        private bool airJumpEnabled = false;


        private void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            //Debug.Log("Hello World!!!!!!!!!");
            //DontDestroyOnLoad(gameObject);
            SetupGUIStyle(fontStyle: FontStyle.Bold, textColor: Color.yellow, fontSize: 36, out textStyle);
            HarmonyPatches.ApplyPatches();

            // add null checks to the following lines, they are omitted for clarity
            // when possible, don't use string and instead use nameof(...)
        }

        /*private void SetupGUIStyle()
        {
            textStyle.fontStyle = FontStyle.Bold;
            textStyle.normal.textColor = Color.yellow;
            textStyle.fontSize = 36;
        }*/


        private void SetupGUIStyle(FontStyle fontStyle, Color textColor, int fontSize, out GUIStyle output)
        {
            GUIStyle style = new GUIStyle();
            style.fontStyle = fontStyle;
            style.normal.textColor = textColor;
            style.fontSize = fontSize;
            output = style;
        }

        public static float loafScale = 1;

        private float loafScaleIncrement = 0.1f;
        private float scaleMultipler = 6;
        
        public void SizeChanger()
        {
            if (Loaf == null || playerController == null)
                return;

            if (Input.GetKey(KeyCode.UpArrow))
            {
                loafScale += loafScaleIncrement * Time.deltaTime * scaleMultipler;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                loafScale -= loafScaleIncrement * Time.deltaTime * scaleMultipler;
            }
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                loafScale = 1;
            }
        }

        private async void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            try
            {
                playerController = FindObjectOfType<PlayerController>();
                Loaf = playerController.gameObject;

                loafBoxCollider = Loaf.GetComponent<BoxCollider2D>();
                loafCircleCollider = Loaf.GetComponent<CircleCollider2D>();
                loafScale = 1;
            }
            catch (NullReferenceException e)
            {
                Debug.LogError("Loaf isn't in this scene");
            }
        }

        private float movementSpeedIncrement = 5f;
        private float jumpForceIncrement = 2f;

        private float defaultMovementSpeed = 12f;
        private float defaultJumpForce = 17f;

        private void SpeedyLoaf()
        {
            if (playerController == null)
                return;

            if (Input.GetKeyDown(KeyCode.L))
            {
                playerController.moveSpeed += movementSpeedIncrement;
                playerController.jumpForce += jumpForceIncrement;
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                playerController.moveSpeed -= movementSpeedIncrement;
                playerController.jumpForce -= jumpForceIncrement;
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                playerController.moveSpeed = defaultMovementSpeed;
                playerController.jumpForce = defaultJumpForce;
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
            if (Loaf == null)
                return;

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
        void Update()
        {
            //ignore horrid code tysm
            //i will not
            //i wont either its horrible
            
            TimeControl();
            SpeedyLoaf();
            AirJump();
            SizeChanger();
        }


        void OnGUI() => GUI.Label(new Rect(250, 5, 100, 25), "LOAFY ENABLED", textStyle);
    }
}
