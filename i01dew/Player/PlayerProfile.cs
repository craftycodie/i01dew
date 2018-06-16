using i01dew.Utility;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace i01dew.Player
{
    //Overrides
    public static class PlayerProfile
    {
        public static i01.Player.PlayerProfile RandomPlayerProfile
        {
            get
            {
                string randomName = Utility.Random.Name();

                i01.Player.PlayerProfile newProfile = new i01.Player.PlayerProfile
                {
                    activePlayerModel = 0,
                    developer = "i01dew",
                    elite = new i01.Player.ArmourSet
                    {
                        chestID = 0,
                        helmetID = 0,
                        leftArmID = 0,
                        leftShoulderID = 0,
                        legsID = 0,
                        rightArmID = 0,
                        rightShoulderID = 0,
                        _primary = UnityEngine.Random.ColorHSV(),
                        _secondary = UnityEngine.Random.ColorHSV(),
                        _detail = UnityEngine.Random.ColorHSV(),
                        _hud = UnityEngine.Random.ColorHSV()
                    },
                    spartan = new i01.Player.ArmourSet
                    {
                        chestID = 0,
                        helmetID = 0,
                        leftArmID = 0,
                        leftShoulderID = 0,
                        legsID = 0,
                        rightArmID = 0,
                        rightShoulderID = 0,
                        _primary = UnityEngine.Random.ColorHSV(),
                        _secondary = UnityEngine.Random.ColorHSV(),
                        _detail = UnityEngine.Random.ColorHSV(),
                        _hud = UnityEngine.Random.ColorHSV()
                    },
                    eliteUnlocks = new i01.Player.Unlocks
                    {
                        chestUnlocks = new List<byte>() { 0, 1, 2, 3 },
                        helmetUnlocks = new List<byte>() { 0, 1, 2, 3 },
                        leftArmUnlocks = new List<byte>() { 0, 1, 2, 3 },
                        leftShoulderUnlocks = new List<byte>() { 0, 1, 2, 3 },
                        legUnlocks = new List<byte>() { 0, 1, 2, 3 },
                        rightArmUnlocks = new List<byte>() { 0, 1, 2, 3 },
                        rightSholderUnlocks = new List<byte>() { 0, 1, 2, 3 },
                    },
                    spartanUnlocks = new i01.Player.Unlocks
                    {
                        chestUnlocks = new List<byte>() { 0, 1, 2, 3 },
                        helmetUnlocks = new List<byte>() { 0, 1, 2, 3 },
                        leftArmUnlocks = new List<byte>() { 0, 1, 2, 3 },
                        leftShoulderUnlocks = new List<byte>() { 0, 1, 2, 3 },
                        legUnlocks = new List<byte>() { 0, 1, 2, 3 },
                        rightArmUnlocks = new List<byte>() { 0, 1, 2, 3 },
                        rightSholderUnlocks = new List<byte>() { 0, 1, 2, 3 },
                    },
                    service = Utility.Random.String(4),
                    tag = randomName,
                    email = randomName + "@i01dew.org",
                    token = "i01dew0.6",
                    name = "i01dew",
                    joined = 1427414400,
                    emblem = new i01.Player.EmblemSet
                    {
                        backgroundID = 0,
                        foregroundEnableLayer2 = true,
                        foregroundID = 1,
                        _background = UnityEngine.Random.ColorHSV(),
                        _primary = UnityEngine.Random.ColorHSV(),
                        _secondary = UnityEngine.Random.ColorHSV()
                    },
                    friendTags = new List<string>() { "Developer01", "Developer02", "Developer03", "Developer04" },
                    picture = @"https:\/\/en.wikipedia.org\/wiki\/Doritos#\/media\/File:Doritos_Logo_(2013).png",
                };

                return newProfile;
            }
        }

        public static void LoadProfile(string token, string email, OnCompletedRequest onComplete)
        {
            i01.Player.PlayerProfile.thisPlayer = PlayerProfileManager.PlayerProfile;
            i01.Player.PlayerProfile.thisPlayer.SetDefaults(PlayerProfileManager.PlayerProfile.token);
            onComplete?.Invoke(JsonUtility.ToJson(i01.Player.PlayerProfile.thisPlayer));
        }

        public static void SendObject(string key, JToken obj)
        {
            i01.Player.PlayerProfile playerProfile = PlayerProfileManager.PlayerProfile;
            Debug.Log(obj.ToString());

            foreach (JProperty x in obj)
            {
                string name = x.Name;
                JToken value = x.Value;

                FieldInfo fieldInfo = typeof(i01.Player.PlayerProfile).GetField(name);
                fieldInfo.SetValue(playerProfile, value.ToObject(fieldInfo.FieldType));
            }

            PlayerProfileManager.PlayerProfile = playerProfile;
        }

        public static void SaveProfile(i01.Player.PlayerProfile profile)
        {
            PlayerProfileManager.PlayerProfile = profile;
        }
    }
}
