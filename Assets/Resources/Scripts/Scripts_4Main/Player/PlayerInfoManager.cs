namespace Biff.BackgroundInfo
{
    using Newtonsoft.Json.Linq;
    using Photon.Pun;
    using Photon.Realtime;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using Unity.VisualScripting;
    using UnityEngine;

    public class PlayerInfoManager : MonoBehaviourPunCallbacks
    {
        public static Dictionary<string, MyCharacDB.NowWearingDTO> myCharacsNowWearingDic_ = new Dictionary<string, MyCharacDB.NowWearingDTO>();
        private static ExitGames.Client.Photon.Hashtable playerHash_ = PhotonNetwork.LocalPlayer.CustomProperties;
        // # Personal Info
        private const string idKey_ = "ID";
        private const string pwKey_ = "PW";
        private const string nicknameKey_ = "Nickname";
        private const string modelKey_ = "Model";
        private const string moneyKey_ = "Money";
        private const string isInGame_ = "InGame";

        // # NowWearing
        private const string nowWearingKey_ = "NowWearing";
        private const string nwClothesKey_ = "Clothes";
        private const string nwHandsKey_ = "Hands";
        private const string nwHeadKey_ = "Head";
        private const string nwBagKey_ = "Bag";
        private const string nwPetKey_ = "Pet";

        private void Awake()
        {
            playerHash_ = PhotonNetwork.LocalPlayer.CustomProperties;
        }
        private static void SetInfo(string _key, string _value)
        {
            ExitGames.Client.Photon.Hashtable innerHash = null;
            // 로컬 플레이어의 Custom Properties에 ID 저장
            if (playerHash_.ContainsKey(_key))
            {
                innerHash = (ExitGames.Client.Photon.Hashtable)playerHash_[_key];
            }
            else
            {
                innerHash = new ExitGames.Client.Photon.Hashtable();
            }
            innerHash[_key] = _value;
            playerHash_[_key] = innerHash;
            //PhotonNetwork.LocalPlayer.SetCustomProperties(hNicknameProp);
        }
        private static void SetNowWearingInfo(string _itemKey, string _item)
        {
            ExitGames.Client.Photon.Hashtable innerHash_NowWearing = null;
            // 로컬 플레이어의 Custom Properties에 ID 저장
            if (playerHash_.ContainsKey(nowWearingKey_))
            {
                innerHash_NowWearing = (ExitGames.Client.Photon.Hashtable)playerHash_[nowWearingKey_];
            }
            else
            {
                innerHash_NowWearing = new ExitGames.Client.Photon.Hashtable();
            }
            innerHash_NowWearing[_itemKey] = _item;
            playerHash_[nowWearingKey_] = innerHash_NowWearing;
            //PhotonNetwork.LocalPlayer.SetCustomProperties(hNicknameProp);
        }
        private static string GetInfo(string _key)
        {
            string str = "";
            if (playerHash_.ContainsKey(_key))
            {
                ExitGames.Client.Photon.Hashtable innerHash = (ExitGames.Client.Photon.Hashtable)playerHash_[_key];
                str = (string)innerHash[_key];
            }
            return str;
        }
        
        // # Setter
        public static void IsInGame(bool _isInGame)
        {
            ExitGames.Client.Photon.Hashtable innerHash = null;
            // 로컬 플레이어의 Custom Properties에 ID 저장
            if (playerHash_.ContainsKey(isInGame_))
            {
                innerHash = (ExitGames.Client.Photon.Hashtable)playerHash_[isInGame_];
            }
            else
            {
                innerHash = new ExitGames.Client.Photon.Hashtable();
            }
            innerHash[isInGame_] = _isInGame;
            playerHash_[isInGame_] = innerHash;
        }

        public static void SetID(string _Id)
        {
            SetInfo(idKey_, _Id);
        }
        public static void SetPW(string _pw)
        {
            SetInfo(pwKey_, _pw);
        }
        public static void SetNickname(string _nickname)
        {
            SetInfo(nicknameKey_, _nickname);
        }
        public static void SetModel(string _model)
        {
            SetInfo(modelKey_, _model);
        }
        public static void SetMoney(int _money)
        {
            SetInfo(moneyKey_, _money.ToString());
        }
        // # Setter_NowWearing
        public static void SetNW_Clothes(string _clothesItem)
        {
            SetNowWearingInfo(nwClothesKey_, _clothesItem);
        }
        public static void SetNW_Hands(string _handsItem)
        {
            SetNowWearingInfo(nwHandsKey_, _handsItem);
        }
        public static void SetNW_Head(string _headItem)
        {
            SetNowWearingInfo(nwHeadKey_, _headItem);
        }
        public static void SetNW_Bag(string _bagItem)
        {
            SetNowWearingInfo(nwBagKey_, _bagItem);
        }
        public static void SetNW_Pet(string _petItem)
        {
            SetNowWearingInfo(nwPetKey_, _petItem);
        }
        // # Getter
        public static bool IsInGame()
        {
            bool isInGame = false;
            if (playerHash_.ContainsKey(isInGame_))
            {
                ExitGames.Client.Photon.Hashtable innerHash = (ExitGames.Client.Photon.Hashtable)playerHash_[isInGame_];
                isInGame = (bool)innerHash[isInGame_];
            }
            return isInGame;
        }
        public static string GetID()
        {
            return GetInfo(idKey_);
        }
        public static string GetPW()
        {
            return GetInfo(pwKey_);
        }
        public static string GetNickname()
        {
            return GetInfo(nicknameKey_);
        }
        public static string GetModel()
        {
            return GetInfo(modelKey_);
        }
        public static int GetMoney()
        {
            return int.Parse(GetInfo(moneyKey_));
        }
        // # Getter_NowWearing 
        public static string GetNowWearing()
        {
            string str = "";
            if (playerHash_.ContainsKey(nowWearingKey_))
            {
                str = (string)playerHash_[nowWearingKey_];
            }
            return str;
        }
        public static string GetNW_Clothes()
        {
            return GetInfo(nwClothesKey_);
        }
        public static string GetNW_Hands()
        {
            return GetInfo(nwHandsKey_);
        }
        public static string GetNW_Head()
        {
            return GetInfo(nwHeadKey_);
        }
        public static string GetNW_Bag()
        {
            return GetInfo(nwBagKey_);
        }
        public static string GetNW_Pet()
        {
            return GetInfo(nwPetKey_);
        }
    } // end of class
}// end of namespace