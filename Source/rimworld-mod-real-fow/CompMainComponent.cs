using RimWorld;
using Verse;

namespace RimWorldRealFoW;

public class CompMainComponent : ThingComp
{
    public static readonly CompProperties CompDef = new(typeof(CompMainComponent));
    public CompComponentsPositionTracker compComponentsPositionTracker;
    public CompFieldOfViewWatcher compFieldOfViewWatcher;
    public CompHiddenable compHiddenable;
    public CompHideFromPlayer compHideFromPlayer;
    private CompTreeViewBlocker compTreeViewBlocker;
    private CompViewBlockerWatcher compViewBlockerWatcher;
    private bool isPlant;
    private bool isTreePlant;
    private bool setup;

    private void performSetup()
    {
        if (setup)
        {
            return;
        }

        setup = true;

        var category = parent.def.category;
        isPlant = category == ThingCategory.Plant;
        isTreePlant = isPlant && parent is Plant plant && plant.def.plant.IsTree;

        compComponentsPositionTracker = new CompComponentsPositionTracker
        {
            parent = parent,
            mainComponent = this
        };

        compHiddenable = new CompHiddenable
        {
            parent = parent,
            mainComponent = this
        };

        compHideFromPlayer = new CompHideFromPlayer
        {
            parent = parent,
            mainComponent = this
        };

        if (category == ThingCategory.Building)
        {
            compViewBlockerWatcher = new CompViewBlockerWatcher
            {
                parent = parent,
                mainComponent = this
            };
        }

        if (
            category is ThingCategory.Pawn or ThingCategory.Building
            //||category == ThingCategory.Projectile
        )
        {
            compFieldOfViewWatcher = new CompFieldOfViewWatcher
            {
                parent = parent,
                mainComponent = this
            };
        }

        // Always create tree view blocker for trees - the setting is checked at runtime
        if (isTreePlant)
        {
            compTreeViewBlocker = new CompTreeViewBlocker
            {
                parent = parent,
                mainComponent = this
            };
        }
    }

    public override void PostSpawnSetup(bool respawningAfterLoad)
    {
        performSetup();

        compComponentsPositionTracker.PostSpawnSetup(respawningAfterLoad);

        compHiddenable.PostSpawnSetup(respawningAfterLoad);

        compHideFromPlayer.PostSpawnSetup(respawningAfterLoad);

        compViewBlockerWatcher?.PostSpawnSetup(respawningAfterLoad);
        compFieldOfViewWatcher?.PostSpawnSetup(respawningAfterLoad);
        compTreeViewBlocker?.PostSpawnSetup(respawningAfterLoad);
    }

    public override void CompTick()
    {
        performSetup();

        compComponentsPositionTracker.CompTick();
        compHiddenable.CompTick();
        compHideFromPlayer.CompTick();
        compViewBlockerWatcher?.CompTick();
        compFieldOfViewWatcher?.CompTick();
        compTreeViewBlocker?.CompTick();
    }

    public override void CompTickRare()
    {
        performSetup();

        compComponentsPositionTracker.CompTickRare();
        compHiddenable.CompTickRare();
        compHideFromPlayer.CompTickRare();
        compViewBlockerWatcher?.CompTickRare();
        compFieldOfViewWatcher?.CompTickRare();
        compTreeViewBlocker?.CompTickRare();
    }

    public override void ReceiveCompSignal(string signal)
    {
        performSetup();

        compComponentsPositionTracker.ReceiveCompSignal(signal);
        compHiddenable.ReceiveCompSignal(signal);
        compHideFromPlayer.ReceiveCompSignal(signal);
        compViewBlockerWatcher?.ReceiveCompSignal(signal);
        compFieldOfViewWatcher?.ReceiveCompSignal(signal);
        compTreeViewBlocker?.ReceiveCompSignal(signal);
    }

    public override void PostDeSpawn(Map map, DestroyMode mode = DestroyMode.Vanish)
    {
        performSetup();

        compComponentsPositionTracker.PostDeSpawn(map);
        compHiddenable.PostDeSpawn(map);
        compHideFromPlayer.PostDeSpawn(map);
        compViewBlockerWatcher?.PostDeSpawn(map);
        compFieldOfViewWatcher?.PostDeSpawn(map);
        compTreeViewBlocker?.PostDeSpawn(map);
    }

    public override void PostExposeData()
    {
        performSetup();

        compComponentsPositionTracker.PostExposeData();
        compHiddenable.PostExposeData();
        compHideFromPlayer.PostExposeData();

        compViewBlockerWatcher?.PostExposeData();
        compFieldOfViewWatcher?.PostExposeData();
        compTreeViewBlocker?.PostExposeData();
        if (!Scribe.saver.savingForDebug)
        {
            return;
        }

        var hasCompComponentsPositionTracker = compComponentsPositionTracker != null;
        var hasCompHiddenable = compHiddenable != null;
        var hasCompHideFromPlayer = compHideFromPlayer != null;
        var hasCompViewBlockerWatcher = compViewBlockerWatcher != null;
        var hasCompFieldOfViewWatcher = compFieldOfViewWatcher != null;
        var hasCompTreeViewBlocker = compTreeViewBlocker != null;
        Scribe_Values.Look(ref hasCompComponentsPositionTracker, "hasCompComponentsPositionTracker");
        Scribe_Values.Look(ref hasCompHiddenable, "hasCompHiddenable");
        Scribe_Values.Look(ref hasCompHideFromPlayer, "hasCompHideFromPlayer");
        Scribe_Values.Look(ref hasCompViewBlockerWatcher, "hasCompViewBlockerWatcher");
        Scribe_Values.Look(ref hasCompFieldOfViewWatcher, "hasCompFieldOfViewWatcher");
        Scribe_Values.Look(ref hasCompTreeViewBlocker, "hasCompTreeViewBlocker");
    }
}