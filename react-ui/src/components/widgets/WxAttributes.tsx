import { Card, Paper, TextField, Typography, useTheme } from "@mui/material";
import { attributeDisplayName, attributeModifier, Character, characterClone } from "../../model/Model"

function WxAttributes(character: Character, setCharacter: (c: Character) => void) {
	const theme = useTheme();
	const onAttrChange = (ev: any, attr_name: string) => {
		let chr = characterClone(character);
		const idx = chr.attributes.findIndex((e) => e.name == attr_name);
		chr.attributes[idx].value = ev.target.value;
		setCharacter(chr);
	}
	return (
		<Paper elevation={0} sx={{ p: 0.25, display: "flex", flexDirection: "row", flexBasis: 2 }}>
			{character.attributes.map((attr) => {
				return (
					<Card sx={{ m: 0.25, width: "50px" }} elevation={2}>
						<Typography textAlign="center">
							{attributeDisplayName(attr.name)[1].toUpperCase()}
						</Typography>
						<Typography textAlign="center" sx={{ bgcolor: theme.palette.action.disabledBackground }}>
							<TextField sx={{ p: 0.5 }} variant="standard" type="number"
								defaultValue={attr.value} onChange={(e) => onAttrChange(e, attr.name)} />
						</Typography>
						<Typography textAlign="center">
							{(attributeModifier(attr.value) > 0 ? "+" : "") + attributeModifier(attr.value).toString()}
						</Typography>
					</Card>);
			})}
		</Paper>
	);
}

export default WxAttributes;
