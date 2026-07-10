using MelonLoader;
using UI.Common.Options;
using UnityEngine;

namespace SD_FOV_Settings_Mod
{
    public class FOV_Mod : MelonMod
    {
        private static GameObject selectedMenu;
        private static MelonPreferences_Category preferencesCategory;
        public static MelonPreferences_Entry<byte> fovPreference;
        private static FovSettings component;

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
#if !RELEASE
            LoggerInstance.Msg($"Scene {sceneName} with build index {buildIndex} has been loaded");
#endif

            if(buildIndex == 0) selectedMenu = GameObject.Find("SelectedMenu");
            else if (buildIndex == 1) selectedMenu = GameObject.Find("PauseCanvas");

            foreach (Camera cam in Camera.allCameras) cam.fieldOfView = fovPreference.Value;
        }

        public override void OnUpdate()
        {
            if (!(selectedMenu.transform.GetChild(0).gameObject.activeSelf && selectedMenu.GetComponentInChildren<VolumeSettings>() != null))
                return;

            GameObject gameObject = selectedMenu.GetComponentInChildren<VolumeSettings>().gameObject;
                if (gameObject.GetComponent<FovSettings>() != null) return;

             component = gameObject.AddComponent<FovSettings>();
        }

        public override void OnInitializeMelon()
        {
            preferencesCategory = MelonPreferences.CreateCategory("FOV_Mod");

            if (preferencesCategory.HasEntry("fov")) fovPreference = preferencesCategory.GetEntry<byte>("fov");
            else fovPreference = preferencesCategory.CreateEntry<byte>("fov", 60);
        }
    }
}
