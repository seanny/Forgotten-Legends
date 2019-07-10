-- OnStartInteract is triggered when the player presses F near an interactable item.
-- This can include potions, NPCs, door's, etc.
-- In this case, we'll be interacting with an NPC with the ActorID of "Guard"
function OnStartInteract(name)
    local actorID = Actor.GetActorID(name)
    if actorID == "Guard" then
        -- If the player presses F whilst facing a guard, show a dialogue menu with 2 options: "Insult" and "Goodbye".
        -- Insulting the guard should trigger the arrest dialogue which won't be covered here.
        Dialogue.InitiateDialogue("GuardDialogueInitial.json", actorID)
    end
    return 0
end

-- This is called when the player presses the spacebar to continue with the dialogue.
function OnDialogueContinue(key)
    -- When the currently shown key is "GuardDialogueSpeech01", then show the player dialogue choices.
    if key == "GuardDialogueSpeech02" then
        Dialogue.ShowDialogueChoices(true)
    end
    if key == "GuardDialogueArrest02" then
        Dialogue.ShowDialogueChoices(true)
    end
end

-- OnDialogueOption is triggered when the player selects a dialogue option in the GUI
function OnDialogueOption(file, key)
    -- We will check for the "GuardDialogueOptionInsult" and "GuardDialogueOptionGoodbye" keys
    -- Those keys are also used in the localisation system, which is used to translate the keys into human readable text.
    -- "GuardDialogueOptionInsult" will then become "Fuck you pig!"
    -- "GuardDialogueOptionGoodbye" will also become "Goodbye!"
    if key == "GuardDialogueOptionBanditBounty" then
        -- If the player insults the guard, he/she will then say that the player is under arrest.
        Dialogue.ChangeDialogueDiscussion("GuardDialogueBanditBounty.json")
    end
    if key == "GuardDialogueOptionInsult" then
        -- If the player insults the guard, he/she will then say that the player is under arrest.
        Dialogue.ChangeDialogueDiscussion("GuardDialogueArrest.json")
    end
    if key == "GuardDialogueOptionGoodbye" then
        -- If the player says "Goodbye", then exit the dialogue.
        Dialogue.ExitDialogue()
    end
    if key == "GuardDialogueArrestOptionSubmit" then
        -- If the player uses the submit option, exit the dialogue and then teleport inside a cell cell.
        Dialogue.ExitDialogue()
    end
    if key == "GuardDialogueArrestOptionEscape" then
        -- If the player uses the escape option, exit the dialogue and make guards hostile.
        Dialogue.ExitDialogue()
    end
    return 0
end