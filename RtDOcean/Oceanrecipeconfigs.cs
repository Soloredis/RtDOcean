using System;
using System.Collections.Generic;
using System.Linq;
using BepInEx.Configuration;
using Jotunn.Entities;
using Jotunn.Managers;
using UnityEngine;

namespace RtDOcean
{
    internal partial class OceanPlugin
    {
        private const string JotunnMockPrefix = "JVLmock_";

        private class OceanRecipeSettings
        {
            public CustomRecipe CustomRecipe;
            public ConfigEntry<bool> Enabled;
            public ConfigEntry<string> Requirements;
            public ConfigEntry<string> CraftingStation;
            public ConfigEntry<int> StationLevel;
            public ConfigEntry<int> Amount;
            public Dictionary<string, bool> Recover = new Dictionary<string, bool>();
        }

        private readonly List<OceanRecipeSettings> oceanRecipeSettings = new List<OceanRecipeSettings>();

        private int SetupRecipeConfigs()
        {
            foreach (string itemName in ItemList.Concat(CapeList))
            {
                var customRecipe = ItemManager.Instance.GetRecipe("Recipe_" + itemName);
                if (customRecipe?.Recipe == null) continue;
                var recipe = customRecipe.Recipe;
                var settings = new OceanRecipeSettings { CustomRecipe = customRecipe };

                var requirementParts = new List<string>();
                foreach (var req in recipe.m_resources ?? new Piece.Requirement[0])
                {
                    if (req?.m_resItem == null) continue;
                    string name = CleanPrefabName(req.m_resItem.name);
                    settings.Recover[name] = req.m_recover;
                    requirementParts.Add(req.m_amountPerLevel > 0 ? $"{name}:{req.m_amount}:{req.m_amountPerLevel}" : $"{name}:{req.m_amount}");
                }
                string station = recipe.m_craftingStation ? CleanPrefabName(recipe.m_craftingStation.name) : "";

                string section = "Recipe: " + itemName;
                settings.Enabled = Config.Bind(section, "Enabled", recipe.m_enabled,
                    Synced("Let players craft this item."));
                settings.Requirements = Config.Bind(section, "Requirements", string.Join(", ", requirementParts),
                    Synced("Ingredients as Item:Amount, separated by commas (Item:Amount:AmountPerLevel for upgradable items). Use prefab names, e.g. SeaWeed_RtD:3, Carrot:2."));
                settings.CraftingStation = Config.Bind(section, "Crafting Station", station,
                    Synced("Prefab name of the station needed, e.g. piece_cauldron, Piece_Prep_Table_RtD, piece_workbench, forge. Leave empty to craft by hand."));
                settings.StationLevel = Config.Bind(section, "Station Level", recipe.m_minStationLevel,
                    Synced("Minimum station level needed.", new AcceptableValueRange<int>(1, 10)));
                settings.Amount = Config.Bind(section, "Amount", recipe.m_amount,
                    Synced("How many items one craft makes.", new AcceptableValueRange<int>(1, 100)));

                EventHandler onChange = (s, e) => ApplyRecipe(settings);
                settings.Enabled.SettingChanged += onChange;
                settings.Requirements.SettingChanged += onChange;
                settings.CraftingStation.SettingChanged += onChange;
                settings.StationLevel.SettingChanged += onChange;
                settings.Amount.SettingChanged += onChange;

                oceanRecipeSettings.Add(settings);
            }

            ItemManager.OnItemsRegistered += ApplyAllRecipes;
            return oceanRecipeSettings.Count;
        }

        private static string CleanPrefabName(string name)
        {
            if (string.IsNullOrEmpty(name)) return "";
            name = name.Replace("(Clone)", "").Trim();
            return name.StartsWith(JotunnMockPrefix) ? name.Substring(JotunnMockPrefix.Length) : name;
        }

        private void ApplyAllRecipes()
        {
            int problems = 0;
            foreach (var settings in oceanRecipeSettings)
                if (!ApplyRecipe(settings)) problems++;
            if (problems > 0)
                Logger.LogWarning($"{problems} RtDOcean recipe config(s) had problems, see the warnings above.");
        }

        private bool ApplyRecipe(OceanRecipeSettings settings)
        {
            var recipe = settings.CustomRecipe.Recipe;
            if (recipe == null || !ObjectDB.instance || ObjectDB.instance.m_items.Count == 0) return true;
            bool ok = true;

            try
            {
                var requirements = new List<Piece.Requirement>();
                foreach (string part in (settings.Requirements.Value ?? "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    var bits = part.Split(':').Select(b => b.Trim()).ToArray();
                    if (bits.Length < 2 || bits[0] == "" || !int.TryParse(bits[1], out int amount))
                    {
                        Logger.LogWarning($"Recipe {recipe.name}: can't read requirement '{part.Trim()}', use Item:Amount.");
                        ok = false;
                        continue;
                    }
                    int perLevel = bits.Length > 2 && int.TryParse(bits[2], out int p) ? p : 0;
                    var itemPrefab = ObjectDB.instance.GetItemPrefab(bits[0]);
                    var itemDrop = itemPrefab ? itemPrefab.GetComponent<ItemDrop>() : null;
                    if (!itemDrop)
                    {
                        Logger.LogWarning($"Recipe {recipe.name}: item '{bits[0]}' not found, skipped.");
                        ok = false;
                        continue;
                    }
                    requirements.Add(new Piece.Requirement
                    {
                        m_resItem = itemDrop,
                        m_amount = amount,
                        m_amountPerLevel = perLevel,
                        m_recover = !settings.Recover.TryGetValue(bits[0], out bool recover) || recover
                    });
                }

                CraftingStation station = null;
                string stationName = (settings.CraftingStation.Value ?? "").Trim();
                if (stationName != "")
                {
                    station = PrefabManager.Cache.GetPrefab<CraftingStation>(stationName);
                    if (!station && ZNetScene.instance)
                    {
                        var stationPrefab = ZNetScene.instance.GetPrefab(stationName);
                        station = stationPrefab ? stationPrefab.GetComponent<CraftingStation>() : null;
                    }
                    if (!station)
                    {
                        Logger.LogWarning($"Recipe {recipe.name}: crafting station '{stationName}' not found, keeping the current one.");
                        station = recipe.m_craftingStation;
                        ok = false;
                    }
                }

                recipe.m_enabled = settings.Enabled.Value;
                recipe.m_resources = requirements.ToArray();
                recipe.m_craftingStation = station;
                recipe.m_minStationLevel = Mathf.Max(1, settings.StationLevel.Value);
                recipe.m_amount = Mathf.Max(1, settings.Amount.Value);
            }
            catch (Exception ex)
            {
                Logger.LogWarning($"Exception caught while applying recipe config for {recipe.name}: {ex}");
                ok = false;
            }
            return ok;
        }
    }
}