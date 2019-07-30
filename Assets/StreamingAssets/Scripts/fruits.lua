function OnStart()
    print("OnStart fruit.lua")
end

function OnAddItem(item)
    if item == "Potion" then
        print("Giving quest Test01")
        Quest.GiveQuest("Test01")
    end
end