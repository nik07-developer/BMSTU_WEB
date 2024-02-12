import { Card, Paper, TextField, Typography, useTheme } from "@mui/material";
import { attributeDisplayName, CharacterData } from "../../@types/Model"

function WxLevel(data: CharacterData, setData: (data: CharacterData) => void) {
	const theme = useTheme();

    const updateLevel = (new_level: number) => {
		setData({...data, level: Math.min(Math.max(new_level, 0), 20)});
	}

	return (
		<Paper elevation={0} sx={{ p: 0.25, width: "130px"}}>
            <Card sx={{ display: "flex", flexDirection: "col", m: 0.25, justifyContent: "space-between" }} elevation={2}>
                <Typography sx={{p: 0.5}} textAlign="center">
                    Уровень
                </Typography>
                <TextField sx={{p: 0.5}} variant="standard" type="number" defaultValue={data.level}/>
            </Card>
		</Paper>
	);
}

export default WxLevel;