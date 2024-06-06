﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace PawnStorages.Farm.Interfaces
{
    public interface IFarmTabParent
    {
        public List<ThingDef> AllowableThing { get; }

        public Dictionary<ThingDef, bool> AllowedThings { get; }
    }
}
