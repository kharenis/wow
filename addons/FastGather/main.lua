function BindEvents()
	local loot_EventFrame = CreateFrame("Frame")

	loot_EventFrame:RegisterEvent("LOOT_READY")

	loot_EventFrame:SetScript("OnEvent",
		function(self, event, ...)
			local numLootItems = GetNumLootItems()
			for i=1, numLootItems, 1 do
				local icon, name, quantity = GetLootSlotInfo(i)
				local itemName, itemLink, itemQuality, itemLevel, itemMinLevel, itemType, itemSubType, itemStackCount, itemEquipLoc, itemTexture, sellPrice, classID, subclassID, bindType, expacID, setID, isCraftingReagent = GetItemInfo(name)

				if itemName == "Stygia" then
					LootSlot(i) --Stygia
				end
				
				if classID == 7 then
					if subclassID == 6 then LootSlot(i) --Leather
					elseif subclassID == 7 then LootSlot(i) -- Metal & Stone
					elseif subclassID == 9 then LootSlot(i) -- Herbs
					end
				end
			end
		end)
end

function Initialize()
	local autoLootEnabled = GetCVar("autoLootDefault");

	if autoLootEnabled == "0" then
		BindEvents()
	else
		print("AutoLoot enabled, FastGather disabled");
	end
end

Initialize()