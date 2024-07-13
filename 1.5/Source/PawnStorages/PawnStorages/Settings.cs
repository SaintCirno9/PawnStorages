﻿using UnityEngine;
using Verse;

namespace PawnStorages;

public class Settings : ModSettings
{
    public bool AllowNeedsDrop = true;
    public bool SpecialReleaseAll = false;
    public string ForcedPawn = "";
    public bool ShowStoredPawnsInBar = false;
    public float ProductionScale = 0.5f;
    public float BreedingScale = 2f;
    public int MaxPawnsInFarm = 16;
    public float MaxFarmStoredNutrition = 500f;
    public int TicksToAbsorbNutrients = 50;
    public int AnimalTickInterval = 250;

    public void DoWindowContents(Rect wrect)
    {
        Listing_Standard options = new();
        options.Begin(wrect);

        options.CheckboxLabeled("PS_Settings_AllowNeedsDrop".Translate(), ref AllowNeedsDrop);
        if (ModsConfig.anomalyActive) options.CheckboxLabeled("PS_Settings_SpecialReleaseAll".Translate(), ref SpecialReleaseAll);
        options.Gap();
        options.Label("PS_Settings_Advanced".Translate());
        ForcedPawn = options.TextEntryLabeled("PS_Settings_ForceNextPawnStatue".Translate(), ForcedPawn);
        options.Gap();
        bool showStoredPawnsInBarBefore = ShowStoredPawnsInBar;
        options.CheckboxLabeled("PS_Settings_ShowStoredPawnsInBar".Translate(), ref ShowStoredPawnsInBar);
        if (showStoredPawnsInBarBefore != ShowStoredPawnsInBar)
        {
            Find.ColonistBar.MarkColonistsDirty();
        }

        options.Gap();
        options.Label("PS_Settings_Production_Scale".Translate(ProductionScale.ToString("0.00")));
        ProductionScale = options.Slider(ProductionScale, 0f, 10f);
        options.Gap();
        options.Label("PS_Settings_Breeding_Scale".Translate(BreedingScale.ToString("0.00")));
        BreedingScale = options.Slider(BreedingScale, 0f, 10f);
        options.Gap();
        options.Label("PS_Settings_Max_Farm".Translate(MaxPawnsInFarm));
        options.IntAdjuster(ref MaxPawnsInFarm, 1, 1);
        options.Gap();
        options.Label("PS_Settings_Max_Farm_Nutrition".Translate(MaxFarmStoredNutrition));
        MaxFarmStoredNutrition = options.Slider(MaxFarmStoredNutrition, 0f, 500f);
        options.Gap();
        options.Label("PS_Settings_Ticks_To_Absorb_Nutrients".Translate(TicksToAbsorbNutrients));
        options.IntAdjuster(ref TicksToAbsorbNutrients, 1, 1);
        options.Gap();
        options.Label("PS_Settings_Animal_Tick_Interval".Translate(AnimalTickInterval));
        options.IntAdjuster(ref AnimalTickInterval, 1, 1);
        options.Gap();

        options.Gap();
        if (options.ButtonText("PS_Reset".Translate()))
        {
            AllowNeedsDrop = true;
            SpecialReleaseAll = false;
            ForcedPawn = "";
            ShowStoredPawnsInBar = false;
            ProductionScale = 0.5f;
            BreedingScale = 2f;
            MaxPawnsInFarm = 16;
            MaxFarmStoredNutrition = 500f;
        }

        options.End();
    }

    public void AllReleased()
    {
        SpecialReleaseAll = false;
        Write();
    }

    public override void ExposeData()
    {
        Scribe_Values.Look(ref AllowNeedsDrop, "AllowNeedsDrop", true);
        Scribe_Values.Look(ref SpecialReleaseAll, "SpecialReleaseAll", false);
        Scribe_Values.Look(ref ShowStoredPawnsInBar, "ShowStoredPawnsInBar", false);
        Scribe_Values.Look(ref ProductionScale, "ProductionScale", 0.5f);
        Scribe_Values.Look(ref BreedingScale, "BreedingScale", 2);
        Scribe_Values.Look(ref MaxPawnsInFarm, "MaxPawnsInFarm", 16);
    }
}
