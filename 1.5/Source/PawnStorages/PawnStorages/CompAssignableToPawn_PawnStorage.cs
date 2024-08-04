﻿using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace PawnStorages;

public class CompAssignableToPawn_PawnStorage : CompAssignableToPawn
{
    public BedOwnerType OwnerType = BedOwnerType.Colonist;
    public new CompProperties_PSAssignableToPawn Props => props as CompProperties_PSAssignableToPawn;

    public override IEnumerable<Pawn> AssigningCandidates
    {
        get
        {
            if(Props.colonyAnimalsOnly) return !parent.Spawned
                ? []
                : parent.Map.mapPawns.SpawnedColonyAnimals.OrderByDescending(p => CanAssignTo(p).Accepted);
            return !parent.Spawned ? [] : OwnerType switch
            {
                BedOwnerType.Colonist => parent.Map.mapPawns.FreeColonists.OrderByDescending(p => CanAssignTo(p).Accepted),
                BedOwnerType.Prisoner => parent.Map.mapPawns.PrisonersOfColony.OrderByDescending(p => CanAssignTo(p).Accepted),
                BedOwnerType.Slave => parent.Map.mapPawns.SlavesOfColonySpawned.OrderByDescending(p => CanAssignTo(p).Accepted),
                _ => []
            };
        }
    }

    public override void PostSpawnSetup(bool respawningAfterLoad)
    {
        base.PostSpawnSetup(respawningAfterLoad);
        PawnStorages_GameComponent.CompAssignables.Add(this);
    }

    public override void PostDeSpawn(Map map)
    {
        base.PostDeSpawn(map);
        PawnStorages_GameComponent.CompAssignables.Remove(this);
    }

    public override string GetAssignmentGizmoDesc()
    {
        return "PS_CommandSetOwnerDesc".Translate();
    }

    public override bool AssignedAnything(Pawn pawn)
    {
        return PawnStorages_GameComponent.CompAssignables.Any(x => x != this && x.AssignedPawns.Contains(pawn));
    }

    public override bool ShouldShowAssignmentGizmo()
    {
        return Props.showGizmo && parent.Faction == Faction.OfPlayer;
    }

    public override void TryAssignPawn(Pawn pawn)
    {
        foreach (CompAssignableToPawn_PawnStorage otherStorage in PawnStorages_GameComponent.CompAssignables)
        {
            if (otherStorage != this) otherStorage.TryUnassignPawn(pawn);
        }

        base.TryAssignPawn(pawn);
    }


    public override void PostExposeData()
    {
        base.PostExposeData();
        Scribe_Values.Look(ref OwnerType, "ownerType", BedOwnerType.Colonist);
    }
}
