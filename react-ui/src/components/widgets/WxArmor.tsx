import { Card, Paper, TextField, Typography, useTheme } from "@mui/material";
import { Character, characterClone } from "../../model/Model"

function WxArmor(character: Character, setCharacter: (c: Character) => void, editMode: boolean) {
    const theme = useTheme();

    const updateArmorClass = (e: any) => {
        let chr = characterClone(character);
        chr.armor_class = e.target.value as number;
        setCharacter(chr);
    }

    return (
        <Paper elevation={0} sx={{ p: 0.25, width: "100px" }}>
            <Card sx={{ display: "flex", flexDirection: "col", m: 0.25, justifyContent: "space-between" }} elevation={2}>
                <Typography sx={{ p: 0.5 }} textAlign="center">
                    КЗ
                </Typography>
                {(editMode) && (
                    <TextField sx={{ p: 0.5 }} variant="standard" type="number"
                        defaultValue={character.armor_class} onChange={updateArmorClass} />)}
                {(!editMode) && (
                    <Typography sx={{ p: 0.5 }}>
                        {character.armor_class}
                    </Typography>)}
            </Card>
        </Paper>
    );
}

export default WxArmor;
