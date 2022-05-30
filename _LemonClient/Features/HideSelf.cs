using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;

namespace _LemonClient.Features
{
    class HideSelf
    {
        public static bool _HideSelfEnabled;

        public static void ClearAssets()
        {
            AssetBundleDownloadManager.field_Private_Static_AssetBundleDownloadManager_0.field_Private_Cache_0.ClearCache();
            AssetBundleDownloadManager.field_Private_Static_AssetBundleDownloadManager_0.field_Private_Queue_1_AssetBundleDownload_0.Clear();
            AssetBundleDownloadManager.field_Private_Static_AssetBundleDownloadManager_0.field_Private_Queue_1_AssetBundleDownload_1.Clear();
        }

        public static void PerformHide(bool value)
        {
            _HideSelfEnabled = value;

            if (_HideSelfEnabled)
            {
                ClearAssets();
                MelonLogger.Msg("Stopping Avatar Queue");
                AssetBundleDownloadManager.field_Private_Static_AssetBundleDownloadManager_0.gameObject.SetActive(false);
                ClearAssets();
            }

            if (!_HideSelfEnabled)
            {
                // ima do this twice since i'm too lazy to see h
                MelonLogger.Msg("Starting Avatar Queue");
                ClearAssets();
                AssetBundleDownloadManager.field_Private_Static_AssetBundleDownloadManager_0.gameObject.SetActive(true);
                ClearAssets();
            }
        }
    }
}
