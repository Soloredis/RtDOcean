using System;
using System.Collections.Generic;
using BepInEx.Configuration;
using Jotunn.Entities;
using Jotunn.Managers;
using UnityEngine;

namespace RtDOcean
{
    internal partial class OceanPlugin
    {
        private static readonly string[][] OceanCreatureNames =
        {
            new[] { "Animal_Crab_RtD", "Crab" },
            new[] { "Animal_Dolphin_RtD", "Dolphin" },
            new[] { "Animal_Cod_RtD", "Cod" },
            new[] { "Animal_Tuna_RtD", "Tuna" },
            new[] { "Monster_GreatWhiteShark_RtD", "Great White Shark" },
            new[] { "Animal_Squid_RtD", "Squid" },
            new[] { "Animal_Manta_RtD", "Manta" },
            new[] { "Monster_HammerheadShark_RtD", "Hammerhead Shark" },
            new[] { "Animal_Marlin_RtD", "Marlin" },
            new[] { "Animal_Turtle_RtD", "Turtle" },
            new[] { "Shark_RtD", "Megalodon" },
            new[] { "Mirmaid_RtD", "Mirmaid" },
            new[] { "BoneFish_RtD", "BoneFish" },
            new[] { "BoneSquid_RtD", "BoneSquid" },
            new[] { "LuminousLooker_RtD", "Luminous Looker" },
            new[] { "MurkPod_RtD", "Murkpod" },
            new[] { "Animal_SpermWhale_RtD", "Sperm Whale" },
            new[] { "Animal_HumpbackWhale_RtD", "Humpback Whale" },
            new[] { "Monster_Orca_RtD", "Orca" },
            new[] { "CatFish_RtD", "Catfish Wanderer" },
            new[] { "Reptile_RtD", "Reptile Wanderer" },
            new[] { "MirLizard_RtD", "Lizard Wanderer" },
            new[] { "MirRake_RtD", "Rake Wanderer" },
            new[] { "Belzor_RtD", "Belzor Wanderer" },
            new[] { "Neck_RtD", "Neck" },
            new[] { "Fairy4_RtD", "Fairy" }
        };

        private ConfigEntry<bool> SeaweedEnabled;
        private ConfigEntry<float> SeaweedAmountMin;
        private ConfigEntry<float> SeaweedAmountMax;
        private ConfigEntry<float> SeaweedScaleMin;
        private ConfigEntry<float> SeaweedScaleMax;
        private ConfigEntry<bool> SeaweedGroundCoverEnabled;
        private ConfigEntry<int> SeaweedGroundCoverAmount;
        private ConfigEntry<float> SeaweedGroundCoverScaleMin;
        private ConfigEntry<float> SeaweedGroundCoverScaleMax;

        private readonly List<ZoneSystem.ZoneVegetation> seaweedVegetation = new List<ZoneSystem.ZoneVegetation>();
        private readonly List<ClutterSystem.Clutter> seaweedGroundCover = new List<ClutterSystem.Clutter>();

        private static ConfigDescription Synced(string description, AcceptableValueBase range = null) =>
            new ConfigDescription(description, range, new ConfigurationManagerAttributes { IsAdminOnly = true });

        private void SetupOceanConfigs()
        {
            int seaweed = 0, foods = 0, monsters = 0;
            try { seaweed = SetupSeaweedConfigs(); }
            catch (Exception ex) { Logger.LogWarning($"Exception caught while adding seaweed configs: {ex}"); }
            try { foods = SetupFoodConfigs(); }
            catch (Exception ex) { Logger.LogWarning($"Exception caught while adding food configs: {ex}"); }
            try { monsters = SetupMonsterConfigs(); }
            catch (Exception ex) { Logger.LogWarning($"Exception caught while adding monster configs: {ex}"); }
            Logger.LogInfo($"Config options added: {seaweed} seaweed entries, {foods} foods, {monsters} monsters.");
        }

        private int SetupSeaweedConfigs()
        {
            foreach (string name in OceanPlantList)
            {
                var veg = ZoneManager.Instance.GetZoneVegetation(name);
                if (veg != null) seaweedVegetation.Add(veg);
            }
            foreach (string name in OceanClutterSeaWeedList)
            {
                var clutter = ZoneManager.Instance.GetClutter(name);
                if (clutter != null) seaweedGroundCover.Add(clutter);
            }

            SeaweedEnabled = Config.Bind("Seaweed", "Enabled", true,
                Synced("Spawn the seaweed plants (SeaWeed1-16) in the ocean. Only affects newly generated areas."));
            SeaweedAmountMin = Config.Bind("Seaweed", "Amount Min", OceanPlantValues.Min,
                Synced("Fewest of each seaweed type per zone.", new AcceptableValueRange<float>(0f, 50f)));
            SeaweedAmountMax = Config.Bind("Seaweed", "Amount Max", OceanPlantValues.Max,
                Synced("Most of each seaweed type per zone.", new AcceptableValueRange<float>(0f, 50f)));
            SeaweedScaleMin = Config.Bind("Seaweed", "Scale Min", OceanPlantValues.ScaleMin,
                Synced("Smallest seaweed size.", new AcceptableValueRange<float>(0.1f, 10f)));
            SeaweedScaleMax = Config.Bind("Seaweed", "Scale Max", OceanPlantValues.ScaleMax,
                Synced("Largest seaweed size.", new AcceptableValueRange<float>(0.1f, 10f)));
            SeaweedGroundCoverEnabled = Config.Bind("Seaweed", "Ground Cover Enabled", true,
                Synced("Show the small seaweed ground cover on the sea floor."));
            SeaweedGroundCoverAmount = Config.Bind("Seaweed", "Ground Cover Amount", OceanClutterSeaWeedValues.Amount,
                Synced("How dense the seaweed ground cover is.", new AcceptableValueRange<int>(0, 100)));
            SeaweedGroundCoverScaleMin = Config.Bind("Seaweed", "Ground Cover Scale Min", OceanClutterSeaWeedValues.ScaleMin,
                Synced("Smallest seaweed ground cover size.", new AcceptableValueRange<float>(0.1f, 10f)));
            SeaweedGroundCoverScaleMax = Config.Bind("Seaweed", "Ground Cover Scale Max", OceanClutterSeaWeedValues.ScaleMax,
                Synced("Largest seaweed ground cover size.", new AcceptableValueRange<float>(0.1f, 10f)));

            ApplySeaweed();
            EventHandler apply = (s, e) => ApplySeaweed();
            SeaweedEnabled.SettingChanged += apply;
            SeaweedAmountMin.SettingChanged += apply;
            SeaweedAmountMax.SettingChanged += apply;
            SeaweedScaleMin.SettingChanged += apply;
            SeaweedScaleMax.SettingChanged += apply;
            SeaweedGroundCoverEnabled.SettingChanged += apply;
            SeaweedGroundCoverAmount.SettingChanged += apply;
            SeaweedGroundCoverScaleMin.SettingChanged += apply;
            SeaweedGroundCoverScaleMax.SettingChanged += apply;

            return seaweedVegetation.Count + seaweedGroundCover.Count;
        }

        private void ApplySeaweed()
        {
            foreach (var veg in seaweedVegetation)
            {
                veg.m_enable = SeaweedEnabled.Value;
                veg.m_min = SeaweedAmountMin.Value;
                veg.m_max = SeaweedAmountMax.Value;
                veg.m_scaleMin = SeaweedScaleMin.Value;
                veg.m_scaleMax = SeaweedScaleMax.Value;
            }
            foreach (var clutter in seaweedGroundCover)
            {
                clutter.m_enabled = SeaweedGroundCoverEnabled.Value;
                clutter.m_amount = SeaweedGroundCoverAmount.Value;
                clutter.m_scaleMin = SeaweedGroundCoverScaleMin.Value;
                clutter.m_scaleMax = SeaweedGroundCoverScaleMax.Value;
            }
            if (ClutterSystem.instance) ClutterSystem.instance.ClearAll();
        }

        private int SetupFoodConfigs()
        {
            int count = 0;
            foreach (string name in ItemList)
            {
                var item = ItemManager.Instance.GetItem(name);
                if (item == null || !item.ItemDrop) continue;
                var shared = item.ItemDrop.m_itemData.m_shared;
                if (shared.m_food <= 0f && shared.m_foodStamina <= 0f && shared.m_foodEitr <= 0f) continue;

                string section = "Food: " + name;
                var health = Config.Bind(section, "Health", shared.m_food, Synced("Health this food gives."));
                var stamina = Config.Bind(section, "Stamina", shared.m_foodStamina, Synced("Stamina this food gives."));
                var eitr = Config.Bind(section, "Eitr", shared.m_foodEitr, Synced("Eitr this food gives."));
                var duration = Config.Bind(section, "Duration", shared.m_foodBurnTime, Synced("How long the food lasts, in seconds."));
                var regen = Config.Bind(section, "Health Regen", shared.m_foodRegen, Synced("Health regenerated per tick while this food is active."));

                Action apply = () =>
                {
                    shared.m_food = health.Value;
                    shared.m_foodStamina = stamina.Value;
                    shared.m_foodEitr = eitr.Value;
                    shared.m_foodBurnTime = duration.Value;
                    shared.m_foodRegen = regen.Value;
                };
                apply();
                EventHandler onChange = (s, e) => apply();
                health.SettingChanged += onChange;
                stamina.SettingChanged += onChange;
                eitr.SettingChanged += onChange;
                duration.SettingChanged += onChange;
                regen.SettingChanged += onChange;
                count++;
            }
            return count;
        }

        private int SetupMonsterConfigs()
        {
            int count = 0;
            foreach (var names in OceanCreatureNames)
            {
                var creature = CreatureManager.Instance.GetCreature(names[0]);
                if (creature == null || !creature.Prefab) continue;
                string section = "Monster: " + names[1];

                var character = creature.Prefab.GetComponent<Character>();
                if (character)
                {
                    var health = Config.Bind(section, "Health", character.m_health,
                        Synced("Base health at 1 star. Higher star levels multiply this."));
                    Action applyHealth = () => character.m_health = health.Value;
                    applyHealth();
                    health.SettingChanged += (s, e) => applyHealth();
                }

                if (creature.Spawns.Count > 0)
                {
                    var spawn = creature.Spawns[0];
                    var enabled = Config.Bind(section, "Spawn Enabled", spawn.m_enabled,
                        Synced("Let this creature spawn in the world."));
                    var chance = Config.Bind(section, "Spawn Chance", spawn.m_spawnChance,
                        Synced("Chance (%) to spawn each time the spawner checks.", new AcceptableValueRange<float>(0f, 100f)));
                    var interval = Config.Bind(section, "Spawn Interval", spawn.m_spawnInterval,
                        Synced("Seconds between spawn checks.", new AcceptableValueRange<float>(1f, 7200f)));
                    var maxSpawned = Config.Bind(section, "Max Spawned", spawn.m_maxSpawned,
                        Synced("Most of this creature allowed around a player at once.", new AcceptableValueRange<int>(1, 20)));
                    var maxLevel = Config.Bind(section, "Max Level", spawn.m_maxLevel,
                        Synced("Highest level it can spawn at (1 = no stars, 2 = one star, 3 = two stars).", new AcceptableValueRange<int>(1, 10)));

                    Action applySpawn = () =>
                    {
                        foreach (var data in creature.Spawns)
                        {
                            data.m_enabled = enabled.Value;
                            data.m_spawnChance = chance.Value;
                            data.m_spawnInterval = interval.Value;
                            data.m_maxSpawned = Mathf.Max(1, maxSpawned.Value);
                            data.m_maxLevel = Mathf.Max(data.m_minLevel, maxLevel.Value);
                        }
                    };
                    applySpawn();
                    EventHandler onChange = (s, e) => applySpawn();
                    enabled.SettingChanged += onChange;
                    chance.SettingChanged += onChange;
                    interval.SettingChanged += onChange;
                    maxSpawned.SettingChanged += onChange;
                    maxLevel.SettingChanged += onChange;
                }
                count++;
            }
            return count;
        }
    }
}