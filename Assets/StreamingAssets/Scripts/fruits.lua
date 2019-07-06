function OnStart()
    print("OnStart fruit.lua")
    Actor.SetActorPos("Player", 1.1, 2.2, 3.3)
    print("Platform = " .. Debug.GetPlatformName())
end
