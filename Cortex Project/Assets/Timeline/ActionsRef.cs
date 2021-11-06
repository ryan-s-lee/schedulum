using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ActionsRef
{
    public static Dictionary<ActionEnum, string> actionToSceneName = new Dictionary<ActionEnum, string>() {
        { ActionEnum.BUY, "StoreScene" },
        { ActionEnum.EAT, "KitchenScene" },
        { ActionEnum.GOTO, "" },
        { ActionEnum.PLAY, "" },
        { ActionEnum.READ, "" },
        { ActionEnum.WATCH, "" }
    };

    public static Dictionary<ActionEnum, Dictionary<string, List<Effect>>> actionToEffectDict = new Dictionary<ActionEnum, Dictionary<string, List<Effect>>>()
    {
        { ActionEnum.BUY, buyObjectEffects },
        { ActionEnum.EAT, eatObjectEffects },
        { ActionEnum.GOTO, gotoObjectEffects },
        { ActionEnum.PLAY, playObjectEffects },
        { ActionEnum.READ, readObjectEffects },
        { ActionEnum.WATCH, watchObjectEffects },

    };

    private static Dictionary<string, List<Effect>> buyObjectEffects = new Dictionary<string, List<Effect>>();
    private static Dictionary<string, List<Effect>> eatObjectEffects = new Dictionary<string, List<Effect>>();
    private static Dictionary<string, List<Effect>> gotoObjectEffects = new Dictionary<string, List<Effect>>();
    private static Dictionary<string, List<Effect>> playObjectEffects = new Dictionary<string, List<Effect>>();
    private static Dictionary<string, List<Effect>> readObjectEffects = new Dictionary<string, List<Effect>>();
    private static Dictionary<string, List<Effect>> watchObjectEffects = new Dictionary<string, List<Effect>>();

    private static Dictionary<string, string> buyObjectScenes = new Dictionary<string, string>();
    private static Dictionary<string, string> eatObjectScenes = new Dictionary<string, string>();
    private static Dictionary<string, string> gotoObjectScenes = new Dictionary<string, string>();
    private static Dictionary<string, string> playObjectScenes = new Dictionary<string, string>();
    private static Dictionary<string, string> readObjectScenes = new Dictionary<string, string>();
    private static Dictionary<string, string> watchObjectScenes = new Dictionary<string, string>();

    public static void Init()
    {
        // Add items to eatObjectEffects
        // Apple
        // Banana Split
        // Rotisserie Chicken
        // Steak Dinner

        // Add items to gotoObjectEffects


        // Add items to playObjectEffects

        // Add items to readObjectEffects

    }
}
