using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Code by VPDInc
// Email: vpd-2000@yandex.ru
// Version: 1.0 (16.06.2022)
namespace General
{
    public class Clipboard : MonoBehaviour
    {
        #region Copy functions
        public static void CopyToClipboard(TMP_Text text) =>
            GUIUtility.systemCopyBuffer = text.text;
    
        public static void CopyToClipboard(TMP_InputField text) =>
            GUIUtility.systemCopyBuffer = text.text;
    
        public static void CopyToClipboard(Text text) =>
            GUIUtility.systemCopyBuffer = text.text;
    
        public static void CopyToClipboard(string line) =>
            GUIUtility.systemCopyBuffer = line;
        #endregion

        #region Past functions
        public static void PastFromClipboard(TMP_Text text) =>
            text.text = GUIUtility.systemCopyBuffer;
    
        public static void PastFromClipboard(TMP_InputField text) =>
            text.text = GUIUtility.systemCopyBuffer;
    
        public static void PastFromClipboard(Text text) =>
            text.text = GUIUtility.systemCopyBuffer;

        public static string GetFromClipboard() =>
            GUIUtility.systemCopyBuffer;
        #endregion
    }
}
