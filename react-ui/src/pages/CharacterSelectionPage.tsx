import { Box, Button, Card, Paper, Stack, TextField, Typography } from "@mui/material";
import { Character } from "../model/Model";
import { characterCreate, characterGetAll } from "../model/ChracterList";
import { useNavigate } from "react-router-dom";

interface CharacterSelectionProps {
    characters: Character[],
    characterAdd: () => void;
    characterRemove: (name: string) => void;
    activeCharacter: Character,
    setActiveCharacter: (character_name: string) => void;
}

function CharacterSelectionPage({ characters, characterAdd, characterRemove, activeCharacter, setActiveCharacter }: CharacterSelectionProps) {
    const navigate = useNavigate();
    const onSelect = (name: string) => {
        setActiveCharacter(name);
        navigate("/player-screen");
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
                            <Typography sx={{ ml: 1, mr: 1 }}>{c.name}, ур. {c.level}</Typography>
                            <Box>
                                <Button onClick={() => onSelect(c.name)}>Выбрать</Button>
                                <Button color="error" disabled={characters.length <= 1} onClick={() => characterRemove(c.name)}>Удалить</Button>
                            </Box>
                        </Card>);
                })}
                <Button sx={{ m: "auto", pu: "5px" }} variant="contained" onClick={characterAdd}>Создать</Button>
            </Paper>
        </Stack>
    )
}

export default CharacterSelectionPage;
