folder = config.folderPath("Folder path")


local function createModInfo(file)
    return mod.__new(file.name, "A mod", "1", file)
end


function getAllMods()
	local files = fs.getFilesInFolder(folder.value .. "mods")
	local mods = {}

	for _, file in ipairs(files) do
		table.insert(mods, createModInfo(file))
	end

	return mods
end
