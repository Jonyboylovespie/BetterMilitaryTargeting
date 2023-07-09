using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Towers.Mods;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Api.ModOptions;
using BTD_Mod_Helper.Extensions;
using MelonLoader;
using BetterMilitaryTargeting;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using BTD_Mod_Helper.Api.Data;
using Il2CppAssets.Scripts.Models.Towers.Weapons;

[assembly: MelonInfo(typeof(BetterMilitaryTargeting.Main), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace BetterMilitaryTargeting
{
    public class Main : BloonsTD6Mod
    {
        
        public override void OnApplicationStart()
        {
            MelonLogger.Msg(System.ConsoleColor.DarkMagenta, "");
        }
        public override void OnNewGameModel(GameModel gameModel, Il2CppSystem.Collections.Generic.List<ModModel> mods)
        {

            foreach (var towerModel in gameModel.towers)
            {
                if (towerModel.baseId == "DartlingGunner")
                {
                    if (Settings.BetterDartlingTargeting == true)
                    {
                        towerModel.GetBehavior<AttackModel>().AddBehavior(Game.instance.model.GetTowerFromId("DartMonkey").GetDescendant<RotateToTargetModel>().Duplicate());
                        towerModel.GetBehavior<AttackModel>().AddBehavior(Game.instance.model.GetTowerFromId("DartMonkey").GetDescendant<TargetFirstPrioCamoModel>().Duplicate());
                        towerModel.GetBehavior<AttackModel>().AddBehavior(Game.instance.model.GetTowerFromId("DartMonkey").GetDescendant<TargetLastPrioCamoModel>().Duplicate());
                        towerModel.GetBehavior<AttackModel>().AddBehavior(Game.instance.model.GetTowerFromId("DartMonkey").GetDescendant<TargetClosePrioCamoModel>().Duplicate());
                        towerModel.GetBehavior<AttackModel>().AddBehavior(Game.instance.model.GetTowerFromId("DartMonkey").GetDescendant<TargetStrongPrioCamoModel>().Duplicate());
                        towerModel.GetBehavior<AttackModel>().RemoveBehavior<TargetPointerModel>();
                        towerModel.GetBehavior<AttackModel>().RemoveBehavior<TargetSelectedPointModel>();
                    }
                }
                if (towerModel.baseId == "MortarMonkey")
                {
                    if (Settings.BetterDartlingTargeting == true)
                    {
                        towerModel.GetBehavior<AttackModel>().AddBehavior(Game.instance.model.GetTowerFromId("DartMonkey").GetDescendant<RotateToTargetModel>().Duplicate());
                        towerModel.GetBehavior<AttackModel>().AddBehavior(Game.instance.model.GetTowerFromId("DartMonkey").GetDescendant<TargetFirstPrioCamoModel>().Duplicate());
                        towerModel.GetBehavior<AttackModel>().AddBehavior(Game.instance.model.GetTowerFromId("DartMonkey").GetDescendant<TargetLastPrioCamoModel>().Duplicate());
                        towerModel.GetBehavior<AttackModel>().AddBehavior(Game.instance.model.GetTowerFromId("DartMonkey").GetDescendant<TargetClosePrioCamoModel>().Duplicate());
                        towerModel.GetBehavior<AttackModel>().AddBehavior(Game.instance.model.GetTowerFromId("DartMonkey").GetDescendant<TargetStrongPrioCamoModel>().Duplicate());
                        towerModel.GetBehavior<AttackModel>().RemoveBehavior<TargetSelectedPointModel>();
                        towerModel.towerSelectionMenuThemeId = "Default";
                    }
                }
                if (towerModel.appliedUpgrades.Contains(UpgradeType.FocusedFiring))
                {
                    if (Settings.FocusedFiringPerfectAim == true)
                    {
                        towerModel.GetDescendants<RandomEmissionModel>().ForEach(e => e.angle = 0);
                    }
                }
            }
        }
    }
    public class Settings : ModSettings
    {
        public static readonly ModSettingCategory DartlingGunnerSettings = new("Dartling Gunner Settings")
        {
            icon = VanillaSprites.DartlingGunner000
        };
        public static readonly ModSettingCategory MortarSettings = new("Mortar Monkey Settings")
        {
            icon = VanillaSprites.MortarMonkey000
        };

        public static readonly ModSettingBool BetterDartlingTargeting = new(true)
        {
            displayName = "Better Targeting For Dartling Gunners",
            description = "Gives the dartling gunners targeting options just like other towers, no micro necessary.",
            category = DartlingGunnerSettings,
            icon = VanillaSprites.FlankingManeuversIcon,
        };
        public static readonly ModSettingBool FocusedFiringPerfectAim = new(true)
        {
            displayName = "Focused Firing Perfect Aim",
            description = "Makes the upgrade focused firing give dartlings perfect aim.",
            category= DartlingGunnerSettings,
            icon = VanillaSprites.FocusedFiringUpgradeIcon,
        };

        public static readonly ModSettingBool BetterMortarTargeting = new(true)
        {
            displayName = "Better Targeting For Mortar Monkeys",
            description = "Gives the mortar monkey targeting options just like other towers, no micro necessary.",
            category = MortarSettings,
            icon = VanillaSprites.FlankingManeuversIcon,
        };
    }
}
