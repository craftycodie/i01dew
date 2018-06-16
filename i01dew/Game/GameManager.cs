using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace i01dew.Game
{
    public static class GameManager
    {
        public static void OnEvent(SendClientID evnt)
        {
            try
            {
                i01.Game.GameManager.myID = (byte)evnt.ID;
                RenderHUD.instance.SetMiniScore(i01.Game.GameManager.myID);
            }
            catch (Exception ex)
            {
                Debug.Log(ex);
                Debug.Log("Caught error in GameManager OnEvent");
            }
        }

    }
}
