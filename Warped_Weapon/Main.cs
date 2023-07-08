using BepInEx;
using HarmonyLib;
using UnityEngine;
using URand = UnityEngine.Random;


namespace Warped_Weapon
{
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class CoolMod : BaseUnityPlugin
    {

        public string curWeapon = "";
        public void Start()
        {
            Harmony harmony = new Harmony(PluginInfo.GUID);
            harmony.PatchAll();
        }

        public void Update()
        {
            if (GunControl.Instance.currentWeapon.GetComponent<Oops>() == null)
            {
                GunControl.Instance.currentWeapon.AddComponent<Oops>();
            }

            //I'm 999% sure theres a better way of doing this but it works so i guess it's fine
            if (GunControl.Instance.currentWeapon.ToString() != curWeapon)
            {
                Debug.Log(curWeapon + " -> " + GunControl.Instance.currentWeapon.ToString());
                Destroy(GunControl.Instance.currentWeapon.GetComponent<Oops>());
                GunControl.Instance.currentWeapon.AddComponent<Oops>();
                curWeapon = GunControl.Instance.currentWeapon.ToString();
            }
        }

        class Oops : MonoBehaviour
        {
            float intensity = 150f;
            float r1;
            float r2;
            float r3;
            void Start()
            {
                RandomizeRotation();
            }
            void RandomizeRotation()
            {
                r1 = URand.Range(-intensity, intensity);
                r2 = URand.Range(-intensity, intensity);
                r3 = URand.Range(-intensity, intensity);
            }

            void Update()
            {
                transform.rotation *= Quaternion.Euler(
                    r1 * Time.deltaTime,
                    r2 * Time.deltaTime,
                    r3 * Time.deltaTime
                );
            }
        }
    }
}
