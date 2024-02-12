import { Box, Card, Paper, TextField, Typography, useTheme } from "@mui/material";
import Button from "@mui/material/Button";
import { CharacterData } from "../../@types/Model"

function WxHealth(data: CharacterData, setData: (data: CharacterData) => void) {
	const theme = useTheme();

	const updateHealth = (new_health: number) => {
		setData({...data, health: Math.min(Math.max(new_health, 0), data.max_health)});
	}

	const updateMaxHealth = (e: any) => {
		const max_health = e.target.value as number;
		const delta = (max_health - data.max_health);
		const health = Math.min(max_health, data.health + Math.max(delta, 0));
		setData({...data, max_health: max_health, health: health});
	}

	return (
		<Paper elevation={0} sx={{ p: 0.25, width:"120px" }}>
			<Card sx={{ m: 0.25 }} elevation={2}>
				<Box sx={{display: "flex"}}>
					<Typography textAlign="right" sx={{width: "50px", pl: "5px"}}>{data.health}/</Typography>
					<TextField id="hp-max" sx={{width: "60px", pr: "5px"}} variant="standard" type="number"
						defaultValue={data.max_health} onChange={updateMaxHealth}/>
				</Box>
				<Box sx={{pt: "5px", bgcolor: theme.palette.action.disabledBackground, display: "flex", justifyContent: "space-between", alignItems: "center"}}>
					<Button
						sx={{minWidth: "25px", maxWidth: "25px", maxHeight: "20px"}}
						size="small" variant="contained" color="error"
						onClick={() => {updateHealth(data.health - 5)}}>-5</Button>
					<Button
						sx={{minWidth: "25px", maxWidth: "25px", maxHeight: "20px"}}
						size="small" variant="contained" color="error"
						onClick={() => {updateHealth(data.health - 1)}}>-</Button>
						
					{/* <TextField variant="standard" type="number"></TextField> */}
					<Button sx={{minWidth: "25px", maxWidth: "25px", maxHeight: "20px"}}
						size="small" variant="contained" color="success"
						onClick={() => {updateHealth(data.health + 1)}}>+</Button>
					<Button sx={{minWidth: "25px", maxWidth: "25px", maxHeight: "20px"}}
						size="small" variant="contained" color="success"
						onClick={() => {updateHealth(data.health + 5)}}>+5</Button>
				</Box>
				<Box sx={{bgcolor: theme.palette.action.disabledBackground, display: "flex", justifyContent: "space-around", alignItems: "center"}}>
				<Button sx={{mt: "5px", minWidth: "75px", maxWidth: "75px", maxHeight: "20px"}}
						size="small" variant="contained" color="warning"
						onClick={() => {updateHealth(data.max_health)}}>сброс</Button>
					{/* <TextField variant="standard" type="number"></TextField> */}
				</Box>
			</Card>
		</Paper>
	);
}

export default WxHealth;
