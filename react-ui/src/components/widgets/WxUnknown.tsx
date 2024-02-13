import { Card, Paper, TextField, Typography, useTheme } from "@mui/material";
import { attributeDisplayName, Character } from "../../model/Model"

function WxUnknown(character: Character, setCharacter: (c: Character) => void, editMode: boolean) {
	const theme = useTheme();
	return (
		<Paper elevation={0} sx={{ p: 0.25}}>
            <Card sx={{ display: "flex", flexDirection: "col", m: 0.25, justifyContent: "space-between" }} elevation={2}>
                <Typography sx={{p: 0.5}} textAlign="center">
                    Неизвестный виджет
                </Typography>
            </Card>
		</Paper>
	);
}

export default WxUnknown;
