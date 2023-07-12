using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Unity;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using BepInEx;

namespace Loafy
{
    [BepInPlugin("Pheonix.Loafy", "Loafy", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        GUIStyle textStyle = new GUIStyle();
        private Scene scene;

        private Boolean advanceNextFrameWhileFrozen = false;
        void Awake()
        {
            Debug.Log("Hello World!!!!!!!!!");

            DontDestroyOnLoad(gameObject);

            scene = SceneManager.GetActiveScene();
        }
        public void Start()
        {
            textStyle.fontStyle = FontStyle.Bold;
            textStyle.normal.textColor = Color.yellow;
            textStyle.fontSize = 36;

            
        }

        void Update()
        {
            if (advanceNextFrameWhileFrozen) {
                advanceNextFrameWhileFrozen = false;
                UnityEngine.Time.timeScale = 0f;
            }
            //ignore horrid code tysm
            //i will not
            if(Input.GetKeyDown(KeyCode.I))
            {
                
                GameObject.Find("Loaf").GetComponent<PlayerController>().groundCheckRadius = 9999f;
                GameObject.Find("Loaf").GetComponent<BoxCollider2D>().enabled = false;
                GameObject.Find("Loaf").GetComponent<CircleCollider2D>().enabled = false;
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                GameObject.Find("Loaf").GetComponent<PlayerController>().groundCheckRadius = 0.2f;
                GameObject.Find("Loaf").GetComponent<BoxCollider2D>().enabled = true;
                GameObject.Find("Loaf").GetComponent<CircleCollider2D>().enabled = true;
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                GameObject.Find("Loaf").GetComponent<PlayerController>().moveSpeed += 5f;
                GameObject.Find("Loaf").GetComponent<PlayerController>().jumpForce += 2f;
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                GameObject.Find("Loaf").GetComponent<PlayerController>().moveSpeed -= 5f;
                GameObject.Find("Loaf").GetComponent<PlayerController>().jumpForce -= 2f;
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                GameObject.Find("Loaf").GetComponent<PlayerController>().moveSpeed = 12f;
                GameObject.Find("Loaf").GetComponent<PlayerController>().jumpForce = 17f;
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
            if (Input.GetKeyDown(KeyCode.Q)) {
                UnityEngine.Time.timeScale = 0f;
            }
            if (Input.GetKeyDown(KeyCode.E)) {
                if (UnityEngine.Time.timeScale == 0f) {
                    advanceNextFrameWhileFrozen = true;
                    UnityEngine.Time.timeScale = 1f;
                }
            }
            if (Input.GetKeyDown(KeyCode.Y)) {
                Array objects = FindGameObjectsInLayer(LayerMask.NameToLayer("Lava"));
                foreach (GameObject gameobject in objects) {
                    gameobject.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
            if (Input.GetKeyDown(KeyCode.U)) {
                Array objects = FindGameObjectsInLayer(LayerMask.NameToLayer("Lava"));
                foreach (GameObject gameobject in objects) {
                    gameobject.GetComponent<BoxCollider2D>().enabled = true;
                }
            }

        }
        void OnGUI()
        {
            GUI.Label(new Rect(250, 5, 100, 25), "LOAFY ENABLED", textStyle);
        }

        GameObject[] FindGameObjectsInLayer(int layer)
        {
            var goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
            var goList = new System.Collections.Generic.List<GameObject>();
            for (int i = 0; i < goArray.Length; i++)
            {
                if (goArray[i].layer == layer)
                {
                    goList.Add(goArray[i]);
                }
            }
            if (goList.Count == 0)
            {
                return null;
            }
            return goList.ToArray();
        }
    }
}
