using i01.Game;
using i01dew.Player;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UdpKit;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace i01dew.Ui
{
    public static class Manager
    {
        static bool showDirectConnect = false;
        static string serverAddress = "Server Address";

        public static void Update()
        {
            if (Input.GetKeyDown(KeyCode.BackQuote))
                showDirectConnect = !showDirectConnect;

            if (BoltNetwork.SessionList != null && BoltNetwork.SessionList.Count > 0)
            {
                foreach (KeyValuePair<Guid, UdpSession> current in BoltNetwork.SessionList)
                {
                    if (current.Value.GetProtocolToken() == null)
                        Debug.Log("TOKEN NULL");

                    Debug.Log((current.Value.GetProtocolToken() as LobbyToken).Description);
                }
            }
        }

        public static void OnGUI()
        {
            float num = Time.deltaTime * 1000f;
            float num2 = 1f / Time.deltaTime;
            string fps = string.Format("{0:0.0} ms ({1:0.} fps)", num, num2);

            GUIStyle gUIStyle = new GUIStyle(GUI.skin.label);
            gUIStyle.alignment = TextAnchor.UpperRight;

            GUI.Label(new Rect(0, 10, Screen.width - 15, Screen.height), "Installation 01 - 0.2.7" + Environment.NewLine + fps, gUIStyle);

            if(showDirectConnect)
            {
                //GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");

                //GUIStyle gUIStyle = new GUIStyle();
                //gUIStyle.alignment = TextAnchor.LowerCenter;
                //gUIStyle.fontSize = 72;
                //gUIStyle.normal.textColor = Color.white;
                //gUIStyle.fontStyle = FontStyle.Bold;

                //GUI.Label(new Rect(0, 0, Screen.width, (Screen.height / 2) - 150), "i01dew v0", gUIStyle);
                //gUIStyle = new GUIStyle(GUI.skin.box);
                //gUIStyle.alignment = TextAnchor.UpperLeft;
                //gUIStyle.fontSize = 18;
                //gUIStyle.fontStyle = FontStyle.Bold;
                //GUI.Box(new Rect((Screen.width / 2) - 315, (Screen.height / 2) - 150, 300, 300), "Player Profile", gUIStyle);
                //GUI.Box(new Rect((Screen.width / 2) + 15, (Screen.height / 2) - 150, 300, 300), "Direct Connect", gUIStyle);

                gUIStyle = new GUIStyle(GUI.skin.box);
                gUIStyle.normal.background = Utility.Texture2D.MakeTexture(2, 2, new Color(0f, 0f, 0f, 0.85f));

                GUI.BeginGroup(new Rect(10, (Screen.height / 2) + 15, 200, 50));

                GUI.Box(new Rect(0, 0, 200, 50), "Direct Connect", gUIStyle);
                serverAddress = GUI.TextField(new Rect(0, 30, 125, 20), serverAddress);
                if (GUI.Button(new Rect(125, 30, 75, 20), "Connect"))
                {
                    int? port = null;
                    string ipstr = null;
                    if (serverAddress.Contains(':'))
                    {
                        string[] ipPort = serverAddress.Split(':');
                        if(ipPort.Length == 2)
                        {
                            int tryPort;
                            if(int.TryParse(ipPort[1], out tryPort))
                            {
                                port = tryPort;

                                ipstr = ipPort[0];
                            }
                        }
                    }

                    if (ipstr == null)
                        ipstr = serverAddress;

                    if (!ipstr.Contains('.'))
                        return;

                    string[] strValues = ipstr.Split('.');
                    int[] intValues = new int[4];
                    if (strValues.Length != 4)
                        return;

                    for (int i = 0; i < 4; i++)
                    {
                        if (int.TryParse(strValues[i], out int intValue))
                        {
                            intValues[i] = intValue;
                        }
                        else
                            return;
                    }

                    PlayerToken playerToken = new PlayerToken();
                    playerToken.ImportFromPlayerProfile(PlayerProfileManager.PlayerProfile);

                    i01.UI.Manager.Instance.activeID = i01.UI.Manager.PanelGroupID.CustomGamesLobby;
                    i01.UI.Manager.Instance.SceneManagerOnActiveSceneChanged(default(Scene), SceneManager.GetActiveScene());
                    i01.UI.Manager.Instance.activeID = i01.UI.Manager.PanelGroupID.InGame;

                    if (port != null)
                        BoltNetwork.Connect(new UdpEndPoint(new UdpIPv4Address((byte)intValues[0], (byte)intValues[1], (byte)intValues[2], (byte)intValues[3]), (ushort)port), playerToken);

                    else
                        BoltNetwork.Connect(new UdpEndPoint(new UdpIPv4Address((byte)intValues[0], (byte)intValues[1], (byte)intValues[2], (byte)intValues[3]), 25000), playerToken);
                }

                GUI.EndGroup();
            }
        }

        public static void SceneManagerOnActiveSceneChanged()
        {
            GameObject boltConsoleObject = new GameObject();
            boltConsoleObject.AddComponent<BoltConsole>();
            showDirectConnect = false;

            ////foreach (GameObject gameObj in UnityEngine.Object.FindObjectsOfType<GameObject>())
            ////{
            ////    Debug.Log(gameObj.name);
            ////}

            //var myLoadedAssetBundle = AssetBundle.LoadFromFile(Directory.GetParent(Application.dataPath).FullName + "\\guardian");
            //if (myLoadedAssetBundle == null)
            //{
            //    Debug.Log("Failed to load AssetBundle!");
            //    return;
            //}

            //var prefab = myLoadedAssetBundle.LoadAsset<GameObject>("GuardianPrefab 1");
            //GameObject shrine = UnityEngine.Object.Instantiate(prefab);

            ////GameObject primitive = GameObject.CreatePrimitive(PrimitiveType.Plane);
            ////primitive.SetActive(false);
            ////Material diffuse = primitive.GetComponent<MeshRenderer>().sharedMaterial;
            ////UnityEngine.Object.DestroyImmediate(primitive);

            ////foreach (Transform transform in shrine.transform)
            ////{
            ////    try
            ////    {
            ////        //transform.gameObject.AddComponent<MeshCollider>();
            ////        //foreach(Material material in transform.gameObject.GetComponent<Renderer>().sharedMaterials)
            ////        //    material.shader = Shader.Find(diffuse.name);

            ////        //foreach (Material material in transform.gameObject.GetComponent<Renderer>().materials)
            ////        //    material.shader = Shader.Find(diffuse.name);

            ////        foreach (Renderer render in transform.gameObject.GetComponents<Renderer>())
            ////        {
            ////            for (int materialIndex = 0; materialIndex < render.sharedMaterials.Length; materialIndex++)
            ////            {
            ////                render.sharedMaterials[materialIndex] = diffuse;
            ////            }
            ////            for (int materialIndex = 0; materialIndex < render.materials.Length; materialIndex++)
            ////            {
            ////                render.sharedMaterials[materialIndex] = diffuse;
            ////            }
            ////        }

            ////        //transform.gameObject.GetComponent<Renderer>().material.shader = Shader.Find(diffuse.name);
            ////    }
            ////    catch(Exception ex)
            ////    {

            ////    }
            ////}

            //Material[] materials;
            //string[] shaders;
            //foreach (Renderer rend in shrine.GetComponentsInChildren<Renderer>())
            //{
            //    try
            //    {

            //        materials = rend.sharedMaterials;
            //        shaders = new string[materials.Length];

            //        for (int i = 0; i < materials.Length; i++)
            //        {
            //            shaders[i] = materials[i].shader.name;
            //        }

            //        for (int i = 0; i < materials.Length; i++)
            //        {
            //            //Texture tex = materials[i].mainTexture;
            //            //Debug.Log(tex.name);
            //            //materials[i].shader = Shader.Find("Standard");
            //            materials[i].shader = Shader.Find(shaders[i]);
            //            //materials[i].SetTexture("_MainTex", tex);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Debug.Log("WOOT:" + ex.ToString());
            //    }
            //}

            //myLoadedAssetBundle.Unload(false);

            //GameObject scene = GameObject.Find("Meshes");
            //if (scene)
            //    UnityEngine.Object.Destroy(scene);
            //else
            //    return;

            //GameObject i01GravLift = null;
            //GameObject i01SpawnPoint = null;
            //GameObject i01Carbine = null;
            //GameObject i01PlasmaPistol = null;
            //GameObject i01Pistol = null;
            //GameObject i01Sniper = null;
            //GameObject i01BR = null;
            //GameObject i01Shotgun = null;
            //GameObject i01Smg = null;
            //foreach (GameObject gameObj in UnityEngine.Object.FindObjectsOfType<GameObject>())
            //{
            //    if (gameObj.name == "GravLift")
            //    {
            //        if (i01GravLift == null)
            //            i01GravLift = gameObj;
            //        else
            //            UnityEngine.Object.Destroy(gameObj);
            //    }

            //    if (gameObj.name == "Spawnpoint")
            //    {
            //        if (i01SpawnPoint == null)
            //            i01SpawnPoint = gameObj;
            //        else
            //            UnityEngine.Object.Destroy(gameObj);
            //    }

            //    if (gameObj.name == "Carbine")
            //    {
            //        if (i01Carbine == null)
            //            i01Carbine = gameObj;
            //        else
            //            UnityEngine.Object.Destroy(gameObj);
            //    }

            //    if (gameObj.name == "SniperRifle")
            //    {
            //        if (i01Sniper == null)
            //            i01Sniper = gameObj;
            //        else
            //            UnityEngine.Object.Destroy(gameObj);
            //    }

            //    if (gameObj.name == "BattleRifle")
            //    {
            //        if (i01BR == null)
            //            i01BR = gameObj;
            //        else
            //            UnityEngine.Object.Destroy(gameObj);
            //    }

            //    if (gameObj.name == "PlasmaPistol")
            //    {
            //        if (i01PlasmaPistol == null)
            //            i01PlasmaPistol = gameObj;
            //        else
            //            UnityEngine.Object.Destroy(gameObj);
            //    }

            //    if (gameObj.name == "Shotgun")
            //    {
            //        if (i01Shotgun == null)
            //            i01Shotgun = gameObj;
            //        else
            //            UnityEngine.Object.Destroy(gameObj);
            //    }

            //    if (gameObj.name == "SMG")
            //    {
            //        if (i01Smg == null)
            //            i01Smg = gameObj;
            //        else
            //            UnityEngine.Object.Destroy(gameObj);
            //    }

            //    if (gameObj.name == "Magnum")
            //    {
            //        if (i01Pistol == null)
            //            i01Pistol = gameObj;
            //        else
            //            UnityEngine.Object.Destroy(gameObj);
            //    }
            //}

            //foreach (GameObject gameObj in UnityEngine.Object.FindObjectsOfType<GameObject>())
            //{
            //    if (gameObj.name == "GRAVLIFT")
            //    {
            //        gameObj.transform.Rotate(Vector3.forward * 90);

            //        GameObject gravLift = UnityEngine.Object.Instantiate(i01GravLift);
            //        gravLift.transform.position = gameObj.transform.position;
            //        gravLift.transform.rotation = gameObj.transform.rotation;
            //        UnityEngine.Object.Destroy(gameObj);
            //    }

            //    if (gameObj.name == "SMG")
            //    {
            //        gameObj.transform.Rotate(Vector3.forward * 90);
            //        GameObject gravLift = UnityEngine.Object.Instantiate(i01Smg);
            //        gravLift.transform.position = gameObj.transform.position;
            //        gravLift.transform.rotation = gameObj.transform.rotation;
            //        UnityEngine.Object.Destroy(gameObj);
            //    }

            //    if (gameObj.name == "BATTLE_RIFLE")
            //    {
            //        gameObj.transform.Rotate(Vector3.forward * 90);
            //        GameObject gravLift = UnityEngine.Object.Instantiate(i01BR);
            //        gravLift.transform.position = gameObj.transform.position;
            //        gravLift.transform.rotation = gameObj.transform.rotation;
            //        UnityEngine.Object.Destroy(gameObj);
            //    }

            //    if (gameObj.name == "SHOTGUN")
            //    {
            //        gameObj.transform.Rotate(Vector3.forward * 90);
            //        GameObject gravLift = UnityEngine.Object.Instantiate(i01Shotgun);
            //        gravLift.transform.position = gameObj.transform.position;
            //        gravLift.transform.rotation = gameObj.transform.rotation;
            //        UnityEngine.Object.Destroy(gameObj);
            //    }

            //    if (gameObj.name == "CARBINE")
            //    {
            //        gameObj.transform.Rotate(Vector3.forward * 90);
            //        GameObject gravLift = UnityEngine.Object.Instantiate(i01Carbine);
            //        gravLift.transform.position = gameObj.transform.position;
            //        gravLift.transform.rotation = gameObj.transform.rotation;
            //        UnityEngine.Object.Destroy(gameObj);
            //    }

            //    if (gameObj.name == "PLASMA_PISTOL")
            //    {
            //        gameObj.transform.Rotate(Vector3.forward * 90);
            //        GameObject gravLift = UnityEngine.Object.Instantiate(i01PlasmaPistol);
            //        gravLift.transform.position = gameObj.transform.position;
            //        gravLift.transform.rotation = gameObj.transform.rotation;
            //        UnityEngine.Object.Destroy(gameObj);
            //    }

            //    if (gameObj.name == "SNIPER_RIFLE")
            //    {
            //        gameObj.transform.Rotate(Vector3.forward * 90);
            //        GameObject gravLift = UnityEngine.Object.Instantiate(i01Sniper);
            //        gravLift.transform.position = gameObj.transform.position;
            //        gravLift.transform.rotation = gameObj.transform.rotation;
            //        UnityEngine.Object.Destroy(gameObj);
            //    }

            //    if (gameObj.name == "PISTOL")
            //    {
            //        gameObj.transform.Rotate(Vector3.forward * 90);
            //        GameObject gravLift = UnityEngine.Object.Instantiate(i01Pistol);
            //        gravLift.transform.position = gameObj.transform.position;
            //        gravLift.transform.rotation = gameObj.transform.rotation;
            //        UnityEngine.Object.Destroy(gameObj);
            //    }

            //    if (gameObj.name == "SPAWN_POINT")
            //    {
            //        gameObj.transform.Rotate(Vector3.right * 90);
            //        GameObject gravLift = UnityEngine.Object.Instantiate(i01SpawnPoint);
            //        gravLift.transform.position = gameObj.transform.position;
            //        gravLift.transform.rotation = gameObj.transform.rotation;
            //        UnityEngine.Object.Destroy(gameObj);
            //    }
            //}

            //UnityEngine.Object.Destroy(i01BR);
            //UnityEngine.Object.Destroy(i01Carbine);
            //UnityEngine.Object.Destroy(i01GravLift);
            //UnityEngine.Object.Destroy(i01Pistol);
            //UnityEngine.Object.Destroy(i01PlasmaPistol);
            //UnityEngine.Object.Destroy(i01Shotgun);
            //UnityEngine.Object.Destroy(i01Smg);
            //UnityEngine.Object.Destroy(i01Sniper);
            //UnityEngine.Object.Destroy(i01SpawnPoint);

            //GameObject shrine = UnityEngine.Object.Instantiate(Resources.Load("shrine")) as GameObject;
            //shrine.transform.Rotate(new Vector3(-90, 0, 0));
            //shrine.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
        }
    }
}
