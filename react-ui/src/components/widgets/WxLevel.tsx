import { Card, Paper, TextField, Typography, useTheme } from "@mui/material";
import { Character, characterClone } from "../../model/Model"

function WxLevel(character: Character, setCharacter: (c: Character) => void) {
    const theme = useTheme();

    const updateLevel = (e: any) => {
        let chr = characterClone(character);
        chr.level = Math.min(Math.max(e.target.value, 0), 20);
        setCharacter(chr);
	}

    return (
        <Paper elevation={0} sx={{ p: 0.25, width: "130px" }}>
            <Card sx={{ display: "flex", flexDirection: "col", m: 0.25, justifyContent: "space-between" }} elevation={2}>
                <Typography sx={{ p: 0.5 }} textAlign="center">
                    Уровень
                </Typography>
                <TextField sx={{ p: 0.5 }} variant="standard" type="number"
                    defaultValue={character.level} onChange={updateLevel}/>
            </Card>
        </Paper>
    );
}

export default WxLevel;
