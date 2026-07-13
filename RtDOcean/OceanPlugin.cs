using BepInEx;
using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
using System;
using UnityEngine;
using System.Reflection;
using BepInEx.Configuration;
using static Heightmap;

namespace RtDOcean
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
    [BepInDependency("com.jotunn.jotunn", BepInDependency.DependencyFlags.HardDependency)]
    [BepInIncompatibility("blacks7ar.SeedBed")]
    
    internal partial class OceanPlugin : BaseUnityPlugin 
    {
        public const string PluginGUID = "soloredis.rtdocean";

        public const string PluginName = "RtDOcean";

        public const string PluginVersion = "2.2.38";
        
        public AssetBundle MyAssets;

        public ConfigEntry<bool> LoggingEnable;

        private void Awake()
        {
            CreateConfigs();
            LoadBundle();
            AddItems();
            AddPrefabs1();
            AddMonsters();
            AddBasicsStatusEffects();
            Addlocalizations();
            JSONSupport();
            SFX();
            Harpooned();
            MeadowsPlantConfig();
            BlackForestPlantConfig();
            SwampPlantConfig();
            PlainsPlantConfig();
            MistlandsPlantConfig();
            OceanPlantConfig();
            OceanBushConfig();
            Spawns();
            CreateRecipes();
            AddPrepPieces();
            AddSaplingCabbage();
            AddSaplingChantenay();
            AddSaplingCorn();
            AddSaplingCucumber();
            AddSaplingGarlic();
            AddSaplingOnion();
            AddSaplingPotato();
            AddSaplingPumpkin();
            AddSaplingRadish();
            AddSaplingRedbeet();
            AddSaplingRice();
            AddSaplingTomato();
            AddSaplingWatermelon();
            AddSaplingWheat();
            AddShrimpTrapConfig();
            AddSummons();
            OceanClutterSeaWeedConfig();
            OceanClutterSeaShellConfig();
            PrefabManager.OnPrefabsRegistered += FixSFX;
            PrefabManager.OnVanillaPrefabsAvailable += EditBoats;
            if (LoggingEnable.Value) { Logger.LogWarning("Logging is enabled in the config."); }
        }

        private void LoadBundle()
        {
            try
            {
                MyAssets = AssetUtils.LoadAssetBundleFromResources("rtd_ocean", Assembly.GetExecutingAssembly());

            }
            catch (Exception ex)
            {
                Logger.LogWarning($"Exception caught while loading bundle: {ex}");
            }
        }

        public string[] HarpoonedList = new string[]
        {
            "SE_CatfishHarpooned_RtD"
        };

        public string[] BasicStatusEffectList = new string[]
        {
            "SE_FaeHeal_RtD",
            "SE_Tared_RtD",
            "SE_AbyssalShield_RtD",
            "SE_AbyssalSpear_RtD",
            "SE_OceanBelt_RtD",
            "SE_SeaFare_RtD",
            "SE_SeaShard_RtD",
            "SE_ShardSummon_RtD"
        };
        
        public string[] SummonList = new string[]
        {
            "NeckSummon_RtD"
        };

        public string[] MonsterList = new string[]
        {
            "Neck_RtD",
            "Fairy4_RtD"
        };

        public string[] ItemList = new string[]
        {
            // Meats
            "Item_Shrimp_RtD",
            "Meat_Cod_RtD",
            "Meat_Crab_RtD",
            "Meat_Manta_RtD",
            "Meat_Marlin_RtD",
            "Meat_Shark_RtD",
            "Meat_Squid_RtD",
            "Meat_Tuna_RtD",
            "Meat_Turtle_RtD",
            "Meat_Whale_RtD",
            // Seeds & Vegetables
            "Seed_Cabbage_RtD",
            "Vegetable_Cabbage_RtD",
            "Seed_Chantenay_RtD",
            "Vegetable_Chantenay_RtD",
            "Seed_Corn_RtD",
            "Vegetable_Corn_RtD",
            "Seed_Cucumber_RtD",
            "Vegetable_Cucumber_RtD",
            "Seed_Garlic_RtD",
            "Vegetable_Garlic_RtD",
            "Seed_Onion_RtD",
            "Vegetable_Onion_RtD",
            "Seed_Potato_RtD",
            "Vegetable_Potato_RtD",
            "Seed_Pumpkin_RtD",
            "Vegetable_Pumpkin_RtD",
            "Seed_Radish_RtD",
            "Vegetable_Radish_RtD",
            "Seed_RedBeet_RtD",
            "Vegetable_RedBeet_RtD",
            "Seed_Rice_RtD",
            "Vegetable_RiceSack_RtD",
            "Seed_Tomato_RtD",
            "Vegetable_Tomato_RtD",
            "Seed_Watermelon_RtD",
            "Vegetable_Watermelon_RtD",
            "Seed_Wheat_RtD",
            "Vegetable_Wheat_RtD",
            "SeaWeed_RtD",
            "SeaMonsterTailRaw_RtD",
            "SharkMeatRaw_RtD",
            
            // Foods
            "Item_Cereal_RtD",
            "Item_Ramen_RtD",
            "Item_Rice_Shrimp_RtD",
            "Item_Soup_Corn_RtD",
            "Item_Soup_Miso_RtD",
            "Item_Soup_Mushroom_RtD",
            "Item_Soup_Pumpkin_RtD",
            "Item_Soup_Tomato_RtD",
            "Item_Stew_Cereal_RtD",
            "Item_Stew_Corn_RtD",
            "Item_Stew_Miso_RtD",
            "Item_Stew_Mushroom_RtD",
            "Item_Stew_Pumpkin_RtD",
            "Item_Stew_RicePudding_RtD",
            "Item_Stew_Shrimp_RtD",
            "Item_Stew_Tomato_RtD",
            "Item_Sushi_Bream_RtD",
            "Item_Sushi_Caviar_RtD",
            "Item_Sushi_Cod_RtD",
            "Item_Sushi_Marlin_RtD",
            "Item_Sushi_Moka_RtD",
            "Item_Sushi_Roll_RtD",
            "Item_Sushi_Shrimp_RtD",
            "Item_Sushi_Squid_RtD",
            "Item_Sushi_Tuna_RtD",
            "Item_Sushi_Unesu_RtD",
            "Item_Sushi_Urchin_RtD",
            "Item_Sushi_Whale_RtD",
            "Item_Tofu_RtD",
            // Old foods
            "FishWraps1_RtD",
            "FishWraps2_RtD",
            "FishWraps3_RtD",
            "FishWraps4_RtD",
            "FishWraps5_RtD",
            "FishWraps6_RtD",
            "FishWraps7_RtD",
            "FishWraps8_RtD",
            "FishWraps9_RtD",
            "FishWraps10_RtD",
            "FishWraps11_RtD",
            "FishWraps12_RtD",
            "SeaMonsterStew_RtD",
            "SharkMeatStew_RtD",
            "SeaMonsterTailCooked_RtD",
            "SharkMeatCooked_RtD",
            "FishCooked1_RtD",
            "FishCooked2_RtD",
            "FishCooked3_RtD",
            "FishCooked4_RtD",
            "FishCooked5_RtD",
            "FishCooked6_RtD",
            "FishCooked7_RtD",
            "FishCooked8_RtD",
            "FishCooked9_RtD",
            "FishCooked10_RtD",
            "FishCooked11_RtD",
            "FishCooked12_RtD",
            // Relic
            "OceanBelt_RtD",
            "SeaFareCape_RtD",
            "SeaScaleShield_RtD",
            "SeaShardSpear_RtD",
            "SeaShardStaff_RtD",
            "SeaShardWand_RtD"
        };

        public string[] SFXList = new string[]
        {
            "sfx_faegreen_alerted_RtD",
            "sfx_watercastSOL_RtD",
            "sfx_faegreen_attack_RtD",
            "sfx_faegreen_cast_RtD",
            "sfx_faegreen_death_RtD",
            "sfx_faegreen_idle_RtD",
            "sfx_fish_hit_RtD",
            "sfx_leech_death_RtD",
            "sfx_leech_hit_RtD",
            "sfx_mushroom_poofinahle_RtD",
            "sfx_neck_alerted_RtD",
            "sfx_neck_attack_hit_RtD",
            "sfx_neck_attack_RtD",
            "sfx_neck_death_RtD",
            "sfx_neck_hit_RtD",
            "sfx_neck_idle_RtD",
            "sfx_serpent_idle_RtD",
            "sfx_serpent_taunt_RtD",
            "sfx_throw_RtD",
            "sfx_mirmaid_alerted_RtD",
            "sfx_mirmaid_attack_RtD",
            "sfx_mirmaid_cast_RtD",
            "sfx_mirmaid_hit_RtD",
            "sfx_mirmaid_idle_RtD",
            "sfx_mirrmaid_death_RtD",
            "sfx_dolphin_chatter_RtD",
            "sfx_whale_humpback_RtD",
            "sfx_whale_orca_RtD",
            "sfx_whale_spermwhale_RtD"
        };

        public string[] PrefabList = new string[]
        {
            //FX
            "AshLandsSkullFX1_RtD",
            "FaeGreenProjectile_RtD",
            "Fairy4_Poof_FX_RtD",
            "fx_DvergerMage_Ice_hit_RtD",
            "NeckSummonProjectile_RtD",
            "projectile_chitinharpoon_catfish_RtD",
            "WaterAOESOL_RtD",
            "WaterExplosionWandSOL_RtD",
            "BlackForestSkullFX1_RtD",
            "DeepNorthSkullFX1_RtD",
            "Fairy4_Poof_FX1_RtD",
            "fx_blobtar_tarball_hit1_RtD",
            "fx_deathsquito_hit_RtD",
            "fx_deatsquito_death_RtD",
            "MistlandsSkullFX1_RtD",
            "NeckSpawnFX1_RtD",
            "PlainsSkullFX1_RtD",
            "Serpent_poofed_FX_RtD",
            "SwampSkullFX1_RtD",
            "vfx_fish_hit_RtD",
            "vfx_mushroom_spores_RtD",
            "vfx_neck_death_RtD",
            "vfx_neck_hit_RtD",
            "vfx_reptilespit_RtD",
            "vfx_serpent_death_RtD",
            "vfx_serpent_hurt_RtD",
            //Attacks
            "BoneFishAttack1_RtD",
            "BoneSquidAttack1_RtD",
            "BoneSquidAttack2_RtD",
            "CatFishAttack1_RtD",
            "CatFishAttack1_RtD1",
            "CatFishAttack2_RtD",
            "CatFishAttack2_RtD1",
            "fae_green_projectile1_RtD",
            "FairyAttackPoison1_RtD",
            "FairyHealAttackG_RtD",
            "heal_aoe_RtD",
            "LookerFishAttack1_RtD",
            "NeckBlue_BiteAttack1_RtD",
            "NeckBlue_BiteAttack2_RtD",
            "NokoAttack1_RtD",
            "NokoAttack2_RtD",
            "projectile_chitinharpoon_catfish07_RtD",
            "projectile_serpent_green_RtD",
            "reptile_projectile_tarlungs_RtD",
            "ReptileAttack1_RtD",
            "ReptileAttack1_RtD1",
            "ReptileAttack2_RtD",
            "ReptileAttack2_RtD1",
            "Shark_Attack1_RtD",
            "Shark_Attack2_RtD",
            "TenticleAttack1_RtD",
            "TenticleAttack2_RtD",
            "TenticleRanged1_RtD",
            "TenticleRanged2_RtD",
            "MirmaidAttack1_RtD",
            "MirmaidAttack2_RtD",
            "MirmaidAttack3_RtD",
            "MirmaidHeal_RtD",
            "MirmaidSpell_RtD",
            "Circle_section_RtD",

            //Ragdolls
            "Ragdoll_Belzor_RtD",
            "Ragdoll_BoneFish_RtD",
            "Ragdoll_BoneSquid_RtD",
            "Ragdoll_CatFish_RtD",
            "Ragdoll_CatFishWanderer_RtD",
            "Ragdoll_LuminousLooker_RtD",
            "Ragdoll_Mirmaid_RtD",
            "Ragdoll_MurkPod_RtD",
            "Ragdoll_Neck_RtD",
            "Ragdoll_Reptile_RtD",
            "Ragdoll_ReptileWanderer_RtD",
            "Ragdoll_MirLizard_RtD",
            "Ragdoll_MirRake_RtD",
            "Ragdoll_Shark_RtD"
        };
        
        public string[] MeadowsPlantList = new string[]
        {
            "Pickable_Rice_RtD",
            "Pickable_Chantenay_RtD"
        };
        
        public string[] BlackForestPlantList = new string[]
        {
            "Pickable_Cucumber_RtD",
            "Pickable_Tomato_RtD",
            "Pickable_Potato_RtD"
        };
        
        public string[] SwampPlantList = new string[]
        {
            "Pickable_Cabbage_RtD",
            "Pickable_RedBeet_RtD",
            "Pickable_Radish_RtD"
        };
        
        public string[] PlainsPlantList = new string[]
        {
            "Pickable_Corn_RtD",
            "Pickable_Wheat_RtD",
            "Pickable_Onion_RtD"
        };
        
        public string[] MistlandsPlantList = new string[]
        {
            "Pickable_Watermelon_RtD",
            "Pickable_Pumpkin_RtD",
            "Pickable_Garlic_RtD"
        };
        
        public string[] OceanPlantList = new string[]
        {
            "SeaWeed1_RtD",
            "SeaWeed2_RtD",
            "SeaWeed3_RtD",
            "SeaWeed4_RtD",
            "SeaWeed5_RtD",
            "SeaWeed6_RtD",
            "SeaWeed7_RtD",
            "SeaWeed8_RtD",
            "SeaWeed9_RtD",
            "SeaWeed10_RtD",
            "SeaWeed11_RtD",
            "SeaWeed12_RtD",
            "SeaWeed13_RtD",
            "SeaWeed14_RtD",
            "SeaWeed15_RtD",
            "SeaWeed16_RtD"
        };
        
        public string[] OceanBushList = new string[]
        {
            "Sponge1_RtD",
            "Sponge2_RtD",
            "Sponge3_RtD",
            "Sponge4_RtD",
            "Sponge5_RtD",
            "Sponge6_RtD",
            "Sponge7_RtD",
            "Sponge8_RtD",
            "Sponge9_RtD",
            "Sponge10_RtD",
            "Sponge11_RtD",
            "CoralStone1_RtD",
            "CoralStone2_RtD",
            "CoralStone3_RtD",
            "CoralStone4_RtD",
            "CoralStone5_RtD",
            "CoralStone6_RtD",
            "CoralStone7_RtD",
            "CoralStone8_RtD",
            "CoralStone9_RtD",
            "CoralStone10_RtD",
            "CoralStone11_RtD",
            "CoralStone12_RtD",
            "CoralStone13_RtD",
            "CoralStone14_RtD",
            "CoralStone15_RtD",
            "CoralStone16_RtD",
            "CoralStone17_RtD",
            "CoralStone18_RtD",
            "CoralStone19_RtD",
            "CoralStone20_RtD",
            "CoralStone21_RtD",
            "CoralStone22_RtD",
            "CoralStone23_RtD",
            "CoralStone24_RtD",
            "CoralStone25_RtD",
            "CoralStone26_RtD",
            "CoralStone27_RtD",
            "CoralStone28_RtD",
            "CoralStone29_RtD",
            "CoralStone30_RtD"
        };
        
        public string[] OceanClutterSeaWeedList = new string[]
        {
            "InstancedSeaWeed01_RtD",
            "InstancedSeaWeed02_RtD",
            "InstancedSeaWeed03_RtD",
            "InstancedSeaWeed04_RtD",
            "InstancedSeaWeed05_RtD",
            "InstancedSeaWeed06_RtD",
            "InstancedSeaWeed07_RtD",
            "InstancedSeaWeed08_RtD",
            "InstancedSeaWeed09_RtD"
        };
        
        public string[] OceanClutterSeaShellList = new string[]
        {
            "Instanced_SeaShell01_RtD",
            "Instanced_SeaShell02_RtD",
            "Instanced_SeaShell03_RtD",
            "Instanced_SeaShell04_RtD",
            "Instanced_SeaShell05_RtD",
            "Instanced_SeaShell06_RtD",
            "Instanced_SeaShell07_RtD",
            "Instanced_SeaShell08_RtD",
            "Instanced_SeaShell09_RtD",
            "Instanced_SeaShell10_RtD",
            "Instanced_SeaShell11_RtD",
            "Instanced_SeaShell12_RtD",
            "Instanced_SeaShell13_RtD",
            "Instanced_SeaShell14_RtD",
            "Instanced_SeaShell15_RtD",
            "Instanced_StarFish_RtD"
        };
        
        public string[] OceanShrimpTrapList = new string[]
        {
            "Pickable_ShrimpTrap_RtD",
        };
        
        public string[] PiecePrepList = new string[]
        {
            "Piece_Prep_Table_RtD"
        };
        
        public string[] SaplingCabbageList = new string[]
        {
            "Sapling_Cabbage_RtD"
        };
        
        public string[] SaplingChantenayList = new string[]
        {
            "Sapling_Chantenay_RtD"
        };
        
        public string[] SaplingCornList = new string[]
        {
            "Sapling_Corn_RtD"
        };
        
        public string[] SaplingCucumberList = new string[]
        {
            "Sapling_Cucumber_RtD"
        };
        
        public string[] SaplingGarlicList = new string[]
        {
            "Sapling_Garlic_RtD"
        };
        
        public string[] SaplingOnionList = new string[]
        {
            "Sapling_Onion_RtD"
        };
        
        public string[] SaplingPotatoList = new string[]
        {
            "Sapling_Potato_RtD"
        };
        
        public string[] SaplingPumpkinList = new string[]
        {
            "Sapling_Pumpkin_RtD"
        };
        
        public string[] SaplingRadishList = new string[]
        {
            "Sapling_Radish_RtD"
        };
        
        public string[] SaplingRedbeetList = new string[]
        {
            "Sapling_RedBeet_RtD"
        };
        
        public string[] SaplingRiceList = new string[]
        {
            "Sapling_Rice_RtD"
        };
        
        public string[] SaplingTomatoList = new string[]
        {
            "Sapling_Tomato_RtD"
        };
        
        public string[] SaplingWatermelonList = new string[]
        {
            "Sapling_Watermelon_RtD"
        };
        
        public string[] SaplingWheatList = new string[]
        {
            "Sapling_Wheat_RtD"
        };
        
        public static VegetationConfig MeadowsPlantValues = new VegetationConfig
        {
            Min = 1.1f,
            Max = 1.7f,
            ScaleMin = 1.1f,
            ScaleMax = 1.7f,
            MinAltitude = 1f,
            MaxAltitude = 100f,
            Biome = Biome.Meadows,
            MaxTilt = 80f,
            MaxTerrainDelta = 7f,
            GroupSizeMin = 1,
            GroupSizeMax = 2
        };
        
        public static VegetationConfig BlackForestPlantValues = new VegetationConfig
        {
            Min = 1.1f,
            Max = 1.7f,
            ScaleMin = 1.1f,
            ScaleMax = 1.7f,
            MinAltitude = 1f,
            MaxAltitude = 250f,
            Biome = Biome.BlackForest,
            MaxTilt = 20f,
            MaxTerrainDelta = 7f,
            GroupSizeMin = 1,
            GroupSizeMax = 2
        };
        
        public static VegetationConfig SwampPlantValues = new VegetationConfig
        {
            Min = 1.1f,
            Max = 1.7f,
            ScaleMin = 1.1f,
            ScaleMax = 1.7f,
            MinAltitude = 1f,
            MaxAltitude = 250f,
            Biome = Biome.Swamp,
            MaxTilt = 20f,
            MaxTerrainDelta = 7f,
            GroupSizeMin = 1,
            GroupSizeMax = 2
        };
        
        public static VegetationConfig PlainsPlantValues = new VegetationConfig
        {
            Min = 1.1f,
            Max = 1.7f,
            ScaleMin = 1.1f,
            ScaleMax = 1.7f,
            MinAltitude = 1f,
            MaxAltitude = 250f,
            Biome = Biome.Plains,
            MaxTilt = 20f,
            MaxTerrainDelta = 7f,
            GroupSizeMin = 1,
            GroupSizeMax = 2
        };
        
        public static VegetationConfig MistlandsPlantValues = new VegetationConfig
        {
            Min = 1.1f,
            Max = 1.7f,
            ScaleMin = 1.1f,
            ScaleMax = 1.7f,
            MinAltitude = 1f,
            MaxAltitude = 250f,
            Biome = Biome.Mistlands,
            MaxTilt = 20f,
            MaxTerrainDelta = 7f,
            GroupSizeMin = 1,
            GroupSizeMax = 2
        };
        
        public static VegetationConfig OceanPlantValues = new VegetationConfig
        {
            Min = 6.4f,
            Max = 8.7f,
            ScaleMin = 4.0f,
            ScaleMax = 4.4f,
            MinAltitude = -150f,
            MaxAltitude = -20f,
            Biome = Biome.Ocean | Biome.Meadows | Biome.BlackForest | Biome.Swamp | Biome.Plains | Biome.AshLands | Biome.DeepNorth,
            MaxTilt = 20f,
            MaxTerrainDelta = 3f,
            GroupSizeMin = 1,
            GroupSizeMax = 2,
            MinOceanDepth = 0f,
            MaxOceanDepth = 500f,
        };
        
        public static VegetationConfig OceanBushValues = new VegetationConfig
        {
            Max = 2f,
            ScaleMin = 3.4f,
            ScaleMax = 4.4f,
            MinAltitude = -150f,
            MaxAltitude = -10f,
            Biome = Biome.Ocean | Biome.Meadows | Biome.BlackForest | Biome.Swamp | Biome.Plains | Biome.AshLands | Biome.DeepNorth,
            MaxTilt = 80f,
            MaxTerrainDelta = 3f,
            GroupSizeMin = 1,
            GroupSizeMax = 2,
            MinOceanDepth = 0f,
            MaxOceanDepth = 500f,
        };
        
        public static VegetationConfig OceanMineRockValues = new VegetationConfig
        {
            Max = 1f,
            ScaleMin = 1.4f,
            ScaleMax = 2.4f,
            MinAltitude = -150f,
            MaxAltitude = -20f,
            Biome = Biome.Ocean,
            MaxTilt = 80f,
            MaxTerrainDelta = 3f,
            GroupSizeMin = 1,
            GroupSizeMax = 2,
            MinOceanDepth = 0f,
            MaxOceanDepth = 500f,
        };
        
        public static ClutterConfig OceanClutterSeaWeedValues = new ClutterConfig()
        {
            Instanced = true,
            Amount = 3,
            OnUncleared = true,
            OnCleared = false,
            ScaleMin = 1.0f,
            ScaleMax = 2.5f,
            MinAltitude = -150f,
            MaxAltitude = -3f,
            MaxTilt = 25f,
            OceanDepthCheck = true,
            MinOceanDepth = 0f,
            MaxOceanDepth = 500f,
            TerrainTilt = true,
            Biome = Biome.Ocean | Biome.Meadows | Biome.BlackForest | Biome.Swamp | Biome.Plains | Biome.AshLands | Biome.DeepNorth,
        };
        
        public static ClutterConfig OceanClutterSeaShellValues = new ClutterConfig()
        {
            Instanced = true,
            Amount = 3,
            OnUncleared = true,
            OnCleared = false,
            ScaleMin = 1.0f,
            ScaleMax = 2.5f,
            MinAltitude = -150f,
            MaxAltitude = -3f,
            MaxTilt = 15f,
            OceanDepthCheck = true,
            MinOceanDepth = 0f,
            MaxOceanDepth = 500f,
            TerrainTilt = true,
            Biome = Biome.Ocean | Biome.Meadows | Biome.BlackForest | Biome.Swamp | Biome.Plains | Biome.AshLands | Biome.DeepNorth,
        };
        
        public static VegetationConfig OceanShrimpTrapValues = new VegetationConfig
        {
            Min = 6f,
            Max = 8f,
            ScaleMin = 1.0f,
            ScaleMax = 1.1f,
            MinAltitude = -2.4f,
            MaxAltitude = 0f,
            Biome = Biome.Meadows| Biome.BlackForest,
            MaxTilt = 80f,
            MaxTerrainDelta = 3f,
            GroupSizeMax = 1,
        };
        
        public static PieceConfig PrepTableValues = new PieceConfig()
        {
            PieceTable = "_HammerPieceTable",
            Category = "Misc",
            Requirements = new RequirementConfig[]
            {
                new RequirementConfig
                {
                    Item = "Flint",
                    Amount = 10,
                    Recover = true
                },
                new RequirementConfig
                {
                    Item = "Wood",
                    Amount = 15,
                    Recover = true
                },
                new RequirementConfig
                {
                    Item = "Dandelion",
                    Amount = 5,
                    Recover = true
                }
            }
        };
        
        public static PieceConfig SaplingCabbageValues = new PieceConfig()
        {
            PieceTable = "_CultivatorPieceTable",
            Category = "Vegetables",
            Requirements = new RequirementConfig[]
            {
                new RequirementConfig
                {
                    Item = "Seed_Cabbage_RtD",
                    Amount = 1,
                    Recover = true
                }
            }
        };
        
        public static PieceConfig SaplingChantenayValues = new PieceConfig()
        {
            PieceTable = "_CultivatorPieceTable",
            Category = "Vegetables",
            Requirements = new RequirementConfig[]
            {
                new RequirementConfig
                {
                    Item = "Seed_Chantenay_RtD",
                    Amount = 1,
                    Recover = true
                }
            }
        };
        
                
        public static PieceConfig SaplingCornValues = new PieceConfig()
        {
            PieceTable = "_CultivatorPieceTable",
            Category = "Vegetables",
            Requirements = new RequirementConfig[]
            {
                new RequirementConfig
                {
                    Item = "Seed_Corn_RtD",
                    Amount = 1,
                    Recover = true
                }
            }
        };
        
        public static PieceConfig SaplingCucumberValues = new PieceConfig()
        {
            PieceTable = "_CultivatorPieceTable",
            Category = "Vegetables",
            Requirements = new RequirementConfig[]
            {
                new RequirementConfig
                {
                    Item = "Seed_Cucumber_RtD",
                    Amount = 1,
                    Recover = true
                }
            }
        };
        
        public static PieceConfig SaplingGarlicValues = new PieceConfig()
        {
            PieceTable = "_CultivatorPieceTable",
            Category = "Vegetables",
            Requirements = new RequirementConfig[]
            {
                new RequirementConfig
                {
                    Item = "Seed_Garlic_RtD",
                    Amount = 1,
                    Recover = true
                }
            }
        };
        
        public static PieceConfig SaplingOnionValues = new PieceConfig()
        {
            PieceTable = "_CultivatorPieceTable",
            Category = "Vegetables",
            Requirements = new RequirementConfig[]
            {
                new RequirementConfig
                {
                    Item = "Seed_Onion_RtD",
                    Amount = 1,
                    Recover = true
                }
            }
        };
        
        public static PieceConfig SaplingPotatoValues = new PieceConfig()
        {
            PieceTable = "_CultivatorPieceTable",
            Category = "Vegetables",
            Requirements = new RequirementConfig[]
            {
                new RequirementConfig
                {
                    Item = "Seed_Potato_RtD",
                    Amount = 1,
                    Recover = true
                }
            }
        };
        
        public static PieceConfig SaplingPumpkinValues = new PieceConfig()
        {
            PieceTable = "_CultivatorPieceTable",
            Category = "Vegetables",
            Requirements = new RequirementConfig[]
            {
                new RequirementConfig
                {
                    Item = "Seed_Pumpkin_RtD",
                    Amount = 1,
                    Recover = true
                }
            }
        };
        
        public static PieceConfig SaplingRadishValues = new PieceConfig()
        {
            PieceTable = "_CultivatorPieceTable",
            Category = "Vegetables",
            Requirements = new RequirementConfig[]
            {
                new RequirementConfig
                {
                    Item = "Seed_Radish_RtD",
                    Amount = 1,
                    Recover = true
                }
            }
        };
        
        public static PieceConfig SaplingRedbeetValues = new PieceConfig()
        {
            PieceTable = "_CultivatorPieceTable",
            Category = "Vegetables",
            Requirements = new RequirementConfig[]
            {
                new RequirementConfig
                {
                    Item = "Seed_RedBeet_RtD",
                    Amount = 1,
                    Recover = true
                }
            }
        };
        
        public static PieceConfig SaplingRiceValues = new PieceConfig()
        {
            PieceTable = "_CultivatorPieceTable",
            Category = "Vegetables",
            Requirements = new RequirementConfig[]
            {
                new RequirementConfig
                {
                    Item = "Seed_Rice_RtD",
                    Amount = 1,
                    Recover = true
                }
            }
        };
        
        public static PieceConfig SaplingTomatoValues = new PieceConfig()
        {
            PieceTable = "_CultivatorPieceTable",
            Category = "Vegetables",
            Requirements = new RequirementConfig[]
            {
                new RequirementConfig
                {
                    Item = "Seed_Tomato_RtD",
                    Amount = 1,
                    Recover = true
                }
            }
        };
        
        public static PieceConfig SaplingWatermelonValues = new PieceConfig()
        {
            PieceTable = "_CultivatorPieceTable",
            Category = "Vegetables",
            Requirements = new RequirementConfig[]
            {
                new RequirementConfig
                {
                    Item = "Seed_Watermelon_RtD",
                    Amount = 1,
                    Recover = true
                }
            }
        };
        
        public static PieceConfig SaplingWheatValues = new PieceConfig()
        {
            PieceTable = "_CultivatorPieceTable",
            Category = "Vegetables",
            Requirements = new RequirementConfig[]
            {
                new RequirementConfig
                {
                    Item = "Seed_Wheat_RtD",
                    Amount = 1,
                    Recover = true
                }
            }
        };
        
        private void CreateConfigs()
        {
            try
            {
                Config.SaveOnConfigSet = true;
                
                LoggingEnable = Config.Bind("Logging", "Enable", false, new ConfigDescription("Enable or Disable logging of mod.", null, new ConfigurationManagerAttributes
                {
                    IsAdminOnly = false,
                    Order = 45
                }));
            }
            catch (Exception arg)
            {
                Logger.LogWarning($"Exception caught while adding configuration values: {arg}");
            }
        }
        
        private void Spawns()
        {
            try
            {

                // Crab - Meadows & Ocean 
                var CrabPrefab = MyAssets.LoadAsset<GameObject>("Animal_Crab_RtD");
                var CrabConfig = new CreatureConfig();
                CrabConfig.Faction = Character.Faction.SeaMonsters;
                CrabConfig.UseCumulativeLevelEffects = true;
                CrabConfig.AddSpawnConfig(new SpawnConfig
                {

                    SpawnDistance = 100,
                    SpawnInterval = 250,
                    SpawnChance = 10,
                    SpawnAtNight = true,
                    SpawnAtDay = true,
                    MaxSpawned = 3,
                    MaxLevel = 2,
                    MaxAltitude = 0,
                    Biome = ZoneManager.AnyBiomeOf(Heightmap.Biome.Ocean, Heightmap.Biome.Meadows),
                });
                CreatureManager.Instance.AddCreature(new CustomCreature(CrabPrefab, true, CrabConfig));
                
                // Dolphin - Meadows & Ocean
                var DolphinPrefab = MyAssets.LoadAsset<GameObject>("Animal_Dolphin_RtD");
                var DolphinConfig = new CreatureConfig();
                DolphinConfig.Faction = Character.Faction.SeaMonsters;
                DolphinConfig.UseCumulativeLevelEffects = true;
                DolphinConfig.AddSpawnConfig(new SpawnConfig
                {

                    SpawnDistance = 100,
                    SpawnInterval = 450,
                    SpawnChance = 10,
                    SpawnAtNight = true,
                    SpawnAtDay = true,
                    MaxSpawned = 1,
                    MaxLevel = 2,
                    MaxAltitude = -5,
                    Biome = ZoneManager.AnyBiomeOf(Heightmap.Biome.Ocean, Heightmap.Biome.Meadows),
                });
                CreatureManager.Instance.AddCreature(new CustomCreature(DolphinPrefab, true, DolphinConfig));
                
                // Cod - BlackForest & Ocean
                var CodPrefab = MyAssets.LoadAsset<GameObject>("Animal_Cod_RtD");
                var CodConfig = new CreatureConfig();
                CodConfig.Faction = Character.Faction.SeaMonsters;
                CodConfig.UseCumulativeLevelEffects = true;
                CodConfig.AddSpawnConfig(new SpawnConfig
                {

                    SpawnDistance = 100,
                    SpawnInterval = 250,
                    SpawnChance = 10,
                    SpawnAtNight = true,
                    SpawnAtDay = true,
                    MaxSpawned = 3,
                    MaxLevel = 2,
                    MaxAltitude = -5,
                    Biome = ZoneManager.AnyBiomeOf(Heightmap.Biome.Ocean, Heightmap.Biome.BlackForest),
                });
                CreatureManager.Instance.AddCreature(new CustomCreature(CodPrefab, true, CodConfig));
                
                // Tuna - BlackForest & Ocean
                var TunaPrefab = MyAssets.LoadAsset<GameObject>("Animal_Tuna_RtD");
                var TunaConfig = new CreatureConfig();
                TunaConfig.Faction = Character.Faction.SeaMonsters;
                TunaConfig.UseCumulativeLevelEffects = true;
                TunaConfig.AddSpawnConfig(new SpawnConfig
                {

                    SpawnDistance = 100,
                    SpawnInterval = 250,
                    SpawnChance = 10,
                    SpawnAtNight = true,
                    SpawnAtDay = true,
                    MaxSpawned = 3,
                    MaxLevel = 2,
                    MaxAltitude = -5,
                    Biome = ZoneManager.AnyBiomeOf(Heightmap.Biome.Ocean, Heightmap.Biome.BlackForest),
                });
                CreatureManager.Instance.AddCreature(new CustomCreature(TunaPrefab, true, TunaConfig));
                
                // Great White Shark - Swamp & Ocean
                var GreatWhitePrefab = MyAssets.LoadAsset<GameObject>("Monster_GreatWhiteShark_RtD");
                var GreatWhiteConfig = new CreatureConfig();
                GreatWhiteConfig.Faction = Character.Faction.SeaMonsters;
                GreatWhiteConfig.UseCumulativeLevelEffects = true;
                GreatWhiteConfig.AddSpawnConfig(new SpawnConfig
                {

                    SpawnDistance = 100,
                    SpawnInterval = 450,
                    SpawnChance = 15,
                    SpawnAtNight = true,
                    SpawnAtDay = true,
                    MaxSpawned = 1,
                    MaxLevel = 2,
                    MaxAltitude = -5,
                    Biome = ZoneManager.AnyBiomeOf(Heightmap.Biome.Ocean, Heightmap.Biome.Swamp),
                });
                CreatureManager.Instance.AddCreature(new CustomCreature(GreatWhitePrefab, true, GreatWhiteConfig));
                
                // Squid - Swamp & Ocean
                var SquidPrefab = MyAssets.LoadAsset<GameObject>("Animal_Squid_RtD");
                var SquidConfig = new CreatureConfig();
                SquidConfig.Faction = Character.Faction.SeaMonsters;
                SquidConfig.UseCumulativeLevelEffects = true;
                SquidConfig.AddSpawnConfig(new SpawnConfig
                {

                    SpawnDistance = 100,
                    SpawnInterval = 250,
                    SpawnChance = 10,
                    SpawnAtNight = true,
                    SpawnAtDay = true,
                    MaxSpawned = 2,
                    MaxLevel = 3,
                    MaxAltitude = -5,
                    Biome = ZoneManager.AnyBiomeOf(Heightmap.Biome.Ocean, Heightmap.Biome.Swamp),
                });
                CreatureManager.Instance.AddCreature(new CustomCreature(SquidPrefab, true, SquidConfig));
                
                // Manta - Swamp & Ocean
                var MantaPrefab = MyAssets.LoadAsset<GameObject>("Animal_Manta_RtD");
                var MantaConfig = new CreatureConfig();
                MantaConfig.Faction = Character.Faction.SeaMonsters;
                MantaConfig.UseCumulativeLevelEffects = true;
                MantaConfig.AddSpawnConfig(new SpawnConfig
                {

                    SpawnDistance = 100,
                    SpawnInterval = 250,
                    SpawnChance = 10,
                    SpawnAtNight = true,
                    SpawnAtDay = true,
                    MaxSpawned = 3,
                    MaxLevel = 2,
                    MaxAltitude = -5,
                    Biome = ZoneManager.AnyBiomeOf(Heightmap.Biome.Ocean, Heightmap.Biome.Swamp),
                });
                CreatureManager.Instance.AddCreature(new CustomCreature(MantaPrefab, true, MantaConfig));
                
                // Hammerhead Shark - Plains & Ocean
                var HammerheadPrefab = MyAssets.LoadAsset<GameObject>("Monster_HammerheadShark_RtD");
                var HammerheadConfig = new CreatureConfig();
                HammerheadConfig.Faction = Character.Faction.SeaMonsters;
                HammerheadConfig.UseCumulativeLevelEffects = true;
                HammerheadConfig.AddSpawnConfig(new SpawnConfig
                {

                    SpawnDistance = 100,
                    SpawnInterval = 450,
                    SpawnChance = 15,
                    SpawnAtNight = true,
                    SpawnAtDay = true,
                    MaxSpawned = 1,
                    MaxLevel = 2,
                    MaxAltitude = -5,
                    Biome = ZoneManager.AnyBiomeOf(Heightmap.Biome.Ocean, Heightmap.Biome.Plains),
                });
                CreatureManager.Instance.AddCreature(new CustomCreature(HammerheadPrefab, true, HammerheadConfig));
                
                // Marlin - Plains & Ocean
                var MarlinPrefab = MyAssets.LoadAsset<GameObject>("Animal_Marlin_RtD");
                var MarlinConfig = new CreatureConfig();
                MarlinConfig.Faction = Character.Faction.SeaMonsters;
                MarlinConfig.UseCumulativeLevelEffects = true;
                MarlinConfig.AddSpawnConfig(new SpawnConfig
                {

                    SpawnDistance = 100,
                    SpawnInterval = 250,
                    SpawnChance = 10,
                    SpawnAtNight = true,
                    SpawnAtDay = true,
                    MaxSpawned = 3,
                    MaxLevel = 2,
                    MaxAltitude = -5,
                    Biome = ZoneManager.AnyBiomeOf(Heightmap.Biome.Ocean, Heightmap.Biome.Plains),
                });
                CreatureManager.Instance.AddCreature(new CustomCreature(MarlinPrefab, true, MarlinConfig));
                
                // Turtle - Mistlands & Ocean
                var TurtlePrefab = MyAssets.LoadAsset<GameObject>("Animal_Turtle_RtD");
                var TurtleConfig = new CreatureConfig();
                TurtleConfig.Faction = Character.Faction.SeaMonsters;
                TurtleConfig.UseCumulativeLevelEffects = true;
                TurtleConfig.AddSpawnConfig(new SpawnConfig
                {

                    SpawnDistance = 100,
                    SpawnInterval = 250,
                    SpawnChance = 10,
                    SpawnAtNight = true,
                    SpawnAtDay = true,
                    MaxSpawned = 3,
                    MaxLevel = 2,
                    MaxAltitude = -5,
                    Biome = ZoneManager.AnyBiomeOf(Heightmap.Biome.Ocean, Heightmap.Biome.Mistlands),
                });
                CreatureManager.Instance.AddCreature(new CustomCreature(TurtlePrefab, true, TurtleConfig));
                
                
                // Megalodon - Mistlands
                var MegalodonPrefab = MyAssets.LoadAsset<GameObject>("Shark_RtD");
                var MegalodonConfig = new CreatureConfig();
                MegalodonConfig.Faction = Character.Faction.SeaMonsters;
                MegalodonConfig.UseCumulativeLevelEffects = true;
                MegalodonConfig.AddSpawnConfig(new SpawnConfig
                {

                    SpawnDistance = 100,
                    SpawnInterval = 450,
                    SpawnChance = 15,
                    SpawnAtNight = true,
                    SpawnAtDay = true,
                    MaxSpawned = 1,
                    MaxLevel = 2,
                    MaxAltitude = -5,
                    Biome = ZoneManager.AnyBiomeOf(Heightmap.Biome.Mistlands),
                });
                CreatureManager.Instance.AddCreature(new CustomCreature(MegalodonPrefab, true,MegalodonConfig));
                
                // Mirmaid - Mistlands & DeepNorth
                var MirmaidPrefab = MyAssets.LoadAsset<GameObject>("Mirmaid_RtD");
                var MirmaidConfig = new CreatureConfig();
                MirmaidConfig.Faction = Character.Faction.SeaMonsters;
                MirmaidConfig.UseCumulativeLevelEffects = true;
                MirmaidConfig.AddSpawnConfig(new SpawnConfig
                {

                    SpawnDistance = 100,
                    SpawnInterval = 500,
                    SpawnChance = 10,
                    SpawnAtNight = true,
                    SpawnAtDay = true,
                    MaxSpawned = 1,
                    MaxLevel = 2,
                    MaxAltitude = -5,
                    Biome = ZoneManager.AnyBiomeOf(Heightmap.Biome.Mistlands, Heightmap.Biome.DeepNorth),
                });
                CreatureManager.Instance.AddCreature(new CustomCreature(MirmaidPrefab, true,MirmaidConfig));
                
                // BoneFish - AshLands
                var BoneFishPrefab = MyAssets.LoadAsset<GameObject>("BoneFish_RtD");
                var BoneFishConfig = new CreatureConfig();
                BoneFishConfig.Faction = Character.Faction.SeaMonsters;
                BoneFishConfig.UseCumulativeLevelEffects = true;
                BoneFishConfig.AddSpawnConfig(new SpawnConfig
                {

                    SpawnDistance = 100,
                    SpawnInterval = 500,
                    SpawnChance = 10,
                    SpawnAtNight = true,
                    SpawnAtDay = true,
                    MaxSpawned = 1,
                    MaxLevel = 2,
                    MaxAltitude = -5,
                    Biome = ZoneManager.AnyBiomeOf(Heightmap.Biome.AshLands),
                });
                CreatureManager.Instance.AddCreature(new CustomCreature(BoneFishPrefab, true,BoneFishConfig));
                
                // BoneSquid - AshLands
                var BoneSquidPrefab = MyAssets.LoadAsset<GameObject>("BoneSquid_RtD");
                var BoneSquidConfig = new CreatureConfig();
                BoneSquidConfig.Faction = Character.Faction.SeaMonsters;
                BoneSquidConfig.UseCumulativeLevelEffects = true;
                BoneSquidConfig.AddSpawnConfig(new SpawnConfig
                {

                    SpawnDistance = 100,
                    SpawnInterval = 500,
                    SpawnChance = 10,
                    SpawnAtNight = true,
                    SpawnAtDay = true,
                    MaxSpawned = 1,
                    MaxLevel = 2,
                    MaxAltitude = -5,
                    Biome = ZoneManager.AnyBiomeOf(Heightmap.Biome.AshLands),
                });
                CreatureManager.Instance.AddCreature(new CustomCreature(BoneSquidPrefab, true,BoneSquidConfig));
                
                // Luminous Looker - DeepNorth
                var LookerPrefab = MyAssets.LoadAsset<GameObject>("LuminousLooker_RtD");
                var LookerConfig = new CreatureConfig();
                LookerConfig.Faction = Character.Faction.SeaMonsters;
                LookerConfig.UseCumulativeLevelEffects = true;
                LookerConfig.AddSpawnConfig(new SpawnConfig
                {

                    SpawnDistance = 100,
                    SpawnInterval = 500,
                    SpawnChance = 10,
                    SpawnAtNight = true,
                    SpawnAtDay = true,
                    MaxSpawned = 1,
                    MaxLevel = 2,
                    MaxAltitude = -5,
                    Biome = ZoneManager.AnyBiomeOf(Heightmap.Biome.DeepNorth),
                });
                CreatureManager.Instance.AddCreature(new CustomCreature(LookerPrefab, true,LookerConfig));
                
                // Murkpod - DeepNorth
                var MurkpodPrefab = MyAssets.LoadAsset<GameObject>("MurkPod_RtD");
                var MurkpodConfig = new CreatureConfig();
                MurkpodConfig.Faction = Character.Faction.SeaMonsters;
                MurkpodConfig.UseCumulativeLevelEffects = true;
                MurkpodConfig.AddSpawnConfig(new SpawnConfig
                {

                    SpawnDistance = 100,
                    SpawnInterval = 500,
                    SpawnChance = 10,
                    SpawnAtNight = true,
                    SpawnAtDay = true,
                    MaxSpawned = 1,
                    MaxLevel = 2,
                    MaxAltitude = -5,
                    Biome = ZoneManager.AnyBiomeOf(Heightmap.Biome.DeepNorth),
                });
                CreatureManager.Instance.AddCreature(new CustomCreature(MurkpodPrefab, true,MurkpodConfig));
                
                // Sperm Whale - Ocean
                var SpermWhalePrefab = MyAssets.LoadAsset<GameObject>("Animal_SpermWhale_RtD");
                var SpermWhaleConfig = new CreatureConfig();
                SpermWhaleConfig.Faction = Character.Faction.SeaMonsters;
                SpermWhaleConfig.UseCumulativeLevelEffects = true;
                SpermWhaleConfig.AddSpawnConfig(new SpawnConfig
                {

                    SpawnDistance = 100,
                    SpawnInterval = 600,
                    SpawnChance = 10,
                    SpawnAtNight = true,
                    SpawnAtDay = true,
                    MaxSpawned = 1,
                    MaxLevel = 2,
                    MaxAltitude = -5,
                    Biome = ZoneManager.AnyBiomeOf(Heightmap.Biome.Ocean),
                });
                CreatureManager.Instance.AddCreature(new CustomCreature(SpermWhalePrefab, true,SpermWhaleConfig));
                
                // Humpback Whale - Ocean
                var HumpbackPrefab = MyAssets.LoadAsset<GameObject>("Animal_HumpbackWhale_RtD");
                var HumpbackConfig = new CreatureConfig();
                HumpbackConfig.Faction = Character.Faction.SeaMonsters;
                HumpbackConfig.UseCumulativeLevelEffects = true;
                HumpbackConfig.AddSpawnConfig(new SpawnConfig
                {

                    SpawnDistance = 100,
                    SpawnInterval = 600,
                    SpawnChance = 10,
                    SpawnAtNight = true,
                    SpawnAtDay = true,
                    MaxSpawned = 1,
                    MaxLevel = 2,
                    MaxAltitude = -5,
                    Biome = ZoneManager.AnyBiomeOf(Heightmap.Biome.Ocean),
                });
                CreatureManager.Instance.AddCreature(new CustomCreature(HumpbackPrefab, true,HumpbackConfig));

                // Orca Whale - Ocean
                var OrcaPrefab = MyAssets.LoadAsset<GameObject>("Monster_Orca_RtD");
                var OrcaConfig = new CreatureConfig();
                OrcaConfig.Faction = Character.Faction.SeaMonsters;
                OrcaConfig.UseCumulativeLevelEffects = true;
                OrcaConfig.AddSpawnConfig(new SpawnConfig
                {

                    SpawnDistance = 100,
                    SpawnInterval = 600,
                    SpawnChance = 10,
                    SpawnAtNight = true,
                    SpawnAtDay = true,
                    MaxSpawned = 1,
                    MaxLevel = 2,
                    MaxAltitude = -5,
                    Biome = ZoneManager.AnyBiomeOf(Heightmap.Biome.Ocean),
                });
                CreatureManager.Instance.AddCreature(new CustomCreature(OrcaPrefab, true,OrcaConfig));
                
                // Catfish Wanderer - Ocean
                var CatFishPrefab = MyAssets.LoadAsset<GameObject>("CatFish_RtD");
                var CatFishConfig = new CreatureConfig();
                CatFishConfig.Faction = Character.Faction.SeaMonsters;
                CatFishConfig.UseCumulativeLevelEffects = true;
                CatFishConfig.AddSpawnConfig(new SpawnConfig
                {

                    SpawnDistance = 100,
                    SpawnInterval = 800,
                    SpawnChance = 5,
                    SpawnAtNight = true,
                    SpawnAtDay = true,
                    MaxSpawned = 1,
                    MaxLevel = 2,
                    MaxAltitude = -5,
                    Biome = ZoneManager.AnyBiomeOf(Heightmap.Biome.Ocean),
                });
                CreatureManager.Instance.AddCreature(new CustomCreature(CatFishPrefab, true,CatFishConfig));
                
                // Reptile Wanderer - Ocean
                var ReptilePrefab = MyAssets.LoadAsset<GameObject>("Reptile_RtD");
                var ReptileConfig = new CreatureConfig();
                ReptileConfig.Faction = Character.Faction.SeaMonsters;
                ReptileConfig.UseCumulativeLevelEffects = true;
                ReptileConfig.AddSpawnConfig(new SpawnConfig
                {

                    SpawnDistance = 100,
                    SpawnInterval = 800,
                    SpawnChance = 5,
                    SpawnAtNight = true,
                    SpawnAtDay = true,
                    MaxSpawned = 1,
                    MaxLevel = 2,
                    MaxAltitude = -5,
                    Biome = ZoneManager.AnyBiomeOf(Heightmap.Biome.Ocean),
                });
                CreatureManager.Instance.AddCreature(new CustomCreature(ReptilePrefab, true,ReptileConfig));
                
                // Lizard Wanderer - Ocean
                var LizardPrefab = MyAssets.LoadAsset<GameObject>("MirLizard_RtD");
                var LizardConfig = new CreatureConfig();
                LizardConfig.Faction = Character.Faction.SeaMonsters;
                LizardConfig.UseCumulativeLevelEffects = true;
                LizardConfig.AddSpawnConfig(new SpawnConfig
                {

                    SpawnDistance = 100,
                    SpawnInterval = 800,
                    SpawnChance = 5,
                    SpawnAtNight = true,
                    SpawnAtDay = true,
                    MaxSpawned = 1,
                    MaxLevel = 2,
                    MaxAltitude = -5,
                    Biome = ZoneManager.AnyBiomeOf(Heightmap.Biome.Ocean),
                });
                CreatureManager.Instance.AddCreature(new CustomCreature(LizardPrefab, true,LizardConfig));
                
                // Rake Wanderer - Ocean
                var RakePrefab = MyAssets.LoadAsset<GameObject>("MirRake_RtD");
                var RakeConfig = new CreatureConfig();
                RakeConfig.Faction = Character.Faction.SeaMonsters;
                RakeConfig.UseCumulativeLevelEffects = true;
                RakeConfig.AddSpawnConfig(new SpawnConfig
                {

                    SpawnDistance = 100,
                    SpawnInterval = 800,
                    SpawnChance = 5,
                    SpawnAtNight = true,
                    SpawnAtDay = true,
                    MaxSpawned = 1,
                    MaxLevel = 2,
                    MaxAltitude = -5,
                    Biome = ZoneManager.AnyBiomeOf(Heightmap.Biome.Ocean),
                });
                CreatureManager.Instance.AddCreature(new CustomCreature(RakePrefab, true,RakeConfig));
                
                // Belzor Wanderer - Ocean
                var BelzorPrefab = MyAssets.LoadAsset<GameObject>("Belzor_RtD");
                var BelzorConfig = new CreatureConfig();
                BelzorConfig.Faction = Character.Faction.SeaMonsters;
                BelzorConfig.UseCumulativeLevelEffects = true;
                BelzorConfig.AddSpawnConfig(new SpawnConfig
                {

                    SpawnDistance = 100,
                    SpawnInterval = 3600,
                    SpawnChance = 5,
                    SpawnAtNight = true,
                    SpawnAtDay = false,
                    MaxSpawned = 1,
                    MaxLevel = 1,
                    MaxAltitude = -5,
                    Biome = ZoneManager.AnyBiomeOf(Heightmap.Biome.Ocean),
                });
                CreatureManager.Instance.AddCreature(new CustomCreature(BelzorPrefab, true,BelzorConfig));
                
            }
            catch (Exception ex)
            {
                Logger.LogWarning($"Exception caught while adding custom object: {ex}");
            }
        }
        
        private void CreateRecipes()
        {
            try
            {
                var fishCooked1 = new RecipeConfig();
                fishCooked1.Item = "FishCooked1_RtD"; // Name of the item prefab to be crafted
                fishCooked1.AddRequirement(new RequirementConfig("Fish1", 1));
                fishCooked1.CraftingStation = CraftingStations.Cauldron;
                ItemManager.Instance.AddRecipe(new CustomRecipe(fishCooked1));

                var fishCooked2 = new RecipeConfig();
                fishCooked2.Item = "FishCooked2_RtD"; // Name of the item prefab to be crafted
                fishCooked2.AddRequirement(new RequirementConfig("Fish2", 1));
                fishCooked2.CraftingStation = CraftingStations.Cauldron;
                ItemManager.Instance.AddRecipe(new CustomRecipe(fishCooked2));

                var fishCooked3 = new RecipeConfig();
                fishCooked3.Item = "FishCooked3_RtD"; // Name of the item prefab to be crafted
                fishCooked3.AddRequirement(new RequirementConfig("Fish3", 1));
                fishCooked3.CraftingStation = CraftingStations.Cauldron;
                ItemManager.Instance.AddRecipe(new CustomRecipe(fishCooked3));

                var fishCooked4 = new RecipeConfig();
                fishCooked4.Item = "FishCooked4_RtD"; // Name of the item prefab to be crafted
                fishCooked4.AddRequirement(new RequirementConfig("Fish4_cave", 1));
                fishCooked4.CraftingStation = CraftingStations.Cauldron;
                ItemManager.Instance.AddRecipe(new CustomRecipe(fishCooked4));

                var fishCooked5 = new RecipeConfig();
                fishCooked5.Item = "FishCooked5_RtD"; // Name of the item prefab to be crafted
                fishCooked5.AddRequirement(new RequirementConfig("Fish5", 1));
                fishCooked5.CraftingStation = CraftingStations.Cauldron;
                ItemManager.Instance.AddRecipe(new CustomRecipe(fishCooked5));

                var fishCooked6 = new RecipeConfig();
                fishCooked6.Item = "FishCooked6_RtD"; // Name of the item prefab to be crafted
                fishCooked6.AddRequirement(new RequirementConfig("Fish6", 1));
                fishCooked6.CraftingStation = CraftingStations.Cauldron;
                ItemManager.Instance.AddRecipe(new CustomRecipe(fishCooked6));

                var fishCooked7 = new RecipeConfig();
                fishCooked7.Item = "FishCooked7_RtD"; // Name of the item prefab to be crafted
                fishCooked7.AddRequirement(new RequirementConfig("Fish7", 1));
                fishCooked7.CraftingStation = CraftingStations.Cauldron;
                ItemManager.Instance.AddRecipe(new CustomRecipe(fishCooked7));

                var fishCooked8 = new RecipeConfig();
                fishCooked8.Item = "FishCooked8_RtD"; // Name of the item prefab to be crafted
                fishCooked8.AddRequirement(new RequirementConfig("Fish8", 1));
                fishCooked8.CraftingStation = CraftingStations.Cauldron;
                ItemManager.Instance.AddRecipe(new CustomRecipe(fishCooked8));

                var fishCooked9 = new RecipeConfig();
                fishCooked9.Item = "FishCooked9_RtD"; // Name of the item prefab to be crafted
                fishCooked9.AddRequirement(new RequirementConfig("Fish9", 1));
                fishCooked9.CraftingStation = CraftingStations.Cauldron;
                ItemManager.Instance.AddRecipe(new CustomRecipe(fishCooked9));

                var fishCooked10 = new RecipeConfig();
                fishCooked10.Item = "FishCooked10_RtD"; // Name of the item prefab to be crafted
                fishCooked10.AddRequirement(new RequirementConfig("Fish10", 1));
                fishCooked10.CraftingStation = CraftingStations.Cauldron;
                ItemManager.Instance.AddRecipe(new CustomRecipe(fishCooked10));

                var fishCooked11 = new RecipeConfig();
                fishCooked11.Item = "FishCooked11_RtD"; // Name of the item prefab to be crafted
                fishCooked11.AddRequirement(new RequirementConfig("Fish11", 1));
                fishCooked11.CraftingStation = CraftingStations.Cauldron;
                ItemManager.Instance.AddRecipe(new CustomRecipe(fishCooked11));

                var fishCooked12 = new RecipeConfig();
                fishCooked12.Item = "FishCooked12_RtD"; // Name of the item prefab to be crafted
                fishCooked12.AddRequirement(new RequirementConfig("Fish12", 1));
                fishCooked12.CraftingStation = CraftingStations.Cauldron;
                ItemManager.Instance.AddRecipe(new CustomRecipe(fishCooked12));

                var fishCooked13 = new RecipeConfig();
                fishCooked13.Item = "SeaMonsterTailCooked_RtD"; // Name of the item prefab to be crafted
                fishCooked13.AddRequirement(new RequirementConfig("SeaMonsterTailRaw_RtD", 1));
                fishCooked13.CraftingStation = CraftingStations.Cauldron;
                ItemManager.Instance.AddRecipe(new CustomRecipe(fishCooked13));

                var fishCooked14 = new RecipeConfig();
                fishCooked14.Item = "SharkMeatCooked_RtD"; // Name of the item prefab to be crafted
                fishCooked14.AddRequirement(new RequirementConfig("SharkMeatRaw_RtD", 1));
                fishCooked14.CraftingStation = CraftingStations.Cauldron;
                ItemManager.Instance.AddRecipe(new CustomRecipe(fishCooked14));

                var fishWrap1 = new RecipeConfig();
                fishWrap1.Item = "FishWraps1_RtD"; // Name of the item prefab to be crafted
                fishWrap1.CraftingStation = CraftingStations.Cauldron;
                fishWrap1.AddRequirement(new RequirementConfig("FishCooked1_RtD", 1));
                fishWrap1.AddRequirement(new RequirementConfig("Mushroom", 1));
                fishWrap1.AddRequirement(new RequirementConfig("SeaWeed_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(fishWrap1));

                var fishWrap2 = new RecipeConfig();
                fishWrap2.Item = "FishWraps2_RtD"; // Name of the item prefab to be crafted
                fishWrap2.CraftingStation = CraftingStations.Cauldron;
                fishWrap2.AddRequirement(new RequirementConfig("FishCooked2_RtD", 1));
                fishWrap2.AddRequirement(new RequirementConfig("Dandelion", 1));
                fishWrap2.AddRequirement(new RequirementConfig("SeaWeed_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(fishWrap2));

                var fishWrap3 = new RecipeConfig();
                fishWrap3.Item = "FishWraps3_RtD"; // Name of the item prefab to be crafted
                fishWrap3.CraftingStation = CraftingStations.Cauldron;
                fishWrap3.AddRequirement(new RequirementConfig("FishCooked3_RtD", 1));
                fishWrap3.AddRequirement(new RequirementConfig("MushroomYellow", 1));
                fishWrap3.AddRequirement(new RequirementConfig("SeaWeed_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(fishWrap3));

                var fishWrap4 = new RecipeConfig();
                fishWrap4.Item = "FishWraps4_RtD"; // Name of the item prefab to be crafted
                fishWrap4.CraftingStation = CraftingStations.Cauldron;
                fishWrap4.AddRequirement(new RequirementConfig("FishCooked4_RtD", 1));
                fishWrap4.AddRequirement(new RequirementConfig("Turnip", 1));
                fishWrap4.AddRequirement(new RequirementConfig("SeaWeed_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(fishWrap4));

                var fishWrap5 = new RecipeConfig();
                fishWrap5.Item = "FishWraps5_RtD"; // Name of the item prefab to be crafted
                fishWrap5.CraftingStation = CraftingStations.Cauldron;
                fishWrap5.AddRequirement(new RequirementConfig("FishCooked5_RtD", 1));
                fishWrap5.AddRequirement(new RequirementConfig("Onion", 1));
                fishWrap5.AddRequirement(new RequirementConfig("SeaWeed_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(fishWrap5));

                var fishWrap6 = new RecipeConfig();
                fishWrap6.Item = "FishWraps6_RtD"; // Name of the item prefab to be crafted
                fishWrap6.CraftingStation = CraftingStations.Cauldron;
                fishWrap6.AddRequirement(new RequirementConfig("FishCooked6_RtD", 1));
                fishWrap6.AddRequirement(new RequirementConfig("Carrot", 1));
                fishWrap6.AddRequirement(new RequirementConfig("SeaWeed_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(fishWrap6));

                var fishWrap7 = new RecipeConfig();
                fishWrap7.Item = "FishWraps7_RtD"; // Name of the item prefab to be crafted
                fishWrap7.CraftingStation = CraftingStations.Cauldron;
                fishWrap7.AddRequirement(new RequirementConfig("FishCooked7_RtD", 1));
                fishWrap7.AddRequirement(new RequirementConfig("Barley", 1));
                fishWrap7.AddRequirement(new RequirementConfig("SeaWeed_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(fishWrap7));

                var fishWrap8 = new RecipeConfig();
                fishWrap8.Item = "FishWraps8_RtD"; // Name of the item prefab to be crafted
                fishWrap8.CraftingStation = CraftingStations.Cauldron;
                fishWrap8.AddRequirement(new RequirementConfig("FishCooked8_RtD", 1));
                fishWrap8.AddRequirement(new RequirementConfig("Thistle", 1));
                fishWrap8.AddRequirement(new RequirementConfig("SeaWeed_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(fishWrap8));

                var fishWrap9 = new RecipeConfig();
                fishWrap9.Item = "FishWraps9_RtD"; // Name of the item prefab to be crafted
                fishWrap9.CraftingStation = CraftingStations.Cauldron;
                fishWrap9.AddRequirement(new RequirementConfig("FishCooked9_RtD", 1));
                fishWrap9.AddRequirement(new RequirementConfig("Onion", 1));
                fishWrap9.AddRequirement(new RequirementConfig("SeaWeed_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(fishWrap9));

                var fishWrap10 = new RecipeConfig();
                fishWrap10.Item = "FishWraps10_RtD"; // Name of the item prefab to be crafted
                fishWrap10.CraftingStation = CraftingStations.Cauldron;
                fishWrap10.AddRequirement(new RequirementConfig("FishCooked10_RtD", 1));
                fishWrap10.AddRequirement(new RequirementConfig("Turnip", 1));
                fishWrap10.AddRequirement(new RequirementConfig("SeaWeed_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(fishWrap10));

                var fishWrap11 = new RecipeConfig();
                fishWrap11.Item = "FishWraps11_RtD"; // Name of the item prefab to be crafted
                fishWrap11.CraftingStation = CraftingStations.Cauldron;
                fishWrap11.AddRequirement(new RequirementConfig("FishCooked11_RtD", 1));
                fishWrap11.AddRequirement(new RequirementConfig("Dandelion", 1));
                fishWrap11.AddRequirement(new RequirementConfig("SeaWeed_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(fishWrap11));

                var fishWrap12 = new RecipeConfig();
                fishWrap12.Item = "FishWraps12_RtD"; // Name of the item prefab to be crafted
                fishWrap12.CraftingStation = CraftingStations.Cauldron;
                fishWrap12.AddRequirement(new RequirementConfig("FishCooked12_RtD", 1));
                fishWrap12.AddRequirement(new RequirementConfig("Carrot", 1));
                fishWrap12.AddRequirement(new RequirementConfig("SeaWeed_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(fishWrap12));

                var fishStew1 = new RecipeConfig();
                fishStew1.Item = "SharkMeatStew_RtD"; // Name of the item prefab to be crafted
                fishStew1.CraftingStation = CraftingStations.Cauldron;
                fishStew1.AddRequirement(new RequirementConfig("SharkMeatCooked_RtD", 1));
                fishStew1.AddRequirement(new RequirementConfig("SeaWeed_RtD", 1));
                fishStew1.AddRequirement(new RequirementConfig("Vegetable_Pumpkin_RtD", 1));
                fishStew1.AddRequirement(new RequirementConfig("MushroomMagecap", 3));
                ItemManager.Instance.AddRecipe(new CustomRecipe(fishStew1));

                var fishStew2 = new RecipeConfig();
                fishStew2.Item = "SeaMonsterStew_RtD"; // Name of the item prefab to be crafted
                fishStew2.CraftingStation = CraftingStations.Cauldron;
                fishStew2.AddRequirement(new RequirementConfig("SeaMonsterTailCooked_RtD", 1));
                fishStew2.AddRequirement(new RequirementConfig("SeaWeed_RtD", 1));
                fishStew2.AddRequirement(new RequirementConfig("Vegetable_Garlic_RtD", 1));
                fishStew2.AddRequirement(new RequirementConfig("MushroomJotunPuffs", 3));
                ItemManager.Instance.AddRecipe(new CustomRecipe(fishStew2));
                
                // Shrimp Stew
                
                var shrimpStew = new RecipeConfig
                {
                    Item = "Item_Stew_Shrimp_RtD",
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                shrimpStew.AddRequirement(new RequirementConfig("Item_Shrimp_RtD", 3));
                shrimpStew.AddRequirement(new RequirementConfig("Vegetable_RiceSack_RtD", 1));
                shrimpStew.AddRequirement(new RequirementConfig("Vegetable_Chantenay_RtD", 3));
                shrimpStew.AddRequirement(new RequirementConfig("SeaWeed_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(shrimpStew));
                
                // Shrimp & Rice Bowl
                
                var shrimpBowl = new RecipeConfig
                {
                    Item = "Item_Rice_Shrimp_RtD ",
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                shrimpBowl.AddRequirement(new RequirementConfig("Item_Shrimp_RtD", 3));
                shrimpBowl.AddRequirement(new RequirementConfig("Vegetable_RiceSack_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(shrimpBowl));
                
                // Shrimp Sushi
                
                var shrimpSushi = new RecipeConfig
                {
                    Item = "Item_Sushi_Shrimp_RtD",
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                shrimpSushi.AddRequirement(new RequirementConfig("Item_Shrimp_RtD", 3));
                shrimpSushi.AddRequirement(new RequirementConfig("Vegetable_RiceSack_RtD", 1));
                shrimpSushi.AddRequirement(new RequirementConfig("SeaWeed_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(shrimpSushi));
                
                // Tofu
                
                var tofu = new RecipeConfig
                {
                    Item = "Item_Tofu_RtD",
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                tofu.AddRequirement(new RequirementConfig("Vegetable_RiceSack_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(tofu));
                
                // Tomato Soup
                
                var tomatoSoup = new RecipeConfig
                {
                    Item = "Item_Soup_Tomato_RtD",
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                tomatoSoup.AddRequirement(new RequirementConfig("Meat_Tuna_RtD", 1));
                tomatoSoup.AddRequirement(new RequirementConfig("Vegetable_Tomato_RtD", 3));
                tomatoSoup.AddRequirement(new RequirementConfig("Vegetable_Potato_RtD", 1));
                tomatoSoup.AddRequirement(new RequirementConfig("SeaWeed_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(tomatoSoup));
                
                // Tomato Stew
                
                var tomatoStew = new RecipeConfig
                {
                    Item = "Item_Stew_Tomato_RtD",
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                tomatoStew.AddRequirement(new RequirementConfig("Meat_Crab_RtD", 1));
                tomatoStew.AddRequirement(new RequirementConfig("Vegetable_Tomato_RtD", 1));
                tomatoStew.AddRequirement(new RequirementConfig("Vegetable_Potato_RtD", 3));
                tomatoStew.AddRequirement(new RequirementConfig("Carrot", 2));
                ItemManager.Instance.AddRecipe(new CustomRecipe(tomatoStew));
                
                // Sushi Bream
                
                var sushiBream = new RecipeConfig
                {
                    Item = "Item_Sushi_Bream_RtD",
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                sushiBream.AddRequirement(new RequirementConfig("Meat_Cod_RtD", 1));
                sushiBream.AddRequirement(new RequirementConfig("Vegetable_Cucumber_RtD", 1));
                sushiBream.AddRequirement(new RequirementConfig("SeaWeed_RtD", 1));
                sushiBream.AddRequirement(new RequirementConfig("Vegetable_RiceSack_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(sushiBream));
                
                // Sushi Ahi Tuna
                
                var sushiAhi = new RecipeConfig
                {
                    Item = "Item_Sushi_Tuna_RtD",
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                sushiAhi.AddRequirement(new RequirementConfig("Meat_Tuna_RtD", 1));
                sushiAhi.AddRequirement(new RequirementConfig("Vegetable_Cucumber_RtD", 1));
                sushiAhi.AddRequirement(new RequirementConfig("SeaWeed_RtD", 1));
                sushiAhi.AddRequirement(new RequirementConfig("Vegetable_RiceSack_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(sushiAhi));
                
                // Sushi Cod
                
                var sushiCod = new RecipeConfig
                {
                    Item = "Item_Sushi_Cod_RtD",
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                sushiCod.AddRequirement(new RequirementConfig("Meat_Cod_RtD", 1));
                sushiCod.AddRequirement(new RequirementConfig("Vegetable_Tomato_RtD", 1));
                sushiCod.AddRequirement(new RequirementConfig("SeaWeed_RtD", 1));
                sushiCod.AddRequirement(new RequirementConfig("Vegetable_RiceSack_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(sushiCod));
                
                // Miso Stew
                
                var misoStew = new RecipeConfig
                {
                    Item = "Item_Stew_Miso_RtD",
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                misoStew.AddRequirement(new RequirementConfig("Meat_Manta_RtD", 1));
                misoStew.AddRequirement(new RequirementConfig("Vegetable_Cabbage_RtD", 1));
                misoStew.AddRequirement(new RequirementConfig("Vegetable_RedBeet_RtD", 2));
                misoStew.AddRequirement(new RequirementConfig("SeaWeed_RtD", 3));
                ItemManager.Instance.AddRecipe(new CustomRecipe(misoStew));
                
                // Miso Soup
                
                var misoSoup = new RecipeConfig
                {
                    Item = "Item_Soup_Miso_RtD",
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                misoSoup.AddRequirement(new RequirementConfig("Meat_Squid_RtD", 1));
                misoSoup.AddRequirement(new RequirementConfig("Vegetable_Cabbage_RtD", 1));
                misoSoup.AddRequirement(new RequirementConfig("Vegetable_Radish_RtD", 1));
                misoSoup.AddRequirement(new RequirementConfig("SeaWeed_RtD", 3));
                ItemManager.Instance.AddRecipe(new CustomRecipe(misoSoup));
                
                // Moka Sushi
                
                var sushiMoka = new RecipeConfig
                {
                    Item = "Item_Sushi_Moka_RtD",
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                sushiMoka.AddRequirement(new RequirementConfig("Meat_Shark_RtD", 1));
                sushiMoka.AddRequirement(new RequirementConfig("Vegetable_RiceSack_RtD", 1));
                sushiMoka.AddRequirement(new RequirementConfig("SeaWeed_RtD", 3));
                ItemManager.Instance.AddRecipe(new CustomRecipe(sushiMoka));
                
                // Squid Sushi
                
                var sushiSquid = new RecipeConfig
                {
                    Item = "Item_Sushi_Squid_RtD",
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                sushiSquid.AddRequirement(new RequirementConfig("Meat_Squid_RtD", 1));
                sushiSquid.AddRequirement(new RequirementConfig("Vegetable_RiceSack_RtD", 1));
                sushiSquid.AddRequirement(new RequirementConfig("SeaWeed_RtD", 3));
                sushiSquid.AddRequirement(new RequirementConfig("Vegetable_Radish_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(sushiSquid));
                
                // Whale Sushi
                
                var sushiWhale = new RecipeConfig
                {
                    Item = "Item_Sushi_Whale_RtD",
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                sushiWhale.AddRequirement(new RequirementConfig("Meat_Whale_RtD", 1));
                sushiWhale.AddRequirement(new RequirementConfig("Vegetable_RiceSack_RtD", 1));
                sushiWhale.AddRequirement(new RequirementConfig("SeaWeed_RtD", 3));
                ItemManager.Instance.AddRecipe(new CustomRecipe(sushiWhale));
                
                // Mushroom Stew
                
                var stewWhale = new RecipeConfig
                {
                    Item = "Item_Stew_Mushroom_RtD",
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                stewWhale.AddRequirement(new RequirementConfig("Meat_Whale_RtD", 1));
                stewWhale.AddRequirement(new RequirementConfig("Vegetable_Chantenay_RtD", 1));
                stewWhale.AddRequirement(new RequirementConfig("Mushroom", 3));
                stewWhale.AddRequirement(new RequirementConfig("Onion", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(stewWhale));
                
                // Mushroom Soup
                
                var soupShark = new RecipeConfig
                {
                    Item = "Item_Soup_Mushroom_RtD",
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                soupShark.AddRequirement(new RequirementConfig("Meat_Shark_RtD", 1));
                soupShark.AddRequirement(new RequirementConfig("Vegetable_RedBeet_RtD", 1));
                soupShark.AddRequirement(new RequirementConfig("Mushroom", 3));
                soupShark.AddRequirement(new RequirementConfig("Onion", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(soupShark));
                
                // Sushi Roll
                
                var sushiRoll = new RecipeConfig
                {
                    Item = "Item_Sushi_Roll_RtD",
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                sushiRoll.AddRequirement(new RequirementConfig("Meat_Shark_RtD", 1));
                sushiRoll.AddRequirement(new RequirementConfig("Onion", 1));
                sushiRoll.AddRequirement(new RequirementConfig("SeaWeed_RtD", 3));
                sushiRoll.AddRequirement(new RequirementConfig("Vegetable_Radish_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(sushiRoll));
                
                // Sushi Caviar
                
                var sushiCaviar = new RecipeConfig
                {
                    Item = "Item_Sushi_Caviar_RtD",
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                sushiCaviar.AddRequirement(new RequirementConfig("Meat_Tuna_RtD", 1));
                sushiCaviar.AddRequirement(new RequirementConfig("Onion", 1));
                sushiCaviar.AddRequirement(new RequirementConfig("SeaWeed_RtD", 2));
                sushiCaviar.AddRequirement(new RequirementConfig("Vegetable_Cucumber_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(sushiCaviar));
                
                // Cereal
                
                var cereal = new RecipeConfig
                {
                    Item = "Item_Cereal_RtD",
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                cereal.AddRequirement(new RequirementConfig("Vegetable_Wheat_RtD", 5));
                ItemManager.Instance.AddRecipe(new CustomRecipe(cereal));
                
                // Cereal Stew
                
                var cerealStew = new RecipeConfig
                {
                    Item = "Item_Stew_Cereal_RtD",
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                cerealStew.AddRequirement(new RequirementConfig("Vegetable_Wheat_RtD", 5));
                cerealStew.AddRequirement(new RequirementConfig("Barley", 2));
                ItemManager.Instance.AddRecipe(new CustomRecipe(cerealStew));
                
                // Soup Corn
                
                var soupCorn = new RecipeConfig
                {
                    Item = "Item_Soup_Corn_RtD",
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                soupCorn.AddRequirement(new RequirementConfig("Vegetable_Corn_RtD", 3));
                soupCorn.AddRequirement(new RequirementConfig("Meat_Marlin_RtD", 1));
                soupCorn.AddRequirement(new RequirementConfig("Vegetable_Onion_RtD", 2));
                soupCorn.AddRequirement(new RequirementConfig("Vegetable_Tomato_RtD", 2));
                ItemManager.Instance.AddRecipe(new CustomRecipe(soupCorn));
                
                // Stew Corn
                
                var stewCorn = new RecipeConfig
                {
                    Item = "Item_Stew_Corn_RtD",
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                stewCorn.AddRequirement(new RequirementConfig("Vegetable_Corn_RtD", 3));
                stewCorn.AddRequirement(new RequirementConfig("Meat_Marlin_RtD", 1));
                stewCorn.AddRequirement(new RequirementConfig("Vegetable_Onion_RtD", 2));
                stewCorn.AddRequirement(new RequirementConfig("Vegetable_Cabbage_RtD", 2));
                ItemManager.Instance.AddRecipe(new CustomRecipe(stewCorn));
                
                // Sushi Marlin
                
                var sushiMarlin = new RecipeConfig
                {
                    Item = "Item_Sushi_Marlin_RtD",
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                sushiMarlin.AddRequirement(new RequirementConfig("SeaWeed_RtD", 3));
                sushiMarlin.AddRequirement(new RequirementConfig("Meat_Marlin_RtD", 1));
                sushiMarlin.AddRequirement(new RequirementConfig("Vegetable_Cucumber_RtD", 1));
                sushiMarlin.AddRequirement(new RequirementConfig("Vegetable_Wheat_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(sushiMarlin));
                
                // Sushi Urchin
                
                var sushiUrchin = new RecipeConfig
                {
                    Item = "Item_Sushi_Urchin_RtD",
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                sushiUrchin.AddRequirement(new RequirementConfig("SeaWeed_RtD", 3));
                sushiUrchin.AddRequirement(new RequirementConfig("Meat_Marlin_RtD", 1));
                sushiUrchin.AddRequirement(new RequirementConfig("Vegetable_Cucumber_RtD", 1));
                sushiUrchin.AddRequirement(new RequirementConfig("Vegetable_Wheat_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(sushiUrchin));
                
                // Soup Ramen
                var soupRamen = new RecipeConfig
                {
                    Item = "Item_Ramen_RtD",
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                soupRamen.AddRequirement(new RequirementConfig("Vegetable_Onion_RtD", 1));
                soupRamen.AddRequirement(new RequirementConfig("Vegetable_Wheat_RtD", 3));
                soupRamen.AddRequirement(new RequirementConfig("ChickenEgg", 1));
                soupRamen.AddRequirement(new RequirementConfig("Vegetable_Garlic_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(soupRamen));
                
                // Soup Pumpkin
                var soupPumpkin = new RecipeConfig
                {
                    Item = "Item_Soup_Pumpkin_RtD",
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                soupPumpkin.AddRequirement(new RequirementConfig("Vegetable_Pumpkin_RtD", 1));
                soupPumpkin.AddRequirement(new RequirementConfig("Vegetable_Tomato_RtD", 3));
                soupPumpkin.AddRequirement(new RequirementConfig("SharkMeatCooked_RtD", 1));
                soupPumpkin.AddRequirement(new RequirementConfig("Vegetable_Garlic_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(soupPumpkin));
                
                // Stew Pumpkin
                var stewPumpkin = new RecipeConfig
                {
                    Item = "Item_Stew_Pumpkin_RtD",
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                stewPumpkin.AddRequirement(new RequirementConfig("Vegetable_Pumpkin_RtD", 1));
                stewPumpkin.AddRequirement(new RequirementConfig("Vegetable_Potato_RtD", 3));
                stewPumpkin.AddRequirement(new RequirementConfig("SharkMeatCooked_RtD", 1));
                stewPumpkin.AddRequirement(new RequirementConfig("Vegetable_Garlic_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(stewPumpkin));
                
                // Rice Pudding
                var ricePudding = new RecipeConfig
                {
                    Item = "Item_Stew_RicePudding_RtD",
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                ricePudding.AddRequirement(new RequirementConfig("Vegetable_Watermelon_RtD", 2));
                ricePudding.AddRequirement(new RequirementConfig("Vegetable_RiceSack_RtD", 3));
                ItemManager.Instance.AddRecipe(new CustomRecipe(ricePudding));
                
                // Sushi Unesu
                var sushiUnesu = new RecipeConfig
                {
                    Item = "Item_Sushi_Unesu_RtD",
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                sushiUnesu.AddRequirement(new RequirementConfig("Vegetable_Watermelon_RtD", 2));
                sushiUnesu.AddRequirement(new RequirementConfig("SeaWeed_RtD", 3));
                sushiUnesu.AddRequirement(new RequirementConfig("Meat_Whale_RtD", 2));
                sushiUnesu.AddRequirement(new RequirementConfig("Meat_Shark_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(sushiUnesu));
                
// Seed Cabbage
                var seedCabbage = new RecipeConfig
                {
                    Item = "Seed_Cabbage_RtD",
                    Amount = 3,
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                seedCabbage.AddRequirement(new RequirementConfig("Vegetable_Cabbage_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(seedCabbage));
                
                // Seed Chantenay
                var seedChantenay = new RecipeConfig
                {
                    Item = "Seed_Chantenay_RtD",
                    Amount = 3,
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                seedChantenay.AddRequirement(new RequirementConfig("Vegetable_Chantenay_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(seedChantenay));
                
                // Seed Corn
                var seedCorn = new RecipeConfig
                {
                    Item = "Seed_Corn_RtD",
                    Amount = 3,
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                seedCorn.AddRequirement(new RequirementConfig("Vegetable_Corn_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(seedCorn));
                
                // Seed Cucumber
                var seedCucumber = new RecipeConfig
                {
                    Item = "Seed_Cucumber_RtD",
                    Amount = 3,
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                seedCucumber.AddRequirement(new RequirementConfig("Vegetable_Cucumber_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(seedCucumber));
                
                // Seed Garlic
                var seedGarlic = new RecipeConfig
                {
                    Item = "Seed_Garlic_RtD",
                    Amount = 3,
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                seedGarlic.AddRequirement(new RequirementConfig("Vegetable_Garlic_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(seedGarlic));
                
                // Seed Onion
                var seedOnion = new RecipeConfig
                {
                    Item = "Seed_Onion_RtD",
                    Amount = 3,
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                seedOnion.AddRequirement(new RequirementConfig("Vegetable_Onion_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(seedOnion));
                
                // Seed Potato
                var seedPotato = new RecipeConfig
                {
                    Item = "Seed_Potato_RtD",
                    Amount = 3,
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                seedPotato.AddRequirement(new RequirementConfig("Vegetable_Potato_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(seedPotato));
                
                // Seed Pumpkin
                var seedPumpkin = new RecipeConfig
                {
                    Item = "Seed_Pumpkin_RtD",
                    Amount = 3,
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                seedPumpkin.AddRequirement(new RequirementConfig("Vegetable_Pumpkin_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(seedPumpkin));
                
                // Seed Radish
                var seedRadish = new RecipeConfig
                {
                    Item = "Seed_Radish_RtD",
                    Amount = 3,
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                seedRadish.AddRequirement(new RequirementConfig("Vegetable_Radish_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(seedRadish));
                
                // Seed Redbeet
                var seedRedbeet = new RecipeConfig
                {
                    Item = "Seed_RedBeet_RtD",
                    Amount = 3,
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                seedRedbeet.AddRequirement(new RequirementConfig("Vegetable_RedBeet_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(seedRedbeet));
                
                // Seed Rice
                var seedRice = new RecipeConfig
                {
                    Item = "Seed_Rice_RtD",
                    Amount = 3,
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                seedRice.AddRequirement(new RequirementConfig("Vegetable_RiceSack_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(seedRice));
                
                // Seed Tomato
                var seedTomato = new RecipeConfig
                {
                    Item = "Seed_Tomato_RtD",
                    Amount = 3,
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                seedTomato.AddRequirement(new RequirementConfig("Vegetable_Tomato_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(seedTomato));
                
                // Seed Watermelon
                var seedWatermelon = new RecipeConfig
                {
                    Item = "Seed_Watermelon_RtD",
                    Amount = 3,
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                seedWatermelon.AddRequirement(new RequirementConfig("Vegetable_Watermelon_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(seedWatermelon));
                
                // Seed Wheat
                var seedWheat = new RecipeConfig
                {
                    Item = "Seed_Wheat_RtD",
                    Amount = 3,
                    CraftingStation = "Piece_Prep_Table_RtD",
                    MinStationLevel = 1                         
                };
                seedWheat.AddRequirement(new RequirementConfig("Vegetable_Wheat_RtD", 1));
                ItemManager.Instance.AddRecipe(new CustomRecipe(seedWheat));
            }
            catch (Exception arg)
            {
                Logger.LogWarning($"Exception caught while adding custom recipes: {arg}");
            }
        }
        
        private void AddSummons()
        {
            try
            {
                foreach (string text in SummonList)
                {
                    GameObject gameObject = MyAssets.LoadAsset<GameObject>(text);
                    bool flag = gameObject != null;
                    if (flag)
                    {
                        CustomCreature customCreature = new CustomCreature(gameObject, true);
                        CreatureManager.Instance.AddCreature(customCreature);
                        bool value = LoggingEnable.Value;
                        if (value)
                        {
                            base.Logger.LogMessage("Added: " + text + " to the Object database");
                        }
                    }
                    else
                    {
                        base.Logger.LogMessage("Failed to add: " + text + " to the object database");
                    }
                }
            }
            catch (Exception arg)
            {
                base.Logger.LogWarning(string.Format("Exception caught while adding prefabs: {0}", arg));
            }
        }
        
        private void MeadowsPlantConfig()
        {
            try
            {
                foreach (string prefabName in MeadowsPlantList)
                {
                    GameObject prefab = MyAssets.LoadAsset<GameObject>(prefabName);
                    if (prefab != null)
                    {
                        ZoneManager.Instance.AddCustomVegetation(new CustomVegetation(prefab, true, MeadowsPlantValues));
                        if (LoggingEnable.Value) { Logger.LogMessage("Added Vegetation: " + prefabName); }
                    }
                    else
                    {
                        Logger.LogMessage("Failed to add: " + prefabName + " to the object database");
                    }
                }
            }
            catch (Exception arg)
            {
                Logger.LogWarning($"Exception caught while adding vegetation: {arg}");
            }
        }
        
        private void BlackForestPlantConfig()
        {
            try
            {
                foreach (string prefabName in BlackForestPlantList)
                {
                    GameObject prefab = MyAssets.LoadAsset<GameObject>(prefabName);
                    if (prefab != null)
                    {
                        ZoneManager.Instance.AddCustomVegetation(new CustomVegetation(prefab, true, BlackForestPlantValues));
                        if (LoggingEnable.Value) { Logger.LogMessage("Added Vegetation: " + prefabName); }
                    }
                    else
                    {
                        Logger.LogMessage("Failed to add: " + prefabName + " to the object database");
                    }
                }
            }
            catch (Exception arg)
            {
                Logger.LogWarning($"Exception caught while adding vegetation: {arg}");
            }
        }
        
        private void SwampPlantConfig()
        {
            try
            {
                foreach (string prefabName in SwampPlantList)
                {
                    GameObject prefab = MyAssets.LoadAsset<GameObject>(prefabName);
                    if (prefab != null)
                    {
                        ZoneManager.Instance.AddCustomVegetation(new CustomVegetation(prefab, true, SwampPlantValues));
                        if (LoggingEnable.Value) { Logger.LogMessage("Added Vegetation: " + prefabName); }
                    }
                    else
                    {
                        Logger.LogMessage("Failed to add: " + prefabName + " to the object database");
                    }
                }
            }
            catch (Exception arg)
            {
                Logger.LogWarning($"Exception caught while adding vegetation: {arg}");
            }
        }
        
        private void PlainsPlantConfig()
        {
            try
            {
                foreach (string prefabName in PlainsPlantList)
                {
                    GameObject prefab = MyAssets.LoadAsset<GameObject>(prefabName);
                    if (prefab != null)
                    {
                        ZoneManager.Instance.AddCustomVegetation(new CustomVegetation(prefab, true, PlainsPlantValues));
                        if (LoggingEnable.Value) { Logger.LogMessage("Added Vegetation: " + prefabName); }
                    }
                    else
                    {
                        Logger.LogMessage("Failed to add: " + prefabName + " to the object database");
                    }
                }
            }
            catch (Exception arg)
            {
                Logger.LogWarning($"Exception caught while adding vegetation: {arg}");
            }
        }
        
        private void MistlandsPlantConfig()
        {
            try
            {
                foreach (string prefabName in MistlandsPlantList)
                {
                    GameObject prefab = MyAssets.LoadAsset<GameObject>(prefabName);
                    if (prefab != null)
                    {
                        ZoneManager.Instance.AddCustomVegetation(new CustomVegetation(prefab, true, MistlandsPlantValues));
                        if (LoggingEnable.Value) { Logger.LogMessage("Added Vegetation: " + prefabName); }
                    }
                    else
                    {
                        Logger.LogMessage("Failed to add: " + prefabName + " to the object database");
                    }
                }
            }
            catch (Exception arg)
            {
                Logger.LogWarning($"Exception caught while adding vegetation: {arg}");
            }
        }
        
        private void OceanPlantConfig()
        {
            try
            {
                foreach (string prefabName in OceanPlantList)
                {
                    GameObject prefab = MyAssets.LoadAsset<GameObject>(prefabName);
                    if (prefab != null)
                    {
                        ZoneManager.Instance.AddCustomVegetation(new CustomVegetation(prefab, true, OceanPlantValues));
                        if (LoggingEnable.Value) { Logger.LogMessage("Added Vegetation: " + prefabName); }
                    }
                    else
                    {
                        Logger.LogMessage("Failed to add: " + prefabName + " to the object database");
                    }
                }
            }
            catch (Exception arg)
            {
                Logger.LogWarning($"Exception caught while adding vegetation: {arg}");
            }
        }
        
        private void OceanBushConfig()
        {
            try
            {
                foreach (string prefabName in OceanBushList)
                {
                    GameObject prefab = MyAssets.LoadAsset<GameObject>(prefabName);
                    if (prefab != null)
                    {
                        ZoneManager.Instance.AddCustomVegetation(new CustomVegetation(prefab, true, OceanBushValues));
                        if (LoggingEnable.Value) { Logger.LogMessage("Added Vegetation: " + prefabName); }
                    }
                    else
                    {
                        Logger.LogMessage("Failed to add: " + prefabName + " to the object database");
                    }
                }
            }
            catch (Exception arg)
            {
                Logger.LogWarning($"Exception caught while adding vegetation: {arg}");
            }
        }
        
        private void OceanClutterSeaWeedConfig()
        {
            try
            {
                foreach (string prefabName in OceanClutterSeaWeedList)
                {
                    GameObject prefab = MyAssets.LoadAsset<GameObject>(prefabName);
                    if (prefab != null)
                    {
                        ZoneManager.Instance.AddCustomClutter(new CustomClutter(prefab, true, OceanClutterSeaWeedValues));
                        if (LoggingEnable.Value) { Logger.LogMessage("Added Clutter: " + prefabName); }
                    }
                    else
                    {
                        Logger.LogMessage("Failed to add: " + prefabName + " to the object database");
                    }
                }
            }
            catch (Exception arg)
            {
                Logger.LogWarning($"Exception caught while adding clutter: {arg}");
            }
        }
        
        private void OceanClutterSeaShellConfig()
        {
            try
            {
                foreach (string prefabName in OceanClutterSeaShellList)
                {
                    GameObject prefab = MyAssets.LoadAsset<GameObject>(prefabName);
                    if (prefab != null)
                    {
                        ZoneManager.Instance.AddCustomClutter(new CustomClutter(prefab, true, OceanClutterSeaShellValues));
                        if (LoggingEnable.Value) { Logger.LogMessage("Added Clutter: " + prefabName); }
                    }
                    else
                    {
                        Logger.LogMessage("Failed to add: " + prefabName + " to the object database");
                    }
                }
            }
            catch (Exception arg)
            {
                Logger.LogWarning($"Exception caught while adding clutter: {arg}");
            }
        }
        
        private void AddShrimpTrapConfig()
        {
            try
            {
                foreach (string prefabName in OceanShrimpTrapList)
                {
                    GameObject prefab = MyAssets.LoadAsset<GameObject>(prefabName);
                    if (prefab != null)
                    {
                        ZoneManager.Instance.AddCustomVegetation(new CustomVegetation(prefab, true, OceanShrimpTrapValues));
                        if (LoggingEnable.Value) { Logger.LogMessage("Added Vegetation: " + prefabName); }
                    }
                    else
                    {
                        Logger.LogMessage("Failed to add: " + prefabName + " to the object database");
                    }
                }
            }
            catch (Exception arg)
            {
                Logger.LogWarning($"Exception caught while adding vegetation: {arg}");
            }
        }


        private void Harpooned()
        {
            try
            {
                foreach (string prefabNameSE99 in HarpoonedList)
                {
                    // You would change SE_Stats here, to what ever SE base you used, like SE_Infection_HS or SE_Smoke etc.
                    SE_Harpooned statusEffect99 = MyAssets.LoadAsset<SE_Harpooned>(prefabNameSE99);
                    if (statusEffect99 != null)
                    {
                        CustomStatusEffect customEffect99 = new(statusEffect99, true);
                        ItemManager.Instance.AddStatusEffect(customEffect99);
                    }
                }
            }
            catch (Exception arg)
            {
                Logger.LogWarning($"Exception caught while adding SE_Stats Effects: {arg}");
            }
        }

        private void SFX()
        {
            try
            {
                foreach (string prefabNameI1 in SFXList)
                {
                    GameObject prefabI1 = MyAssets.LoadAsset<GameObject>(prefabNameI1);
                    if (prefabI1 != null)
                    {
                        CustomPrefab customPrefab4 = new CustomPrefab(prefabI1, true);
                        PrefabManager.Instance.AddPrefab(customPrefab4);

                        if (LoggingEnable.Value) { Logger.LogMessage("Added: " + prefabNameI1 + " to the Object database"); }
                    }
                    else
                    {
                        Logger.LogMessage("Failed to add: " + prefabNameI1 + " to the object database");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogWarning($"Exception caught while adding prefabs: {ex}");
            }
        }

        private void FixSFX()
        {
            try
            {
                AudioSource mixerGroupSFX = PrefabManager.Cache.GetPrefab<AudioSource>("sfx_arrow_hit");

                foreach (string prefabName in SFXList)
                {
                    GameObject prefab = PrefabManager.Cache.GetPrefab<GameObject>(prefabName);
                    prefab.GetComponentInChildren<AudioSource>().outputAudioMixerGroup = mixerGroupSFX.outputAudioMixerGroup;

                    if (LoggingEnable.Value) { Logger.LogMessage("Audio Mixer set on: " + prefabName); }
                }
            }
            catch (Exception arg)
            {
                Logger.LogWarning($"Exception caught while fixing custom audio: {arg}");
            }
            finally
            {
                PrefabManager.OnPrefabsRegistered -= FixSFX;
            }
        }

        private void AddItems()
        {
            try
            {
                foreach (string prefabNames in ItemList)
                {
                    GameObject prefabs = MyAssets.LoadAsset<GameObject>(prefabNames);
                    if (prefabs != null)
                    {
                        CustomItem customPrefabs = new CustomItem(prefabs, true);
                        ItemManager.Instance.AddItem(customPrefabs);

                        if (LoggingEnable.Value) { Logger.LogMessage("Added: " + prefabNames + " to the Object database"); }
                    }
                    else
                    {
                        Logger.LogMessage("Failed to add: " + prefabNames + " to the object database");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogWarning($"Exception caught while adding prefabs: {ex}");
            }
        }

        private void AddPrefabs1()
        {
            try
            {
                foreach (string prefabNameItem1 in PrefabList)
                {
                    GameObject prefabItem1 = MyAssets.LoadAsset<GameObject>(prefabNameItem1);
                    if (prefabItem1 != null)
                    {
                        CustomPrefab customPrefabItem = new CustomPrefab(prefabItem1, true);
                        PrefabManager.Instance.AddPrefab(customPrefabItem);

                        if (LoggingEnable.Value) { Logger.LogMessage("Added: " + prefabNameItem1 + " to the Object database"); }
                    }
                    else
                    {
                        Logger.LogMessage("Failed to add: " + prefabNameItem1 + " to the object database");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogWarning($"Exception caught while adding prefabs: {ex}");
            }
        }

        private void AddBasicsStatusEffects()
        {
            try
            {
                foreach (string prefabNameSE1 in BasicStatusEffectList)
                {
                    // You would change SE_Stats here, to what ever SE base you used, like SE_Infection_HS or SE_Smoke etc.
                    SE_Stats statusEffect1 = MyAssets.LoadAsset<SE_Stats>(prefabNameSE1);
                    if (statusEffect1 != null)
                    {
                        CustomStatusEffect customEffect1 = new(statusEffect1, true);
                        ItemManager.Instance.AddStatusEffect(customEffect1);

                        if (LoggingEnable.Value) { Logger.LogMessage("Added: " + customEffect1 + " to the Object database"); }
                    }
                }
            }
            catch (Exception arg)
            {
                Logger.LogWarning($"Exception caught while adding SE_Stats Effects: {arg}");
            }
        }

        private void AddMonsters()
        {
            try
            {
                foreach (string prefabNameM1 in MonsterList)
                {
                    GameObject prefabM1 = MyAssets.LoadAsset<GameObject>(prefabNameM1);
                    if (prefabM1 != null)
                    {
                        CustomCreature customPrefabM1 = new CustomCreature(prefabM1, true);
                        CreatureManager.Instance.AddCreature(customPrefabM1);

                        if (LoggingEnable.Value) { Logger.LogMessage("Added: " + prefabNameM1 + " to the Object database"); }
                    }
                    else
                    {
                        Logger.LogMessage("Failed to add: " + prefabNameM1 + " to the object database");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogWarning($"Exception caught while adding prefabs: {ex}");
            }
        }
       
        private void AddPrepPieces()
        {
            try
            {
                foreach (string prefabName in PiecePrepList)
                {
                    GameObject prefab = MyAssets.LoadAsset<GameObject>(prefabName);
                    if (prefab != null)
                    {
                        PieceManager.Instance.AddPiece(new CustomPiece(prefab, true, PrepTableValues));
                        if (LoggingEnable.Value) { Logger.LogMessage("Added object: " + prefabName); }
                    }
                    else
                    {
                        Logger.LogMessage("Failed to add: " + prefabName + " to the object database");
                    }
                }
            }
            catch (Exception arg)
            {
                Logger.LogWarning($"Exception caught while adding object: {arg}");
            }
        }
        
        private void AddSaplingCabbage()
        {
            try
            {
                foreach (string prefabName in SaplingCabbageList)
                {
                    GameObject prefab = MyAssets.LoadAsset<GameObject>(prefabName);
                    if (prefab != null)
                    {
                        PieceManager.Instance.AddPiece(new CustomPiece(prefab, true, SaplingCabbageValues));
                        if (LoggingEnable.Value) { Logger.LogMessage("Added object: " + prefabName); }
                    }
                    else
                    {
                        Logger.LogMessage("Failed to add: " + prefabName + " to the object database");
                    }
                }
            }
            catch (Exception arg)
            {
                Logger.LogWarning($"Exception caught while adding object: {arg}");
            }
        }
        
        private void AddSaplingChantenay()
        {
            try
            {
                foreach (string prefabName in SaplingChantenayList)
                {
                    GameObject prefab = MyAssets.LoadAsset<GameObject>(prefabName);
                    if (prefab != null)
                    {
                        PieceManager.Instance.AddPiece(new CustomPiece(prefab, true, SaplingChantenayValues));
                        if (LoggingEnable.Value) { Logger.LogMessage("Added object: " + prefabName); }
                    }
                    else
                    {
                        Logger.LogMessage("Failed to add: " + prefabName + " to the object database");
                    }
                }
            }
            catch (Exception arg)
            {
                Logger.LogWarning($"Exception caught while adding object: {arg}");
            }
        }
        
        private void AddSaplingCorn()
        {
            try
            {
                foreach (string prefabName in SaplingCornList)
                {
                    GameObject prefab = MyAssets.LoadAsset<GameObject>(prefabName);
                    if (prefab != null)
                    {
                        PieceManager.Instance.AddPiece(new CustomPiece(prefab, true, SaplingCornValues));
                        if (LoggingEnable.Value) { Logger.LogMessage("Added object: " + prefabName); }
                    }
                    else
                    {
                        Logger.LogMessage("Failed to add: " + prefabName + " to the object database");
                    }
                }
            }
            catch (Exception arg)
            {
                Logger.LogWarning($"Exception caught while adding object: {arg}");
            }
        }
        
        private void AddSaplingCucumber()
        {
            try
            {
                foreach (string prefabName in SaplingCucumberList)
                {
                    GameObject prefab = MyAssets.LoadAsset<GameObject>(prefabName);
                    if (prefab != null)
                    {
                        PieceManager.Instance.AddPiece(new CustomPiece(prefab, true, SaplingCucumberValues));
                        if (LoggingEnable.Value) { Logger.LogMessage("Added object: " + prefabName); }
                    }
                    else
                    {
                        Logger.LogMessage("Failed to add: " + prefabName + " to the object database");
                    }
                }
            }
            catch (Exception arg)
            {
                Logger.LogWarning($"Exception caught while adding object: {arg}");
            }
        }
        
        private void AddSaplingGarlic()
        {
            try
            {
                foreach (string prefabName in SaplingGarlicList)
                {
                    GameObject prefab = MyAssets.LoadAsset<GameObject>(prefabName);
                    if (prefab != null)
                    {
                        PieceManager.Instance.AddPiece(new CustomPiece(prefab, true, SaplingGarlicValues));
                        if (LoggingEnable.Value) { Logger.LogMessage("Added object: " + prefabName); }
                    }
                    else
                    {
                        Logger.LogMessage("Failed to add: " + prefabName + " to the object database");
                    }
                }
            }
            catch (Exception arg)
            {
                Logger.LogWarning($"Exception caught while adding object: {arg}");
            }
        }
        
        private void AddSaplingOnion()
        {
            try
            {
                foreach (string prefabName in SaplingOnionList)
                {
                    GameObject prefab = MyAssets.LoadAsset<GameObject>(prefabName);
                    if (prefab != null)
                    {
                        PieceManager.Instance.AddPiece(new CustomPiece(prefab, true, SaplingOnionValues));
                        if (LoggingEnable.Value) { Logger.LogMessage("Added object: " + prefabName); }
                    }
                    else
                    {
                        Logger.LogMessage("Failed to add: " + prefabName + " to the object database");
                    }
                }
            }
            catch (Exception arg)
            {
                Logger.LogWarning($"Exception caught while adding object: {arg}");
            }
        }
        
        private void AddSaplingPotato()
        {
            try
            {
                foreach (string prefabName in SaplingPotatoList)
                {
                    GameObject prefab = MyAssets.LoadAsset<GameObject>(prefabName);
                    if (prefab != null)
                    {
                        PieceManager.Instance.AddPiece(new CustomPiece(prefab, true, SaplingPotatoValues));
                        if (LoggingEnable.Value) { Logger.LogMessage("Added object: " + prefabName); }
                    }
                    else
                    {
                        Logger.LogMessage("Failed to add: " + prefabName + " to the object database");
                    }
                }
            }
            catch (Exception arg)
            {
                Logger.LogWarning($"Exception caught while adding object: {arg}");
            }
        }
        
        private void AddSaplingPumpkin()
        {
            try
            {
                foreach (string prefabName in SaplingPumpkinList)
                {
                    GameObject prefab = MyAssets.LoadAsset<GameObject>(prefabName);
                    if (prefab != null)
                    {
                        PieceManager.Instance.AddPiece(new CustomPiece(prefab, true, SaplingPumpkinValues));
                        if (LoggingEnable.Value) { Logger.LogMessage("Added object: " + prefabName); }
                    }
                    else
                    {
                        Logger.LogMessage("Failed to add: " + prefabName + " to the object database");
                    }
                }
            }
            catch (Exception arg)
            {
                Logger.LogWarning($"Exception caught while adding object: {arg}");
            }
        }
        
        private void AddSaplingRadish()
        {
            try
            {
                foreach (string prefabName in SaplingRadishList)
                {
                    GameObject prefab = MyAssets.LoadAsset<GameObject>(prefabName);
                    if (prefab != null)
                    {
                        PieceManager.Instance.AddPiece(new CustomPiece(prefab, true, SaplingRadishValues));
                        if (LoggingEnable.Value) { Logger.LogMessage("Added object: " + prefabName); }
                    }
                    else
                    {
                        Logger.LogMessage("Failed to add: " + prefabName + " to the object database");
                    }
                }
            }
            catch (Exception arg)
            {
                Logger.LogWarning($"Exception caught while adding object: {arg}");
            }
        }
        
        private void AddSaplingRedbeet()
        {
            try
            {
                foreach (string prefabName in SaplingRedbeetList)
                {
                    GameObject prefab = MyAssets.LoadAsset<GameObject>(prefabName);
                    if (prefab != null)
                    {
                        PieceManager.Instance.AddPiece(new CustomPiece(prefab, true, SaplingRedbeetValues));
                        if (LoggingEnable.Value) { Logger.LogMessage("Added object: " + prefabName); }
                    }
                    else
                    {
                        Logger.LogMessage("Failed to add: " + prefabName + " to the object database");
                    }
                }
            }
            catch (Exception arg)
            {
                Logger.LogWarning($"Exception caught while adding object: {arg}");
            }
        }
        
        private void AddSaplingRice()
        {
            try
            {
                foreach (string prefabName in SaplingRiceList)
                {
                    GameObject prefab = MyAssets.LoadAsset<GameObject>(prefabName);
                    if (prefab != null)
                    {
                        PieceManager.Instance.AddPiece(new CustomPiece(prefab, true, SaplingRiceValues));
                        if (LoggingEnable.Value) { Logger.LogMessage("Added object: " + prefabName); }
                    }
                    else
                    {
                        Logger.LogMessage("Failed to add: " + prefabName + " to the object database");
                    }
                }
            }
            catch (Exception arg)
            {
                Logger.LogWarning($"Exception caught while adding object: {arg}");
            }
        }
        
        private void AddSaplingTomato()
        {
            try
            {
                foreach (string prefabName in SaplingTomatoList)
                {
                    GameObject prefab = MyAssets.LoadAsset<GameObject>(prefabName);
                    if (prefab != null)
                    {
                        PieceManager.Instance.AddPiece(new CustomPiece(prefab, true, SaplingTomatoValues));
                        if (LoggingEnable.Value) { Logger.LogMessage("Added object: " + prefabName); }
                    }
                    else
                    {
                        Logger.LogMessage("Failed to add: " + prefabName + " to the object database");
                    }
                }
            }
            catch (Exception arg)
            {
                Logger.LogWarning($"Exception caught while adding object: {arg}");
            }
        }
        
        private void AddSaplingWatermelon()
        {
            try
            {
                foreach (string prefabName in SaplingWatermelonList)
                {
                    GameObject prefab = MyAssets.LoadAsset<GameObject>(prefabName);
                    if (prefab != null)
                    {
                        PieceManager.Instance.AddPiece(new CustomPiece(prefab, true, SaplingWatermelonValues));
                        if (LoggingEnable.Value) { Logger.LogMessage("Added object: " + prefabName); }
                    }
                    else
                    {
                        Logger.LogMessage("Failed to add: " + prefabName + " to the object database");
                    }
                }
            }
            catch (Exception arg)
            {
                Logger.LogWarning($"Exception caught while adding object: {arg}");
            }
        }
        
        private void AddSaplingWheat()
        {
            try
            {
                foreach (string prefabName in SaplingWheatList)
                {
                    GameObject prefab = MyAssets.LoadAsset<GameObject>(prefabName);
                    if (prefab != null)
                    {
                        PieceManager.Instance.AddPiece(new CustomPiece(prefab, true, SaplingWheatValues));
                        if (LoggingEnable.Value) { Logger.LogMessage("Added object: " + prefabName); }
                    }
                    else
                    {
                        Logger.LogMessage("Failed to add: " + prefabName + " to the object database");
                    }
                }
            }
            catch (Exception arg)
            {
                Logger.LogWarning($"Exception caught while adding object: {arg}");
            }
        }

        private void EditBoats()
        {
            try
            {
                var boat1 = PrefabManager.Cache.GetPrefab<WearNTear>("Raft");
                boat1.m_health = 500;

                var boat2 = PrefabManager.Cache.GetPrefab<WearNTear>("Karve");
                boat2.m_health = 1000;

                var boat3 = PrefabManager.Cache.GetPrefab<WearNTear>("VikingShip");
                boat3.m_health = 1500;
            }
            catch (Exception ex)
            {
                Logger.LogWarning($"Exception caught while editing boats: {ex}");
            }
            finally
            {
                PrefabManager.OnVanillaPrefabsAvailable -= EditBoats;
            }
        }

    }
}
