using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class StatusHelper
{
    
    public static void InitializeStatus()
    {
        List<StatusEffect> inactiveStatuses = new List<StatusEffect>();
        PlayerData playerData = GameManager.current.playerData;
        // Enlightened: <color=red>Lose</color> will 10% slower if you have read GoW within the past week
        StatusEffect enlightened = new StatusEffect();
        enlightened.name = "enlightened";
        enlightened.description = "Having read a powerful narrative, you feel that you understand yourself better. <color=green>Lose will 10% slower for three days.</color>";
        enlightened.SetActivationCondition(() => { 
            return Calendar.current.GetMostRecentEvent() == "READModern Literature" && !GameManager.current.playerData.StatusIsActive("bored of modern literature"); 
        });
        enlightened.SetDeactivationCondition(() => { return Calendar.current.GetNumOfEventsWithinTime("READModern Literature", 3) <= 0; });
        enlightened.SetStatusEffect(() => { return; }); // Runs after each event
        inactiveStatuses.Add(enlightened);

        // Informed
        StatusEffect informed = new StatusEffect();
        informed.name = "enlightened";
        informed.description = "Having read about how people have behaved throughout history, you understand how you can improve your work to better serve people. <color=green>Gain wealth 20% faster for three days.</color>";
        informed.SetActivationCondition(() => {
            return Calendar.current.GetMostRecentEvent() == "READHistorical Analysis" && !playerData.StatusIsActive("bored of historical analysis");
        });
        informed.SetDeactivationCondition(() => { return Calendar.current.GetNumOfEventsWithinTime("READHistorical Analysis", 3) <= 0; });
        informed.SetStatusEffect(() => { return; }); // Runs after each event
        inactiveStatuses.Add(informed);

        // Inspired
        StatusEffect inspired = new StatusEffect();
        inspired.name = "inspired";
        inspired.description = "Having read a tale of success, you feel that with effort you can be succesful as well. <color=green>Gain wealth 20% faster for three days.</color>";
        inspired.SetActivationCondition(() => {
            return Calendar.current.GetMostRecentEvent() == "READAutobiography" && !playerData.StatusIsActive("bored of autobiography");
        });
        inspired.SetDeactivationCondition(() => { return Calendar.current.GetNumOfEventsWithinTime("READAutobiography", 3) <= 0; });
        inspired.SetStatusEffect(() => { return; }); // Runs after each event
        inactiveStatuses.Add(inspired);

        // Fit: <color=red>Lose</color> happiness 10% slower if your fitness is above 90
        StatusEffect fit = new StatusEffect();
        fit.name = "fit";
        fit.description = "Your muscles feel limber. <color=green>Lose happiness 10% slower.</color>";
        fit.SetActivationCondition(() => { return playerData.fitness >= 90; });
        fit.SetDeactivationCondition(() => { return playerData.fitness < 90; });
        inactiveStatuses.Add(fit);

        // Unfit: <color=red>Lose</color> happiness 10% faster if your fitness is lower than 30. 
        StatusEffect unfit = new StatusEffect();
        unfit.name = "out of shape";
        unfit.description = "You feel sluggish and your muscles feel stiff. <color=red>Lose happiness 10% faster.</color>";
        unfit.SetActivationCondition(() => { return playerData.fitness < 50; });
        unfit.SetDeactivationCondition(() => { return playerData.fitness >= 50; });
        inactiveStatuses.Add(unfit);

        // Sore: <color=red>Lose</color> will 30% faster while exercising. 
        StatusEffect sore = new StatusEffect();
        sore.name = "sore";
        sore.description = "Your muscles ache from a recent workout. <color=red>Lose will 30% faster while exercising.</color>";
        sore.SetActivationCondition(() => { return Calendar.current.GetNumOfEventsWithinTime("GOTOGym", 1) > 0; });
        sore.SetDeactivationCondition(() => { return Calendar.current.GetNumOfEventsWithinTime("GOTOGym", 3) == 0; });
        inactiveStatuses.Add(sore);

        // Elated: Gain will +20% faster when sleeping and lose will 10% slower while your happiness is above 90
        StatusEffect elated = new StatusEffect();
        elated.name = "elated";
        elated.description = "You feel so happy you can do anything. <color=green>Gain will +20% faster when sleeping and lose will 10% slower.</color>";
        elated.SetActivationCondition(() => { return playerData.happiness >= 90; });
        elated.SetDeactivationCondition(() => { return playerData.happiness < 90; });
        inactiveStatuses.Add(elated);

        // depressed: <color=red>Lose</color> will 10% faster and gain will 20% slower when sleeping while your happiness is below 30
        StatusEffect depressed = new StatusEffect();
        depressed.name = "depressed";
        depressed.description = "<color=red>Lose will 10% faster and gain will 20% slower when sleeping.</color>";
        depressed.SetActivationCondition(() => { return playerData.happiness < 50; });
        depressed.SetDeactivationCondition(() => { return playerData.happiness >= 50; });
        inactiveStatuses.Add(depressed);

        // overstuffed: <color=red>Lose</color> will 10% faster for the rest of the day
        StatusEffect overstuffed = new StatusEffect();
        overstuffed.name = "overstuffed";
        overstuffed.description = "You are nauseous from eating too much. <color=red>Lose will 10% faster for the rest of the day.</color>";
        overstuffed.SetActivationCondition(() => { return playerData.hunger > 100; });
        overstuffed.SetDeactivationCondition(() => { return playerData.hunger <= 100; });
        inactiveStatuses.Add(overstuffed);

        // Hungry: lose will 20% faster. 
        StatusEffect hungry = new StatusEffect();
        hungry.name = "hungry";
        hungry.description = "You have an empty feeling in your stomach. <color=red>Lose will 20% faster.</color>";
        hungry.SetActivationCondition(() => { return playerData.hunger < 50; });
        hungry.SetDeactivationCondition(() => { return playerData.hunger >= 50; });
        inactiveStatuses.Add(hungry);

        // Apartment rentor: <color=red>Lose</color> up to $300 every week to pay rent
        StatusEffect rentor = new StatusEffect();
        rentor.name = "rentor and citizen";
        rentor.description = "<color=red>Lose $300 every 3 days</color> to pay the rent for your apartment and taxes to the government.";
        rentor.SetActivationCondition(() => { return true; });
        rentor.SetDeactivationCondition(() => { return false; });
        inactiveStatuses.Add(rentor);

        // Willful: No impulse decisions will be made while you’re above 90 will
        StatusEffect willful = new StatusEffect();
        willful.name = "willful";
        willful.description = "You have complete control of yourself. <color=green>No impulse decisions will be made.</color>";
        willful.SetActivationCondition(() => { return playerData.will >= 90; });
        willful.SetDeactivationCondition(() => { return playerData.will < 90; });
        inactiveStatuses.Add(willful);

        // Tired: 30% chance of impulse decisions while you’re below 60 will
        StatusEffect tired = new StatusEffect();
        tired.name = "tired";
        tired.description = "You can still control yourself, but your will is draining. <color=yellow>30% chance for you to make impulse decisions</color>";
        tired.SetActivationCondition(() => { return playerData.will < 60; });
        tired.SetDeactivationCondition(() => { return playerData.will >= 60; });
        inactiveStatuses.Add(tired);

        // Exhausted: 70% chance of impulse decisions while you’re below 30 will
        StatusEffect exhausted = new StatusEffect();
        exhausted.name = "exhausted";
        exhausted.description = "You don't have the will to control your impulses. <color=red>70% chance for you to make impulse decisions</color>";
        exhausted.SetActivationCondition(() => { return playerData.will < 30; });
        exhausted.SetDeactivationCondition(() => { return playerData.will >= 30; });
        inactiveStatuses.Add(exhausted);

        // BORED EFFECTS ARE LOWER BECAUSE THEY SHOULD APPEAR FARTHER DOWN THE STATUS AREA

        // Bored of GoW: no more will and happiness bonuses from GoW for 3 days
        StatusEffect boredOfGoW = new StatusEffect();
        boredOfGoW.name = "bored of modern literature";
        boredOfGoW.description = "You've already read the piece of modern literature you currently own. <color=red>negates happiness gains from reading modern literature</color>";
        boredOfGoW.SetActivationCondition(() => { return Calendar.current.GetMostRecentEvent() == "READModern Literature"; });
        boredOfGoW.SetDeactivationCondition(() => { return Calendar.current.GetEventSinceMostRecent(1) == "bought New Modern Literature"; });
        boredOfGoW.SetStatusEffect(() => { }); // Runs after each event, but nothing should happen
        inactiveStatuses.Add(boredOfGoW);

        // Bored of Batman: no more happiness bonuses from reading Batman for 3 days
        StatusEffect boredOfComics = new StatusEffect();
        boredOfComics.name = "bored of comic";
        boredOfComics.description = "You've know what the plot of the comic you currently own is. <color=red>negates happiness and effects gained from reading comics.</color>";
        boredOfComics.SetActivationCondition(() => { return Calendar.current.GetMostRecentEvent() == "READComic"; });
        boredOfComics.SetDeactivationCondition(() => { return Calendar.current.GetEventSinceMostRecent(1).Equals("bought New Comic"); });
        inactiveStatuses.Add(boredOfComics);

        // Bored of romance novel
        StatusEffect boredOfRomance = new StatusEffect();
        boredOfRomance.name = "bored of romance novel";
        boredOfRomance.description = "You've already read the romance novel you currently own. <color=red>negates happiness and effects gained from reading romance novels.</color>";
        boredOfRomance.SetActivationCondition(() => { return Calendar.current.GetMostRecentEvent() == "READRomance Novel"; });
        boredOfRomance.SetDeactivationCondition(() => { return Calendar.current.GetEventSinceMostRecent(1) == "bought New Romance Novel"; });
        inactiveStatuses.Add(boredOfRomance);

        // boredOfHistory
        StatusEffect boredOfHistory = new StatusEffect();
        boredOfHistory.name = "bored of historical analysis";
        boredOfHistory.description = "You've already absorbed all the information from the historical analysis you currently own. <color=red>negates happiness and effects gained from reading historical analysis.</color>";
        boredOfHistory.SetActivationCondition(() => { return Calendar.current.GetMostRecentEvent() == "READHistorical Analysis"; });
        boredOfHistory.SetDeactivationCondition(() => { return Calendar.current.GetEventSinceMostRecent(1) == "bought New Historical Analysis"; });
        inactiveStatuses.Add(boredOfHistory);

        // boredOfAutobiography
        StatusEffect boredOfAutobiography = new StatusEffect();
        boredOfAutobiography.name = "bored of autobiography";
        boredOfAutobiography.description = "You've already absorbed all the information from the autobiography you currently own. <color=red>negates happiness and effects gained from autobiography.</color>";
        boredOfAutobiography.SetActivationCondition(() => { return Calendar.current.GetMostRecentEvent() == "READAutobiography"; });
        boredOfAutobiography.SetDeactivationCondition(() => { return Calendar.current.GetEventSinceMostRecent(1) == "bought New Autobiography"; });
        inactiveStatuses.Add(boredOfAutobiography);

        GameManager.current.playerData.inactiveStatuses = inactiveStatuses;

        foreach(StatusEffect status in inactiveStatuses)
        {
            StatusInfoObjectHandler generatedStatusInfoObject = StatusMenuHandler.current.AddStatusInfoObject(status.name, status.description);
            status.statusInfoObject = generatedStatusInfoObject;
            // since these statuses are inactive, we don't want people to see them. 
            generatedStatusInfoObject.gameObject.SetActive(false);
        }

        GameManager.current.playerData.activeStatuses = new List<StatusEffect>();

        foreach (StatusEffect status in GameManager.current.playerData.activeStatuses)
        {
            StatusInfoObjectHandler generatedStatusInfoObject = StatusMenuHandler.current.AddStatusInfoObject(status.name, status.description);
            status.statusInfoObject = generatedStatusInfoObject;
        }
    }
    
    
}
