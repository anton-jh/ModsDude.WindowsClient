vars = {
    modsFolder = config.folderPath("Mods folder"),
	savegamesFolder = config.folderPath("Savegames folder")
}


local function createModInfo(file)
    local modDescStream = zip.read(file).get("modDesc.xml").open()
    local document = xml.read(modDescStream)

    local version = document.get("/modDesc/version")
    local name = document.get("/modDesc/title/en")
	local description = document.get("/modDesc/description/en")

    return mod.fromFile(file.name, version, name, description, file)
end


function getAllMods()
    local files = fs.getFilesInFolder(vars.modsFolder.value)
    local mods = {}

    for _, file in ipairs(files) do
        table.insert(mods, createModInfo(file))
    end

    return mods
end
