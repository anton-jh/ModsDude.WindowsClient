vars = {
    modsFolder = config.folderPath("Mods folder"),
	savegamesFolder = config.folderPath("Savegames folder")
}


local function createModInfo(file)
    local modDescStream = zip.read(file).get("modDesc.xml").open()
    local document = xml.read(modDescStream)

    local name = document.get("/modDesc/title/en")
    local version = document.get("/modDesc/version")

    return mod.__new(file.name, name, version, file)
end


function getAllMods()
    local files = fs.getFilesInFolder(modsFolder.value)
    local mods = {}

    for _, file in ipairs(files) do
        table.insert(mods, createModInfo(file))
    end

    return mods
end
