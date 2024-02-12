import { Card, Paper, TextField, Typography, useTheme } from "@mui/material";
import { attributeDisplayName, CharacterData } from "../../@types/Model"

function WxArmor(data: CharacterData) {
	const theme = useTheme();
	return (
		<Paper elevation={0} sx={{ p: 0.25, width: "100px"}}>
            <Card sx={{ display: "flex", flexDirection: "col", m: 0.25, justifyContent: "space-between" }} elevation={2}>
                <Typography sx={{p: 0.5}} textAlign="center">
                    КЗ
                </Typography>
                <TextField sx={{p: 0.5}} variant="standard" type="number" defaultValue={data.armor_class}/>
            </Card>
		</Paper>
	);
}

export default WxArmor;