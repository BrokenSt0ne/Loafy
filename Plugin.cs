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
            //ignore horrid code tysm
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
                UnityEngine.Time.timeScale -= 0.25f;
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                UnityEngine.Time.timeScale = 1f;
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                UnityEngine.Time.timeScale += 0.25f;
            }

        }
        void OnGUI()
        {
            GUI.Label(new Rect(250, 5, 100, 25), "LOAFY ENABLED", textStyle);
        }
    }
}
