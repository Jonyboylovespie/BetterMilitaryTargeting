using System.Linq;
using Il2CppAssets.Scripts.Models;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Api.ModOptions;
using BTD_Mod_Helper.Extensions;
using MelonLoader;
using BetterMilitaryTargeting;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using BTD_Mod_Helper.Api.Data;

[assembly: MelonInfo(typeof(BetterMilitaryTargeting.Main), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace BetterMilitaryTargeting
{
    public class Main : BloonsTD6Mod
    {
        public override void OnNewGameModel(GameModel gameModel, Il2CppSystem.Collections.Generic.List<ModModel> mods)
        {
            foreach (var towerModel in gameModel.towers)
            {
                var behaviors = Game.instance.model.GetTowerFromId("DartMonkey").GetAttackModel().behaviors.Where(x => x.name.Contains("Target"));
                if (towerModel.baseId == "DartlingGunner" && Settings.BetterDartlingTargeting)
                {
                    var attackModel = towerModel.GetAttackModel();
                    foreach (var behavior in behaviors)
                    {
                        attackModel.AddBehavior(behavior.Duplicate());
                    }
                    attackModel.RemoveBehavior<TargetPointerModel>();
                    attackModel.RemoveBehavior<TargetSelectedPointModel>();
                }
                if (towerModel.baseId == "MortarMonkey" && Settings.BetterMortarTargeting)
                {
                    var attackModel = towerModel.GetAttackModel();
                    foreach (var behavior in behaviors)
                    {
                        attackModel.AddBehavior(behavior.Duplicate());
                    }
                    towerModel.GetBehavior<AttackModel>().RemoveBehavior<TargetSelectedPointModel>();
                    towerModel.towerSelectionMenuThemeId = "Default";
                }
                if (towerModel.appliedUpgrades.Contains(UpgradeType.FocusedFiring) && Settings.FocusedFiringPerfectAim)
                {
                    towerModel.GetDescendants<RandomEmissionModel>().ForEach(e => e.angle = 0);
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
