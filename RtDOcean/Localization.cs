using System.Collections.Generic;
using BepInEx;
using Jotunn.Entities;
using Jotunn.Managers;
using UnityEngine;

namespace RtDOcean                        
{
    internal partial class OceanPlugin : BaseUnityPlugin   
    {
        private CustomLocalization Localization;
        
        public void JSONSupport()
        {
            TextAsset[] textAssets = MyAssets.LoadAllAssets<TextAsset>();
            foreach (var textAsset in textAssets)
            {
                var lang = textAsset.name.Replace("_RtDOcean.json", null);
                Localization.AddJsonFile(lang, textAsset.ToString());
            }
        }
        
        public void Addlocalizations()
        {
 
            Localization = LocalizationManager.Instance.GetLocalization();
            Localization.AddTranslation("English", new Dictionary<string, string>
            {
                {"BoneFish_RtD", "Bone Fish"},
                {"BoneSquid_RtD", "Bone Squid"},
                {"LuminousLooker_RtD", "Luminous Looker"},
                {"MurkPod_RtD", "MurkPod"},
                {"Shark_RtD", "Megalodon"},
                {"CoralRock1_RtD", "Coral Rock"},
                {"CoralRock2_RtD", "Coral Rock"},
                {"CoralRock3_RtD", "Coral Rock"},
                {"CoralRock4_RtD", "Coral Rock"},
                {"CoralRock5_RtD", "Coral Rock"},
                {"CoralStone1_RtD", "Coral"},
                {"CoralStone2_RtD", "Coral"},
                {"CoralStone3_RtD", "Coral"},
                {"CoralStone4_RtD", "Coral"},
                {"CoralStone5_RtD", "Coral"},
                {"CoralStone6_RtD", "Coral"},
                {"CoralStone7_RtD", "Coral"},
                {"CoralStone8_RtD", "Coral"},
                {"CoralStone9_RtD", "Coral"},
                {"CoralStone10_RtD", "Coral"},
                {"CoralStone11_RtD", "Coral"},
                {"CoralStone12_RtD", "Coral"},
                {"CoralStone13_RtD", "Coral"},
                {"CoralStone14_RtD", "Coral"},
                {"CoralStone15_RtD", "Coral"},
                {"CoralStone16_RtD", "Coral"},
                {"CoralStone17_RtD", "Coral"},
                {"CoralStone18_RtD", "Coral"},
                {"CoralStone19_RtD", "Coral"},
                {"CoralStone20_RtD", "Coral"},
                {"CoralStone21_RtD", "Coral"},
                {"CoralStone22_RtD", "Coral"},
                {"CoralStone23_RtD", "Coral"},
                {"CoralStone24_RtD", "Coral"},
                {"CoralStone25_RtD", "Coral"},
                {"CoralStone26_RtD", "Coral"},
                {"CoralStone27_RtD", "Coral"},
                {"CoralStone28_RtD", "Coral"},
                {"CoralStone29_RtD", "Coral"},
                {"CoralStone30_RtD", "Coral"},
                {"Sponge1_RtD", "Marine Sponge"},
                {"Sponge2_RtD", "Marine Sponge"},
                {"Sponge3_RtD", "Marine Spongee"},
                {"Sponge4_RtD", "Marine Sponge"},
                {"Sponge5_RtD", "Marine Sponge"},
                {"Sponge6_RtD", "Marine Sponge"},
                {"Sponge7_RtD", "Marine Sponge"},
                {"Sponge8_RtD", "Marine Sponge"},
                {"Sponge9_RtD", "Marine Sponge"},
                {"Sponge10_RtD", "Marine Sponge"},
                {"Sponge11_RtD", "Marine Sponge"},
                {"CoralReef1_RtD", "Coral Reef"},
                {"CoralReef2_RtD", "Coral Reef"},
                {"CoralReef3_RtD", "Coral Reef"},
                {"CoralReef4_RtD", "Coral Reef"},
                {"CoralReef5_RtD", "Coral Reef"},
                {"CoralReef6_RtD", "Coral Reef"},
                {"CoralReef7_RtD", "Coral Reef"},
                {"CoralReef8_RtD", "Coral Reef"},
                {"CoralReef9_RtD", "Coral Reef"},
                {"Pickable_Coins_RtD", "Lost Treasure"},
                {"Pickable_Pearl_RtD", "Lost Treasure"},
                {"SeaWeed1_RtD", "SeaWeed"},
                {"SeaWeed2_RtD", "SeaWeed"},
                {"SeaWeed3_RtD", "SeaWeed"},
                {"SeaWeed4_RtD", "SeaWeed"},
                {"SeaWeed5_RtD", "SeaWeed"},
                {"SeaWeed6_RtD", "SeaWeed"},
                {"SeaWeed7_RtD", "SeaWeed"},
                {"SeaWeed8_RtD", "SeaWeed"},
                {"SeaWeed9_RtD", "SeaWeed"},
                {"SeaWeed10_RtD", "SeaWeed"},
                {"SeaWeed11_RtD", "SeaWeed"},
                {"SeaWeed12_RtD", "SeaWeed"},
                {"SeaWeed13_RtD", "SeaWeed"},
                {"SeaWeed14_RtD", "SeaWeed"},
                {"SeaWeed15_RtD", "SeaWeed"},
                {"SeaWeed16_RtD", "SeaWeed"},
                {"Belzor_RtD","Belzor"},
                {"CatFish_RtD","Mirfolk"},
                {"Neck_RtD","Sea Neck"},
                {"NeckSummon","Sea Neck Summon"},
                {"Reptile_RtD","Mirfolk Mutant"},
                {"Fairy4_RtD","Nature Fae"},
                {"Mirmaid_RtD","Margygr"},
                {"MineRock_Electrum_RtD","Electrum Rock"},
                {"SkullStone_text_RtD","The Jotunn here are neither living nor dead, they are suffering from eternal decay and cannot be saved."},
                
                
                // Wraps & RtDOceanFoods localization
                { "SeaWeed_RtD", "Seaweed" },
                { "SeaWeed_desc_RtD", "Fresh ocean-harvested seaweed, lightly salted by the sea breeze." },

                { "FishCooked1_RtD", "Cooked Perch" },
                { "FishCooked1_desc_RtD", "A flaky perch roasted to perfection over an open flame." },

                { "FishCooked2_RtD", "Cooked Pike" },
                { "FishCooked2_desc_RtD", "Firm pike meat grilled until tender and savory." },

                { "FishCooked3_RtD", "Cooked Tuna" },
                { "FishCooked3_desc_RtD", "Rich tuna steak seared for a hearty meal." },

                { "FishCooked4_RtD", "Cooked Tetra" },
                { "FishCooked4_desc_RtD", "Delicate tetra cooked to a light, satisfying finish." },

                { "FishCooked5_RtD", "Cooked Trollfish" },
                { "FishCooked5_desc_RtD", "A massive cut of trollfish, surprisingly flavorful." },

                { "FishCooked6_RtD", "Cooked Herring" },
                { "FishCooked6_desc_RtD", "Small but packed with bold ocean flavor." },

                { "FishCooked7_RtD", "Cooked Grouper" },
                { "FishCooked7_desc_RtD", "Thick grouper fillet roasted until golden brown." },

                { "FishCooked8_RtD", "Cooked Cod" },
                { "FishCooked8_desc_RtD", "A classic cod filet with a mild, buttery taste." },

                { "FishCooked9_RtD", "Cooked Angler" },
                { "FishCooked9_desc_RtD", "Deep-sea angler meat with a rich, dense texture." },

                { "FishCooked10_RtD", "Cooked Salmon" },
                { "FishCooked10_desc_RtD", "Juicy salmon with a crisped outer layer." },

                { "FishCooked11_RtD", "Cooked Magmafish" },
                { "FishCooked11_desc_RtD", "Blazing-hot magmafish meat, infused with volcanic heat." },

                { "FishCooked12_RtD", "Cooked Pufferfish" },
                { "FishCooked12_desc_RtD", "Carefully prepared pufferfish, tender and rare." },

                { "SeaMonsterTailCooked_RtD", "Cooked Sea Monster Tail" },
                { "SeaMonsterTailCooked_desc_RtD", "A massive tail steak carved from a legendary sea beast." },

                { "SeaMonsterTailRaw_RtD", "Raw Sea Monster Tail" },
                { "SeaMonsterTailRaw_desc_RtD", "Heavy and dripping with brine. Best cooked before eating." },

                { "SharkMeatCooked_RtD", "Cooked Shark Meat" },
                { "SharkMeatCooked_desc_RtD", "Dense shark meat grilled until smoky and rich." },

                { "SharkMeatRaw_RtD", "Raw Shark Meat" },
                { "SharkMeatRaw_desc_RtD", "Freshly cut shark meat. Needs proper cooking." },

                { "SeaMonsterStew_RtD", "Sea Monster Stew" },
                { "SeaMonsterStew_desc_RtD", "A hearty stew brewed from the depths of the ocean." },

                { "SharkMeatStew_RtD", "Shark Stew" },
                { "SharkMeatStew_desc_RtD", "Thick, savory shark stew with a bold ocean taste." },

                { "FishWraps1_RtD", "Perch Wrap" },
                { "FishWraps1_desc_RtD", "Grilled perch wrapped in warm flatbread." },

                { "FishWraps2_RtD", "Pike Wrap" },
                { "FishWraps2_desc_RtD", "Tender pike slices tucked inside fresh barley bread." },

                { "FishWraps3_RtD", "Tuna Wrap" },
                { "FishWraps3_desc_RtD", "Rich tuna paired with crisp greens in a soft wrap." },

                { "FishWraps4_RtD", "Tetra Wrap" },
                { "FishWraps4_desc_RtD", "Light tetra fillet folded into warm bread." },

                { "FishWraps5_RtD", "Trollfish Wrap" },
                { "FishWraps5_desc_RtD", "A giant trollfish portion wrapped for travel." },

                { "FishWraps6_RtD", "Herring Wrap" },
                { "FishWraps6_desc_RtD", "Savory herring blended with herbs in a hearty wrap." },

                { "FishWraps7_RtD", "Grouper Wrap" },
                { "FishWraps7_desc_RtD", "Thick grouper chunks wrapped with crunchy barley." },

                { "FishWraps8_RtD", "Cod Wrap" },
                { "FishWraps8_desc_RtD", "Classic cod wrapped with simple, wholesome ingredients." },

                { "FishWraps9_RtD", "Angler Wrap" },
                { "FishWraps9_desc_RtD", "Dense angler meat folded into a satisfying wrap." },

                { "FishWraps10_RtD", "Salmon Wrap" },
                { "FishWraps10_desc_RtD", "Juicy salmon paired with warm barley bread." },

                { "FishWraps11_RtD", "Magma Wrap" },
                { "FishWraps11_desc_RtD", "Spicy magmafish wrapped in fire-baked flatbread." },

                { "FishWraps12_RtD", "Puffer Wrap" },
                { "FishWraps12_desc_RtD", "Carefully prepared pufferfish served in a soft wrap." },
                
                // New stuff & better localization code
                
                // Cabbage
                {"Seed_Cabbage_RtD","Cabbage Seed"},
                {"Seed_Cabbage_desc_RtD","A viable seed. Plant in cultivated soil and allow time for it to grow."},
                {"Vegetable_Cabbage_RtD","Cabbage"},
                {"Vegetable_Cabbage_desc_RtD","A fully matured head of cabbage, harvested at peak freshness. Nutritious and ready for cooking."},
                {"Sapling_Cabbage_RtD","Cabbage Sprout"},
                {"Sapling_Cabbage_desc_RtD","A young cabbage plant in its early growth stage. With proper care and time, it will mature into a full head of cabbage ready for harvest."},
                // Chantenay
                {"Sapling_Chantenay_RtD","Chantenay Sprout"},
                {"Sapling_Chantenay_desc_RtD","A young Chantenay carrot sprout just beginning to take root. With time and care, it will grow into a sweet, sturdy harvest."},
                {"Seed_Chantenay_RtD","Chantenay Seed"},
                {"Seed_Chantenay_desc_RtD","A viable seed. Plant in cultivated soil and allow time for it to grow."},
                {"Vegetable_Chantenay_RtD","Chantenay"},
                {"Vegetable_Chantenay_desc_RtD","A mature Chantenay carrot pulled fresh from the soil. Compact, sturdy, and prized for its sweetness."},
                // Corn
                {"Sapling_Corn_RtD","Corn Sprout"},
                {"Sapling_Corn_desc_RtD","A young corn sprout just emerging from the soil. With time and care, it will grow into a tall stalk bearing golden ears."},
                {"Seed_Corn_RtD","Corn Seed"},
                {"Seed_Corn_desc_RtD","A viable seed. Plant in cultivated soil and allow time for it to grow."},
                {"Vegetable_Corn_RtD","Corn"},
                {"Vegetable_Corn_desc_RtD","A mature ear of corn harvested from a tall stalk. Sweet, firm, and ready for cooking."},
                // Cucumber
                {"Sapling_Cucumber_RtD","Cucumber Sprout"},
                {"Sapling_Cucumber_desc_RtD","A young cucumber sapling just beginning to vine. With time and care, it will produce crisp green cucumbers."},
                {"Seed_Cucumber_RtD","Cucumber Seed"},
                {"Seed_Cucumber_desc_RtD","A viable cucumber seed. Plant in cultivated soil and allow time for it to mature into a harvestable plant."},
                {"Vegetable_Cucumber_RtD","Cucumber"},
                {"Vegetable_Cucumber_desc_RtD","A mature cucumber harvested from the vine. Firm, cool, and ready to eat or cook."},
                // Garlic
                { "Sapling_Garlic_RtD",        "Garlic Sprout" },
                { "Sapling_Garlic_desc_RtD",   "A tender garlic sprout pushing thin leaves toward the sun while its bulb strengthens underground." },
                { "Seed_Garlic_RtD",           "Garlic Seed" },
                { "Seed_Garlic_desc_RtD",      "A single garlic clove set aside for planting. Buried in good soil, it will take root and multiply beneath the earth." },
                { "Vegetable_Garlic_RtD",      "Garlic" },
                { "Vegetable_Garlic_desc_RtD", "A firm bulb of garlic, layered in pale skin and rich with sharp aroma. A small ingredient with powerful flavor." },
                // Wild Onion
                { "Sapling_Onion_RtD",        "Wild Onion Sprout" },
                { "Sapling_Onion_desc_RtD",   "A thin wild onion sprout rising from the earth, its bulb slowly forming beneath the soil." },
                { "Seed_Onion_RtD",           "Wild Onion Seed" },
                { "Seed_Onion_desc_RtD",      "A tiny wild onion seed, resilient and untamed. Buried in fertile soil, it will take root and thrive." },
                { "Vegetable_Onion_RtD",      "Wild Onion" },
                { "Vegetable_Onion_desc_RtD", "A hardy wild onion pulled from the soil, its strong aroma and bold taste suited for hearty meals." },
                // Potato
                { "Sapling_Potato_RtD",        "Potato Sprout" },
                { "Sapling_Potato_desc_RtD",   "A sturdy potato sprout stretching toward the sun while its roots swell with growing tubers below." },
                { "Seed_Potato_RtD",           "Potato Seed" },
                { "Seed_Potato_desc_RtD",      "A seed potato prepared for planting. With proper soil and time, it will grow into a cluster of hearty tubers." },
                { "Vegetable_Potato_RtD",      "Potato" },
                { "Vegetable_Potato_desc_RtD", "A freshly harvested potato, dense and filling. A reliable staple for many meals." },
                // Pumpkin
                { "Sapling_Pumpkin_RtD",        "Pumpkin Sprout" },
                { "Sapling_Pumpkin_desc_RtD",   "A young pumpkin plant beginning to form broad leaves and creeping vines. With time, it will produce a mature pumpkin." },
                { "Seed_Pumpkin_RtD",           "Pumpkin Seed" },
                { "Seed_Pumpkin_desc_RtD",      "A small pumpkin seed filled with quiet promise. Given earth and sun, it will spread its vines and bear a heavy harvest." },
                { "Vegetable_Pumpkin_RtD",      "Pumpkin" },
                { "Vegetable_Pumpkin_desc_RtD", "A fully grown pumpkin with a thick rind and rich orange flesh." },
                // Radish
                { "Sapling_Radish_RtD",        "Radish Sprout" },
                { "Sapling_Radish_desc_RtD",   "A young radish plant with tender green leaves. Beneath the soil, a crisp root is beginning to form." },
                { "Seed_Radish_RtD",           "Radish Seed" },
                { "Seed_Radish_desc_RtD",      "A tiny radish seed waiting beneath the soil. Given patience and care, it will swell into a sharp and hearty root." },
                { "Vegetable_Radish_RtD",      "Radish" },
                { "Vegetable_Radish_desc_RtD", "A freshly harvested radish with crisp flesh and a mildly peppery bite." },
                // Redbeet
                { "Sapling_Redbeet_RtD",        "Redbeet Sprout" },
                { "Sapling_Redbeet_desc_RtD",   "A young red beet plant with broad green leaves. Beneath the soil, a vibrant red root is beginning to form." },
                { "Seed_Redbeet_RtD",           "Redbeet Seed" },
                { "Seed_Redbeet_desc_RtD",      "A small red beet seed ready to be planted. With proper soil and time, it will grow into a rich, earthy root." },
                { "Vegetable_Redbeet_RtD",      "Redbeet" },
                { "Vegetable_Redbeet_desc_RtD", "A freshly harvested red beet with deep crimson flesh and a sweet, earthy flavor." },
                // Rice
                { "Sapling_Rice_RtD",        "Rice Sprout" },
                { "Sapling_Rice_desc_RtD",   "A young rice plant with thin green blades. With time and moisture, it will mature and produce harvestable grains." },
                { "Seed_Rice_RtD",           "Rice Seed" },
                { "Seed_Rice_desc_RtD",      "A humble grain of rice ready to be sown. Given fertile earth and steady water, it will rise in slender stalks." },
                { "Vegetable_Rice_RtD",      "Rice" },
                { "Vegetable_Rice_desc_RtD", "Freshly harvested rice grains, ready to be processed or cooked." },
                // Tomato
                { "Sapling_Tomato_RtD",        "Tomato Sprout" },
                { "Sapling_Tomato_desc_RtD",   "A young tomato plant beginning to form sturdy stems and leaves. With time, it will produce ripe tomatoes." },
                { "Seed_Tomato_RtD",           "Tomato Seed" },
                { "Seed_Tomato_desc_RtD",      "A small tomato seed ready to be planted. With proper soil and time, it will grow into a fruit-bearing vine." },
                { "Vegetable_Tomato_RtD",      "Tomato" },
                { "Vegetable_Tomato_desc_RtD", "A plump tomato picked fresh from the vine, rich in color and bursting with sweet, tangy flavor." },
                // Watermelon
                { "Sapling_Watermelon_RtD",        "Watermelon Sprout" },
                { "Sapling_Watermelon_desc_RtD",   "A young watermelon plant beginning to form broad leaves and creeping vines. With time, it will produce a large, juicy melon." },
                { "Seed_Watermelon_RtD",           "Watermelon Seed" },
                { "Seed_Watermelon_desc_RtD",      "A small watermelon seed ready to be planted. With proper soil and time, it will grow into a sprawling vine that bears large fruit." },
                { "Vegetable_Watermelon_RtD",      "Watermelon" },
                { "Vegetable_Watermelon_desc_RtD", "A ripe watermelon with a thick green rind and sweet, juicy flesh." },
                // Wheat
                { "Sapling_Wheat_RtD",        "Wheat Sprout" },
                { "Sapling_Wheat_desc_RtD",   "A thin wheat sprout swaying gently in the breeze, its stalk strengthening as grain forms above." },
                { "Seed_Wheat_RtD",           "Wheat Seed" },
                { "Seed_Wheat_desc_RtD",      "A humble wheat grain meant for sowing. Given earth and sun, it will rise into a field of golden heads." },
                { "Vegetable_Wheat_RtD",      "Wheat" },
                { "Vegetable_Wheat_desc_RtD", "Golden wheat cut from the field, its grain ripe and ready to be ground into sustaining meal." },
                // Meats 
                { "Item_Shrimp_RtD",      "Shrimp" },
                { "Item_Shrimp_desc_RtD", "Fresh shrimp meat, tender and mildly sweet. A versatile ingredient suitable for a variety of dishes." },
                { "Meat_Cod_RtD",      "Cod" },
                { "Meat_Cod_desc_RtD", "Fresh cod meat, mild in flavor and firm in texture. A reliable fish for many dishes." },
                { "Meat_Crab_RtD",      "Crab Meat" },
                { "Meat_Crab_desc_RtD", "Fresh crab meat, tender and slightly sweet. A flavorful addition to many dishes." },
                { "Meat_Manta_RtD",      "Manta Meat" },
                { "Meat_Manta_desc_RtD", "Cleaned manta meat cut from a large sea creature. Dense in texture and suitable for hearty seafood dishes." },
                { "Meat_Marlin_RtD",      "Marlin Meat" },
                { "Meat_Marlin_desc_RtD", "Marlin meat taken from a swift and formidable sea hunter. Lean, strong, and worthy of a seasoned angler’s effort." },
                { "Meat_Shark_RtD",      "Shark Meat" },
                { "Meat_Shark_desc_RtD", "Cleaned shark meat cut from a powerful ocean predator. Thick and substantial, suited for hearty meals." },
                { "Meat_Squid_RtD",      "Squid Meat" },
                { "Meat_Squid_desc_RtD", "Cleaned squid meat taken from deep waters. Soft yet firm, ideal for soups and coastal dishes." },
                { "Meat_Tuna_RtD",      "Tuna Meat" },
                { "Meat_Tuna_desc_RtD", "Cleaned tuna meat cut from a large ocean fish. Dense in texture and well-suited for hearty meals." },
                { "Meat_Turtle_RtD",      "Turtle Meat" },
                { "Meat_Turtle_desc_RtD", "Cleaned turtle meat taken from a hard-shelled sea creature. Thick and substantial, suited for slow-cooked dishes." },
                { "Meat_Whale_RtD",      "Whale Meat" },
                { "Meat_Whale_desc_RtD", "Whale meat taken from the vast open sea. Heavy and nourishing, with a bold, briny richness fit for seasoned sailors." },
                // Soups 
                { "Item_Cereal_RtD",      "Cheery Yo's" },
                { "Item_Cereal_desc_RtD", "A bowl of crunchy cheery yo's cereal made from toasted grains. Crisp, simple, and ready to eat." },
                { "Item_Ramen_RtD",      "Ramen Noodles" },
                { "Item_Ramen_desc_RtD", "A bowl of soft noodles served in a seasoned broth. Simple, filling, and comforting." },
                { "Item_Rice_Shrimp_RtD",      "Rice Bowl" },
                { "Item_Rice_Shrimp_desc_RtD", "Rice stir-fried with shrimp and fresh ingredients. Warm, hearty, and full of flavor." },
                { "Item_Soup_Corn_RtD",      "Corn Soup" },
                { "Item_Soup_Corn_desc_RtD", "Creamy corn soup simmered to bring out the natural sweetness of fresh corn." },
                { "Item_Soup_Miso_RtD",      "Miso Soup" },
                { "Item_Soup_Miso_desc_RtD", "Savory miso broth simmered with delicate ingredients. Light, warming, and restorative." },
                { "Item_Soup_Mushroom_RtD",      "Mushroom Soup" },
                { "Item_Soup_Mushroom_desc_RtD", "A steaming bowl of mushroom soup, thick with forest harvest and deep, earthy flavor. A comforting meal after long journeys." },
                { "Item_Soup_Pumpkin_RtD",      "Pumpkin Soup" },
                { "Item_Soup_Pumpkin_desc_RtD", "A steaming bowl of pumpkin soup, thick and golden with a touch of natural sweetness. A comforting meal for cool evenings." },
                { "Item_Soup_Tomato_RtD",      "Tomato Soup" },
                { "Item_Soup_Tomato_desc_RtD", "Ripe tomatoes simmered into a smooth, comforting soup with a balanced sweet and tangy flavor." },
                // Stews
                { "Item_Stew_Cereal_RtD",      "Special Krunch" },
                { "Item_Stew_Cereal_desc_RtD", "A bowl of Special Krunch made from toasted wheat flakes. Light, crisp, and satisfyingly crunchy." },
                { "Item_Stew_Corn_RtD",      "Corn Stew" },
                { "Item_Stew_Corn_desc_RtD", "A steaming bowl of corn stew, thick with sweet kernels and rich broth. A filling meal fit for long days of labor." },
                { "Item_Stew_Miso_RtD",      "Miso Stew" },
                { "Item_Stew_Miso_desc_RtD", "A steaming bowl of miso stew, deep with fermented richness and slow-simmered ingredients. A warming meal that restores strength after long journeys." },
                { "Item_Stew_Mushroom_RtD",      "Mushroom Stew" },
                { "Item_Stew_Mushroom_desc_RtD", "Wild mushrooms slow-cooked into a thick, earthy stew. Rich, warming, and filling." },
                { "Item_Stew_Pumpkin_RtD",      "Pumpkin Stew" },
                { "Item_Stew_Pumpkin_desc_RtD", "Pumpkin slow-cooked with savory ingredients into a rich, golden stew." },
                { "Item_Stew_RicePudding_RtD",      "Rice Pudding" },
                { "Item_Stew_RicePudding_desc_RtD", "Creamy rice pudding blended with sweet watermelon chunks. A smooth dessert with a refreshing finish." },
                { "Item_Stew_Shrimp_RtD",      "Shrimp Stew" },
                { "Item_Stew_Shrimp_desc_RtD", "A steaming bowl of shrimp stew ladled over soft rice, rich with ocean flavor and slow-simmered depth. A sustaining dish fit for seasoned adventurers." },
                { "Item_Stew_Tomato_RtD",      "Tomato Stew" },
                { "Item_Stew_Tomato_desc_RtD", "A steaming bowl of tomato stew, deep red and richly seasoned. A warm, sustaining meal after long days of toil." },
                // Sushi
                { "Item_Sushi_Bream_RtD",      "Bream Sushi" },
                { "Item_Sushi_Bream_desc_RtD", "Thinly sliced bream layered atop vinegared rice. A simple yet refined coastal dish." },
                { "Item_Sushi_Caviar_RtD",      "Caviar Sushi" },
                { "Item_Sushi_Caviar_desc_RtD", "Salted fish roe layered atop vinegared rice. Briny, smooth, and luxurious in flavor." },
                { "Item_Sushi_Cod_RtD",      "Cod Sushi" },
                { "Item_Sushi_Cod_desc_RtD", "Slices of fresh cod placed upon seasoned rice, simple yet skillfully prepared. A refined meal drawn from cold waters." },
                { "Item_Sushi_Marlin_RtD",      "Marlin Sushi" },
                { "Item_Sushi_Marlin_desc_RtD", "Slices of powerful marlin set upon seasoned rice, prepared with steady hands. A refined dish worthy of seasoned hunters of the sea." },
                { "Item_Sushi_Moka_RtD",      "Moka Sushi" },
                { "Item_Sushi_Moka_desc_RtD", "Delicate cuts of moka placed upon seasoned rice, drawn from rare waters and prepared with care. A refined dish worthy of skilled anglers." },
                { "Item_Sushi_Roll_RtD",      "Roll Sushi" },
                { "Item_Sushi_Roll_desc_RtD", "Rice and seafood wrapped together in a tight roll. Balanced, fresh, and satisfying." },
                { "Item_Sushi_Shrimp_RtD",      "Shrimp Sushi" },
                { "Item_Sushi_Shrimp_desc_RtD", "Sweet shrimp laid carefully upon seasoned rice, drawn from coastal waters and prepared with steady hands. A refined taste of the sea." },
                { "Item_Sushi_Squid_RtD",      "Squid Sushi" },
                { "Item_Sushi_Squid_desc_RtD", "Pale slices of squid laid upon seasoned rice, drawn from dark waters and prepared with care. A refined dish from the deep." },
                { "Item_Sushi_Tuna_RtD",      "Tuna Sushi" },
                { "Item_Sushi_Tuna_desc_RtD", "Deep red tuna set upon seasoned rice, rich and satisfying. A refined catch worthy of skilled fishermen." },
                { "Item_Sushi_Unesu_RtD",      "Unesu Sushi" },
                { "Item_Sushi_Unesu_desc_RtD", "Delicate cuts of unesu set upon seasoned rice, prized for their richness and depth. A luxurious dish drawn from the finest part of the catch." },
                { "Item_Sushi_Urchin_RtD",      "Urchin Sushi" },
                { "Item_Sushi_Urchin_desc_RtD", "Silky sea urchin paired with perfectly seasoned rice. Briny, smooth, and luxuriously refined." },
                { "Item_Sushi_Whale_RtD",      "Whale Sushi" },
                { "Item_Sushi_Whale_desc_RtD", "Dark cuts of whale set upon seasoned rice, taken from the vast open sea. Heavy, nourishing, and fit for seasoned voyagers." },
                { "Item_Tofu_RtD",      "Tofu" },
                { "Item_Tofu_desc_RtD", "A compact block of pressed rice, simple and sustaining. Mild on its own, but strengthened when paired with richer fare." },
                { "Animal_Cod_RtD",      "Cod" },
                { "Animal_Crab_RtD",      "Crab" },
                { "Animal_Dolphin_RtD",      "Dolphin" },
                { "Animal_GreatWhiteShark_RtD",      "Great White" },
                { "Animal_HammerheadShark_RtD",      "Hammer Head" },
                { "Animal_HumpbackWhale_RtD",      "Humpback Whale" },
                { "Animal_Manta_RtD",      "Manta Ray" },
                { "Animal_Marlin_RtD",      "Marlin" },
                { "Animal_Orca_RtD",      "Orca" },
                { "Animal_SpermWhale_RtD",      "Physeter Macrocephalus" },
                { "Animal_Squid_RtD",      "Squid" },
                { "Animal_Tuna_RtD",      "Tuna" },
                { "Animal_Turtle_RtD",      "Turtle" },
                { "Piece_Prep_Table_RtD",      "Fisherman’s Counter" },
                { "Piece_Prep_Table_desc_RtD", "A well-worn counter favored by seasoned fishermen and coastal cooks. As you gather seaweed, fresh vegetables, and meat from the sea’s bounty, new recipes will become available for preparation." },
                { "OceanBelt_RtD", "<#20B2AA>SeaShard Belt" },
                { "OceanBelt_desc_RtD", "Grants the user health, stamina, and eitr regen." },

                { "SeaFareCape_RtD", "<#20B2AA>SeaFare Cape" },
                { "SeaFareCape_desc_RtD", "grants the user the ability to swim with ease." },

                { "SeaScaleShield_RtD", "<#20B2AA>Seashard Serpent Shield" },
                { "SeaScaleShield_desc_RtD", "You can actually parry with shield!" },

                { "SeaShardSpear_RtD", "<#20B2AA>Seashard Harpoon" },
                { "SeaShardSpear_desc_RtD", "Has a close range melee attack, and you can also throw it as a powerful spear while harpooning your target." },

                { "SeaShardStaff_RtD", "<#20B2AA>Seashard Greatstaff" },
                { "SeaShardStaff_desc_RtD", "High pierce & poison damage. Also has some lightening damage." },

                { "SeaShardWand_RtD", "<#20B2AA>Seashard Wand" },
                { "SeaShardWand_desc_RtD", "Used to summon a powerful companion." },

                { "SE_AbyssalShield_RtD", "<#20B2AA>Serpents Strength" },
                { "SE_AbyssalShield_desc_RtD", "Greatly increases blocking skill." },

                { "SE_AbyssalSpear_RtD", "<#20B2AA>Poseidons Accuracy" },
                { "SE_AbyssalSpear_desc_RtD", "Greatly increases spears skill" },

                { "SE_OceanBelt_RtD", "<#20B2AA>Poseidons Strength" },
                { "SE_OceanBelt_desc_RtD", "Faster regen times" },

                { "SE_SeaFare_RtD", "<#20B2AA>Poseidon Endurance" },
                { "SE_SeaFare_desc_RtD", "Greatly increases swimming skill" },

                { "SE_SeaShard_RtD", "<#20B2AA>Poseidons Wisdom" },
                { "SE_SeaShard_desc_RtD", "Greatly increases elemental magic skill" },

                { "SE_ShardSummon_RtD", "<#20B2AA>Poseidons Sacrifice" },
                { "SE_ShardSummon_desc_RtD", "Reduces rengeration times while equipped." },
            }); 
        }
    }
}