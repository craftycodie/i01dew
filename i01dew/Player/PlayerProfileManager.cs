using System.IO;
using UnityEngine;

namespace i01dew.Player
{
    public static class PlayerProfileManager
    {
        public static i01.Player.PlayerProfile PlayerProfile
        {
            get { return GetPlayerProfile(); }
            set { SetPlayerProfile(value); }
        }

        private static string PlayerProfilePath
        {
            get { return Directory.GetParent(Application.dataPath).FullName + "\\" + "PlayerProfile.json"; }
        }

        private static i01.Player.PlayerProfile GetPlayerProfile()
        {
            if (!File.Exists(PlayerProfilePath))
                CreateNewPlayerProfile();

            return JsonUtility.FromJson<i01.Player.PlayerProfile>(File.ReadAllText(PlayerProfilePath));
        }

        private static void SetPlayerProfile(i01.Player.PlayerProfile playerProfile)
        {
            if (!File.Exists(PlayerProfilePath))
                CreateNewPlayerProfileFile();

            File.WriteAllText(PlayerProfilePath, JsonUtility.ToJson(playerProfile));        
        }

        private static void CreateNewPlayerProfile()
        {
            SetPlayerProfile(Player.PlayerProfile.RandomPlayerProfile);
        }

        private static void CreateNewPlayerProfileFile()
        {
            File.Create(PlayerProfilePath).Close();
        }
    }
}
