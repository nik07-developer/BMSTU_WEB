import { Card, Paper, TextField, Typography, useTheme } from "@mui/material";
import { attributeDisplayName, Character } from "../../model/Model"

function WxArmor(character: Character, setCharacter: (c: Character) => void) {
	const theme = useTheme();
	return (
		<Paper elevation={0} sx={{ p: 0.25, width: "100px"}}>
            <Card sx={{ display: "flex", flexDirection: "col", m: 0.25, justifyContent: "space-between" }} elevation={2}>
                <Typography sx={{p: 0.5}} textAlign="center">
                    Неизвестный виджет
                </Typography>
            </Card>
		</Paper>
	);
}

export default WxArmor;
