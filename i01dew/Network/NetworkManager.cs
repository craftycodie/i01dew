using i01.Game;
using i01dew.Player;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UdpKit;
using UnityEngine;

namespace i01dew.Network
{
    public static class NetworkManager
    {
        public static void InitializeClient()
        {
            BoltLauncher.StartClient(25565);
        }

        public static void InitializeHost()
        {
            BoltLauncher.StartServer(25565);
        }

        public static void BoltStartDone()
        {
            //BoltNetwork.EnableUPnP();

            BoltNetwork.EnableLanBroadcast();
            Debug.Log("Enabled Lan Broadcasting");
            if (BoltNetwork.isServer)
            {
                //BoltNetwork.UdpSocket.masterClient = new UdpSocket.MasterClient(BoltNetwork.UdpSocket, new UdpKit.Protocol.ProtocolClient(BoltNetwork.UdpSocket.platformSocket, BoltNetwork.UdpSocket.GameId, BoltNetwork.UdpSocket.PeerId));

                LobbyToken lobbyToken = new LobbyToken
                {
                    Description = PlayerProfileManager.PlayerProfile.tag + "s Server",
                    Gametype = (!GameManager.TeamGame) ? 0 : 1,
                    ID = "ID",
                    Map = GameParameters.Instance.maps[i01.UI.Manager.Instance.SelectedMapIndex].sceneName,
                    PlayerCount = BoltNetwork.connections.Count()
                };

                BoltNetwork.SetHostInfo("game", lobbyToken);
                //BoltNetwork.OpenPortUPnP(BoltNetwork.server.RemoteEndPoint.Port);
                //Debug.Log("Bolt server started! Woo!");

                //if (File.Exists(Directory.GetParent(Application.dataPath).FullName + "\\" + "Session.dat"))
                //{
                //    FileStream fs2 = new FileStream(Directory.GetParent(Application.dataPath).FullName + "\\" + "Session.dat", FileMode.Open);
                //    BinaryFormatter formatter2 = new BinaryFormatter();
                //    Debug.Log((formatter2.Deserialize(fs2) as UdpSession).WanEndPoint.Address.Byte0.ToString() + "." + (formatter2.Deserialize(fs2) as UdpSession).WanEndPoint.Address.Byte1.ToString() + "." + (formatter2.Deserialize(fs2) as UdpSession).WanEndPoint.Address.Byte2.ToString() + "." + (formatter2.Deserialize(fs2) as UdpSession).WanEndPoint.Address.Byte3.ToString() + ":" + (formatter2.Deserialize(fs2) as UdpSession).WanEndPoint.Port.ToString());

                //}

                //FileStream fs = new FileStream(Directory.GetParent(Application.dataPath).FullName + "\\" + "Session.dat", FileMode.Create);
                //BinaryFormatter formatter = new BinaryFormatter();
                //formatter.Serialize(fs, BoltNetwork.UdpSocket.sessionManager.GetLocalSession());

                //Debug.Log(JsonUtility.ToJson(BoltNetwork.UdpSocket.sessionManager.GetLocalSession()));
                //Debug.Log(BoltNetwork.UdpSocket.sessionManager.GetLocalSession()._hostData[0]);
            }
            if (!BoltNetwork.isServer)
            {
                Debug.Log("Bolt client started! Woo!");

                //BoltNetwork.Connect(UdpEndPoint.Parse("10.48.173.2:51368"), global::NetworkManager.playerToken);
            }
        }
    }
}
