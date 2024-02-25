import { Box, Button, Card, Paper, Stack, TextField, Typography } from "@mui/material";
import { Character, CharacterScreen } from "../model/Model";
import { characterCreate, characterGetAll, getBlankCharacter } from "../model/ChracterList";
import { useNavigate } from "react-router-dom";
import { Guid } from "guid-typescript";
import { ApiCharacter, useApiGetCharacters } from "../api/ApiCharacter";
import { useApiGetCharacterScreens } from "../api/ApiView";
import { characterComposeFromApi } from "../api/CharacterConvert";
import { Cloud, CloudOff } from "@mui/icons-material";

interface CharacterSelectionProps {
    characterAdd: () => void;
    characterRemove: (guid: Guid) => void;
    activeCharacter: Character,
    setActiveCharacter: (guid: Guid) => void;
}

function CharacterSelectionPage({ characterAdd, characterRemove, activeCharacter, setActiveCharacter }: CharacterSelectionProps) {
    const navigate = useNavigate();
    const onSelect = (guid: Guid) => {
        setActiveCharacter(guid);
        navigate("/player-screen");
    }

    const [fetchResult, getCharacters] = useApiGetCharacters();
    const [fetchScreensResult, getCharacterScreens] = useApiGetCharacterScreens();

    const characters = characterGetAll();

    const onCharactersReceiveFromServer = async () => {
        let received_characters: ApiCharacter[] = [];
        getCharacters(l => received_characters = l);
        let current_characters = characterGetAll();

        received_characters.forEach(async (rc, i) => {
            const guid = Guid.parse(rc.character_id);
            if (!current_characters.find(cc => cc.guid == guid)) {
                let sc: CharacterScreen[] = getBlankCharacter().screens;
                await getCharacterScreens(guid, s => sc = s);

                current_characters.push(characterComposeFromApi(rc, sc));
            }
        });
    }

    return (
        <Stack spacing={3} sx={{ m: "auto", mt: "16vh", alignItems: "center" }}>
            <Typography component="h1" variant="h4">
                Выбор персонажа
            </Typography>
            <Paper elevation={0} sx={{ p: 0.25, display: "flex", flexDirection: "column" }}>
                {characters.map((c: Character, idx: number) => {
                    return (
                        <Card sx={{ m: 0.25, display: "flex", justifyContent: "space-between", alignItems: "center", width: "500px" }} elevation={2}>
                            <Box sx={{ display: "flex" }}>
                                <Typography sx={{ ml: 1, mr: 1 }}>{c.name}, ур. {c.level} </Typography>
                                {c.stored_on_server ? <Cloud /> : <CloudOff />}
                            </Box>
                            <Box>
                                <Button onClick={() => onSelect(c.guid)}>Выбрать</Button>
                                <Button color="error" disabled={characters.length <= 1} onClick={() => characterRemove(c.guid)}>Удалить</Button>
                            </Box>
                        </Card>);
                })}
                <Box sx={{ display: "flex", m: "auto" }}>
                    <Button sx={{ m: "auto", mu: "5px", mr: "5px" }} variant="contained" onClick={characterAdd}>Создать</Button>
                    <Button sx={{ m: "auto", mu: "5px" }} variant="outlined" onClick={onCharactersReceiveFromServer}>Загрузить с сервера</Button>
                </Box>
            </Paper>
        </Stack>
    )
}

export default CharacterSelectionPage;
