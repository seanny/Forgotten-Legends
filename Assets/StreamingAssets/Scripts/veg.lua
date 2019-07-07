function OnDialogueOption(file, key)
    print("OnDialogueOption veg.lua key = " .. key)
    if key == "TEST_NEUTRAL" then
        Dialogue.ChangeDialogueDiscussion("lol.json")
        Dialogue.ShowDialogueChoices(false)
    end
    if key == "TEST_BYE" then
        Dialogue.ExitDialogue()
    end
    return 0
end

function OnStartInteract(name)
    local actorID = Actor.GetActorID(name)
    print("OnStartInteract veg.lua: " .. name .. " @ " .. actorID)
    if name == "Guard" then
        print("Starting dialogue with " .. name .. " (actorID: " .. actorID .. ")")
        Dialogue.InitiateDialogue("test.json", name)
    end
    return 0
end

function OnDialogueContinue(nextKey)
    if nextKey == "DIALOGUE_FOUR" then
        Dialogue.ShowDialogueChoices(true)
    end
end