using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;
using _LemonClient;
using HarmonyLib;
using MelonLoader;
using PlagueButtonAPI;
using PlagueButtonAPI.Controls;
using PlagueButtonAPI.Controls.Grouping;
using PlagueButtonAPI.Main;
using PlagueButtonAPI.Misc;
using PlagueButtonAPI.Pages;
using UnityEngine;
using VRC;
using VRC.Core;



namespace _LemonClient.PlagueButtons
{
    class PlagueButtonScript : MelonMod
    {

        private Dictionary<string, Sprite> UserImages = new Dictionary<string, Sprite>();
        public static string MurdererName = "?????";
        public override void OnApplicationStart()
        {
            try
            {
                ButtonImage = (Environment.CurrentDirectory + "\\ImageToLoad.png").LoadSpriteFromDisk();
            }
            catch { }

            //NetworkEvents.OnAvatarInstantiated += NetworkEvents_OnAvatarInstantiated;
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (sceneName == "ui") // If VRC UI Init
            {
                ButtonAPI.OnInit += () => // Assign, This Will Happen BEFORE QuickMenu Init, ButtonAPI Will Call OnInit On QuickMenu Init. - This is assigning to an event; eventname += delegatehere
                {

                    //Combi sets up button for page
                    //Page sets up new page
                    //Page.AddButtonGroup adds a group
                    //SubMenu in new variable, var subMenu = Page.AddSubMenu(Sprite, "Name", "Page Title")

                    //Button Groups
                    //Regular, Page.AddButtonGroup("Title")
                    //Collapsable, AddCollapsableButtonGroup("Title")

                    //Within a button group;
                    //Toggle Button: AddToggleButton("String", "Disabled Tooltip", "Enabled Tooltip", value => {Method to call(value)}).SetToggleState(false, true);
                    //Button: AddSimpleSingleButton("Name", "Tooltip", () => {Method to call});
                    //Slider: AddSlider("String", "Tooltip", value => {On adjust}, [min], [max], [default value]);

                    //Within ButtonAPI (Put within a simple single button)
                    //Alert Pre-code: ButtonAPI.GetQuickMenuInstance().ShowAlert("Alert");       Gives an "okay" button on alert
                    //Okay box: ButtonAPI.GetQuickMenuInstance().ShowOKDialog("Title", "Message", () => On click functionality);
                    //Confirm box: ButtonAPI.GetQuickMenuInstance().ShowConfirmDialog("Title", "Message", () => { Yes }, () => { No });

                    var wingMenuOpen = MenuPage.CreatePage(WingSingleButton.Wing.Both, ButtonImage, "Lemon Client", "Lemon Client");
                    var exploitPage = wingMenuOpen.Item1;

                    var exploitCat = exploitPage.AddButtonGroup("Game Exploits");
                    
                    //Murder 4 Buttons

                    var murderCat = exploitCat.AddSubMenu(ButtonImage, "Murder 4", "Murder 4");
                    var murderPage = murderCat.Item1;

                    var firstEXGroup = murderPage.AddCollapsibleButtonGroup("Murder 4 Roles");
                    var secondEXGroup = murderPage.AddCollapsibleButtonGroup("Cosmetic Changes");
                    var thirdEXGroup = murderPage.AddButtonGroup("Bring Items");

                    firstEXGroup.AddSimpleSingleButton("Murderer", "Sets self to Murderer in Murder 4", () => Exploits.MurderExploits.SelfMurderer());
                    firstEXGroup.AddSimpleSingleButton("Bystander", "Sets self to Bystander in Murder 4", () => Exploits.MurderExploits.SelfBystander());
                    firstEXGroup.AddSimpleSingleButton("Alert Murderer", "Sends alert containing Murderer name", () =>
                    {
                        MelonCoroutines.Start(RunCode());
                        IEnumerator RunCode()
                        {
                            MelonCoroutines.Start(Exploits.MurderExploits.FindKillerEnumerator(0.1f));
                            yield return new WaitForSeconds(1f);
                            ButtonAPI.GetQuickMenuInstance().ShowAlert("Murderer is: " + MurdererName);
                            yield break;
                        }
                    });

                    secondEXGroup.AddToggleButton("Anti-blind", "Enable Anti-blind", "Disable anti-blind", value => { Exploits.MurderExploits.AntiBlind(value); }).SetToggleState(false, true);
                    secondEXGroup.AddSimpleSingleButton("Gold Gun", "Enable Gold gun skin", () => Exploits.MurderExploits.GoldenGunForYou());
                    secondEXGroup.AddSimpleSingleButton("Clue ESP", "Shows clues through walls", () => MelonCoroutines.Start(Exploits.MurderExploits.ClueEsp()));
                    secondEXGroup.AddSimpleSingleButton("Respawn Pickups", "Respawns all pickups to world position", () => Exploits.MurderExploits.RespawnPickups());

                    thirdEXGroup.AddSimpleSingleButton("Bring Revolver", "Brings revolver to player", () => Exploits.MurderExploits.BringRoleWeapon(1));
                    thirdEXGroup.AddSimpleSingleButton("Bring Luger", "Brings luger to player", () => Exploits.MurderExploits.BringClueWeapon(2));
                    thirdEXGroup.AddSimpleSingleButton("Bring Shotgun", "Brings shotgun to player", () => Exploits.MurderExploits.BringClueWeapon(3));
                    thirdEXGroup.AddSimpleSingleButton("Bring Knife", "Brings knife to player", () => Exploits.MurderExploits.BringRoleWeapon(4));
                    thirdEXGroup.AddSimpleSingleButton("Bring Frag", "Brings Frag grenade to player", () => Exploits.MurderExploits.BringClueWeapon(5));
                    thirdEXGroup.AddSimpleSingleButton("Bring Frag", "Brings smoke grenade to player", () => Exploits.MurderExploits.BringClueWeapon(6));

                    //Rank Crash
                    var rCCat = exploitCat.AddSubMenu("Rank Crash", "Rank Crash");
                    var rCPage = rCCat.Item1;
                    var rCCont = rCPage.AddButtonGroup("Crash Ranks");

                    rCCont.AddSimpleSingleButton("Visitors", "Blocks everyone except for Visitor Users", () => MelonCoroutines.Start(Exploits.ByRankCrash.BoolCombined("(0.8, 0.8, 0.8)")));
                    rCCont.AddSimpleSingleButton("New Users", "Blocks everyone except for New Users", () => MelonCoroutines.Start(Exploits.ByRankCrash.BoolCombined("NewUser")));
                    rCCont.AddSimpleSingleButton("Users", "Blocks everyone except for Users", () => MelonCoroutines.Start(Exploits.ByRankCrash.BoolCombined("User")));
                    rCCont.AddSimpleSingleButton("Known Users", "Blocks everyone except for Known Users", () => MelonCoroutines.Start(Exploits.ByRankCrash.BoolCombined("KnownUser")));
                    rCCont.AddSimpleSingleButton("Trusted Users", "Blocks everyone except for Trusted Users", () => MelonCoroutines.Start(Exploits.ByRankCrash.BoolCombined("TrustedUser")));

                    //Features
                    var featureCat = exploitPage.AddButtonGroup("Features");
                    featureCat.AddToggleButton("Flight", "Turn flight on", "Turn flight off", value => { Exploits.FlightControl.EnableFlightMethod2(value); }).SetToggleState(false, true);
                    featureCat.AddToggleButton("Anti-theft", "Toggle on anti-theft", "(Probably) Toggle off anti-theft", value => { Exploits.GeneralExploits.BetterAntiTheft(value); }).SetToggleState(false, true);
                    exploitPage.AddSlider("Flight Speed", "Speed of Flight", value => { Exploits.FlightControl.flightSpeed = value; }, 1, 100, 2, false, false);
                    
                    //World Functionality
                    var worldCat = exploitPage.AddButtonGroup("World Stuff");
                    worldCat.AddSimpleSingleButton("Join World", "Join Instance ID from Clipboard", () => ExtraDependencies.WorldWrapper.JoinWorld(Clipboard.GetText()));
                    worldCat.AddSimpleSingleButton("Copy World ID", "copies world ID to Clipboard", () => Clipboard.SetText(ExtraDependencies.WorldWrapper.GetJoinID()));
                };
            }
        }

        #region Variables

        internal static bool DisablePortals = false;
        internal static Sprite ButtonImage = null;

        #endregion
    }
}
