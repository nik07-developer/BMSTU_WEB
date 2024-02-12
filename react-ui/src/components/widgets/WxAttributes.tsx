import { Box, Card, Paper, Typography, useTheme } from "@mui/material";
import { CharacterData } from "../../@types/Model"

function sign(value: number): string {
	if (value > 0)
		return "+";
	if (value < 0)
		return "-";
	return "";
}

function WxAttributes(data: CharacterData, posx: number, posy: number) {
	const theme = useTheme();
	return (
		<Paper elevation={0} sx={{ p: 0.25, display: "flex", flexDirection: "row", flexBasis: 2}}>
			{data.attributes.map((attr) => {
				return (
					<Card sx={{ m: 0.25, width:"50px"}} elevation={2}>
						<Typography textAlign="center">{attr.name_short.toUpperCase()}</Typography>
						<Typography textAlign="center" sx={{bgcolor: theme.palette.action.disabledBackground}}>{(attr.value > 0 ? "+" : "") + attr.value.toString()}</Typography>
					</Card>);
			})}
		</Paper>
	);
}

export default WxAttributes;
