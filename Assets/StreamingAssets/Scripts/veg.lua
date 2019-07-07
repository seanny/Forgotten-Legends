function OnDialogueOption(file, key)
    print("OnDialogueOption veg.lua: " .. key .. " @ " .. file)
    if key == "TEST_NEUTRAL" then
        Dialogue.ChangeDialogueDiscussion("lol.json")
    end
    return 0
end

function OnStartInteract(name)
    local actorID = Actor.GetActorID(name)
    print("OnStartInteract veg.lua: " .. name .. " @ " .. actorID)
    if name == "Guard" then
        print("Starting dialogue with " .. name .. " (actorID: " .. actorID .. ")")
        Dialogue.InitiateDialogue("test.json", name)
        print("Adding neutral choice")
        Dialogue.AddDialogueChoice("test.json", "TEST_NEUTRAL")
        print("Adding bye choice")
        Dialogue.AddDialogueChoice("test.json", "TEST_BYE")
    end
    return 0
end

function OnDialogueContinue(nextKey)
    if nextKey == "DIALOGUE_FOUR" then
        Dialogue.ShowDialogueChoices(true)
    end
end