namespace Biff.BackgroundInfo
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Networking;
    public static class DBUtility
    {
        public static bool CheckError(UnityEngine.Networking.UnityWebRequest _www)
        {
            return _www.result == UnityWebRequest.Result.ConnectionError ||
                _www.result == UnityWebRequest.Result.DataProcessingError ||
                _www.result == UnityWebRequest.Result.ProtocolError;
        }

    } // end of class
} // end of namespace
