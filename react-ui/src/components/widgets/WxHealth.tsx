import { Box, Card, Paper, TextField, Typography, useTheme } from "@mui/material";
import Button from "@mui/material/Button";
import { Character, characterClone } from "../../model/Model"

function WxHealth(character: Character, setCharacter: (c: Character) => void, editMode: boolean) {
	const theme = useTheme();

	const updateHealth = (new_health: number) => {
		let chr = characterClone(character);
		chr.health = Math.min(Math.max(new_health, 0), character.max_health);
		setCharacter(chr);
	}

	const updateMaxHealth = (e: any) => {
		let chr = characterClone(character);
		chr.max_health = e.target.value as number;
		const delta = (chr.max_health - character.max_health);
		chr.health = Math.min(chr.max_health, character.health + Math.max(delta, 0))
		setCharacter(chr);
	}

	return (
		<Paper elevation={0} sx={{ p: 0.25, width: "120px" }}>
			<Card sx={{ m: 0.25 }} elevation={2}>
				<Box sx={{ display: "flex", justifyContent: "center" }}>
					<Typography textAlign="right" sx={{ width: "50px", pl: "5px" }}>{character.health}/</Typography>
					{editMode && (
						<TextField sx={{ width: "50px", pr: "5px" }} variant="standard" type="number"
							defaultValue={character.max_health} onChange={updateMaxHealth} />)}
					{!editMode && (
						<Typography textAlign="left" sx={{ width: "50px", pr: "5px" }}>
							{character.max_health}
						</Typography>
					)}
				</Box>
				<Box sx={{ pt: "5px", bgcolor: theme.palette.action.disabledBackground, display: "flex", justifyContent: "space-between", alignItems: "center" }}>
					<Button
						sx={{ minWidth: "25px", maxWidth: "25px", maxHeight: "20px" }}
						size="small" variant="contained" color="error"
						onClick={() => { updateHealth(character.health - 5) }}>-5</Button>
					<Button
						sx={{ minWidth: "25px", maxWidth: "25px", maxHeight: "20px" }}
						size="small" variant="contained" color="error"
						onClick={() => { updateHealth(character.health - 1) }}>-</Button>
					<Button sx={{ minWidth: "25px", maxWidth: "25px", maxHeight: "20px" }}
						size="small" variant="contained" color="success"
						onClick={() => { updateHealth(character.health + 1) }}>+</Button>
					<Button sx={{ minWidth: "25px", maxWidth: "25px", maxHeight: "20px" }}
						size="small" variant="contained" color="success"
						onClick={() => { updateHealth(character.health + 5) }}>+5</Button>
				</Box>
				<Box sx={{ bgcolor: theme.palette.action.disabledBackground, display: "flex", justifyContent: "space-around", alignItems: "center" }}>
					<Button sx={{ mt: "5px", minWidth: "75px", maxWidth: "75px", maxHeight: "20px" }}
						size="small" variant="contained" color="warning"
						onClick={() => { updateHealth(character.max_health) }}>сброс</Button>
				</Box>
			</Card>
		</Paper>
	);
}

export default WxHealth;
